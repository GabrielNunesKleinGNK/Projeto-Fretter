using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using Fretter.Domain.Enum;

namespace Fretter.Api.Models
{
    public class TabelaArquivoStatusViewModel : IViewModel<TabelaArquivoStatus>
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime Criacao { get; set; }

        public TabelaArquivoStatus Model()
        {
            return new TabelaArquivoStatus(Id, Status, Criacao);
        }
    }
}
