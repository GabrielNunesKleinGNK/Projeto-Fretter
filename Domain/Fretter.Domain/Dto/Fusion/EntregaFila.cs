using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Fretter.Domain.Dto.Fusion
{
    public class entradaEntrega
    {
        /// <summary>
        /// Tipo de Entrega
        /// </summary>
        [Required]
        public tipo[] tipos { get; set; }
        public entradaEntrega() { }
        public entradaEntrega(Whirlpool.Notfis notfis)
        {
            var entregasAdd = new List<entrega>();

            foreach (var ship in notfis.shipToParties?.shipToParty)
            {
                foreach (var inv in ship.invoices?.invoice)
                {
                   
                        var entregaAdd = new entrega()
                        {
                            cepOrigem = notfis.header.postalCodeShipper.RemoveSpecialCharacters().ToInt(),
                            cepDestino = ship.postalCode.RemoveSpecialCharacters(),
                            codigoRastreio = inv.deliveryNumber ?? inv.shippingNumber ?? inv.logisticOperatorCode,
                            dataPostagem = notfis.header?.dateTimeBoarding ?? string.Format("{0:yyyy-MM-dd}", notfis.header.postingDateTime),
                            dataEmissaoNF = string.Format("{0:yyyy-MM-dd}", inv.date),
                            contato = new contatos()
                            {
                                Email = ship.email,
                                Telefone1 = ship.comunicationNumber.RemoveSpecialCharacters()
                            },
                            canalVendas = notfis.header.shipperName,
                            codigoPedido = inv.purchaseOrderNumber,
                            codigoEntrega = inv.shippingNumber,
                            danfe = inv.products.product.FirstOrDefault().danfe,
                            nf = Convert.ToInt64(inv.invoice),
                            serie = inv.serie,
                            ValorFrete = Convert.ToDecimal(inv.totalValueFreight),
                            dataPrevistaEntrega = inv.estimatedDeliveryDate,
                            detalhe = new detalhes()
                            {
                                cnpjPagador = inv.freightResponsable.cnpj.ToString(),
                                valorDeclarado = Convert.ToDecimal(inv.totalAmount),
                                pesoCubico = inv.weightVolumesDensity,
                                peso = inv.totalWeightProduct.ToDecimal() > 0 ? inv.totalWeightProduct.ToDecimal() / (decimal)1000.00 : inv.totalWeightProduct.ToDecimal(),
                                valorFreteCusto = Convert.ToDecimal(inv.totalValueFreight),
                                quantidadeItem = inv.volumeQuantity.ToInt(),
                                nomeDestinatario = ship.name,
                                docDestinatario = (ship.cnpj > 0 ? ship.cnpj : ship.cpf).ToString(),
                                enderecoNumero = "0",
                                endereco = ship.address,
                                cidade = ship.city,
                                bairro = ship.district,
                                uf = ship.state,
                                altura = 1,
                                largura = 1,
                                comprimento = 1
                            },
                            itens = new List<item>()
                        };

                    int qtdProdutos = inv.products?.product?.Count() ?? 1;

                    decimal? valorPorItem = qtdProdutos > 1 ? Convert.ToDecimal(inv.totalAmount) / qtdProdutos : Convert.ToDecimal(inv.totalAmount);
                    
                    foreach (var prod in inv.products?.product)
                    {
                        entregaAdd.itens.Add(new item()
                        {
                            quantidadeItens = 1,
                            valorItem = valorPorItem > 0 ? decimal.Round((decimal)valorPorItem, 3, MidpointRounding.AwayFromZero) : valorPorItem,
                            codigoItem = prod.material.Truncate(99),
                            codigoItemDetalhe = prod.description.Truncate(99),
                        });
                        
                    }

                    entregasAdd.Add(entregaAdd);
                }
            }

            var transportadoresAdd = new List<transportador>();
            transportadoresAdd.Add(new transportador()
            {
                cnpj = notfis.header.cnpjCarrier.ToString(),
                entregas = entregasAdd.ToArray(),
            });

            var filiaisAdd = new List<filial>();
            filiaisAdd.Add(new filial()
            {
                cnpj = notfis.header.cnpjBranch.ToString(),
                transportadores = transportadoresAdd.ToArray(),

            });

            var tipos = new List<tipo>();
            tipos.Add(new tipo()
            {
                servico = "Entrega",
                filiais = filiaisAdd.ToArray()
            });

            this.tipos = tipos.ToArray();
        }
    }
    public class contatos
    {
        /// <summary>
        /// Email Cliente
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Telefone Fixo
        /// </summary>
        public string Telefone1 { get; set; }

        /// <summary>
        /// Telefone Celular
        /// </summary>
        public string Telefone2 { get; set; }

        /// <summary>
        /// Telefone Comercial
        /// </summary>
        public string Telefone3 { get; set; }
    }
    /// <summary>
    /// detalhes da entrega, obrigatório em caso de abertura de PI.
    /// </summary>
    public class detalhes
    {
        /// <summary>
        /// Peso da entrega com a embalagem
        /// </summary>
        public decimal? peso { get; set; }

        /// <summary>
        /// Formato da embalagem
        /// valores aceitos:
        /// <ul>
        /// <li>Caixa/Pacote</li>
        /// <li>Rolo/Prisma</li>
        /// <li>Envelope</li>
        /// </ul>
        /// </summary>
        public string formatoEmbalagemCorreios { get; set; }

        /// <summary>
        /// Comprimento da embalagem
        /// </summary>
        public decimal? comprimento { get; set; }

        /// <summary>
        /// Altura da embalagem
        /// </summary>
        public decimal? altura { get; set; }

        /// <summary>
        /// Largura da embalagem
        /// </summary>
        public decimal? largura { get; set; }

        /// <summary>
        /// Diametro da embalagem
        /// </summary>
        public decimal? diametro { get; set; }

        /// <summary>
        /// Mão própria
        /// </summary>
        public bool? servicoMaoPropria { get; set; }

        /// <summary>
        /// aviso de recebimento
        /// </summary>
        public bool? avisoRecebimento { get; set; }

        /// <summary>
        /// CNPJ do Pagador
        /// </summary>
        public string cnpjPagador { get; set; }

        /// <summary>
        /// Valor declarado
        /// </summary>
        public decimal? valorDeclarado { get; set; }

        /// <summary>
        /// Nome Destinatario
        /// </summary>
        public string nomeDestinatario { get; set; }

        /// <summary>
        /// Documento Destinatario
        /// </summary>
        public string docDestinatario { get; set; }

        /// <summary>
        /// Endereco Numero
        /// </summary>
        public string enderecoNumero { get; set; }

        /// <summary>
        /// Endereco
        /// </summary>
        public string endereco { get; set; }

        /// <summary>
        /// Endereco Cidade
        /// </summary>
        public string cidade { get; set; }

        /// <summary>
        /// Endereco Uf
        /// </summary>
        public string uf { get; set; }

        /// <summary>
        /// Bairro
        /// </summary>
        public string bairro { get; set; }

        /// <summary>
        /// Complemento Endereço
        /// </summary>
        public string complemento { get; set; }

        /// <summary>
        /// Inscrição Estadual
        /// </summary>
        public string inscricaoEstadual { get; set; }
        /// <summary>
        /// Sku
        /// </summary>
        public string sku { get; set; }
        /// <summary>
        /// Frete Receita
        /// </summary>
        public decimal? valorFreteReceita { get; set; }
        /// <summary>
        /// Frete Custo
        /// </summary>
        public decimal? valorFreteCusto { get; set; }
        /// <summary>
        /// Peso Cubico
        /// </summary>
        public decimal? pesoCubico { get; set; }
        /// <summary>
        /// Quantidade de Item
        /// </summary>
        public int? quantidadeItem { get; set; }

    }
    /// <summary>
    /// itens da entrega.
    /// </summary>
    public class item
    {
        public string codigoVolume { get; set; }
        public string codigoItem { get; set; }
        public string codigoItemDetalhe { get; set; }
        public int? quantidadeItens { get; set; }
        public decimal? freteCliente { get; set; }
        public string sku { get; set; }
        public decimal? altura { get; set; }
        public decimal? largura { get; set; }
        public decimal? comprimento { get; set; }
        public decimal? peso { get; set; }
        public decimal? freteCusto { get; set; }
        public decimal? valorItem { get; set; }
        public string urlImagem { get; set; }
        public string urlNotaFiscal { get; set; }
        public decimal? valorTotalItem { get; set; }
    }

    public class volume
    {
        public string nome { get; set; }
        public string codigo { get; set; }
        public string codigoRastreio { get; set; }
        public int? numero { get; set; }
        public int? quantidadeItens { get; set; }
        public string notaFiscal { get; set; }
        public string serie { get; set; }
        public string danfe { get; set; }
        public string codigoCFOP { get; set; }
        public decimal? altura { get; set; }
        public decimal? largura { get; set; }
        public decimal? comprimento { get; set; }
        public decimal? peso { get; set; }
        public decimal? pesoCubico { get; set; }
        public decimal? valorDeclarado { get; set; }
        public decimal? valorTotal { get; set; }
    }
    /// <summary>
    /// Objeto de entrada agrupador por tipo de entrega 
    /// </summary>
    public class tipo
    {
        /// <summary>
        /// Serviço do tipo de entrega
        /// Valores aceitos: Entrega, Reversa
        /// Caso vazio padrão é Entrega
        /// </summary>
        public string servico { get; set; }

        /// <summary>
        /// Filiais 
        /// </summary>
        [Required]
        public filial[] filiais { get; set; }
    }

    /// <summary>
    /// Objetos de entrada agrupador por transportador
    /// </summary>
    /// 
    public class transportador
    {

        /// <summary>
        /// CNPJ do transportador
        /// </summary>
        //[Required]
        public string cnpj { get; set; }

        /// <summary>
        /// Entregas 
        /// </summary>
        [Required]
        public entrega[] entregas { get; set; }
    }

    /// <summary>
    /// Objeto de entrada agrupador de por filial
    /// </summary>
    public class filial
    {
        /// <summary>
        /// CNPJ Filial
        /// </summary>
        //[Required]
        public string cnpj { get; set; }

        /// <summary>
        /// Código Filial
        /// </summary>
        public string codigo { get; set; }

        /// <summary>
        /// Nome Segmento
        /// </summary>
        public string segmento { get; set; }

        /// <summary>
        /// Transportadores 
        /// </summary>
        [Required]
        public transportador[] transportadores { get; set; }
    }

    /// <summary>
    /// Objeto de entrada contém os dados da entrega
    /// </summary>
    public class entrega
    {
        ///// <summary>
        ///// Numero da Entrega
        ///// </summary>
        //public string Id { get; set; }

        /// <summary>
        /// Número Nota Fiscal
        /// </summary>
        [Required]
        public long nf { get; set; }

        /// <summary>
        /// Série da Nota fiscal
        /// </summary>
        [Required]
        public int serie { get; set; }

        /// <summary>
        /// Chave da NFe
        /// </summary>
        public string danfe { get; set; }

        /// <summary>
        /// Número da pedido / código da pedido
        /// </summary>
        public string codigoPedido { get; set; }

        /// <summary>
        /// Número da Entrega / Código da Entrega
        /// </summary>
        [Required]
        public string codigoEntrega { get; set; }

        /// <summary>
        /// CEP de Origem
        /// </summary>
        [Required]
        public int cepOrigem { get; set; }

        /// <summary>
        /// Cep Destino
        /// </summary>
        [Required]
        public string cepDestino { get; set; }

        /// <summary>
        /// Data de postagem
        /// ex.: yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string dataPostagem { get; set; }

        /// <summary>
        /// Data de Prevista Entrega
        /// ex.: yyyy-MM-dd
        /// </summary>
        public string dataPrevistaEntrega { get; set; }

        /// <summary>
        /// Data de Emissão NF
        /// ex.: yyyy-MM-dd
        /// </summary>
        public string dataEmissaoNF { get; set; }

        /// <summary>
        /// Prazo comercial
        /// </summary>
        public string dataPrazoComercial { get; set; }

        /// <summary>
        /// Código de Rastreio / Sro
        /// </summary>
        public string codigoRastreio { get; set; }

        /// <summary>
        /// Código de PLP
        /// </summary>
        public string codigoPLP { get; set; }

        /// <summary>
        /// Código de Etiqueta Embarcador
        /// </summary>
        public string awb { get; set; }

        /// <summary>
        /// Canal de Vendas
        /// </summary>
        public string canalVendas { get; set; }

        /// <summary>
        /// Código do contrato com o transportador ou correios
        /// </summary>
        public string codigoContrato { get; set; }

        /// <summary>
        /// Valor do Frete
        /// </summary>
        public decimal? ValorFrete { get; set; }

        /// <summary>
        /// Custo do Frete
        /// </summary>
        public decimal? valorCustoFrete { get; set; }

        /// <summary>
        /// Código do serviço dos correios
        /// </summary>
        [Obsolete]
        public string servicoCorreio { get; set; }

        /// <summary>
        /// Código Serviço transportador / Micro Serviço
        /// </summary>
        public string servicoTransportador { get; set; }

        /// <summary>
        /// Cnpj emissor da nota fiscal do terceiro
        /// </summary>
        public string cnpjTerceiro { get; set; }

        /// <summary>
        /// Número da nota fiscal do terceiro
        /// </summary>
        public string notaTerceiro { get; set; }

        /// <summary>
        /// Serie da notafiscal do terceiro
        /// </summary>
        public string serieTerceiro { get; set; }

        /// <summary>
        /// Número da carga do cliente
        /// </summary>
        public int? numeroCargaCliente { get; set; }
        /// <summary>
        /// Quantidade de Volumes da Entrega
        /// </summary>
        public int? quantidadeVolume { get; set; }

        /// <summary>
        /// Dados do Marketplace
        /// </summary>
        public entregaMarketplace marketplace { get; set; }

        /// <summary>
        /// campos obrigatórios em caso de PI
        /// </summary>
        public detalhes detalhe { get; set; }

        /// <summary>
        /// campos para contato ao cliente
        /// </summary>
        public contatos contato { get; set; }
        public List<item> itens { get; set; }
        public List<volume> volumes { get; set; }
    }
    public class entregaMarketplace
    {
        /// <summary>
        /// CNPJ Marketplace
        /// </summary>
        public string cnpj { get; set; }

        /// <summary>
        /// Nome canal de venda Marketplace
        /// </summary>
        public string canalVenda { get; set; }

        /// <summary>
        /// Código interno marketplace entrada (OrderId)
        /// </summary>
        public string codigoEntregaEntrada { get; set; }

        /// <summary>
        /// Código interno marketplace saída (GerencialId)
        /// </summary>
        public string codigoEntregaSaida { get; set; }

        /// <summary>
        /// código logista 
        /// </summary>
        public int? idLojista { get; set; }

        /// <summary>
        /// código rastreio marketplace
        /// </summary>
        public string codigoRastreio { get; set; }
    }
}
