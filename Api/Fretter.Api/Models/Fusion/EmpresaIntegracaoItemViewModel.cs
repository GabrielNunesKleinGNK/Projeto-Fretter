using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.Api.Models
{
    public class EmpresaIntegracaoItemViewModel : IViewModel<EmpresaIntegracaoItem>
    {
        public int Id { get; set; }
        public int EmpresaIntegracaoId { get;  set; }
        public string URLBase { get;  set; }
        public string URL { get;  set; }
        public string Verbo { get;  set; }
        public bool? Lote { get;  set; }
        public string LayoutHeader { get;  set; }
        public string Layout { get;  set; }
        public DateTime? DataProcessamento { get;  set; }
        public bool? ProcessamentoSucesso { get;  set; }
        public int Registros { get;  set; }
        public int Paralelo { get;  set; }
        public bool? Producao { get;  set; }
        public bool? EnvioBody { get;  set; }
        public int? EnvioConfigId { get;  set; }
        public bool? IntegracaoGatilho { get;  set; }
        public bool Ativo { get; set; }

        public EmpresaIntegracaoItem Model()
        {
            return new EmpresaIntegracaoItem(this.Id, EmpresaIntegracaoId, URLBase, URL, Verbo, Lote, LayoutHeader, Layout,
                          DataProcessamento, ProcessamentoSucesso, Registros, Paralelo, Producao,  EnvioBody,
                           EnvioConfigId, IntegracaoGatilho, Ativo);
        }
    }
}
