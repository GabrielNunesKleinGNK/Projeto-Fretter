
using System;
using System.Xml.Serialization;

namespace Fretter.Domain.Dto.CTe
{
    [Serializable]
    public class Emitente
    {
        [XmlElement("CNPJ")]
        public string CNPJ { get; set; }
        [XmlElement("IE")]
        public double IE { get; set; }
        [XmlElement("xNome")]
        public string RazaoSocial { get; set; }
        [XmlElement("xFant")]
        public string NomeFantasia { get; set; }
        [XmlElement("enderEmit")]
        public Endereco Endereco { get; set; }

    }
}
