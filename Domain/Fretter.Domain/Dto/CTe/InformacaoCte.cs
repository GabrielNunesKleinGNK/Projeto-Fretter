
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Fretter.Domain.Dto.CTe
{
    [Serializable]
    public class InformacaoCTe
    {
        [XmlElement("infCarga")]
        public InformacaoCarga InformacaoCarga { get; set; }

        [XmlElement("infDoc")]
        public InformacaoDocumento InformacaoDocumento { get; set; }

        //[XmlElement("seg")]
        //public SeguroCarga SeguroCarga { get; set; }

        //[XmlElement("infModal")]
        //public InformacaoModal InformacaoModal { get; set; }
    }

    [Serializable]
    public class InformacaoCTeComplementar
    {
        [XmlElement("chCTe")]
        public string Chave { get; set; }

    }

    [Serializable]
    public class InformacaoCarga
    {
        [XmlElement("vCarga")]
        public double ValorTotal { get; set; }

        [XmlElement("proPred")]
        public string ProdutoPredominante { get; set; }

        [XmlElement("xOutCat")]
        public string OutraCaracteristica { get; set; }

        [XmlElement("infQ")]
        public List<InformacaoQuantidadeCarga> InformacaoQuantidadeCarga { get; set; }
    }

    [Serializable]
    public class InformacaoQuantidadeCarga
    {
        [XmlElement("cUnid")]
        public string CodigoUnidadeMedida { get; set; }

        [XmlElement("tpMed")]
        public string TipoMedia { get; set; }

        [XmlElement("qCarga")]
        public double Quantidade { get; set; }
    }

    [Serializable]

    public class InformacaoDocumento
    {

        [XmlElement("infNF")]
        public InformacaoNF InformacaoNF { get; set; }
        [XmlElement("infNFe")]
        public InformacaoNfe InformacaoNFe { get; set; }
    }

    [Serializable]

    public class InformacaoNF
    {
        [XmlElement("nRoma")]
        public string NumeroRomaneio { get; set; }
        [XmlElement("nPed")]
        public string NumeroPedido { get; set; }
        [XmlElement("mod")]
        public double ModeloNF { get; set; }
        [XmlElement("serie")]
        public string  Serie { get; set; }
        [XmlElement("nDoc")]
        public DateTime  Numero { get; set; }
        [XmlElement("dEmi")]
        public double DataEmissao { get; set; }
        [XmlElement("vBC")]
        public double ValorBaseCalculo { get; set; }
        [XmlElement("vICMS")]
        public double ValorTotalICMS { get; set; }
        [XmlElement("vBCST")]
        public double ValorBCST { get; set; }
        [XmlElement("vST")]
        public double ValorTotalICMSST { get; set; }
        [XmlElement("vProd")]
        public double ValorTotalProdutos { get; set; }
        [XmlElement("vNF")]
        public double ValorTotalNF { get; set; }
        [XmlElement("nCFOP")]
        public double CFOP { get; set; }
        [XmlElement("nPeso")]
        public double Peso { get; set; }
        [XmlElement("PIN")]
        public double PIN { get; set; }
        [XmlElement("dPrev")]
        public double DataPrevista { get; set; }


    }

    [Serializable]
    public class InformacaoNfe
    {
        [XmlElement("chave")]
        public string ChaveNFe { get; set; }
        [XmlElement("PIN")]
        public double PIN { get; set; }
        [XmlElement("dPrev")]
        public DateTime  DataPrevista { get; set; }

        //[XmlElement("infUnidTransp")]
        //public InformacaoUnidadeTransporte InformacaoUnidadeTransporte { get; set; }
    }

}
