using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Fatura
{
    public class EmissaoNotaFiscalDTO
    {
        public EmissaoNotaFiscalDTO(string notaSerie, string notaNumero, DateTime dataEmissao, int linha, int posicao)
        {
            NotaSerie = notaSerie;
            NotaNumero = notaNumero;
            DataEmissao = dataEmissao;
            Linha = linha;
            Posicao = posicao;
        }

        public string NotaSerie { get; set; }
        public string NotaNumero { get; set; }
        public DateTime DataEmissao { get; set; }
        public int Linha { get; set; }
        public int Posicao { get; set; }
    }
}
