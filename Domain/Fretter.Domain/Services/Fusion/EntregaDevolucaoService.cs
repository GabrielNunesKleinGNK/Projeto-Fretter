using Fretter.Domain.Config;
using Fretter.Domain.Dto.Carrefour;
using Fretter.Domain.Dto.EntregaDevolucao;
using Fretter.Domain.Dto.LogisticaReversa;
using Fretter.Domain.Dto.LogisticaReversa.Enum;
using Fretter.Domain.Entities;
using Fretter.Domain.Enum;
using Fretter.Domain.Helpers;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static WSLogisticaReversa.logisticaReversaWSClient;

namespace Fretter.Domain.Services
{
    public class EntregaDevolucaoService<TContext> : ServiceBase<TContext, EntregaDevolucao>, IEntregaDevolucaoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        private readonly IEntregaDevolucaoRepository<TContext> _repository;
        private readonly IMessageBusService<EntregaDevolucaoService<TContext>> _messageBusService;
        private readonly IMessageBusService<EntregaDevolucaoAcaoService<TContext>> _messageBusServiceReversa;
        private readonly IMessageBusService<MessageBusConfig> _messageBusServiceStage;
        private readonly MessageBusConfig _messageBusConfig;
        private readonly MessageBusShipNConfig _messageBusShipNConfig;
        private readonly IEntregaRepository<TContext> _entregaRepository;
        private readonly IEntregaStageErroRepository<TContext> _entregaStageErroRepository;
        private readonly IEntregaDevolucaoStatusAcaoService<TContext> _entregaStatusAcaoService;
        private readonly IEntregaDevolucaoHistoricoService<TContext> _entregaHistoricoService;
        private readonly IEntregaTransporteTipoRepository<TContext> _tipoRepository;

