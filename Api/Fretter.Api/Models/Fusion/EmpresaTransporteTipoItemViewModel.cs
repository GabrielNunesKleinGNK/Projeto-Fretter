using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Fretter.Api.Models.Fusion
{
    public class EmpresaTransporteTipoItemViewModel : IViewModel<EmpresaTransporteTipoItem>
    {
        public int Id { get; set; }
        public int EmpresaTransporteTipoId { get;  set; }
        public int TransportadorId { get;  set; }
        public string Url { get;  set; }
        public string Alias { get;  set; }
        public string CodigoIntegracao { get;  set; }

        public EmpresaTransporteTipoViewModel EmpresaTransporteTipo { get; set; }
        public TransportadorViewModel Transportador { get; set; }
        public List<EmpresaTransporteConfiguracaoViewModel> EmpresaTransporteConfiguracoes { get; set; }

        public EmpresaTransporteTipoItem Model()
        {
            var model =  new EmpresaTransporteTipoItem(Id, EmpresaTransporteTipoId, TransportadorId, Url, Alias, CodigoIntegracao);
            foreach (var item in EmpresaTransporteConfiguracoes)
            {
                model.AdicionarConfiguracao(item.Model());
            }
            return model;
        }
    }
}
