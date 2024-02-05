using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Entities.Fretter
{
    public class FaturaArquivoCritica : EntityBase
    {
        public int FaturaArquivoId { get; set; }
        public int Linha { get; set; }
        public int Posicao { get; set; }
        public string Descricao { get; set; }
    }
}
