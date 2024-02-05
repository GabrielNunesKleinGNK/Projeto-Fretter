using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fretter;
using System;
using System.Collections.Generic;

namespace Fretter.Api.Models
{
    public class AgendamentoRegraViewModel : IViewModel<AgendamentoRegra>
    {
        public int Id { get; set; }
        public int? EmpresaTransportadorId { get; set; }
        public int RegraStatusId { get; set; }
        public int RegraTipoId => 1;
        public int? EmpresaId { get; set; }
        public int? CanalId { get; set; }
        public int? TransportadorId { get; set; }
        public int? TransportadorCnpjId { get; set; }
        public string Nome { get; set; }
        public bool DefinirVigencia => DataInicio != null && DataTermino != null;
        public DateTime? DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }
        public DateTime DataCadastro { get; set; }
        public List<RegraItemViewModel> RegraItens { get; set; }

        public Canal Canal { get; set; }
        public Fusion.TransportadorViewModel Transportador { get; set; }
        public Fusion.TransportadorCnpjViewModel TransportadorCnpj { get; set; }

        public AgendamentoRegra Model()
        {
            var model = new AgendamentoRegra(Id, EmpresaTransportadorId, EmpresaId, CanalId, TransportadorId, TransportadorCnpjId, RegraStatusId, RegraTipoId, Nome, DataInicio, DataTermino);

            foreach (var item in RegraItens)
            {
                model.AdicionarRegraItem(item.Model());
            }

            return model;
        }
    }
}
