using AutoMapper;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.Api.Models
{
    public class EmpresaIntegracaoViewModel : IViewModel<EmpresaIntegracao>
    {
        public int Id { get; set; }
        public int? EmpresaId { get;  set; }
        public int? CanalVendaId { get;  set; }
        public string URLBase { get;  set; }
        public string URLToken { get;  set; }
        public string ApiKey { get;  set; }
        public string Usuario { get;  set; }
        public string Senha { get;  set; }
        public string ClientId { get;  set; }
        public string ClientSecret { get;  set; }
        public string ClientScope { get;  set; }
        public int? EntregaOrigemImportacaoId { get;  set; }
        public bool Ativo{ get; set; }

        public List<EmpresaIntegracaoItemViewModel> ListaIntegracoes { get; set; }

        public EmpresaIntegracao Model()
        {

            return new EmpresaIntegracao(this.Id, EmpresaId, CanalVendaId, URLBase, URLToken, ApiKey, Usuario, Senha,
            ClientId, ClientSecret, ClientScope, EntregaOrigemImportacaoId, ConvertViewModelInModel(ListaIntegracoes), Ativo);
        }

        private List<EmpresaIntegracaoItem> ConvertViewModelInModel(List<EmpresaIntegracaoItemViewModel> integracao)
        {
            List<EmpresaIntegracaoItem> lista = new List<EmpresaIntegracaoItem>();

            integracao.ForEach(x => {
                lista.Add(x.Model());
            });
            return lista;
        }
    }
}
