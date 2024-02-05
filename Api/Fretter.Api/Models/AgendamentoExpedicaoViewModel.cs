using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fretter;
using System;

namespace Fretter.Api.Models
{
    public class AgendamentoExpedicaoViewModel : IViewModel<AgendamentoExpedicao>
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public int? CanalId { get; set; }
        public int? TransportadorId { get; set; }
        public int? TransportadorCnpjId { get; set; }
        public bool ExpedicaoAutomatica { get; set; }
        public byte PrazoComercial { get; set; }
        public DateTime DataCadastro { get; set; }

        public Canal Canal { get; set; }
        public Fusion.TransportadorViewModel Transportador { get; set; }
        public Fusion.TransportadorCnpjViewModel TransportadorCnpj { get; set; }

        public AgendamentoExpedicao Model()
        {
            return new AgendamentoExpedicao(Id, EmpresaId, CanalId, TransportadorId, TransportadorCnpjId, ExpedicaoAutomatica, PrazoComercial, DataCadastro);
        }
    }
}
