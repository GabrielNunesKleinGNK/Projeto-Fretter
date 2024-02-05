using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;

namespace Fretter.Api.Models.Fusion
{
    public class EmpresaTransporteTipoViewModel : IViewModel<EmpresaTransporteTipo>
    {
        public int Id { get; set; }
        public string Nome { get;  set; }
       
        public EmpresaTransporteTipo Model()
        {
            return new EmpresaTransporteTipo(Id, Nome);
        }
    }
}
