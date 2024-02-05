using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Conciliacao
{
    public class EntregaConciliacaoRecotacaoDTO
    {
        public long ConciliacaoRecotacaoId { get; set; }
        public long ConciliacaoId { get; set; }
        public long EntregaId { get; set; }
        public int EmpresaId { get; set; }
        public int? MicroServicoId { get; set; }
        public int CanalId { get; set; }
        public string CanalVenda { get; set; }
        public string CanalCnpj { get; set; }
        public string CodigoPedido { get; set; }
        public string CepOrigem { get; set; }
        public string CepDestino { get; set; }
        public string CodigoSku { get; set; }
        public string CodigoItem { get; set; }
        public decimal? Valor { get; set; }
        public decimal? Altura { get; set; }
        public decimal? Largura { get; set; }
        public decimal? Comprimento { get; set; }
        public decimal? Peso { get; set; }
        public decimal? PesoConsiderado { get; set; }
        public int Quantidade { get; set; }
        public string TipoServico { get; set; }
        public string Token { get; set; }
    }

}
