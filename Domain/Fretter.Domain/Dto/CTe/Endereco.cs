
using System;
using System.Xml.Serialization;

namespace Fretter.Domain.Dto.CTe
{
    [Serializable]
    public class Endereco
    {
        [XmlElement("xLgr")]
        public string Logradouro { get; set; }
        [XmlElement("nro")]
        public string Numero { get; set; }
        [XmlElement("xCpl")]
        public string Complemento { get; set; }
        [XmlElement("xBairro")]
        public string Bairro { get; set; }
        [XmlElement("cMun")]
        public int CodigoMunicio { get; set; }
        [XmlElement("xMun")]
        public string Municio { get; set; }
        [XmlElement("CEP")]
        public int CEP { get; set; }
        [XmlElement("UF")]
        public string UF { get; set; }
        [XmlElement("cPais")]
        public double CodigoPais { get; set; }
        [XmlElement("xPais")]
        public string Pais { get; set; }

        [XmlElement("fone")]
        public string Telefone { get; set; }
    }
}
