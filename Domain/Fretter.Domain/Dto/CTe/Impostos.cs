
using System;
using System.Xml.Serialization;

namespace Fretter.Domain.Dto.CTe
{
    [Serializable]
    public class Impostos
    {
        [XmlElement("ICMS")]
        public ICMS ICMS { get; set; }
        [XmlElement("vTotTrib")]
        public decimal ValorTotalTributos { get; set; }
        [XmlElement("infAdFisco")]
        public string InformacoesAdicionais { get; set; }

    }


    [Serializable]
    public class ICMS
    {
        [XmlElement("ICMS00")]
        public ICMS00 ICMS00 { get; set; }

        [XmlElement("ICMS20")]
        public ICMS20 ICMS20 { get; set; }

        [XmlElement("ICMS45")]
        public ICMS45 ICMS45 { get; set; }

        [XmlElement("ICMS60")]
        public ICMS60 ICMS60 { get; set; }

        [XmlElement("ICMS90")]
        public ICMS90 ICMS90 { get; set; }

        [XmlElement("ICMSSN")]
        public ICMSSN ICMSSN { get; set; }
        [XmlElement("ICMSOutraUF")]
        public ICMSOutraUF ICMSOutraUF { get; set; }
    }

    [Serializable]
    public class ICMS00
    {
        [XmlElement("CST")]
        public string CST { get; set; }
        [XmlElement("vBC")]
        public double ValorBC { get; set; }
        [XmlElement("pICMS")]
        public double AliquotaICMS { get; set; }
        [XmlElement("vICMS")]
        public double ValorICMS { get; set; }

    }

    [Serializable]
    public class ICMS20
    {
        [XmlElement("CST")]
        public string CST { get; set; }
        [XmlElement("pRedBC")]
        public double AliquotaReducao { get; set; }
        [XmlElement("vBC")]
        public double ValorBC { get; set; }
        [XmlElement("Picms")]
        public double AliquotaICMS { get; set; }
        [XmlElement("vICMS")]
        public double ValorICMS { get; set; }

    }

    [Serializable]
    public class ICMS45
    {
        [XmlElement("CST")]
        public string CST { get; set; }
    }

    [Serializable]
    public class ICMS60
    {
        [XmlElement("CST")]
        public string CST { get; set; }
        [XmlElement("vBCSTRet")]
        public double ValorBCST { get; set; }
        [XmlElement("vICMSSTRet")]
        public double ValorICMSST { get; set; }
        [XmlElement("pICMSSTRet")]
        public double AliquotaICMS { get; set; }
        [XmlElement("vCred")]
        public double ValorCredito { get; set; }

    }

    [Serializable]
    public class ICMS90
    {
        [XmlElement("CST")]
        public string CST { get; set; }
        [XmlElement("pRedBC")]
        public double AliquotaReducao { get; set; }
        [XmlElement("vBC")]
        public double ValorBC { get; set; }
        [XmlElement("pICMS")]
        public double AliquotaICMS { get; set; }
        [XmlElement("vICMS")]
        public double ValorICMS { get; set; }
        [XmlElement("vCred")]
        public double ValorCredito { get; set; }

    }

    [Serializable]
    public class ICMSSN
    {
        [XmlElement("indSN")]
        public double IsSimplesNacional { get; set; }

    }

    [Serializable]
    public class ICMSOutraUF
    {
        [XmlElement("CST")]
        public string CST { get; set; }
        [XmlElement("vBCOutraUF")]
        public double ValorBC { get; set; }
        [XmlElement("pICMSOutraUF")]
        public double AliquotaICMS { get; set; }
        [XmlElement("vICMSOutraUF")]
        public double ValorICMS { get; set; }

    }
}
