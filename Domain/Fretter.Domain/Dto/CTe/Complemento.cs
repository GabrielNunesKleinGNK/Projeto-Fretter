using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Fretter.Domain.Dto.CTe
{
    public class Complemento
    {
        [XmlElement("xCaracAd")]
        public string Caracteristica { get; set; }
    }
}
