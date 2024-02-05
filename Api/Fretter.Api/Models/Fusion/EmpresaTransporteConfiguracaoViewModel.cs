using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Fretter.Api.Models.Fusion
{
    public class EmpresaTransporteConfiguracaoViewModel : IViewModel<EmpresaTransporteConfiguracao>
    {
        public int Id { get; set; }
        public int EmpresaTransporteTipoItemId { get; set; }
        public int EmpresaId { get; set; }
        public string CodigoContrato { get; set; }
        public string CodigoIntegracao { get; set; }
        public string CodigoCartao { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public DateTime? VigenciaInicial { get; set; }
        public DateTime? VigenciaFinal { get; set; }
        public int DiasValidade { get; set; }
        public string RetornoValidacao { get; set; }
        public DateTime? DataValidacao { get; set; }
        public bool Valido { get; set; }
        public bool Producao { get; set; }

        public EmpresaTransporteTipoItemViewModel EmpresaTransporteTipoItem { get; set; }
        public List<EmpresaTransporteConfiguracaoItemViewModel> EmpresaTransporteConfiguracaoItems { get; set; }

        public EmpresaTransporteConfiguracao Model()
        {
            return new EmpresaTransporteConfiguracao
                (Id, EmpresaTransporteTipoItemId, EmpresaId, CodigoContrato, CodigoIntegracao, CodigoCartao, Usuario, Senha, VigenciaInicial, VigenciaFinal, DiasValidade, RetornoValidacao,
                 DataValidacao, Valido, Producao);
        }
    }
}
