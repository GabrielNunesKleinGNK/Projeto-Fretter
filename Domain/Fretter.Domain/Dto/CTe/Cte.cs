using System;
using System.Xml.Serialization;

namespace Fretter.Domain.Dto.CTe
{
    [Serializable]
    [XmlRoot("cteProc", Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class enviCTe
    {
        public enviCTe()
        {
            
        }

        [XmlAttribute(AttributeName="versao")]
        public string Versao { get; set; }
        [XmlElement(ElementName = "idLote")]
        public string IdLote { get; set; }
        [XmlElement(ElementName = "CTe")]
        public CTe CTe { get; set; }
        [XmlElement(ElementName = "protCTe")]
        public ProtCTe ProtCTe { get; set; }
    }

    [Serializable]
    public class CTe
    {
        [XmlElement(ElementName="infCte")]
        public InfCte InfCte { get; set; }
    }
}
