
using System;
using System.Xml.Serialization;

namespace Fretter.Domain.Dto.CTe
{
    [Serializable]
    public class InfCte
    {
        [XmlAttribute("versao")]
        public string Versao { get; set; }
        [XmlAttribute("Id")]
        public string Id { get; set; }
        [XmlElement("ide")]
        public Identificacao Identificacao { get; set; }
        [XmlElement("compl")]
        public Complemento Complemento { get; set; }
        [XmlElement("xJust")]
        public string Justificativa { get; set; }
        [XmlElement("emit")]
        public Emitente Emitente { get; set; }
        [XmlElement("rem")]
        public Remetente Remetente { get; set; }
        [XmlElement("exped")]
        public Expedidor Expedidor { get; set; }
        [XmlElement("receb")]
        public Recebedor Recebedor { get; set; }
        [XmlElement("dest")]
        public Destinatario Destinatario { get; set; }
        [XmlElement("vPrest")]
        public ValorPrestacao ValorPrestacao { get; set; }
        [XmlElement("imp")]
        public Impostos Impostos { get; set; }
        [XmlElement("infCTeNorm")]
        public InformacaoCTe InformacaoCte { get; set; }
        [XmlElement("infCteComp")]
        public InformacaoCTeComplementar InformacaoCteComplementar { get; set; }

    }
}