        public EntregaDevolucaoService(IEntregaDevolucaoRepository<TContext> repository,
                                       IUnitOfWork<TContext> unitOfWork,
                                       IMessageBusService<EntregaDevolucaoService<TContext>> messageBusService,
                                       IMessageBusService<EntregaDevolucaoAcaoService<TContext>> messageBusServiceReversa,
                                       IMessageBusService<MessageBusConfig> messageBusServiceStage,
                                       IOptions<MessageBusConfig> messageBusConfig,
                                       IOptions<MessageBusShipNConfig> messageBusShipNConfig,
                                       IEntregaRepository<TContext> entregaRepository,
                                       IEntregaStageErroRepository<TContext> entregaStageErroRepository,
                                       IEntregaDevolucaoStatusAcaoService<TContext> entregaStatusAcaoService,
                                       IEntregaDevolucaoHistoricoService<TContext> entregaHistoricoService,
                                       IEntregaTransporteTipoRepository<TContext> tipoRepository,
                                    IUsuarioHelper user) : base(repository, unitOfWork, user)
        {
            _repository = repository;
            _entregaRepository = entregaRepository;
            _entregaStageErroRepository = entregaStageErroRepository;
            _entregaStatusAcaoService = entregaStatusAcaoService;
            _entregaHistoricoService = entregaHistoricoService;
            _user = user;
            _tipoRepository = tipoRepository;
            _messageBusConfig = messageBusConfig.Value;
            _messageBusShipNConfig = messageBusShipNConfig.Value;

            _messageBusService = messageBusService;
            _messageBusService.InitReceiver(Enum.ReceiverType.Queue, _messageBusConfig.ConnectionString, _messageBusConfig.EntregaIncidenteQueue);
            _messageBusServiceReversa = messageBusServiceReversa;
            _messageBusServiceReversa.InitSender(_messageBusConfig.ConnectionString, _messageBusConfig.EntregaDevolucaoCallbackQueue);
            _messageBusServiceStage = messageBusServiceStage;
            _messageBusServiceStage.InitSender(_messageBusConfig.ConnectionString, _messageBusShipNConfig.EntregaStageTopic);
        }
        public async Task<int> ProcessaEntregaDevolucaoIntegracao()
        {
            var listEntregaReversa = _repository.BuscaEntregaDevolucaoPendente();
            if (listEntregaReversa.Count > 0)
            {
                var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
                binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
                binding.UseDefaultWebProxy = true;
                var endpointAddress = new EndpointAddress(listEntregaReversa.FirstOrDefault().Ds_ServicoURL);
                var clientWS = new WSLogisticaReversa.logisticaReversaWSClient(binding, endpointAddress);
                clientWS.ClientCredentials.UserName.UserName = listEntregaReversa.FirstOrDefault().Ds_ServicoUsuario;
                clientWS.ClientCredentials.UserName.Password = listEntregaReversa.FirstOrDefault().Ds_ServicoSenha;

                List<DevolucaoCorreioRetorno> listDevolucaoRetorno = new List<DevolucaoCorreioRetorno>();
                DevolucaoCorreioRetorno devolucaoRetorno = new DevolucaoCorreioRetorno();
                WSLogisticaReversa.solicitarPostagemReversaResponse reversaRetorno = new WSLogisticaReversa.solicitarPostagemReversaResponse();

                foreach (var entrega in listEntregaReversa)
                {
                    try
                    {
                        var entregaDestinatarioCorreio = new WSLogisticaReversa.pessoa()
                        {
                            nome = entrega.Ds_DestinatarioNome,
                            logradouro = entrega.Ds_DestinatarioLogradouro,
                            numero = entrega.Ds_DestinatarioNumero,
                            bairro = entrega.Ds_DestinatarioBairro,
                            cidade = entrega.Ds_DestinatarioCidade,
                            cep = entrega.Ds_DestinatarioCEP,
                            complemento = entrega.Ds_DestinatarioComplemento,
                            uf = entrega.Ds_DestinatarioUF,
                            ddd = entrega.Ds_DestinatarioDDD,
                            telefone = entrega.Ds_DestinatarioTelefone,
                            email = entrega.Ds_DestinatarioEmail,
                            ciencia_conteudo_proibido = "N"
                        };
                        WSLogisticaReversa.coletaReversa entregaColetaCorreio = CarregaEntregaReversa(entrega);
                        var listColetas = new List<WSLogisticaReversa.coletaReversa>();
                        listColetas.Add(entregaColetaCorreio);

                        reversaRetorno = await clientWS.solicitarPostagemReversaAsync(entrega.Cd_CodigoIntegracao, entrega.Cd_CodigoServico, entrega.Cd_CodigoCartao, entregaDestinatarioCorreio, listColetas.ToArray());
                        devolucaoRetorno = ConverteRetornoPostagem(reversaRetorno, entrega, EnumDevolucaoCorreioRetornoTipo.Solicitacao, false);
                        devolucaoRetorno.Ds_Mensagem = GeraObjetoDevolucaoSolicitacao(entrega.Cd_CodigoIntegracao, entrega.Cd_CodigoServico, entrega.Cd_CodigoCartao, entregaDestinatarioCorreio, listColetas.ToArray());
                    }
                    catch (System.Exception ex)
                    {
                        devolucaoRetorno.Flg_Sucesso = false;
                        devolucaoRetorno.Ds_Retorno = ex.Message;
                    }
                    finally
                    {
                        listDevolucaoRetorno.Add(devolucaoRetorno);
                    }
                }
                await clientWS.CloseAsync();
                await _messageBusServiceReversa.SendRange<DevolucaoCorreioRetorno>(listDevolucaoRetorno.Where(a => a.Flg_Sucesso).ToList());
                _repository.SalvarEntregaDevolucaoProcessada(CollectionHelper.ConvertTo<DevolucaoCorreioRetorno>(listDevolucaoRetorno));
            }
            return listEntregaReversa.Count;
        }

        private string GeraObjetoDevolucaoSolicitacao(string cd_CodigoIntegracao, string cd_CodigoServico, string cd_CodigoCartao, WSLogisticaReversa.pessoa entregaDestinatarioCorreio, WSLogisticaReversa.coletaReversa[] coletaReversas)
        {
            return JsonConvert.SerializeObject
                 (new
                 {
                     CodigoIntegracao = cd_CodigoIntegracao,
                     CodigoServico = cd_CodigoServico,
                     Destinatario = JsonConvert.SerializeObject(entregaDestinatarioCorreio),
                     Objetos = JsonConvert.SerializeObject(coletaReversas)
                 }
                 ).Truncate(4000);
        }

