using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Wordprocessing;
using Fretter.Domain.Dto.Dashboard;
using Fretter.Domain.Entities.Fusion;
using Nest;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace Fretter.Domain.Entities.Fretter
{
	public class AgendamentoRegra : EntityBase
	{
        public AgendamentoRegra(int id, int? empresaTransportadorId, int? empresaId, int? canalId, int? transportadorId, int? transportadorCnpjId, int regraStatusId, int regraTipoId, string nome, DateTime? dataInicio, DateTime? dataTermino)
        {
            Id = id;
            EmpresaTransportadorId = empresaTransportadorId;
            EmpresaId = empresaId;
            CanalId = canalId;
            TransportadorId = transportadorId;
            TransportadorCnpjId = transportadorCnpjId;
            RegraStatusId = regraStatusId;
            RegraTipoId = regraTipoId;
            Nome = nome;
            DataInicio = dataInicio;
            DataTermino = dataTermino;
        }

        public int? EmpresaTransportadorId { get; set; }
		public int RegraStatusId { get; set; }
        public int RegraTipoId { get; set; }
        public int? EmpresaId { get; set; }
        public int? CanalId { get; set; }
        public int? TransportadorId { get; set; }
        public int? TransportadorCnpjId { get; set; }
        public string Nome { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }

        #region Referências
        public List<RegraItem> RegraItens { get; set; }

        public virtual Canal Canal { get; set; }
        public virtual Fusion.Transportador Transportador { get; set; }
        public virtual Fusion.TransportadorCnpj TransportadorCnpj { get; set; }
        #endregion

        #region Métodos

        public void AdicionarRegraItem(RegraItem regraItem)
        {
            RegraItens ??= new List<RegraItem>();

            RegraItens.Add(regraItem);
        } 
        #endregion

    }
}
