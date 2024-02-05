using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class OcorrenciaArquivoViewModel : IViewModel<OcorrenciaArquivo>
    {
        public int Id { get; set; }
        public int TabelaArquivoStatusId { get;  set; }
        public int EmpresaId { get;  set; }
        public string NomeArquivo { get;  set; }
        public string Url { get;  set; }
        public string Retorno { get;  set; }
        public int? QtAdvertencia { get;  set; }
        public int? QtErros { get;  set; }
        public int? QtRegistros { get;  set; }
        public int? PercentualAtualizacao { get;  set; }
        public DateTime? UltimaAtualizacao { get;  set; }
        public string Usuario { get;  set; }
        public OcorrenciaArquivo Model()
        {
            return new OcorrenciaArquivo(Id, EmpresaId, TabelaArquivoStatusId,  NomeArquivo, Url, Retorno, Usuario, UltimaAtualizacao, QtAdvertencia, QtErros, QtRegistros, PercentualAtualizacao);

        }
    }
}
