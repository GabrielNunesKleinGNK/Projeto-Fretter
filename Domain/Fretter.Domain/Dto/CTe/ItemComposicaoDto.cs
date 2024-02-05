
using Fretter.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Fretter.Domain.Dto.CTe
{
    public class ItemComposicaoDto
    {
        public ItemComposicaoDto(string chave, decimal valor, EnumCteComposicaoTipo tipo)
        {
            this.chave = chave;
            this.valor = valor;
            this.tipo = tipo;
        }

        public string chave { get; set; }
        public decimal valor { get; set; }
        public EnumCteComposicaoTipo tipo { get; set; }
    }
}