        public async Task<int> ProcessaEntregaDevolucaoCancelamento()
        {
            var listEntregaReversa = _repository.BuscaEntregaDevolucaoCancela();
            if (listEntregaReversa.Count > 0)
            {
                var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
                binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
                binding.UseDefaultWebProxy = true;
                var endpointAddress = new EndpointAddress(listEntregaReversa.FirstOrDefault().Ds_ServicoURL);
                var clientWS = new WSLogisticaReversa.logisticaReversaWSClient(binding, endpointAddress);
                clientWS.ClientCredentials.UserName.UserName = listEntregaReversa.FirstOrDefault().Ds_ServicoUsuario;
                clientWS.ClientCredentials.UserName.Password = listEntregaReversa.FirstOrDefault().Ds_ServicoSenha;

                List<DevolucaoCorreioRetorno> listDevolucaoRetorno = new List<DevolucaoCorreioRetorno>();
                DevolucaoCorreioRetorno devolucaoRetorno = new DevolucaoCorreioRetorno();
                WSLogisticaReversa.cancelarPedidoResponse cancelaRetorno = new WSLogisticaReversa.cancelarPedidoResponse();

                foreach (var entrega in listEntregaReversa)
                {
                    try
                    {
                        cancelaRetorno = await clientWS.cancelarPedidoAsync(entrega.Cd_CodigoIntegracao, entrega.Cd_CodigoColeta, entrega.Ds_SolicitacaoTipo);
                        devolucaoRetorno = ConverteRetornoCancelamento(cancelaRetorno, entrega);
                    }
                    catch (System.Exception ex)
                    {
                        devolucaoRetorno.Flg_Sucesso = false;
                        devolucaoRetorno.Ds_Retorno = ex.Message;
                    }
                    finally
                    {
                        listDevolucaoRetorno.Add(devolucaoRetorno);
                    }
                }

                await clientWS.CloseAsync();
                _repository.SalvarEntregaDevolucaoProcessada(CollectionHelper.ConvertTo<DevolucaoCorreioRetorno>(listDevolucaoRetorno));
            }

            return listEntregaReversa.Count;
        }
        public async Task<List<DevolucaoCorreioOcorrencia>> ProcessaEntregaDevolucaoOcorrencia()
        {
            Console.WriteLine($"Inicio do método ProcessaEntregaDevolucaoOcorrencia");
            var lstEntregaTransporteTipo = _tipoRepository.BuscaTipoTransporteAtivo();
            List<DevolucaoCorreioOcorrencia> listOcorrencias = new List<DevolucaoCorreioOcorrencia>();

            Console.WriteLine($"Inicio do foreach de leitura do lstEntregaTransporteTipo no método ProcessaEntregaDevolucaoOcorrencia");

            foreach (var tipo in lstEntregaTransporteTipo)
            {
                Console.WriteLine($"Inicio da busca de ocorrências no método ProcessaEntregaDevolucaoOcorrencia");
                DateTime dataBusca = DateTime.Now.AddDays(-2);
                for (int i = 0; dataBusca <= DateTime.Now; i++)
                {
                    string dataFormatada = $"{dataBusca.Day}/{dataBusca.Month}/{dataBusca.Year}";
                    Console.WriteLine($"Consultando {dataFormatada} - ProcessaEntregaDevolucaoOcorrencia");
                    string endPointUrl = Path.Combine(tipo.EntregaTransporteServico.URLBase, tipo.URL);
                    var binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);
                    binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
                    binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
                    binding.UseDefaultWebProxy = true;
                    binding.MaxReceivedMessageSize = Int32.MaxValue;
                    binding.MaxBufferSize = Int32.MaxValue;
                    binding.MaxBufferPoolSize = Int32.MaxValue;
                    binding.SendTimeout = new TimeSpan(2, 15, 0);

                    var endpointAddress = new EndpointAddress(endPointUrl);

                    var clientWS = new LogisticaReversaServiceCWS.logisticaReversaWSClient(binding, endpointAddress);
                    clientWS.ClientCredentials.UserName.UserName = tipo.EntregaTransporteServico.Usuario;
                    clientWS.ClientCredentials.UserName.Password = tipo.EntregaTransporteServico.Senha;

                    var ocorrencias = clientWS.acompanharPedidoPorData(tipo.EntregaTransporteServico.CodigoIntegracao, tipo.Alias, dataFormatada);
                    var coletas = ocorrencias?.coleta;

                    if (coletas != null)
                        foreach (var coleta in coletas)
                            listOcorrencias.AddRange(ConverteRetornoOcorrencia(coleta));

                    dataBusca = dataBusca.AddDays(1);

                    await clientWS.CloseAsync();
                }

                Console.WriteLine($"Quantidade de ocorrências encontradas no método ProcessaEntregaDevolucaoOcorrencia - {listOcorrencias.Count}");
                if (listOcorrencias.Count > 0)
                {
                    Console.WriteLine($"Inicio do salvamento dos dados e criação da Tb_entrega para reversa ProcessaEntregaDevolucaoOcorrencia");
                    try
                    {
                        _repository.BeginTransaction();
                        _repository.SalvarEntregaDevolucaoOcorrencia(CollectionHelper.ConvertTo<DevolucaoCorreioOcorrencia>(listOcorrencias));
                        InserirEntrega();
                        //await _messageBusServiceReversa.SendRange(ConverteOcorrenciaDevolucaoCorreio(listOcorrencias));

                        _repository.CommitTransaction();
                        Console.WriteLine($"Fim do salvamento dos dados e criação da Tb_entrega para reversa ProcessaEntregaDevolucaoOcorrencia");
                    }
                    catch (Exception ex)
                    {
                        _repository.RollbackTransaction();
                        Console.WriteLine($"Exception no salvamento dos dados e criação da Tb_entrega para reversa ProcessaEntregaDevolucaoOcorrencia - Mensagem : {ex}");
                        throw ex;
                    }
                }
            }

