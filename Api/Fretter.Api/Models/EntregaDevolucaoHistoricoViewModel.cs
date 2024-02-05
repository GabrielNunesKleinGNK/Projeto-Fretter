using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;

namespace Fretter.Api.Models
{
    public class EntregaDevolucaoHistoricoViewModel : IViewModel<EntregaDevolucaoHistorico>
    {
        public int Id { get; set; }
        public int EntregaDevolucaoId { get; set; }
        public string CodigoColeta { get; set; }
        public string CodigoRastreio { get; set; }
        public DateTime? Validade { get; set; }
        public string Mensagem { get; set; }
        public string Retorno { get; set; }
        public int EntregaDevolucaoStatusAnteriorId { get; set; }
        public int EntregaDevolucaoStatusAtualId { get; set; }
        public DateTime? Inclusao { get; set; }
        public string XmlRetorno { get; set; }

        //public EntregaDevolucaoAcaoViewModel Acao { get; set; }
        public EntregaDevolucaoStatusViewModel StatusAtual { get; set; }
        public EntregaDevolucaoStatusViewModel StatusAnterior { get; set; }

        public EntregaDevolucaoHistorico Model()
        {
            return new EntregaDevolucaoHistorico(Id, EntregaDevolucaoId, CodigoColeta, CodigoRastreio,
            Validade, Mensagem, Retorno, Inclusao, EntregaDevolucaoStatusAnteriorId, EntregaDevolucaoStatusAtualId, XmlRetorno);
        }
    }
}
