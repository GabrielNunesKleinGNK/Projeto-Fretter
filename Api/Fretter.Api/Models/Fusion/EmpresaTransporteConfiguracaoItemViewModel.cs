using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Fretter.Api.Models.Fusion
{
    public class EmpresaTransporteConfiguracaoItemViewModel : IViewModel<EmpresaTransporteConfiguracaoItem>
    {
        public int Id { get; set; }
        public int EmpresaTransporteConfiguracaoId { get; set; }
        public string CodigoServico { get; set; }
        public string CodigoServicoCategoria { get; set; }
        public string CodigoIntegracao { get; set; }
        public string Nome { get; set; }
        public DateTime? DataCadastroServico { get; set; }
        public DateTime? VigenciaInicial { get; set; }
        public DateTime? VigenciaFinal { get; set; }
        public DateTime? DataAtualizacao { get; set; }


        public EmpresaTransporteConfiguracaoItem Model()
        {
            return new EmpresaTransporteConfiguracaoItem(Id, EmpresaTransporteConfiguracaoId, CodigoServico, CodigoServicoCategoria, CodigoIntegracao, Nome, DataCadastroServico,
                VigenciaInicial, VigenciaFinal, DataAtualizacao);
        }
    }
}