            return listOcorrencias;
        }
        
        private List<DevolucaoCorreioRetorno> ConverteOcorrenciaDevolucaoCorreio(List<DevolucaoCorreioOcorrencia> listOcorrencias)
        {
            return _repository.ProcessarEntregaDevolucaoOcorrencia(CollectionHelper.ConvertTo<DevolucaoCorreioOcorrencia>(listOcorrencias));
        }
        
        private List<DevolucaoCorreioOcorrencia> ConverteRetornoOcorrencia(LogisticaReversaServiceCWS.coletasSolicitadas coleta)
        {
            List<DevolucaoCorreioOcorrencia> listOcorrencias = new List<DevolucaoCorreioOcorrencia>();
            var historicos = coleta?.historico;
            var objetos = coleta?.objeto;

            foreach (var historico in historicos)
            {
                var ocorrencia = new DevolucaoCorreioOcorrencia()
                {
                    Cd_CodigoColeta = coleta.numero_pedido.ToString(),
                    Cd_CodigoIntegracao = coleta.controle_cliente,
                    Ds_Observacao = historico.observacao,
                    Ds_Ocorrencia = historico.descricao_status,
                    Dt_Ocorrencia = ConvertStringToDate(historico.data_atualizacao, "dd-MM-yyyy"),
                    Nm_Sigla = historico.status.ToString(),
                };

                ocorrencia.Dt_Ocorrencia = ocorrencia.Dt_Ocorrencia.Value.Add(ConvertStringToTime(historico.hora_atualizacao, "HH:mm:ss"));
                listOcorrencias.Add(ocorrencia);
            }

            foreach (var objeto in objetos)
            {
                var ocorrencia = new DevolucaoCorreioOcorrencia()
                {
                    Cd_CodigoColeta = coleta.numero_pedido.ToString(),
                    Cd_CodigoIntegracao = coleta.controle_cliente,
                    Ds_Observacao = objeto.descricao_status,
                    Ds_Ocorrencia = objeto.descricao_status,
                    Dt_Ocorrencia = ConvertStringToDate(objeto.data_ultima_atualizacao, "dd-MM-yyyy"),
                    Nm_Sigla = objeto.ultimo_status.ToString(),
                    Vl_PesoCubico = objeto.peso_cubico,
                    Vl_PesoReal = objeto.peso_real,
                    Vl_Postagem = objeto.valor_postagem,
                    Cd_CodigoRastreio = objeto.numero_etiqueta
                };

                ocorrencia.Dt_Ocorrencia = ocorrencia.Dt_Ocorrencia.Value.Add(ConvertStringToTime(objeto.hora_ultima_atualizacao, "HH:mm:ss"));
                listOcorrencias.Add(ocorrencia);
            }

            return listOcorrencias;
        }
        
        private DevolucaoCorreioRetorno ConverteRetornoPostagem(WSLogisticaReversa.solicitarPostagemReversaResponse reversaRetorno, DevolucaoCorreio entregaDevolucao, EnumDevolucaoCorreioRetornoTipo tipoRetorno, bool insereOcorrencia = false)
        {
            string xmlEnvelope;

            try
            {
                var stringwriter = new System.IO.StringWriter();
                var serializer = new XmlSerializer(typeof(WSLogisticaReversa.solicitarPostagemReversaResponse));
                serializer.Serialize(stringwriter, reversaRetorno);
                xmlEnvelope = stringwriter.ToString();
            }
            catch (Exception ex)
            {
                xmlEnvelope = $"Houve um erro ao montar o XML do Envelope : {ex.Message}";
            }


            var resultado = reversaRetorno?.solicitarPostagemReversa?.resultado_solicitacao;

            string mensagemRetorno = $"{resultado?.FirstOrDefault()?.codigo_erro} - {resultado?.FirstOrDefault()?.descricao_erro}";
            bool erro = false;
            if (resultado == null)
            {
                mensagemRetorno = reversaRetorno?.solicitarPostagemReversa?.msg_erro;
                erro = true;
            }

			var devolucaoRetorno = new DevolucaoCorreioRetorno()
            {
                Id_Empresa = entregaDevolucao.Id_Empresa,
                Cd_Pedido = entregaDevolucao.Cd_Pedido,
                Id_Entrega = entregaDevolucao.Id_Entrega,
                Id_DevolucaoCorreioRetornoTipo = tipoRetorno.GetHashCode(),
                Id_EntregaDevolucao = entregaDevolucao.Id_EntregaDevolucao,
                Id_EntregaDevolucaoStatus = entregaDevolucao.Id_EntregaDevolucaoStatus,
                Cd_CodigoColeta = resultado?.FirstOrDefault()?.numero_coleta,
                Cd_CodigoRastreio = resultado?.FirstOrDefault()?.numero_etiqueta,
                Dt_Validade = null,
                Ds_Retorno = mensagemRetorno,
                Ds_Mensagem = resultado?.FirstOrDefault()?.status_objeto,
                Cd_CodigoStatusIntegracao = resultado?.FirstOrDefault()?.status_objeto,
                Flg_InsereOcorrencia = insereOcorrencia,
                Cd_CodigoRetorno = resultado?.FirstOrDefault()?.codigo_erro.ToString(),
                Ds_XmlRetorno = xmlEnvelope.Truncate(4095),
            };

            if (resultado?.FirstOrDefault()?.prazo != null)
            {
                var splitData = resultado.FirstOrDefault().prazo.Split('/');
                devolucaoRetorno.Dt_Validade = DateTime.Parse($"{splitData[2]}-{splitData[1]}-{splitData[0]}");
            }

            if (resultado?.FirstOrDefault()?.codigo_erro.ToInt64() > 0 || erro)
                devolucaoRetorno.Flg_Sucesso = false;
            else devolucaoRetorno.Flg_Sucesso = true;

            return devolucaoRetorno;
        }
        
        private DevolucaoCorreioRetorno ConverteRetornoCancelamento(WSLogisticaReversa.cancelarPedidoResponse cancelaRetorno, DevolucaoCorreioCancela entregaDevolucao)
        {
            var devolucaoRetorno = new DevolucaoCorreioRetorno()
            {
                Id_Entrega = entregaDevolucao.Id_Entrega,
                Id_EntregaDevolucao = entregaDevolucao.Id_EntregaDevolucao,
                Id_EntregaDevolucaoStatus = entregaDevolucao.Id_EntregaDevolucaoStatus,
                Cd_CodigoColeta = entregaDevolucao.Cd_CodigoColeta,
                Cd_CodigoRastreio = entregaDevolucao.Cd_CodigoRastreio,
                Ds_Retorno = $"{cancelaRetorno?.cancelarPedido.cod_erro} - {cancelaRetorno?.cancelarPedido.msg_erro}",
                Ds_Mensagem = $"Status: {cancelaRetorno?.cancelarPedido.objeto_postal?.status_pedido} Dt Hora:{cancelaRetorno?.cancelarPedido.objeto_postal?.datahora_cancelamento}",
                Cd_CodigoStatusIntegracao = cancelaRetorno?.cancelarPedido.objeto_postal?.status_pedido,
                Flg_InsereOcorrencia = true,
            };

            if (cancelaRetorno?.cancelarPedido.cod_erro?.Length > 0)
                devolucaoRetorno.Flg_Sucesso = false;
            else devolucaoRetorno.Flg_Sucesso = true;

            return devolucaoRetorno;
        }
        
        private WSLogisticaReversa.coletaReversa CarregaEntregaReversa(Dto.LogisticaReversa.DevolucaoCorreio entrega)
        {
            var listObj = new List<WSLogisticaReversa.objeto>();
            listObj.Add(new WSLogisticaReversa.objeto()
            {
                item = entrega.Ds_SolicitacaoItemQuantidade,
                id = entrega.Ds_SolicitacaoItemEntrega, //CodigoPedido
                desc = entrega.Ds_SolicitacaoItemDescricao, //Descricao do Item Coletado
            });

            var rem = new WSLogisticaReversa.remetente
            {
                nome = entrega.Ds_RemetenteNome,
                logradouro = entrega.Ds_RemetenteLogradouro,
                numero = entrega.Ds_RemetenteNumero,
                bairro = entrega.Ds_RemetenteBairro,
                cidade = entrega.Ds_RemetenteCidade,
                cep = entrega.Ds_RemetenteCEP,
                complemento = entrega.Ds_RemetenteComplemento,
                uf = entrega.Ds_RemetenteUF,
                ddd = entrega.Ds_RemetenteDDD,
                telefone = entrega.Ds_RemetenteNumero,
                email = entrega.Ds_RemetenteEmail
            };

            var listProd = new List<WSLogisticaReversa.produto>();
            if (entrega.Ds_SolicitacaoProdutoPossui == true)
                listProd.Add(new WSLogisticaReversa.produto() { tipo = entrega.Ds_SolicitacaoProdutoTipo, codigo = entrega.Ds_SolicitacaoProdutoCodigo, qtd = entrega.Ds_SolicitacaoProdutoQtd });

            return new WSLogisticaReversa.coletaReversa()
            {
                ag = string.IsNullOrEmpty(entrega.Ds_SolicitacaoAG) ? "15" : entrega.Ds_SolicitacaoAG,
                tipo = entrega.Ds_SolicitacaoTipo,
                obj_col = listObj.ToArray(),
                produto = listProd.ToArray(),
                remetente = rem,
            };
        }
        
        private void InserirEntrega()
        {
            _repository.InserirEntrega();
        }
        
        private int QtdeDiasCorridos(int qtdeDiasUteis = 1)
        {
            DateTime dataAtual = DateTime.Now;
            int qtdeDiasCorridos = 0;
            int qtdeDias = 0;
            while (qtdeDias < qtdeDiasUteis)
            {
                if (dataAtual.AddDays(qtdeDiasCorridos).DayOfWeek != DayOfWeek.Sunday)
                    qtdeDias++;

                qtdeDiasCorridos++;
            }

            return qtdeDiasCorridos;
        }
        
        public List<EntregaDevolucao> GetEntregasDevolucoes(EntregaDevolucaoFiltro filtro)
        {
            var entregaIdInt = 0;
            int.TryParse(filtro.GerencialId, out entregaIdInt);

            var devolucoes = _repository.GetEntregasDevolucoes(x =>
                (filtro.DataInicio == default && filtro.DataTermino == default) ? true : (x.Inclusao >= filtro.DataInicio && x.Inclusao <= filtro.DataTermino) &&
                (filtro.EntregaDevolucaoStatusId == default ? true : x.EntregaDevolucaoStatus == filtro.EntregaDevolucaoStatusId) &&
                (!string.IsNullOrEmpty(filtro.Danfe) ? x.Entrega.Danfe == filtro.Danfe : true) &&
                ((entregaIdInt != 0) ? (x.EntregaId == entregaIdInt) : true) &&
                ((!string.IsNullOrEmpty(filtro.GerencialId) && entregaIdInt == 0) ? (x.Entrega.CodigoEntrega == filtro.GerencialId || x.Entrega.CodigoPedido == filtro.GerencialId) : true));

            return devolucoes.OrderByDescending(t => t.Id).ToList();
        }
        
        public byte[] Download(List<EntregaDevolucaoDto> entregas)
        {
            var listGenerica = new List<object>();
            foreach (var entrega in entregas)
                listGenerica.Add(entrega);

            return listGenerica.ConvertToXlsx("Entrega_Reversa.Xlsx", true);
        }
        
        public void RealizarAcao(EntregaDevolucaoAcaoDto acao)
        {
            var entregaDevolucao = _repository.Get(acao.EntregaDevolucaoId);
            var statusParaAtualizar = _entregaStatusAcaoService.GetAll(x =>
                                x.EntregaTransporteTipoId == entregaDevolucao.EntregaTransporteTipoId
                                && x.EntregaDevolucaoAcaoId == acao.AcaoId
                                && x.EntregaDevolucaoStatusId == entregaDevolucao.EntregaDevolucaoStatus
                                ).ToList();

            if (statusParaAtualizar == null)
                throw new Exception("Não existe um novo status para essa ação executar.");

            var historicoObjeto = new EntregaDevolucaoHistorico(0, acao.EntregaDevolucaoId, entregaDevolucao.CodigoColeta, entregaDevolucao.CodigoRastreio,
                                  entregaDevolucao.Validade, acao.Motivo, string.Empty, DateTime.Now, entregaDevolucao.EntregaDevolucaoStatus, (int)statusParaAtualizar?.FirstOrDefault().EntregaDevolucaoResultadoId, string.Empty);

            int statusFinal = (int)statusParaAtualizar?.FirstOrDefault()?.EntregaDevolucaoResultadoId;
            historicoObjeto.AtualizarUsuarioCriacao(_user.UsuarioLogado.Id);
            entregaDevolucao.AtualizarEntregaDevolucaoStatus(statusFinal);
            entregaDevolucao.AtualizarObservacao(acao.Motivo);

            if (statusFinal == (int)EnumEntregaDevolucaoStatus.AgCancelamento || statusFinal == (int)EnumEntregaDevolucaoStatus.AgProcessamento)
                entregaDevolucao.AtualizarProcessado(false);

            _repository.Update(entregaDevolucao);
            _entregaHistoricoService.Save(historicoObjeto);
        }
        
        private DateTime? ConvertStringToDate(string dt_string, string dt_format)
        {
            DateTime? dtConvertida;
            try
            {
                dtConvertida = DateTime.ParseExact(dt_string, dt_format, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                dtConvertida = default;
            }

            return dtConvertida;
        }
        
        private TimeSpan ConvertStringToTime(string dt_string, string dt_format)
        {
            TimeSpan dtConvertida;
            try
            {
                dtConvertida = TimeSpan.ParseExact(dt_string, dt_format, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                try
                {
                    dtConvertida = TimeSpan.Parse(dt_string);
                }
                catch (Exception)
                {
                    dtConvertida = new TimeSpan(0, 0, 0, 0);
                }
            }

            return dtConvertida;
        }
    }
}
