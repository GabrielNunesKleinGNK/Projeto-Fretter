using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Helpers.Proceda.Entidades
{
    public class CONEMB
    {
        public CONEMB()
        {
            Cabecalhos = new List<CONEMB_Cabecalho>();
        }

        public string Id_Remetente { get; set; }
        public string Id_Destinatario { get; set; }
        public DateTime Data { get; set; }
        public List<CONEMB_Cabecalho> Cabecalhos { get; set; }
    }
    public class CONEMB_Cabecalho
    {
        public CONEMB_Cabecalho()
        {
            Transportadora = new CONEMB_Transportadora();
        }
        public string Id_Documento { get; set; }
        public CONEMB_Transportadora Transportadora { get; set; }
    }
    public class CONEMB_Transportadora
    {
        public CONEMB_Transportadora()
        {
            Conhecimentos = new List<CONEMB_Conhecimento>();
        }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public List<CONEMB_Conhecimento> Conhecimentos { get; set; }
    }
    public class CONEMB_Conhecimento
    {
        public string Filial { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public DateTime? DataEmissao { get; set; }
        public string CondicaoFrete { get; set; }
        public string TipoDocumento { get; set; } //A = INDICA ACRÉSCIMO  D = INDICA DESCONTO OU N = INDICA NORMAL(NÃO HÁ DIFERENÇA)
        public decimal PesoTransportado { get; set; }
        public decimal ValorTotalFrete { get; set; }
        public decimal BaseCalculoICMS { get; set; }
        public decimal TaxaICMS { get; set; }
        public decimal ValorICMS { get; set; }
        public decimal ValorFretePorVolume { get; set; }
        public decimal FreteValor { get; set; }
        public decimal ValorSECCAT { get; set; }
        public decimal ValorITR { get; set; }
        public decimal ValorDespacho { get; set; }
        public decimal ValorPedagio { get; set; }
        public decimal ValorADEME { get; set; }
        public int SubstituicaoTributaria { get; set; }
        public string NaturezaOperacao { get; set; }
        public string CNPJFilialEmissora { get; set; }
        public string CNPJEmissorNota { get; set; }
        public string DANFE { get; internal set; }
        public string CFOP { get; set; }
        public string ProtocoloAutorizacao { get; set; }
        public string DigestValue { get; set; }
        public string UFEnvio { get; set; }
        public string UFInicio { get; set; }
        public string UFFim { get; set; }
        public string CNPJEmissorNF { get; set; }
        public string NumeroNF { get; set; }
        public string SerieNF { get; set; }
        public DateTime? DataEmissaoNF { get; set; }
        public int Linha { get; set; }

    }

    public class CONEMB50
    {
        public CONEMB50()
        {
            Cabecalhos = new List<CONEMB50_Cabecalho>();
        }

        public string Id_Remetente { get; set; }
        public string Id_Destinatario { get; set; }
        public DateTime Data { get; set; }
        public List<CONEMB50_Cabecalho> Cabecalhos { get; set; }
    }
    public class CONEMB50_Cabecalho
    {
        public CONEMB50_Cabecalho()
        {
            Transportadora = new CONEMB50_Transportadora();
        }
        public string Id_Documento { get; set; }
        public CONEMB50_Transportadora Transportadora { get; set; }
    }
    public class CONEMB50_Transportadora
    {
        public CONEMB50_Transportadora()
        {
            Conhecimentos = new List<CONEMB50_Conhecimento>();
        }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public List<CONEMB50_Conhecimento> Conhecimentos { get; set; }
    }
    public class CONEMB50_Conhecimento
    {
        public string Filial { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public DateTime DataEmissao { get; set; }
        public string CondicaoFrete { get; set; }
        public string CNPJFilialEmissora { get; set; }
        public string CNPJEmissorNota { get; set; }
        public string CNPJDestinoDevolucao { get; set; }
        public string CNPJDestinatario { get; set; }
        public string CNPJConsignatario { get; set; }
        public string Cod_NaturezaOperacao { get; set; }
        public string PlacaVeiculo { get; set; }
    }
}
