using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Conciliacao
{
    public class EntregaConciliacaoDTO
    {
        public Int64? ControleProcessoConciliacaoId { get; set; }
        public long EntregaConciliacaoId { get; set; }
        public long EntregaId { get; set; }
        public int EmpresaId { get; set; }
        public int TransportadorId { get; set; }
        public DateTime DataImportacao { get; set; }
        public string URLRequisicao { get; set; }
        public string URLApiKey { get; set; }
        public string CodigoPLP { get; set; }
        public string CodigoRastreio { get; set; }
        public string CodigoPedido { get; set; }
        public string CEPOrigem { get; set; }
        public string CEPDestino { get; set; }
        public string SistemaUsuario { get; set; }
        public string SistemaSenha { get; set; }
    }

}
