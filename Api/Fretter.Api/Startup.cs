using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Fretter.Api.Authorization;
using Fretter.Api.Filters;
using Fretter.Api.Helpers;
using Fretter.Domain.Interfaces;
using Fretter.IoC;
using System;
using IdentityServer4.AccessTokenValidation;
using Microsoft.Extensions.Hosting;
using static IdentityServer4.IdentityServerConstants;
using Fretter.Api.AutoMapper;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Fretter.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {          
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(DomainExceptionFilter));
                options.ModelBinderProviders.Insert(0, new ProviderModelBinder());
                options.EnableEndpointRouting = false;
            });

            services.AddCors();
            services.AddControllers();
            services.AddMemoryCache();

            /* Auto Mapper */
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //services.AddAutoMapper();
            IdentityModelEventSource.ShowPII = true;
            services.Configure<AuthorizationConfigs>(Configuration.GetSection("AuthorizationConfigs"));
            services.Configure<AuthorizationPortalConfigs>(Configuration.GetSection("AuthorizationPortalConfigs"));
            services.Configure<UrlsConfigs>(Configuration.GetSection("FretterUrlsConfig"));

            FretterMensageriaIoc.Initialize(services, Configuration);
            FretterCoreIoc.Initialize(services, Configuration);

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.OnAppendCookie = cookieContext =>
                    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
                options.OnDeleteCookie = cookieContext =>
                    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            });

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<IUsuarioHelper, UsuarioHelper>();

            ServiceLocator.Init(services.BuildServiceProvider());

            var rsa = new RsaKeyService(Environment, TimeSpan.FromDays(30));
            services.AddSingleton<RsaKeyService>(provider => rsa);

            var auth = services.AddIdentityServer(options =>
            {                
                options.Events.RaiseSuccessEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseErrorEvents = true;
            })
             .AddSigningCredential(rsa.GetKey(), RsaSigningAlgorithm.RS512)
             .AddInMemoryApiResources(AuthorizationConfig.GetApiResources())
             .AddInMemoryApiScopes(AuthorizationConfig.GetApiScopes())
             .AddInMemoryClients(AuthorizationConfig.GetClients())
             .AddResourceOwnerValidator<GrantValidator>()
             .AddProfileService<CustomProfileService>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            //Configuração Client
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                    .AddIdentityServerAuthentication(options => AuthorizationConfig.GetServerAutentication(options))
                    .AddOAuth("fusion", fusion => AuthorizationConfig.GetFusionAutentication(fusion));

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser()
                    .Build());
            });


            services.AddCors(o => o.AddPolicy("EnableAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            CorsAuthorization.SetAllowAll(services);
            services.AddMvcCore().AddAuthorization();

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Não remover o FowardOptions ele mantém o estado do protocolo dentro do AKS https -> https
            var forwardOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                RequireHeaderSymmetry = false
            };

            forwardOptions.KnownNetworks.Clear();
            forwardOptions.KnownProxies.Clear();

            // ref: https://github.com/aspnet/Docs/issues/2384
            app.UseForwardedHeaders(forwardOptions);

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            //app.UseMvc();
            app.UseCors("EnableAll");
            app.UseIdentityServer();
            app.UseRouting();

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseAuthorization();
            //app.UseHttpsRedirection(); 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void CheckSameSite(HttpContext httpContext, CookieOptions options)
        {
            if (options.SameSite == SameSiteMode.None)
            {
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
                if (!httpContext.Request.IsHttps || DisallowsSameSiteNone(userAgent))
                {
                    options.SameSite = SameSiteMode.Unspecified;
                }
            }
        }

        public bool DisallowsSameSiteNone(string userAgent)
        {
            // Check if a null or empty string has been passed in, since this
            // will cause further interrogation of the useragent to fail.
            if (String.IsNullOrWhiteSpace(userAgent))
                return false;

            // Cover all iOS based browsers here. This includes:
            // - Safari on iOS 12 for iPhone, iPod Touch, iPad
            // - WkWebview on iOS 12 for iPhone, iPod Touch, iPad
            // - Chrome on iOS 12 for iPhone, iPod Touch, iPad
            // All of which are broken by SameSite=None, because they use the iOS networking
            // stack.
            if (userAgent.Contains("CPU iPhone OS 12") ||
                userAgent.Contains("iPad; CPU OS 12"))
            {
                return true;
            }

            // Cover Mac OS X based browsers that use the Mac OS networking stack. 
            // This includes:
            // - Safari on Mac OS X.
            // This does not include:
            // - Chrome on Mac OS X
            // Because they do not use the Mac OS networking stack.
            if (userAgent.Contains("Macintosh; Intel Mac OS X 10_14") &&
                userAgent.Contains("Version/") && userAgent.Contains("Safari"))
            {
                return true;
            }

            // Cover Chrome 50-69, because some versions are broken by SameSite=None, 
            // and none in this range require it.
            // Note: this covers some pre-Chromium Edge versions, 
            // but pre-Chromium Edge does not require SameSite=None.
            if (userAgent.Contains("Chrome/5") || userAgent.Contains("Chrome/6"))
            {
                return true;
            }

            return false;
        }
    }
}
