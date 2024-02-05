using Fretter.Domain.Config;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Fretter.Domain.Helpers;
using Microsoft.Extensions.Options;
using Fretter.Domain.Dto.Carrefour.Mirakl;
using System.Reflection;

namespace Fretter.Domain.Services
{
    public class EntregaStageCallBackService<TContext> : ServiceBase<TContext, EntregaStageCallBack>, IEntregaStageCallBackService<TContext>
      where TContext : IUnitOfWork<TContext>
    {
        private readonly IMessageBusService<EntregaStageCallBackService<TContext>> _bus;
        private readonly MessageBusConfig _config;
        private readonly IEntregaStageCallBackRepository<TContext> _Repository;
        private readonly IEntregaConfiguracaoService<TContext> _serviceConfiguracao;
        private readonly IEntregaStageEnvioLogService<TContext> _serviceEnvioLog;
        private readonly IEntregaStageErroRepository<TContext> _entregaStageErroRepository;
		private readonly ImportacaoEntregaConfig _importacaoEntregaConfig;

		public EntregaStageCallBackService(IEntregaStageCallBackRepository<TContext> Repository,
                                            IEntregaStageErroRepository<TContext> entregaStageErroRepository,
                                            IUnitOfWork<TContext> unitOfWork, IUsuarioHelper user,
                                            IMessageBusService<EntregaStageCallBackService<TContext>> bus,
                                            IEntregaConfiguracaoService<TContext> serviceConfiguracao,
                                            IEntregaStageEnvioLogService<TContext> serviceEnvioLog,
                                            IOptions<MessageBusConfig> config,
											IOptions<ImportacaoEntregaConfig> importacaoEntregaConfig

											)
            : base(Repository, unitOfWork, user)
        {
            _entregaStageErroRepository = entregaStageErroRepository;
            _Repository = Repository;
            _serviceConfiguracao = serviceConfiguracao;
            _serviceEnvioLog = serviceEnvioLog;
            _config = config.Value;
            _bus = bus;
			_importacaoEntregaConfig = importacaoEntregaConfig.Value;
			_bus.InitReceiver(Enum.ReceiverType.Queue, _config.ConnectionString, _config.EntregaStageCallbackQueue, "", _config.PreFetchCount);
        }

        public async Task<int> ProcessaCallbackEntregaStage()
        {
            IList<MessageData<EntregaStageCallBack>> entregas;

            try
            {
                entregas = await _bus.Receive<EntregaStageCallBack>(_config.ConsumeCount, 10);
            }
            catch (Exception ex)
            {
                var messageException = ex.Message + " " + (ex.InnerException != null ? ex.InnerException.Message : string.Empty);

                Thread.Sleep(60000);
                return 0;
            }

            if (entregas.Any())
            {
                var configs = _serviceConfiguracao.GetListaConfiguracoesPorIdTipo(Enum.EnumEntregaConfiguracaoTipo.CallBackUrl.GetHashCode());
                var marketplaces = entregas.GroupBy(a => a.Body.Id_EmpresaMarketplace);

                foreach (var empresaMarketplace in marketplaces)
                {
                    bool eCarrefour = _importacaoEntregaConfig.IntegracaoCRF.EmpresaId == empresaMarketplace.Key;

					var config = configs.Where(a => a.EmpresaId == empresaMarketplace.Key).FirstOrDefault();
                    WebApiClient webApiClient = eCarrefour ? new WebApiClient(config.Caminho, config.ApiKey) : new WebApiClient(config.Caminho);

                    if (!eCarrefour)
                        webApiClient.AddHeader("ApiKey", config.ApiKey);

					EntregaStageEnvioLog envioLog = new EntregaStageEnvioLog();

                    foreach (var entrega in entregas.Where(a => a.Body.Id_EmpresaMarketplace == empresaMarketplace.Key))
                    {
                        try
                        {
                            var jsonDados = JsonConvert.SerializeObject(entrega.Body);
                            EntregaStageCallBack etiquetaStageCallBack = entrega.Body;

                            List<OrderAdditionalFieldsDTO> orderAdditionals = new List<OrderAdditionalFieldsDTO>();

                            if (eCarrefour)
                                orderAdditionals = ObterCamposAdicionais(etiquetaStageCallBack);
                            else
								orderAdditionals = ObterCamposAdicionaisLRY(etiquetaStageCallBack);

							var json = JsonConvert.SerializeObject(new { order_additional_fields = orderAdditionals });
                            var result = await webApiClient.Put<object>(config.URLEtiquetaCallBack.Replace(":order_id", entrega.Body.Cd_EntregaSaida), new { order_additional_fields = orderAdditionals });

                            if (result.StatusCode != System.Net.HttpStatusCode.Accepted && result.StatusCode != System.Net.HttpStatusCode.OK)
                            {
                                envioLog = PreparaLog(config, entrega.Body, json, result.Content.ReadAsStringAsync().Result, false);
                                entrega.IsCompleted = false;
                            }
                            else
                            {
                                envioLog = PreparaLog(config, entrega.Body, json, result.Content.ReadAsStringAsync().Result, true);
                                entrega.IsCompleted = true;
                            }

                            var configsIncidente = _serviceConfiguracao.GetListaConfiguracoesPorIdTipo(Enum.EnumEntregaConfiguracaoTipo.GerenciarIncidenteMirakl.GetHashCode());

                            var configIncidente = configsIncidente.FirstOrDefault(a => a.EmpresaId == empresaMarketplace.Key);

                            if (configIncidente == null)
                                continue;

							if (etiquetaStageCallBack.Erros.Any() && etiquetaStageCallBack.Cd_EntregaSaidaItens?.Count > 0)
                                GerenciarIncidenteMiraklEntregasComErro(configIncidente, etiquetaStageCallBack, Enum.EnumEntregaConfiguracaoItemTipo.AberturaIncidente);
                            else if (!etiquetaStageCallBack.Erros.Any() && etiquetaStageCallBack.Cd_EntregaSaidaItens?.Count > 0 && (etiquetaStageCallBack.Flg_ContemIncidente ?? false))
                                GerenciarIncidenteMiraklEntregasComErro(configIncidente, etiquetaStageCallBack, Enum.EnumEntregaConfiguracaoItemTipo.FechamentoIncidente);
                        }
                        catch (Exception ex)
                        {
                            envioLog = PreparaLog(config, entrega.Body, JsonConvert.SerializeObject(entrega.Body), "Mensagem: " + ex.Message + "StackTrace: " + ex.StackTrace, false);
                            entrega.IsCompleted = false;
                        }
				}

                    _serviceEnvioLog.Save(envioLog);
                    _unitOfWork.Commit();
                }

                if (entregas.Where(e => e.IsCompleted).Count() > 0)
                    await _bus.Commit(entregas.Where(e => e.IsCompleted).ToList());
            }
            return entregas.Count();
        }

        private List<OrderAdditionalFieldsDTO> ObterCamposAdicionais(EntregaStageCallBack etiquetaStageCallBack)
        {
            List<OrderAdditionalFieldsDTO> list = new List<OrderAdditionalFieldsDTO>();

            foreach (PropertyInfo propertyInfo in etiquetaStageCallBack.GetType().GetProperties())
            {
                if (!string.IsNullOrEmpty(propertyInfo.Name) &&
                    (propertyInfo.Name == "Ds_LinkEtiquetaPDF" || propertyInfo.Name == "Dt_ValidadeFimEtiqueta" || propertyInfo.Name == "Ds_NomeTransportador" || propertyInfo.Name == "Erros" ||
                    propertyInfo.Name == "Cd_Sro" || propertyInfo.Name == "Flg_NovoEnvio"))
                {
                    if (!etiquetaStageCallBack.Erros.Any()) //Quando não existe erros
                    {
                        if (propertyInfo.Name == "Ds_LinkEtiquetaPDF" && !string.IsNullOrEmpty(propertyInfo.GetValue(etiquetaStageCallBack)?.ToString()))
                        {
                            list.Add(new OrderAdditionalFieldsDTO()
                            {
                                code = "etiquetaenvios",
                                value = propertyInfo.GetValue(etiquetaStageCallBack)?.ToString()
                            });

                            list.Add(new OrderAdditionalFieldsDTO()
                            {
                                code = "envios-callback-descricao",
                                value = "Sucesso"
                            });

                            list.Add(new OrderAdditionalFieldsDTO()
                            {
                                code = "envios-callback-id",
                                value = "0"
                            });
                        }

                        else if (propertyInfo.Name == "Dt_ValidadeFimEtiqueta" && !string.IsNullOrEmpty(propertyInfo.GetValue(etiquetaStageCallBack)?.ToString()))
                            list.Add(new OrderAdditionalFieldsDTO()
                            {
                                code = "validadeetiquetaenvios",
                                value = Convert.ToDateTime(propertyInfo.GetValue(etiquetaStageCallBack)).ToString("yyyy-MM-ddTHH:mm:ss.fff")
                            });

                        else if (propertyInfo.Name == "Cd_Sro" && !string.IsNullOrEmpty(propertyInfo.GetValue(etiquetaStageCallBack)?.ToString()))
                            list.Add(new OrderAdditionalFieldsDTO()
                            {
                                code = "codigo-tacking",
                                value = propertyInfo.GetValue(etiquetaStageCallBack)?.ToString()
                            });

                        else if (propertyInfo.Name == "Flg_NovoEnvio" && !string.IsNullOrEmpty(propertyInfo.GetValue(etiquetaStageCallBack)?.ToString()))
                        {
                            bool.TryParse(propertyInfo.GetValue(etiquetaStageCallBack)?.ToString(), out bool novoenvio);
                            list.Add(new OrderAdditionalFieldsDTO()
                            {
                                code = "novoenvio",
                                value = novoenvio.ToString().ToLower()
                            });

                            if (novoenvio)
                                list.Add(new OrderAdditionalFieldsDTO()
                                {
                                    code = "shipping-tracking-id",
                                    value = ""
                                });

                        }
                    }
                    else if (etiquetaStageCallBack.Erros.Any()) //Quando existe erros
                    {
                        if (propertyInfo.Name == "Erros")
                        {
                            list.Add(new OrderAdditionalFieldsDTO()
                            {
                                code = "envios-callback-descricao",
                                value = etiquetaStageCallBack.Erros[0].Ds_Descricao
                            });

                            list.Add(new OrderAdditionalFieldsDTO()
                            {
                                code = "envios-callback-id",
                                value = etiquetaStageCallBack.Erros[0].Cd_Codigo
                            });
                        }
                    }
                }
            }

            return list;
        }

		private List<OrderAdditionalFieldsDTO> ObterCamposAdicionaisLRY(EntregaStageCallBack etiquetaStageCallBack)
		{
			List<OrderAdditionalFieldsDTO> list = new List<OrderAdditionalFieldsDTO>();

			foreach (PropertyInfo propertyInfo in etiquetaStageCallBack.GetType().GetProperties())
			{
				if (!string.IsNullOrEmpty(propertyInfo.Name) &&
					(propertyInfo.Name == "Ds_LinkEtiquetaPDF" || 
                    propertyInfo.Name == "Dt_ValidadeFimEtiqueta" || 
                    propertyInfo.Name == "Ds_NomeTransportador" || 
                    propertyInfo.Name == "Erros" 
					))
				{
					if (!etiquetaStageCallBack.Erros.Any()) //Quando não existe erros
					{
						if (propertyInfo.Name == "Ds_LinkEtiquetaPDF" && !string.IsNullOrEmpty(propertyInfo.GetValue(etiquetaStageCallBack)?.ToString()))
						{
							list.Add(new OrderAdditionalFieldsDTO()
							{
								code = "shipping-label-link",
								value = propertyInfo.GetValue(etiquetaStageCallBack)?.ToString()
							});

							list.Add(new OrderAdditionalFieldsDTO()
							{
								code = "shipping-label-callback-description",
								value = "Sucesso"
							});

							list.Add(new OrderAdditionalFieldsDTO()
							{
								code = "shipping-label-callback-id",
								value = "0"
							});
						}
						else if (propertyInfo.Name == "Dt_ValidadeFimEtiqueta" && !string.IsNullOrEmpty(propertyInfo.GetValue(etiquetaStageCallBack)?.ToString()))
							list.Add(new OrderAdditionalFieldsDTO()
							{
								code = "shipping-label-expiration-date",
								value = Convert.ToDateTime(propertyInfo.GetValue(etiquetaStageCallBack)).ToString("yyyy-MM-ddTHH:mm:ss.fff")
							});										
					}
					else if (etiquetaStageCallBack.Erros.Any()) //Quando existe erros
					{
						if (propertyInfo.Name == "Erros")
						{
							list.Add(new OrderAdditionalFieldsDTO()
							{
								code = "shipping-label-callback-description",
								value = etiquetaStageCallBack.Erros[0].Ds_Descricao
							});

							list.Add(new OrderAdditionalFieldsDTO()
							{
								code = "shipping-label-callback-id",
								value = etiquetaStageCallBack.Erros[0].Cd_Codigo
							});
						}
					}
				}
			}

			return list;
		}

		private EntregaStageEnvioLog PreparaLog(EntregaConfiguracao configs, EntregaStageCallBack entregas, string json, string retorno, bool sucesso)
        {
            var log = new EntregaStageEnvioLog();
            log.AtualizarEntregaConfiguracaoId(configs.Id);
            log.AtualizarCodidoDasEntregas(entregas.Cd_EntregaSaida);
            log.AtualizarProcessamento(DateTime.Now);
            log.AtualizarJson(json);
            log.AtualizarRetorno(retorno);
            log.AtualizarSucesso(sucesso);
            log.AtualizarProcessado(sucesso);

            return log;
        }

        private void GerenciarIncidenteMiraklEntregasComErro(EntregaConfiguracao entregaConfig, EntregaStageCallBack entrega, Enum.EnumEntregaConfiguracaoItemTipo tipo)
        {
            List<EntregaStageErro> errosDeAbertura = new List<EntregaStageErro>();
            foreach (var entregaItem in entrega?.Cd_EntregaSaidaItens)
            {
                try
                {
                    var webApiClient = new WebApiClient(entregaConfig.Caminho, entregaConfig.ApiKey);
                    webApiClient.AddHeader("Authorization", entregaConfig.ApiKey);
                    HttpResponseMessage retorno = new HttpResponseMessage();
                    switch (tipo)
                    {
                        case Enum.EnumEntregaConfiguracaoItemTipo.AberturaIncidente:
                            retorno = webApiClient.Post(
                                entregaConfig.Itens?.FirstOrDefault(x => x.EntregaConfiguracaoItemTipoId == Enum.EnumEntregaConfiguracaoItemTipo.AberturaIncidente.GetHashCode()).Url
                                .Replace(":order_id", entrega.Cd_EntregaSaida).Replace(":line", entregaItem),
                                new { reason_code = "118" }).Result;
                            break;
                        case Enum.EnumEntregaConfiguracaoItemTipo.FechamentoIncidente:
                            retorno = webApiClient.Put(
                                entregaConfig.Itens.FirstOrDefault(x => x.EntregaConfiguracaoItemTipoId == Enum.EnumEntregaConfiguracaoItemTipo.FechamentoIncidente.GetHashCode()).Url
                                .Replace(":order_id", entrega.Cd_EntregaSaida).Replace(":line", entregaItem),
                                new { reason_code = "97" }).Result;
                            break;
                    }

                    if (!retorno.IsSuccessStatusCode)
                        errosDeAbertura.Add(new EntregaStageErro
                        (
                            Id: 0,
                            Importacao: DateTime.Now,
                            Arquivo: 0,
                            Retorno: $"Item: {entregaItem}. Retorno: { retorno.Content.ReadAsStringAsync().Result }",
                            CodigoErro: 27,
                            EntregaSaida: entrega.Cd_EntregaSaida
                        ));
                }
                catch (Exception ex)
                {
                    errosDeAbertura.Add(new EntregaStageErro
                    (
                        Id: 0,
                        Importacao: DateTime.Now,
                        Arquivo: 0,
                        Retorno: $"Item: {entregaItem}. Exception: { ex.Message }",
                        CodigoErro: 27,
                        EntregaSaida: entrega.Cd_EntregaSaida
                    ));
                }
            }

            if (errosDeAbertura.Any())
                _entregaStageErroRepository.SaveRange(errosDeAbertura);
        }
    }
}
