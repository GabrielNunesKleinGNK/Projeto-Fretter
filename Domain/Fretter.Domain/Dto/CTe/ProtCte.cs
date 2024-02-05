using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Fretter.Domain.Dto.CTe
{

    [XmlRoot(ElementName = "infProt")]
    public class InfProt
    {
        [XmlElement(ElementName = "tpAmb")]
        public string TipoAmbiente { get; set; }
        [XmlElement(ElementName = "verAplic")]
        public string VersaoAplicacao { get; set; }
        [XmlElement(ElementName = "chCTe")]
        public string ChaveCTe { get; set; }
        [XmlElement(ElementName = "dhRecbto")]
        public DateTime? DataAutorizacao { get; set; }
        [XmlElement(ElementName = "nProt")]
        public string ProtocoloAutorizacao { get; set; }
        [XmlElement(ElementName = "digVal")]
        public string DigestValue { get; set; }
        [XmlElement(ElementName = "cStat")]
        public string StatusAutorizacao { get; set; }
        [XmlElement(ElementName = "xMotivo")]
        public string MotivoAutorizacao { get; set; }
    }

    [Serializable]
    public class ProtCTe
    {
        [XmlElement(ElementName = "infProt")]
        public InfProt InfProt { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; }
    }
}
