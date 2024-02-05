using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.Api.Models
{

    public class EmpresaImportacaoArquivoViewModel : IViewModel<EmpresaImportacaoArquivo>
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int? EmpresaId { get; set; }
        public string ArquivoURL { get; set; }
        public int? QuantidadeEmpresa { get; set; }
        public bool? Processado { get; set; }
        public bool? Sucesso { get; set; }
        public DateTime DataCadastro { get; set; }

        public EmpresaImportacaoArquivo Model()
        {
            return new EmpresaImportacaoArquivo(this.Id);
        }
    }
}
