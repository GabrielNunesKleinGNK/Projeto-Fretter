using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class ArquivoImportacaoViewModel : IViewModel<ArquivoImportacao>
    {
		public int Id { get; set; }
        public int FaturaId { get; set; }
        public string IdentificacaoRemetente { get; set; }
        public string IdentificacaoDestinatario { get; set; }
        public DateTime Data { get; set; }
        public int QtdTotal { get; set; }
        public int QtdItens { get; set; }
        public string ArquivoUrl { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataCadastro { get; set; }
        
        ArquivoImportacao IViewModel<ArquivoImportacao>.Model()
        {
            return new ArquivoImportacao();
        }
    }
}      
