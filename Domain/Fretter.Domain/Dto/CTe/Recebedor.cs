
using System;
using System.Xml.Serialization;

namespace Fretter.Domain.Dto.CTe
{
    [Serializable]
    public class Recebedor
    {
        [XmlElement("CNPJ")]
        public double  CNPJ { get; set; }
        [XmlElement("CPF")]
        public double  CPF { get; set; }
        [XmlElement("IE")]
        public string IE { get; set; }
        [XmlElement("xNome")]
        public string RazaoSocial { get; set; }
        [XmlElement("fone")]
        public double  Telefone { get; set; }

        [XmlElement("enderReceb")]
        public Endereco Endereco { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }


    }
}
