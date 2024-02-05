using Fretter.Domain.Dto.Conciliacao;
using Fretter.Domain.Dto.CTe;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Fretter.Conciliacao
{
    public class RelatorioDetalhadoDTO
    {
        public long CodigoConciliacao { get; set; }
        public string CodigoEntrega { get; set; }
        public string CodigoPedido { get; set; }
        public string Transportador { get; set; }
        public decimal? ValorCustoFrete { get; set; }
        public decimal? ValorCustoAdicional { get; set; }
        public decimal? ValorCustoReal { get; set; }
        public int QtdTentativas { get; set; }
        public bool PossuiDivergenciaPeso { get; set; }
        public bool PossuiDivergenciaTarifa { get; set; }
        public string StatusConciliacao { get; set; }
        public int StatusConciliacaoId { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime? Finalizado { get; set; }
        public bool? ProcessadoIndicador { get; set; }
        public decimal? EntregaPeso { get; set; }
        public decimal? EntregaAltura { get; set; }
        public decimal? EntregaComprimento { get; set; }
        public decimal? EntregaLargura { get; set; }
        public decimal? EntregaValorDeclarado { get; set; }
        public int MicroServicoId { get; set; }
        public string MicroServico { get; set; }
        public int CanalId { get; set; }
        public string CanalCNPJ { get; set; }
        public int CanalVendaId { get; set; }
        public string CanalVenda { get; set; }
        public string JsonValoresRecotacao { get; set; }
        public string JsonValoresCte { get; set; }
        public string TipoCobranca { get; set; }
        public bool Selecionado { get; set; }
        public bool DesabilitaEnvioRecalcular => StatusConciliacaoId == 1 || StatusConciliacaoId == 4;

        public List<ItemComposicaoDto> ListComposicaoCte { get; set; }
        public List<ItemComposicaoDto> ListComposicaoRecotacao { get; set; }
        public List<ConciliacaoRecotacaoDTO> ListRecotacoes { get; set; }
        //Paginacao Front
        public int QtdRegistrosQuery { get; set; }
    }
}
