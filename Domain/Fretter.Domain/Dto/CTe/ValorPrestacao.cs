
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Fretter.Domain.Dto.CTe
{
    [Serializable]
    public class ValorPrestacao
    {
        [XmlElement("vTPrest")]
        public double? ValorTotal { get; set; }
        [XmlElement("vRec")]
        public double? ValorReceber { get; set; }

         [XmlElement("Comp")]
        public List<Componentes> Componentes { get; set; }

    }

    [Serializable]
    public class Componentes
    {
        [XmlElement("xNome")]
        public string NomeComponente { get; set; }
        [XmlElement("vComp")]
        public decimal? ValorComponente { get; set; }

    }
}
