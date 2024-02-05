using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fretter.Domain.Config;
using Fretter.Domain.Dto.Dashboard;
using Fretter.Domain.Dto.LogElasticSearch;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.Extensions.Options;
using Nest;
using Newtonsoft.Json;

namespace Fretter.Domain.Services
{
    public class LogElasticSearchService : ILogElasticSearchService
    {
        private readonly IElasticSearchRepository _repositoryElastic;
        private readonly ElasticSearchConfig _elasticSearchConfig;

        public LogElasticSearchService(
            IElasticSearchRepository repositoryElastic,
            IOptions<ElasticSearchConfig> elasticSearchConfig)
        {
            _elasticSearchConfig = elasticSearchConfig.Value;
            _repositoryElastic = repositoryElastic;
            _repositoryElastic.InitElasticSearch(_elasticSearchConfig, Enum.EnumElasticConexaoTipo.PorUri);
        }

        public async Task<List<LogDashboardLista>> GetLogDashboardLista(LogDashboardFiltro logDashboardFiltro)
        {
            var result = new List<LogDashboardLista>();
            ValidarDataFiltro(logDashboardFiltro);
            try
            {
                var parameters = GetParameters();
                var resultElastic = await _repositoryElastic.Search(_elasticSearchConfig.AppLogUxIndex, parameters, logDashboardFiltro);
                var total = resultElastic.Total;
                var objResultJson = JsonConvert.SerializeObject(resultElastic.Documents);
                var resultMessage = JsonConvert.DeserializeObject<List<LogElasticSearchMessage>>(objResultJson);

                resultMessage.ForEach(messageElastic =>
                {
                    result.Add(new LogDashboardLista()
                    {
                        ApplicationName = messageElastic.fields.ApplicationName,
                        ClassName = messageElastic.fields.ClassName,
                        Duracao = messageElastic.fields.Duracao,
                        EnvironmentName = messageElastic.fields.EnvironmentName,
                        ExceptionDetail = messageElastic.fields.ExceptionDetail?.Type,
                        Level = messageElastic.level,
                        LineNumber = messageElastic.fields.LineNumber,
                        MachineName = messageElastic.fields.MachineName,
                        Message = messageElastic.message,
                        MethodName = messageElastic.fields.MethodName,
                        ObjectJson = JsonConvert.SerializeObject(messageElastic),
                        ProcessName = messageElastic.fields.ProcessName,
                        Termino = messageElastic.fields.Termino,
                        Timestamp = messageElastic.timestamp.AddHours(3),
                    });
                });

                return result;
            }
            catch (System.Exception e)
            {
                return null;
            }

            return result;
        }

