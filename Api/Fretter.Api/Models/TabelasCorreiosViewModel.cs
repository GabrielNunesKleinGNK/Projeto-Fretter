using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using Fretter.Domain.Enum;

namespace Fretter.Api.Models
{
    public class TabelasCorreiosViewModel : IViewModel<TabelasCorreiosArquivo>
    {
        public int Id { get; set; }
        public int TabelaArquivoStatusId { get; set; }
        public string NomeArquivo { get; set; }
        public string Url { get; set; }
        public DateTime Criacao { get; set; }
        public DateTime? ImportacaoDados { get; set; }
        public DateTime? AtualizacaoTabelas { get; set; }
        public string Erro { get; set; }

        public TabelaArquivoStatusViewModel TabelaArquivoStatus { get; set; }
        public TabelasCorreiosArquivo Model()
        {
            return new TabelasCorreiosArquivo(Id, TabelaArquivoStatusId, NomeArquivo, Url, Criacao, ImportacaoDados, AtualizacaoTabelas, Erro);
        }
    }
}
