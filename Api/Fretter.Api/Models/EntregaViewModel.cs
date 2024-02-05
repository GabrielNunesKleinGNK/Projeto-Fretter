using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class EntregaViewModel : IViewModel<Entrega>
    {
        public int Id { get; set; }
        public DateTime Importacao { get; set; }
        public int Canal { get; set; }
        public int EmpresaTransportador { get; set; }
        public DateTime? Finalizado { get; set; }
        public string Sro { get; set; }
        public string CodigoEntrega { get; set; }
        public string CodigoPedido { get; set; }
        public string NotaFiscal { get; set; }
        public string Serie { get; set; }
        public char CepOrigem { get; set; }
        public char? CepDestino { get; set; }
        public char? CepPostagem { get; set; }
        public DateTime? DataPostagem { get; set; }
        public DateTime? PrazoTransportador { get; set; }
        public DateTime? PrevistaEntrega { get; set; }
        public DateTime? DataPesquisa { get; set; }
        public string Contrato { get; set; }
        public string Postagem { get; set; }
        public string LocalPostagem { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public int? CanalVenda { get; set; }
        public string CategoriaEntrega { get; set; }
        public int TransportadorCnpj { get; set; }
        public string PI { get; set; }
        public DateTime? DataAbertura { get; set; }
        public string ServicoCorreios { get; set; }
        public decimal? Frete { get; set; }
        public int? Arquivo { get; set; }
        public int? PrazoDias { get; set; }
        public bool? EntregaSabado { get; set; }
        public bool? ServicoCorreiosVerificado { get; set; }
        public int IsNULL { get; set; }
        public int EmpresaId { get; set; }
        public int? Transportador { get; set; }
        public int? EmpresaMarketPlace { get; set; }
        public byte TipoServico { get; set; }
        public DateTime? Devolucao { get; set; }
        public DateTime? PrazoDevolucao { get; set; }
        public DateTime? DataOcorrencia { get; set; }
        public string Ocorrencia { get; set; }
        public string Sigla { get; set; }
        public int? EntregaOcorrencia { get; set; }
        public bool? DtDevolucaoVerificada { get; set; }
        public DateTime? ImportacaoEmpresa { get; set; }
        public DateTime? ImportacaoMarketplace { get; set; }
        public DateTime? ImportacaoTransportador { get; set; }
        public string CnpjTerceiro { get; set; }
        public string NotaTerceiro { get; set; }
        public string SerieTerceiro { get; set; }
        public int? DtOcorrencia { get; set; }
        public int? FinalizadoNr { get; set; }
        public DateTime? Alteracao { get; set; }
        public string NfSerie { get; set; }
        public int? MicroServico { get; set; }
        public string AWB { get; set; }
        public int? Tabela { get; set; }
        public DateTime? EmissaoNF { get; set; }
        public string Danfe { get; set; }
        public DateTime? PostagemDATE { get; set; }
        public int TransportadorPadrao { get; set; }
        public decimal? CustoFrete { get; set; }
        public DateTime? DataDin { get; set; }
        public int? Din { get; set; }
        public string SroUnico { get; set; }
        public int? SroComprimento { get; set; }
        public bool? Rastreiar { get; set; }
        public string SroTratado { get; set; }
        public string Tempos { get; set; }
        public DateTime? ImportacaoDATE { get; set; }
        public bool? EnviadoElastic { get; set; }
        public int? TransportadorAlias { get; set; }
        public int? Volumes { get; set; }
        public bool? DropShipping { get; set; }
        public int? EmpresaDRS { get; set; }
        public int? CanalDRS { get; set; }
        public string NotaFiscalDRS { get; set; }
        public string NfSerieDRS { get; set; }
        public string SerieDRS { get; set; }
        public string DanfeDRS { get; set; }
        public int? CargaCliente { get; set; }
        public Fusion.EmpresaViewModel Empresa { get; set; }
        public Entrega Model()
        {
            return new Entrega(Id, Importacao, Canal, EmpresaTransportador, Finalizado, Sro,
             CodigoEntrega, CodigoPedido, NotaFiscal, Serie, CepOrigem, CepDestino,
              CepPostagem, DataPostagem, PrazoTransportador, PrevistaEntrega, DataPesquisa,
             Contrato, Postagem, LocalPostagem, Cidade, Uf, CanalVenda,
             CategoriaEntrega, TransportadorCnpj, PI, DataAbertura, ServicoCorreios,
              Frete, Arquivo, PrazoDias, EntregaSabado, ServicoCorreiosVerificado, IsNULL,
             EmpresaId, Transportador, EmpresaMarketPlace, TipoServico, Devolucao,
              PrazoDevolucao, Ocorrencia, DataOcorrencia, Sigla, EntregaOcorrencia,
             DtDevolucaoVerificada, ImportacaoEmpresa, ImportacaoMarketplace,
              ImportacaoTransportador, CnpjTerceiro, NotaTerceiro, SerieTerceiro,
              DtOcorrencia, FinalizadoNr, Alteracao, NfSerie, MicroServico, AWB,
              Tabela, EmissaoNF, Danfe, PostagemDATE, TransportadorPadrao, CustoFrete,
              DataDin, Din, SroUnico, SroComprimento, Rastreiar, SroTratado, Tempos,
              ImportacaoDATE, EnviadoElastic, TransportadorAlias, Volumes, DropShipping,
              EmpresaDRS, CanalDRS, NotaFiscalDRS, SerieDRS, DanfeDRS, NfSerieDRS,
              CargaCliente);
        }
    }
}