        public async Task<List<LogDashboardConsolidado>> GetLogDashboardResumo(LogDashboardFiltro logDashboardFiltro)
        {
            try
            {
                ValidarDataFiltro(logDashboardFiltro);
                var result = await GetResultAggregate(logDashboardFiltro, "level.raw", "fields.ApplicationName.raw");

                result.ForEach(f =>
                {
                    switch (f.Type)
                    {
                        case "Information":
                            f.Color = "blue";
                            f.Type = "Informação";
                            break;
                        case "Warning":
                            f.Color = "yellow";
                            f.Type = "Alerta";
                            break;
                        case "Error":
                            f.Color = "red";
                            f.Type = "Erro";
                            break;
                    }
                });

                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<LogDashboardDiario>> GetLogDashboardDiario(LogDashboardFiltro logDashboardFiltro)
        {
            try
            {
                ValidarDataFiltro(logDashboardFiltro);
                var result = new List<LogDashboardDiario>();
                var resultLevel = await GetResultAggregate(logDashboardFiltro, "level.raw", null);

                await resultLevel.ForEachAsync(async item =>
                {
                    logDashboardFiltro.Level = item.Type;
                    var resultData = await GetResultAggregate(logDashboardFiltro, "@timestamp", null, dateInterval: DateInterval.Day);

                    resultData.ForEach(dataResult =>
                    {
                        result.Add(new LogDashboardDiario()
                        {
                            Data = dataResult.Date.AddHours(3),
                            Quantidade = dataResult.Total,
                            Status = item.Type,
                        });
                    });
                });

                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<LogDashboardConsolidado>> GetLogDashboardProcesso(LogDashboardFiltro logDashboardFiltro)
        {
            try
            {
                ValidarDataFiltro(logDashboardFiltro);
                return await GetResultAggregate(logDashboardFiltro, "fields.ProcessName.raw", null);
            }
            catch
            {
                return null;
            }
        }

        private static void ValidarDataFiltro(LogDashboardFiltro logDashboardFiltro)
        {
            if (logDashboardFiltro.DataInicio.HasValue)
                logDashboardFiltro.DataInicio = logDashboardFiltro.DataInicio.Value.ToLocalTime();

            if (logDashboardFiltro.DataTermino.HasValue)
                logDashboardFiltro.DataTermino = logDashboardFiltro.DataTermino.Value.ToLocalTime();

            if (logDashboardFiltro.DataInicio != null && logDashboardFiltro.DataInicio == logDashboardFiltro.DataTermino)
                logDashboardFiltro.DataInicio = new DateTime(logDashboardFiltro.DataInicio.Value.Year,
                                                logDashboardFiltro.DataInicio.Value.Month,
                                                logDashboardFiltro.DataInicio.Value.Day);
        }

        private async Task<List<LogDashboardConsolidado>> GetResultAggregate(LogDashboardFiltro logDashboardFiltro, string fieldAggregate,
            string subFieldAggregate, long total = 0, DateInterval? dateInterval = null)
        {
            var resultElastic = await _repositoryElastic.Search(_elasticSearchConfig.AppLogUxIndex, GetParameters(),
                   logDashboardFiltro, fieldAggregate: fieldAggregate, dateInterval: dateInterval);

            var result = new List<LogDashboardConsolidado>();
            if (resultElastic.IsValid)
            {
                var aggregate = ((BucketAggregate)resultElastic.Aggregations["item"]).Items.ToList();
                if (total == 0)
                    total = resultElastic.Total;
                aggregate.ForEach(f =>
                {

                    if (dateInterval != null)
                    {
                        var keyedBucket = (Nest.DateHistogramBucket)f;
                        result.Add(new LogDashboardConsolidado()
                        {
                            Date = keyedBucket.Date,
                            Total = (long)keyedBucket.DocCount
                        });
                    }
                    else
                    {
                        var keyedBucket = (KeyedBucket<object>)f;
                        var percent = Math.Round(((decimal)keyedBucket.DocCount / (decimal)total) * 100, 2);

                        result.Add(new LogDashboardConsolidado()
                        {
                            Type = keyedBucket.Key.ToString(),
                            Total = (int)keyedBucket.DocCount,
                            Percent = percent,
                        });
                    }
                });

                if (!string.IsNullOrEmpty(subFieldAggregate))
                    await result.ForEachAsync(async item =>
                    {
                        logDashboardFiltro.Level = item.Type;
                        item.Subs = await GetResultAggregate(logDashboardFiltro, subFieldAggregate, null, total);
                    });
            }
            return result;
        }

        private static Dictionary<string, string> GetParameters()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("Level", "level");
            parameters.Add("DataInicio", ">=@timestamp");
            parameters.Add("DataTermino", "<=@timestamp");
            parameters.Add("Application", "fields.ApplicationName");
            parameters.Add("Method", "fields.MethodName");
            parameters.Add("Process", "fields.ProcessName");
            return parameters;
        }

        private static Dictionary<string, string> GetParametersMenuFrete()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("Level", "level");
            parameters.Add("DataInicio", ">=timeStamp");
            parameters.Add("DataTermino", "<=timeStamp");
            parameters.Add("Exists_CotacaoCepInvalido", "Exists_messageObject.delivery_not_available");
            parameters.Add("EmpresaId", "messageObject.empresaId");
            parameters.Add("Instancia", "messageObject.instancia");
            return parameters;
        }

        private async Task<List<LogElasticSearchMessage>> GetAllDocuments(LogDashboardFiltro logDashboardFiltro, long total)
        {
            var maxSizeElastic = 10000;

            if (maxSizeElastic > total)
            {
                var resultElastic = await _repositoryElastic.Search(_elasticSearchConfig.AppLogUxIndex, GetParameters(),
                    logDashboardFiltro, (int)total);
                var objResultJson = JsonConvert.SerializeObject(resultElastic.Documents);
                return JsonConvert.DeserializeObject<List<LogElasticSearchMessage>>(objResultJson);
            }

            var result = new List<LogElasticSearchMessage>();
            var index = -1;
            var resultPages = (new int[((long)total / maxSizeElastic) + 1]).Select(s =>
            {
                index++;
                return index;
            }).ToList();

            var resultDocuments = new List<object>();
            await resultPages.ForEachAsync(async page =>
            {
                int? skip = page > 0 ? (int?)null : maxSizeElastic * page;
                var size = maxSizeElastic > (total - skip) ? (total - skip) : maxSizeElastic;
                var resultElastic = await _repositoryElastic.Search(_elasticSearchConfig.AppLogUxIndex, GetParameters(),
                logDashboardFiltro, maxSizeElastic, skip);
                resultDocuments.AddRange(resultElastic.Documents);

            });
            var objDocumentsJson = JsonConvert.SerializeObject(resultDocuments);
            return JsonConvert.DeserializeObject<List<LogElasticSearchMessage>>(objDocumentsJson);
        }

        public async Task<List<LogCotacaoFreteLista>> GetLogCotacaoFreteLista(LogCotacaoFreteFiltro logDashboardFiltro)
        {
            var result = new List<LogCotacaoFreteLista>();
            try
            {                
                var parameters = GetParametersMenuFrete();
                var resultElastic = await _repositoryElastic.Search(_elasticSearchConfig.LogMenuFreteIndex, parameters, logDashboardFiltro,1001);
                var total = resultElastic.Total;
                var objResultJson = JsonConvert.SerializeObject(resultElastic.Documents);
                var resultMessage = JsonConvert.DeserializeObject<List<Dto.LogCotacaoFrete.LogCotacaoFreteMessage>>(objResultJson);

                resultMessage.ForEach(logFreteMessage =>
                {
                    result.Add(new LogCotacaoFreteLista()
                    {
                        CotacaoPeso = logFreteMessage.messageObject.peso.ToString(),
                        CotacaoCepInvalido = logFreteMessage.messageObject.delivery_not_available,
                        EmpresaId = logFreteMessage.messageObject.empresaId.ToString(),
                        MessageURI = logFreteMessage.messageObject.uri,
                        MessageAction = logFreteMessage.messageObject.action,
                        MessageBody = JsonConvert.SerializeObject(logFreteMessage.messageObject.data.body),
                        MessageHeader = JsonConvert.SerializeObject(logFreteMessage.messageObject.data.header),
                        ProcessName = logFreteMessage.dsProcesso,
                        HostName = logFreteMessage.hostName,
                        Level = logFreteMessage.level,
                        Instancia = logFreteMessage.messageObject.instancia,
                        ObjectJson = JsonConvert.SerializeObject(logFreteMessage.messageObject),
                        Timestamp = logFreteMessage.timeStamp.AddHours(3),
                    }); ;
                });

                return result;
            }
            catch (System.Exception e)
            {
                return null;
            }

            return result;
        }

    }
}
