using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Fatura
{
    public class FaturaArquivoCriticaDTO
    {
        public int FaturaArquivoCriticaId { get; set; }
        public int FaturaArquivoId { get; set; }
        public int Linha { get; set; }
        public int Posicao { get; set; }
        public string Descricao { get; set; }
        public int UsuarioCriacao { get; set; }
    }
}
