using Fretter.Domain.Config;
using Fretter.Domain.Dto.Fusion;
using Fretter.Domain.Dto.Webhook;
using Fretter.Domain.Dto.Webhook.Entrega;
using Fretter.Domain.Dto.Webhook.Entrega.Entrada;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fusion;
using Fretter.Domain.Helpers.Webhook;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Helper;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Fretter.Domain.Interfaces.Service.Webhook;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Fretter.Domain.Enum.Webhook.Enums;

namespace Fretter.Domain.Services.Webhook
{
    public class EntregaService<TContext> : ServiceBase<TContext, Entrega>, IEntregaService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly ILogHelper _logger;
        private readonly ICacheService<TContext> _cache;
        private readonly IEntregaRepository<TContext> _Repository;
        private readonly IMessageBusService<EntregaService<TContext>> _bus;
        private readonly MessageBusConfig _config;
        private readonly IArquivoImportacaoRepository<TContext> _arquivoImportacaoRepository;

        public EntregaService(ILogHelper logger,
                              ICacheService<TContext> cache, IEntregaRepository<TContext> Repository,
                              IStageConfigRepository<TContext> RepositoryStageConfig,
                              IOptions<MessageBusConfig> config,
                              IOptions<ShipNConfig> shipNConfig,
                              IArquivoImportacaoRepository<TContext> arquivoImportacaoRepository,
                              IMessageBusService<EntregaService<TContext>> bus,
                              IUnitOfWork<TContext> unitOfWork, IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _cache = cache;
            _logger = logger;
            _Repository = Repository;
            _config = config.Value;
            _arquivoImportacaoRepository = arquivoImportacaoRepository;
            _bus = bus;
            _bus.InitSender(_config.ConnectionString, _config.EntregaQueue);
        }

