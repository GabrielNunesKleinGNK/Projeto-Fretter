using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.IO;
using Newtonsoft.Json;
using Fretter.Domain.Interfaces.Mensageria;
using Fretter.WebHook.Api.Helpers;
using Fretter.WebHook.Api.Models;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.IoC;
using Fretter.Repository.Contexts;

namespace Fretter.WebHook.Api.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private readonly ApiKeyMemoryCache _cache;
        private User _user;
        private LogRequest _logRequest;
        private Stopwatch _watch;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, ApiKeyMemoryCache cache)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddleware>();
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
            _watch = new Stopwatch();
            _cache = cache;
        }

        public async Task Invoke(HttpContext context)
        {
            StringValues extractedApiKey = "";

            extractedApiKey = context.Request.Query["ApiKey"].FirstOrDefault();

            if (string.IsNullOrEmpty(extractedApiKey))
                context.Request.Headers.TryGetValue("ApiKey", out extractedApiKey);

            var apikeyCache = _cache.Get(extractedApiKey);
            var apiKeyEmCache = string.IsNullOrEmpty(apikeyCache) ? false : true;

            if (apiKeyEmCache)
                _user = JsonConvert.DeserializeObject<User>(apikeyCache);

            await LogRequest(context);
            await LogResponse(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            _watch.Reset();
            _watch.Start();
            context.Request.EnableBuffering();
            var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);

            _logRequest = new LogRequest(context.Request.Host.ToString(), context.Request.Path, context.Request.Method, context.Request.QueryString.ToString(), ReadStreamInChunks(requestStream), "", 1, context.Connection.RemoteIpAddress.ToString(), 0, 0);
            context.Request.Body.Position = 0;
        }

        private async Task LogResponse(HttpContext context)
        {
            var publisher = ServiceLocator.Resolve<IRabbitMQPublisher>();

            var originalBodyStream = context.Response.Body;
            var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;
            await _next(context);
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            _logRequest.ExecutionTime = _watch.ElapsedMilliseconds;
            _logRequest.StatusCode = context.Response.StatusCode;
            _logRequest.ResponseBody = text;
            _logRequest.DataCadastro = DateTime.Now;

            if (_user != null && _user.Id > 0)
                _logRequest.UsuarioCadastro = _user.Id;

            _watch.Stop();

            try
            {

                publisher.PublishMessage<LogRequest>("LogRequest", _logRequest);

                // _repository.ExecuteStoredProcedure<LogRequest, LogRequest>(_logRequest, "SetLogRequest");
            }
            catch (Exception)
            {

            }

            await responseBody.CopyToAsync(originalBodyStream);
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            var textWriter = new StringWriter();
            var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }
    }
}
