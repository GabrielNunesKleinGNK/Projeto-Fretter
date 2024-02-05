
using System;
using System.Xml.Serialization;

namespace Fretter.Domain.Dto.CTe
{
    [Serializable]
    public class Identificacao
    {
        [XmlElement("cUF")]
        public double UF { get; set; }
        [XmlElement("cCT")]
        public string Codigo { get; set; }
        [XmlElement("CFOP")]
        public double CFOP { get; set; }
        [XmlElement("natOp")]
        public string NaturezaOperacao { get; set; }
        [XmlElement("forPag")]
        public double FormaPagamento { get; set; }
        [XmlElement("mod")]
        public double Modelo { get; set; }
        [XmlElement("serie")]
        public double Serie { get; set; }
        [XmlElement("nCT")]
        public string Numero { get; set; }
        [XmlElement("dhEmi")]
        public string DataEmissao { get; set; }
        [XmlElement("tpImp")]
        public double FormatoImpressao { get; set; }
        [XmlElement("tpEmis")]
        public double FormaEmissao { get; set; }
        [XmlElement("cDV")]
        public double DigitoVerificador { get; set; }
        [XmlElement("tpAmb")]
        public int TipoAmbiente { get; set; }
        [XmlElement("tpCTe")]
        public int TipoCte { get; set; }
        [XmlElement("procEmi")]
        public double ProcessoEmissao { get; set; }
        [XmlElement("verProc")]
        public string VersaoProcesso { get; set; }
        [XmlElement("refCTE")]
        public double ChaveReferencia { get; set; }
        [XmlElement("cMunEnv")]
        public int CodigoMunicipio { get; set; }
        [XmlElement("xMunEnv")]
        public string NomeMunicipio { get; set; }
        [XmlElement("UFEnv")]
        public string UFEnvio { get; set; }
        [XmlElement("modal")]
        public string Modal { get; set; }
        [XmlElement("tpServ")]
        public int TipoServico { get; set; }
        [XmlElement("cMunIni")]
        public int CodigoMunicipioInicio { get; set; }
        [XmlElement("xMunIni")]
        public string NomeMunicipioInicio { get; set; }
        [XmlElement("UFIni")]
        public string UFInicio { get; set; }
        [XmlElement("cMunFim")]
        public int CodigoMunicipioFim { get; set; }
        [XmlElement("xMunFim")]
        public string NomeMunicipioFim { get; set; }
        [XmlElement("UFFim")]
        public string UFFim { get; set; }
        [XmlElement("retira")]
        public double Retira { get; set; }
        [XmlElement("xDetRetira")]
        public string RetiraDetalhes { get; set; }
        [XmlElement("indIEToma")]
        public byte? IETomadorIndicador { get; set; }
        [XmlElement("dhCont")]
        public string DataEntregaContigencia { get; set; }
        [XmlElement("toma3")]
        public Tomador03 Tomador03 { get; set; }
        [XmlElement("toma4")]
        public Tomador04 Tomador04 { get; set; }

    }


    [Serializable]
    public class Tomador03
    {
        [XmlElement("toma")]
        public int Tomador { get; set; }

    }

    [Serializable]
    public class Tomador04
    {
        [XmlElement("toma")]
        public double Tomador { get; set; }
        [XmlElement("CNPJ")]
        public double CNPJ { get; set; }
        [XmlElement("CPF")]
        public double CPF { get; set; }
        [XmlElement("IE")]
        public string IE { get; set; }
        [XmlElement("xNome")]
        public string RazaoSocial { get; set; }
        [XmlElement("xFant")]
        public string NomeFantasia { get; set; }
        [XmlElement("fone")]
        public double Telefone { get; set; }

        [XmlElement("enderToma")]
        public Endereco Endereco { get; set; }
        [XmlElement("email")]
        public string Email { get; set; }
    }
}