        public async Task<RetornoWs<EEntregaErro>> EntregaPadraoAsync(entradaEntrega entrega, string requestString, Guid token, List<Tuple<string, string>> headers)
        {
            string processName = "Entrega_Fila";
            var hash = Guid.NewGuid();
            return await PreparaEntrega(entrega, requestString, token, hash, processName, headers);
        }
        public async Task<RetornoWs<EEntregaErro>> EntregaCustomRestoqueAsync(Shipment entrega, string requestString, Guid token, List<Tuple<string, string>> headers)
        {
            string processName = "Entrega_FilaCustom";
            var hash = Guid.NewGuid();
            _logger.LogInfo(processName, "Entrada de Entrega Custom", new { hash, requestString, token });

            var retorno = new RetornoProcessamentoWs<EEntregaErro, retornoEntregaErro>(new RetornoWs<EEntregaErro>()) { dsAppCnn = "Entrga Custom Restoque" };

            try
            {
                var entregas = new List<entrega>();
                foreach (var ent in entrega.shipment_order_volume_array)
                {
                    string danfe = ent.shipment_order_volume_invoice?.invoice_key;
                    long notaFiscal, notaFiscalSerie;

                    if (string.IsNullOrEmpty(danfe) || danfe.Length != 44)
                    {
                        _logger.LogInfo(processName, "A Danfe do Pedido {shipment.order_number} informada é inválida - EntregaCustom", new { hash, token, danfe });
                        return retorno.ret.RetMsg($"A Danfe do Pedido {entrega.order_number} informada é inválida");
                    }

                    if (!long.TryParse(danfe.Substring(25, 9), out notaFiscal))
                        notaFiscal = Convert.ToInt64(ent.shipment_order_volume_invoice.invoice_number);

                    if (!long.TryParse(danfe.Substring(22, 3), out notaFiscalSerie))
                        notaFiscalSerie = Convert.ToInt64(ent.shipment_order_volume_invoice.invoice_series);

                    if (notaFiscal != Convert.ToInt64(ent.shipment_order_volume_invoice.invoice_number))
                    {
                        _logger.LogInfo(processName, $"A NotaFiscal da Danfe é Diferente da NF do Pedido: {ent.shipment_order_volume_invoice.invoice_number} - EntregaCustom", new { hash, token, danfe });
                        return retorno.ret.RetMsg($"A NotaFiscal da Danfe é Diferente da NF do Pedido: {ent.shipment_order_volume_invoice.invoice_number}");
                    }

                    string destinatarioNumero = EntregaValidator.ValidarEnderecoNumero(entrega.end_customer?.shipping_number) ? entrega.end_customer?.shipping_number : EntregaValidator.ExtrairEnderecoNumero(entrega.end_customer?.shipping_number);

                    var entregaFormat = new entrega()
                    {
                        cepOrigem = Convert.ToInt32(entrega.origin_zip_code),
                        cepDestino = entrega.end_customer.shipping_zip_code,
                        servicoTransportador = $"{entrega.carrierId}_{entrega.delivery_method_id}",
                        codigoEntrega = entrega.order_number,
                        codigoPedido = entrega.order_number,
                        codigoRastreio = ent.tracking_code,
                        dataEmissaoNF = ent.shipment_order_volume_invoice.invoice_date.ToShortDateString(),
                        dataPostagem = entrega.shipped_date.ToShortDateString(),
                        dataPrazoComercial = entrega.estimated_delivery_date.ToShortDateString(),
                        dataPrevistaEntrega = entrega.estimated_delivery_date_lp.ToShortDateString(),
                        danfe = danfe,
                        nf = notaFiscal,
                        awb = entrega.origin_warehouse_code,
                        serie = Convert.ToInt32(notaFiscalSerie),
                        ValorFrete = entrega.customer_shipping_costs,
                        codigoContrato = $"{entrega.carrierId}_{entrega.delivery_method_id}",
                        canalVendas = entrega.sales_channel,
                        quantidadeVolume = (ent?.shipment_order_volume_number == null ? 1 : ent?.shipment_order_volume_number),
                        marketplace = new entregaMarketplace()
                        {
                            canalVenda = entrega.sales_channel,
                            codigoRastreio = ent.tracking_code,
                            codigoEntregaSaida = entrega.order_number,
                            codigoEntregaEntrada = entrega.order_number,
                        },
                        detalhe = new detalhes()
                        {
                            altura = ent.height,
                            largura = ent.width,
                            comprimento = ent.length,
                            peso = ((ent.weight <= 0 || ent.weight == null) ? 0.3M : ent.weight),
                            nomeDestinatario = $"{entrega.end_customer?.first_name} {entrega.end_customer?.last_name}",
                            docDestinatario = entrega.end_customer?.federal_tax_payer_id.CleanCnpj(),
                            inscricaoEstadual = entrega.end_customer?.state_tax_payer_id.CleanCnpj(),
                            endereco = entrega.end_customer?.shipping_address,
                            enderecoNumero = destinatarioNumero,
                            complemento = (EntregaValidator.ValidarEnderecoNumero(entrega.end_customer?.shipping_number) ? entrega.end_customer?.shipping_additional : $"{entrega.end_customer?.shipping_number} - {entrega.end_customer?.shipping_additional}").Truncate(100),
                            bairro = entrega.end_customer?.shipping_quarter,
                            valorDeclarado = ent.shipment_order_volume_invoice?.invoice_total_value,
                            uf = entrega.end_customer.shipping_state,
                            cidade = entrega.end_customer.shipping_city
                            //sku =  A Rever 
                        },
                        contato = new contatos()
                        {
                            Email = entrega.end_customer.email,
                            Telefone1 = entrega.end_customer.phone,
                            Telefone2 = entrega.end_customer.cellphone,
                        },
                        itens = new List<item>(),
                        volumes = new List<volume>()
                    };

                    decimal? valorPorItem = ent.products_quantity > 1 ? ent.shipment_order_volume_invoice?.invoice_total_value / ent.products_quantity : ent.shipment_order_volume_invoice?.invoice_total_value;
                    decimal? valorPorVolume = entregaFormat.quantidadeVolume > 1 ? ent.shipment_order_volume_invoice?.invoice_total_value / entregaFormat.quantidadeVolume : ent.shipment_order_volume_invoice?.invoice_total_value;

                    int? qtdItemPorVolume = ent.products_quantity / entregaFormat.quantidadeVolume;
                    int? sobraDeItem = ent.products_quantity % entregaFormat.quantidadeVolume;

                    for (int i = 1; i <= entregaFormat.quantidadeVolume; i++)
                    {
                        string codigoVolume = $"{i}_volume_{Guid.NewGuid().ToString()}";

                        entregaFormat.volumes.Add(new volume()
                        {
                            nome = ent.name,
                            codigo = codigoVolume,
                            codigoCFOP = ent.shipment_order_volume_invoice?.invoice_cfop,
                            codigoRastreio = ent.tracking_code,
                            danfe = danfe,
                            notaFiscal = notaFiscal.ToString(),
                            serie = notaFiscalSerie.ToString(),
                            altura = ent.height,
                            largura = ent.width,
                            comprimento = ent.length,
                            quantidadeItens = (i == entregaFormat.quantidadeVolume ? qtdItemPorVolume + sobraDeItem : qtdItemPorVolume),
                            peso = ((ent.weight <= 0 || ent.weight == null) ? 0.3M : ent.weight),
                            numero = i,
                            valorDeclarado = valorPorVolume > 0 ? decimal.Round((decimal)valorPorVolume, 3, MidpointRounding.AwayFromZero) : valorPorVolume,
                            valorTotal = valorPorVolume > 0 ? decimal.Round((decimal)valorPorVolume, 3, MidpointRounding.AwayFromZero) : valorPorVolume
                        });

                        entregaFormat.itens.Add(new item()
                        {
                            codigoVolume = codigoVolume,
                            codigoItemDetalhe = ent.name,
                            altura = ent.height,
                            largura = ent.width,
                            comprimento = ent.length,
                            freteCliente = entrega.customer_shipping_costs,
                            quantidadeItens = ent.products_quantity,
                            valorItem = valorPorItem > 0 ? decimal.Round((decimal)valorPorItem, 3, MidpointRounding.AwayFromZero) : valorPorItem,
                        });

                    }

                    entregas.Add(entregaFormat);
                }

                var transportadores = new List<transportador>();
                transportadores.Add(new transportador()
                {
                    cnpj = entrega.cnpj_transportadora.CleanCnpj(),
                    entregas = entregas.ToArray(),
                });

                var filiais = new List<filial>();
                filiais.Add(new filial()
                {
                    cnpj = entrega.origin_federal_tax_payer_id.CleanCnpj(),
                    segmento = entrega.sales_channel,
                    transportadores = transportadores.ToArray(),
                });

                var tipos = new List<tipo>();
                tipos.Add(new tipo()
                {
                    servico = "Entrega",
                    filiais = filiais.ToArray()
                });

                var entregaPadrao = new entradaEntrega()
                {
                    tipos = tipos.ToArray()
                };

                _logger.LogInfo(processName, "Entrega Custom convertida em padrão.", new { hash, requestString, token });
                return await PreparaEntrega(entregaPadrao, requestString, token, hash, processName, headers);
            }
            catch (Exception ex)
            {
                _logger.LogError(processName, "Falha ao converter envelope custom restoque em envelope padrão", new { hash, requestString, token }, DateTime.Now, 0, ex);

                return retorno.ret.RetMsg("Falha ao converter envelope custom restoque em envelope padrão");
            }

        }
        public async Task<RetornoWs<EEntregaErro>> EntregaCustomAsync(EntradaEntregaDirect entrega)
        {
            var hash = Guid.NewGuid();
            var ret = new RetornoProcessamentoWs<EEntregaErro, retornoEntregaErro>(new RetornoWs<EEntregaErro>());

            try
            {
                var requestString = JsonConvert.SerializeObject(entrega);
                _logger.LogInfo("Customizada", "Entrada de Entrega Customizada", new { hash, requestString });

                if (entrega.usuario != "directweb" && entrega.senha != "davjweb")
                {
                    _logger.LogInfo("Customizada", "Usuario e Senha Incorretos", new { hash, requestString });
                    return ret.ret.RetMsg("Usuario e Senha Incorretos");
                }

                var idArquivo = 0;//_arquivoImportacaoRepository.InserirArquivo(new ArquivoImportacao($"Entrega_{hash}", "Elastic", OrigemImportacao.Ws));
                ret.ret.protocolo = idArquivo;
                Empresa empresa = _cache.ObterLista<Empresa>(e => e.Id == 1761).FirstOrDefault();

                ret.err = new retornoEntregaErro
                {
                    nf = entrega.codigoCorreio,
                    serie = "1",
                    empresaId = empresa.Id
                };

                try
                {
                    var entregaEnvio = new EntregaEnvioFila
                    {
                        Id_Arquivo = idArquivo,
                        Ds_Hash = hash,
                        Ds_ServicoTipo = "Entrega",
                        Cd_CnpjFilial = "",
                        Cd_CodigoFilial = "",
                        Cd_SegmentoFilial = "",
                        Cd_CnpjTransportador = "",
                        Nr_Linha = 1,
                        Ds_CanalVendas = "",
                        Cd_CepDestino = entrega.cep?.ToString()?.Replace("-", ""),
                        Cd_CepOrigem = string.Empty,
                        Cd_Contrato = "",
                        Cd_Entrega = entrega.codigoPedido,
                        Cd_Pedido = entrega.codigoPedido,
                        Vl_Frete = null,
                        Vl_CustoFrete = null,
                        Cd_Rastreio = entrega.codigoCorreio,
                        Cd_CodigoPLP = null,
                        Ds_Awb = entrega.awb,
                        Dt_Postagem = entrega.dataPostagem.ToDateTimeCulture(),
                        Dt_PrazoComercial = entrega.dataPostagem.ToDateTimeCulture(),
                        Dt_PrevistaEntrega = (DateTime?)null,
                        Dt_EmissaoNF = (DateTime?)null,
                        Cd_NotaFiscal = entrega.codigoCorreio,
                        Cd_Danfe = null,
                        Ds_ServicoCorreio = null,
                        Ds_ServicoTransportador = null,
                        Cd_Serie = "1",
                        Ds_CnpjTerceiro = null,
                        Cd_NotaTerceiro = null,
                        Cd_SerieTerceiro = null,
                        Vl_Peso = (decimal)0.3,
                        Ds_FormatoEmbalagemCorreios = "Caixa",
                        Vl_Comprimento = 16,
                        Vl_Altura = 2,
                        Vl_Largura = 12,
                        Vl_Diametro = 0,
                        Flg_ServicoMaoPropria = false,
                        Flg_AvisoRecebimento = false,
                        Vl_Declarado = Convert.ToDecimal(entrega.ValorDeclarado),
                        Ds_NomeDestinatario = entrega.DescricaoCliente,
                        Nr_DocDestinatario = null,
                        Ds_InscricaoEstadual = null,
                        Ds_EnderecoNumero = entrega.nroEndereco,
                        Ds_Bairro = entrega.bairro,
                        Ds_Endereco = entrega.endereco,
                        Ds_EnderecoComplemento = entrega.complemento,

                        Ds_Email = entrega.emailCliente,
                        Ds_Telefone1 = entrega.ddd + "-" + entrega.fone,
                        Ds_Telefone2 = entrega.dddCelular + "-" + entrega.foneCelular,
                        Ds_Telefone3 = null,
                        Cd_CnpjMarketplace = null,
                        Ds_CanalVendaMarketplace = null,
                        Cd_EntregaEntrada = null,
                        Cd_EntregaSaida = null,
                        Id_Lojista = null,
                        Cd_RastreioMarketplace = null,
                        Responsavel = Responsavel.Indefinido,
                        Id_Empresa = empresa.Id,
                        Ds_Cidade = null,
                        Cd_Uf = null,
                    };

                    ret.ret.qtdRegistrosImportados++;
                    entregaEnvio.Itens = new List<EntregaItens>();

                    _logger.LogInfo("Customizada", "Entrega de envio para fila.", new { hash, entregaEnvio });
                    try
                    {
                        await _bus.Send(entregaEnvio);
                        _logger.LogInfo("Customizada", "Entrega enviada para fila", new { hash, entregaEnvio });
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Customizada", "Erro de envio para fila.", new { hash, entregaEnvio }, DateTime.Now, 0, ex);
                        Err(ex, ref ret, 1);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Customizada", "Erro na conversão do objeto de entrada para objeto de envio para fila.", new { hash, empresa }, DateTime.Now, 0, ex);
                    Err(ex, ref ret, 1);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Customizada", "Erro no processamento do envelope.", new { hash, entrega }, DateTime.Now, 0, ex);
                Err(ex, ref ret, 1);
            }

            return ret.ret.RetMsg();
        }
        public static void Err(Exception ex, ref RetornoProcessamentoWs<EEntregaErro, retornoEntregaErro> ret, int linha)
        {
            string er;
            var erro = new StringBuilder();
            if (ex is SqlException sqlEx)
            {
                for (int k = 0; k < sqlEx.Errors.Count; k++)
                {
                    erro.Append("Index #" + k + "\n" +
                                "Message: " + sqlEx.Errors[k].Message + "\n" +
                                "LineNumber: " + sqlEx.Errors[k].LineNumber + "\n" +
                                "Source: " + sqlEx.Errors[k].Source + "\n" +
                                "Procedure: " + sqlEx.Errors[k].Procedure + "\n");
                }
                er = erro.ToString();
            }
            else
            {
                er = ex.InnerException?.InnerException.Message ?? ex.Message;
            }
            ret.Msg(er, linha);
        }
        private async Task<RetornoWs<EEntregaErro>> PreparaEntrega(entradaEntrega entrega, string requestString, Guid token, Guid hash, string processName, List<Tuple<string, string>> headers = null)
        {
            var ret = new RetornoProcessamentoWs<EEntregaErro, retornoEntregaErro>(new RetornoWs<EEntregaErro>() { protocoloHash = hash.ToString() }) { dsAppCnn = "EntregaPadrao" };
            if (string.IsNullOrEmpty(requestString))
                requestString = JsonConvert.SerializeObject(entrega);

            _logger.LogInfo(processName, "Entrada da Entrega / Objeto de Entrega.", new { hash, requestString, entrega });

            try
            {
                var gravaErroBanco = false;
                if (headers != null && headers.Count > 0)
                    _logger.LogInfo(processName, "Detalhes do Header.", new { hash, headers });

                if (token == default)
                {
                    _logger.LogInfo(processName, "Você não tem permissão para executar esta ação.", new { hash, headers });
                    return ret.ret.RetMsg("Você não tem permissão para executar esta ação.");
                }

                Empresa empresa = _cache.ObterLista<Empresa>(e => e.TokenId == token).FirstOrDefault();
                Transportador transportador = _cache.ObterLista<Transportador>(t => t.TokenId == token).FirstOrDefault();
                if (empresa == null)
                {
                    var msg = string.Empty;
                    if (transportador == null)
                        msg = "Token inválido.";
                    else
                        msg = "Empresa não localizada.";

                    _logger.LogInfo(processName, msg, new { hash, token, empresa, transportador });
                    return ret.ret.RetMsg(msg);
                }

                if (!empresa.Ativo)
                {
                    _logger.LogInfo(processName, "Empresa bloqueada.", new { hash, token, empresa, transportador });
                    return ret.ret.RetMsg("Empresa bloqueada.");
                }

                Empresa empToken = empresa;

                bool isMkt = false;
                if (headers.Any(x => x.Item1 == "mkt") && headers.FirstOrDefault(x => x.Item1 == "mkt")?.Item2 == "1" || empresa.Id == 63)
                    isMkt = true;

                if (isMkt && !empToken.Ativo)
                {
                    _logger.LogInfo(processName, "Empresa não é marketplace.", new { hash, token, empresa, empToken, isMkt });
                    return ret.ret.RetMsg("Empresa não é marketplace.");
                }

                var resp = transportador != null ? Responsavel.Transportador : (isMkt ? Responsavel.MarketPlace : Responsavel.Embarcador);
                var i = new[] { 0, 0, 0, 0 };
                ret.TotalLinhas = entrega.tipos.Sum(a => a.filiais.Sum(o => o.transportadores.Sum(k => k.entregas.Length)));
                var errL = new retornoEntregaErro
                {
                    empresaId = empresa.Id
                };
                ret.err = errL;

                try
                {
                    if (entrega.tipos == null)
                    {
                        _logger.LogInfo(processName, "A informação 'tipos' é obrigatória.", new { hash });
                        return ret.ret.RetMsg("A informação 'tipos' é obrigatória.");
                    }

                    foreach (var tipo in entrega.tipos)
                    {
                        i[0]++;
                        i[1] = i[2] = i[3] = 0;
                        ret.Set(i);
                        errL.transportador = null;
                        ret.err = errL;
                        if (string.IsNullOrEmpty(tipo.servico))
                        {
                            tipo.servico = "Entrega";
                        }

                        var serv = _cache.ObterLista<Tipo>(e => e.Descricao.ToUpper() == tipo.servico.ToUpper(), false).FirstOrDefault();
                        if (serv == null)
                        {
                            _logger.LogInfo(processName, "Serviço inválido.", new { hash });
                            ret.Msg("Serviço inválido.", i[0]);
                            continue;
                        }

                        if (tipo.filiais == null)
                        {
                            _logger.LogInfo(processName, "Filiais é obrigatório.", new { hash });
                            ret.Msg("Filiais é obrigatório.", i[0]);
                            continue;
                        }

                        foreach (var filial in tipo.filiais)
                        {
                            i[1]++;
                            i[2] = i[3] = 0;
                            ret.Set(i);
                            try
                            {
                                foreach (var transp in filial.transportadores)
                                {
                                    i[2]++;
                                    i[3] = 0;
                                    ret.Set(i);
                                    try
                                    {
                                        transp.cnpj = transp?.cnpj?.CleanCnpj();
                                        errL.transportador = transp.cnpj;
                                        ret.err = errL;

                                        if (transp.entregas == null)
                                        {
                                            _logger.LogInfo(processName, "Entregas é obrigatório.", new { hash });
                                            ret.Msg("Entregas é obrigatório.", i[0]);
                                            continue;
                                        }

                                        foreach (var ent in transp.entregas)
                                        {
                                            i[3]++;
                                            ret.Set(i);
                                            ret.ret.qtdRegistros++;

                                            ent.danfe = ent.danfe?.Replace(" ", string.Empty);
                                            if (!string.IsNullOrEmpty(ent.danfe) && ent.danfe.Any(x => x != '0') && ent.danfe.All(x => char.IsNumber(x)))
                                            {
                                                filial.cnpj = ent.danfe.Substring(6, 14);
                                                ent.nf = long.Parse(ent.danfe.Substring(25, 9));
                                                ent.serie = int.Parse(ent.danfe.Substring(22, 3));
                                            }

                                            filial.cnpj = filial?.cnpj?.CleanCnpj();
                                            if (string.IsNullOrEmpty(filial?.cnpj))
                                            {
                                                if (resp == Responsavel.Embarcador)
                                                {
                                                    if (string.IsNullOrEmpty(filial?.codigo))
                                                    {
                                                        _logger.LogInfo(processName, "CNPJ filial e código filial estão vazios.", new { hash });
                                                        ret.Msg("CNPJ filial e código filial estão vazios.", i[0]);
                                                        continue;
                                                    }
                                                }
                                                else
                                                {
                                                    _logger.LogInfo(processName, "CNPJ filial esta vazio.", new { hash });
                                                    ret.Msg("CNPJ filial esta vazio.", i[0]);
                                                    continue;
                                                }
                                            }
                                            errL.filial = filial?.cnpj;
                                            ret.err = errL;

                                            try
                                            {
                                                string cnpjTransportador = null;

                                                if (isMkt)
                                                {
                                                    if (
                                                        (!string.IsNullOrEmpty(ent.codigoRastreio) && ent.codigoRastreio.IsCorreio()) ||
                                                        (ent.marketplace != null && !string.IsNullOrEmpty(ent.marketplace.codigoRastreio) && ent.marketplace.codigoRastreio.IsCorreio()))
                                                    {
                                                        var t = _cache.ObterLista<Transportador>(x => x.RastreamentoConfigTipoId == (byte)RastreamentoConfigTipo.Correio).FirstOrDefault();
                                                        var tc = _cache.ObterLista<TransportadorCnpj>(x => x.TransportadorId == t.Id).FirstOrDefault();
                                                        cnpjTransportador = tc.CNPJ;
                                                    }
                                                    else if (!string.IsNullOrEmpty(transp.cnpj))
                                                    {
                                                        var tc = _cache.ObterLista<TransportadorCnpj>(x => x.CNPJ == transp.cnpj).FirstOrDefault();
                                                        //LOGGI
                                                        if (tc?.TransportadorId == 340)
                                                            cnpjTransportador = tc.CNPJ;
                                                        else
                                                            cnpjTransportador = transp.cnpj;
                                                    }
                                                }
                                                else
                                                    cnpjTransportador = transp.cnpj;

                                                var idArquivo = 0;//_arquivoImportacaoRepository.InserirArquivo(new ArquivoImportacao($"Entrega_{hash}", "Elastic", OrigemImportacao.Ws));

                                                var entregaEnvio = new EntregaEnvioFila
                                                {
                                                    Id_Arquivo = idArquivo,
                                                    Ds_Hash = hash,
                                                    Ds_ServicoTipo = serv.Descricao ?? "Entrega",
                                                    Cd_CnpjFilial = filial.cnpj,
                                                    Cd_CodigoFilial = filial.codigo,
                                                    Cd_SegmentoFilial = filial.segmento,
                                                    Cd_CnpjTransportador = cnpjTransportador,
                                                    Nr_Linha = ret.ret.qtdRegistros,
                                                    Ds_CanalVendas = ent.canalVendas,
                                                    Cd_CepDestino = ent.cepDestino?.ToString()?.Replace("-", ""),
                                                    Cd_CepOrigem = ent.cepOrigem == 0 ? string.Empty : ent.cepOrigem.ToString(),
                                                    Cd_Contrato = ent.codigoContrato,
                                                    Cd_Entrega = !isMkt ? ent.codigoEntrega : null,
                                                    Cd_Pedido = !isMkt ? ent.codigoPedido : null,
                                                    Vl_Frete = ent.ValorFrete,
                                                    Vl_CustoFrete = ent.valorCustoFrete,
                                                    Cd_Rastreio = isMkt ? null : ent.codigoRastreio,
                                                    Cd_CodigoPLP = ent.codigoPLP,
                                                    Ds_Awb = ent.awb,
                                                    Dt_Postagem = string.IsNullOrEmpty(ent.dataPostagem) ? (DateTime?)null : ent.dataPostagem.ToDateTimeCulture(),
                                                    Dt_PrazoComercial = string.IsNullOrEmpty(ent.dataPrazoComercial) ? (DateTime?)null : ent.dataPrazoComercial.ToDateTimeCulture(),
                                                    Dt_PrevistaEntrega = string.IsNullOrEmpty(ent.dataPrevistaEntrega) ? (DateTime?)null : ent.dataPrevistaEntrega.ToDateTimeCulture(),
                                                    Dt_EmissaoNF = string.IsNullOrEmpty(ent.dataEmissaoNF) ? (DateTime?)null : ent.dataEmissaoNF.ToDateTimeCulture(),
                                                    Cd_NotaFiscal = !string.IsNullOrEmpty(ent.cnpjTerceiro) && ent.nf == default(long) && !string.IsNullOrEmpty(ent.codigoRastreio) ? ent.codigoRastreio : Convert.ToString(ent.nf),
                                                    Cd_Danfe = ent.danfe,
                                                    Ds_ServicoCorreio = ent.servicoCorreio,
                                                    Ds_ServicoTransportador = ent.servicoTransportador,
                                                    Cd_Serie = ent.serie.ToString(),
                                                    Ds_CnpjTerceiro = ent.cnpjTerceiro,
                                                    Cd_NotaTerceiro = ent.notaTerceiro,
                                                    Cd_SerieTerceiro = ent.serieTerceiro,
                                                    numeroCargaCliente = ent.numeroCargaCliente,
                                                    Ds_PackingList = Convert.ToString(ent.numeroCargaCliente),
                                                    Vl_Peso = ent.detalhe?.peso,
                                                    Ds_FormatoEmbalagemCorreios = ent.detalhe?.formatoEmbalagemCorreios,
                                                    Vl_Comprimento = ent.detalhe?.comprimento,
                                                    Vl_Altura = ent.detalhe?.altura,
                                                    Vl_Largura = ent.detalhe?.largura,
                                                    Vl_Diametro = ent.detalhe?.diametro,
                                                    Flg_ServicoMaoPropria = ent.detalhe?.servicoMaoPropria,
                                                    Flg_AvisoRecebimento = ent.detalhe?.avisoRecebimento,
                                                    Vl_Declarado = ent.detalhe?.valorDeclarado,
                                                    Ds_NomeDestinatario = ent.detalhe?.nomeDestinatario,
                                                    Nr_DocDestinatario = ent.detalhe?.docDestinatario,
                                                    Ds_InscricaoEstadual = ent.detalhe?.inscricaoEstadual,
                                                    Ds_EnderecoNumero = ent.detalhe?.enderecoNumero,
                                                    Ds_Bairro = ent.detalhe?.bairro,
                                                    Ds_Endereco = ent.detalhe?.endereco,
                                                    Ds_EnderecoComplemento = ent.detalhe?.complemento,
                                                    Ds_Email = ent.contato?.Email,
                                                    Ds_Telefone1 = ent.contato?.Telefone1,
                                                    Ds_Telefone2 = ent.contato?.Telefone2,
                                                    Ds_Telefone3 = ent.contato?.Telefone3,
                                                    Cd_CnpjMarketplace = ent.marketplace?.cnpj,
                                                    Ds_CanalVendaMarketplace = ent.marketplace?.canalVenda,
                                                    Cd_EntregaEntrada = string.IsNullOrEmpty(ent.marketplace?.codigoEntregaEntrada) ? isMkt ? ent.codigoPedido : null : ent.marketplace?.codigoEntregaEntrada,
                                                    Cd_EntregaSaida = string.IsNullOrEmpty(ent.marketplace?.codigoEntregaSaida) ? isMkt ? ent.codigoEntrega : null : ent.marketplace?.codigoEntregaSaida,
                                                    Id_Lojista = ent.marketplace?.idLojista,
                                                    Cd_RastreioMarketplace = string.IsNullOrEmpty(ent.marketplace?.codigoRastreio) ? isMkt ? ent.codigoRastreio : null : ent.marketplace?.codigoRastreio,
                                                    Responsavel = resp,
                                                    Id_Empresa = empToken.Id,
                                                    Nr_QtdVolumes = (ent?.quantidadeVolume == null ? 1 : ent.quantidadeVolume),
                                                    //EmpresaMarketplace = isMkt ? empToken : null,
                                                    //Vl_FreteReceita = ent.detalhe?.valorFreteReceita,
                                                    //Vl_FreteCusto = ent.detalhe?.valorFreteCusto,
                                                    //Nr_PesoCubico = ent.detalhe?.pesoCubico,
                                                    //Nr_QuantidadetItem = ent.detalhe?.quantidadeItem,
                                                    Ds_Cidade = ent.detalhe?.cidade,
                                                    Cd_Uf = ent.detalhe?.uf,
                                                    Cd_CnpjPagador = ent.detalhe?.cnpjPagador
                                                };

                                                entregaEnvio.Volumes = new List<EntregaVolumes>();
                                                ent.volumes?.ForEach(e =>
                                                {
                                                    entregaEnvio.Volumes?.Add(
                                                            new EntregaVolumes
                                                            {
                                                                Ds_Nome = e?.nome,
                                                                Nr_Volume = e?.numero,
                                                                Cd_CFOP = e?.codigoCFOP,
                                                                Cd_Danfe = e?.danfe,
                                                                Cd_NotaFiscal = e?.notaFiscal,
                                                                Cd_Serie = e?.serie,
                                                                Qtd_Itens = e?.quantidadeItens,
                                                                Vl_Altura = e?.altura,
                                                                Vl_Largura = e?.largura,
                                                                Vl_Comprimento = e?.comprimento,
                                                                Vl_Peso = e?.peso,
                                                                Vl_PesoCubico = e?.pesoCubico,
                                                                Vl_Declarado = e?.valorDeclarado,
                                                                Vl_Total = e?.valorTotal,
                                                                Ds_Codigo = e?.codigo,
                                                                Ds_CodigoRastreio = e?.codigoRastreio
                                                            });
                                                });

                                                entregaEnvio.Itens = new List<EntregaItens>();
                                                ent.itens?.ForEach(e =>
                                                {
                                                    entregaEnvio.Itens?.Add(
                                                            new EntregaItens
                                                            {
                                                                Cd_Item = e?.codigoItem,
                                                                Qtd_Itens = e?.quantidadeItens,
                                                                Vl_FreteCliente = e?.freteCliente,
                                                                Cd_ItemDetalhe = e?.codigoItemDetalhe,
                                                                Cd_Sku = e?.sku,
                                                                Vl_Altura = e?.altura,
                                                                Vl_Largura = e?.largura,
                                                                Vl_Comprimento = e?.comprimento,
                                                                Vl_PesoCubico = e?.peso,
                                                                Vl_FreteCusto = e?.freteCusto,
                                                                Vl_PrecoItem = e?.valorItem,
                                                                Ds_CodigoVolume = e?.codigoVolume,
                                                                Vl_Total = e?.valorTotalItem,
                                                                Ds_UrlImagem = e?.urlImagem,
                                                                Ds_UrlNotaFiscal = e?.urlNotaFiscal
                                                            });
                                                });

                                                try
                                                {
                                                    await _bus.Send(entregaEnvio);
                                                    _logger.LogInfo(processName, "Entrega enviada para fila.", new { hash, entregaEnvio });
                                                    ret.ret.qtdRegistrosImportados++;
                                                }
                                                catch (Exception ex)
                                                {
                                                    _logger.LogError(processName, "Entrega: Erro na publicação na fila.", new { hash, entregaEnvio }, DateTime.Now, 0, ex);
                                                    Err(ex, ref ret, i[0]);
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                _logger.LogError(processName, "Entrega: Erro no processamento", new { hash, entrega }, DateTime.Now, 0, ex);
                                                Err(ex, ref ret, i[0]);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.LogError(processName, "Transportadora: Erro no processamento.", new { hash, transp }, DateTime.Now, 0, ex);
                                        Err(ex, ref ret, i[0]);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError(processName, "Filial: Erro no processamento.", new { hash, filial }, DateTime.Now, 0, ex);
                                Err(ex, ref ret, i[0]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(processName, "Tipos: Erro no processamento.", new { hash, entrega.tipos }, DateTime.Now, 0, ex);
                    Err(ex, ref ret, i[0]);
                }

                ret.Fim(null, true, false, !gravaErroBanco);
            }
            catch (Exception ex)
            {
                _logger.LogError(processName, "Erro no processamento do envelope.", new { hash, requestString, entrega, token, headers }, DateTime.Now, 0, ex);
                ret.Fim(null, false);
            }
            return ret.ret.RetMsg();
        }
    }

    public static class EntregaValidator
    {
        public static bool ValidarEnderecoNumero(string valor)
        {
            if (valor.Length > 6 || !Regex.IsMatch(valor, @"^\d*$"))
                return false;

            return true;
        }

        public static string ExtrairEnderecoNumero(string valor)
        {
            string numeroExtraido = "0";

            var match = Regex.Match(valor, @"^(\d+)[^\d]*");

            if (match.Success)
            {
                numeroExtraido = match.Groups[1].Value;

                if (numeroExtraido.Length > 6)
                    numeroExtraido = "0";
            }
            else
            {
                match = Regex.Match(valor, @"^(\d+)");

                if (match.Success)
                {
                    numeroExtraido = match.Groups[1].Value;
                }
            }

            return numeroExtraido;
        }
    }
}
