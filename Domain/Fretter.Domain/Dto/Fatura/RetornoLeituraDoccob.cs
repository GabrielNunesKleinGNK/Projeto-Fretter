using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Helpers.Proceda.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Fatura
{
    public class RetornoLeituraDoccob
    {
        public RetornoLeituraDoccob()
        {
            this.Criticas = new List<FaturaArquivoCriticaDTO>();
        }

        public DOCCOB Doccob { get; set; }
        public List<FaturaArquivoCriticaDTO> Criticas { get; set; }
        public EnumDOCCOBTipo EnumDOCCOBTipo { get; set; }
    }
}
