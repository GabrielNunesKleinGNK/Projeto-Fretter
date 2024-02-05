using Fretter.Domain.Enum;
using Nest;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class ContratoTransportadorRegra : EntityBase
    {
        #region Construtores
        public ContratoTransportadorRegra()
        { }

        public ContratoTransportadorRegra(int Id,byte TipoCondicao, bool operacao, decimal valor, int transportadorId, bool ativo, int? ocorrenciaId, string Ocorrencia, int? ConciliacaoTipoId)
        {
            this.Id = Id;
            this.OcorrenciaId = ocorrenciaId;
            this.Ocorrencia = Ocorrencia;
            this.Operacao = operacao;
            this.TipoCondicao = TipoCondicao;
            this.Valor = valor;
            this.TransportadorId = transportadorId;
            this.Ativo = ativo;
            this.ConciliacaoTipoId = ConciliacaoTipoId;            
        }

        #endregion

        #region "Propriedades"
        public int? OcorrenciaId { get;   set; }
        [Ignore]
        public virtual string Ocorrencia { get;  set; }
        public byte TipoCondicao { get;  set; }
        public bool Operacao { get;  set; }
        public decimal Valor { get;  set; }
        public int TransportadorId { get;  set; }        
        public int? ConciliacaoTipoId { get;  set; }
        public int? EmpresaTransportadorConfigId { get;  set; }
        #endregion
    }
}
