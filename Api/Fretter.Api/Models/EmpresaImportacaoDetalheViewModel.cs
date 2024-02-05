using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.Api.Models
{

    public class EmpresaImportacaoDetalheViewModel : IViewModel<EmpresaImportacaoDetalhe>
    {
        public int Id { get; set; }
        public int? EmpresaImportacaoArquivoId { get; set; }
        public int? EmpresaId { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Cnpj { get; set; }
        public string Descricao { get; set; }
        public string Cep { get; set; }
        public string Nome { get; set; }
        public bool CorreioBalcao { get; set; }
        public bool ConsomeApiFrete { get; set; }
        public bool SucessoEmpresa { get; set; }
        public bool SucessoCanal { get; set; }
        public bool SucessoPermissao { get; set; }
        public DateTime DataCadastro { get; set; }

        public EmpresaImportacaoDetalhe Model()
        {
            return new EmpresaImportacaoDetalhe(this.Id);
        }
    }
}
