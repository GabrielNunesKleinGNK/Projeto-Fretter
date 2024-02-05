
using System;
using System.Xml.Serialization;

namespace Fretter.Domain.Dto.CTe
{
    [Serializable]
    public class Remetente
    {
        [XmlElement("CNPJ")]
        public string CNPJ { get; set; }
        [XmlElement("CPF")]
        public double CPF { get; set; }
        [XmlElement("IE")]
        public string IE { get; set; }
        [XmlElement("xNome")]
        public string RazaoSocial { get; set; }
        [XmlElement("xFant")]
        public string NomeFantasia { get; set; }
        [XmlElement("fone")]
        public string Telefone { get; set; }
        [XmlElement("enderReme")]
        public Endereco Endereco { get; set; }
        [XmlElement("email")]
        public string Email { get; set; }


    }

    [Serializable]
    public class LocalColeta
    {
        [XmlElement("CNPJ")]
        public double CNPJ { get; set; }
        [XmlElement("CPF")]
        public double CPF { get; set; }
        [XmlElement("xNome")]
        public string RazaoSocial { get; set; }
        [XmlElement("xLgr")]
        public string Logradouro { get; set; }
        [XmlElement("nro")]
        public string Numero { get; set; }
        [XmlElement("xCpl")]
        public string Complemento { get; set; }
        [XmlElement("xBairro")]
        public string Bairro { get; set; }
        [XmlElement("cMun")]
        public double CodigoMunicipio { get; set; }
        [XmlElement("xMun")]
        public string Municio { get; set; }
        [XmlElement("UF")]
        public string UF { get; set; }

    }
}
