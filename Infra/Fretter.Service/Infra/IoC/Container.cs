using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Impl;
using Fretter.IoC;
using System;
using Fretter.Service.Tasks;
using Fretter.Domain.Interfaces;
using Fretter.Service.Helper;
using Fretter.Domain.Interfaces.Service;
using Fretter.Domain.Services;
using Fretter.Service.Tasks.Conciliacao;

namespace Fretter.Service.Infra.IoC
{
    public static class Container
    {
        public static IServiceProvider Register(IConfiguration configuration)
        {
            var services = new ServiceCollection();

            FretterCoreIoc.Initialize(services, configuration);
            FretterMensageriaIoc.Initialize(services, configuration);

            services.AddSingleton(typeof(IMessageBusService<>), typeof(MessageBusService<>));

            services.AddScoped<ProcessaIndicadorTask>();
            services.AddScoped<ProcessaCTeTask>();
            services.AddScoped<LogRequestTask>();
            services.AddScoped<ImportacaoConfiguracaoTask>();
            services.AddScoped<ImportacaoEntregaConfiguracaoTask>();
            services.AddScoped<ProcessaCallbackEntregaTask>();
            services.AddScoped<ProcessaEntregaDevolucaoTask>();
            services.AddScoped<ProcessaEntregaDevolucaoCancelamentoTask>();
            services.AddScoped<ProcessaEntregaDevolucaoOcorrenciaTask>();
            services.AddScoped<ProcessarOcorrenciaArquivoTask>();
            services.AddScoped<ProcessaFilaIncidentesTask>();
            services.AddScoped<ProcessaCallbackEntregaDevolucaoTask>();
            services.AddScoped<ProcessarPedidoPendenteBSellerTask>();
            services.AddScoped<ProcessarPedidoPendenteTransportadorTask>();
            services.AddScoped<PopulaFilaReciclagemEtiquetaShipNTask>();
            services.AddScoped<ProcessarMenuFreteTabelaArquivoTask>();
            services.AddScoped<ProcessaEntregaStageReprocessamentoTask>();
            services.AddScoped<ProcessarRecotacaoFreteTask>();
            services.AddScoped<ImportacaoSkuAnymarketTask>();
            services.AddScoped<ImportacaoSkuVtexTask>();
            services.AddScoped<ProcessaConciliacaoTask>();
            services.AddScoped<ProcessaFaturaTask>();
            services.AddScoped<ProcessaConciliacaoControleTask>();
            services.AddScoped<ProcessaConciliacaoTransportadorTask>();
            services.AddScoped<ProcessaConciliacaoRecotacaoTask>();
            services.AddScoped<ProcessarAtualizacaoTabelasCorreiosTask>();

            services.RegisterScheduler();
            services.AddScoped<IUsuarioHelper, UsuarioHelper>();

            return services.BuildServiceProvider();
        }

        public static IServiceCollection RegisterScheduler(this IServiceCollection services)
        {
            services.AddScoped(x =>
            {
                var sched = new StdSchedulerFactory().GetScheduler()
                    .GetAwaiter()
                    .GetResult();

                sched.JobFactory = new JobTaskFactory(x);
                return sched;
            });

            return services;
        }
    }
}

