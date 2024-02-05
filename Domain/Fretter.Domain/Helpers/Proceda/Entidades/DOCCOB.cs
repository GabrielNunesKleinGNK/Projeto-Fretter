using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Helpers.Proceda.Entidades
{
    public class DOCCOB
    {
        public DOCCOB()
        {
            Cabecalhos = new List<DOCCOB_Cabecalho>();
        }

        public string Id_Remetente { get; set; }
        public string Id_Destinatario { get; set; }
        public DateTime Data { get; set; }
        public List<DOCCOB_Cabecalho> Cabecalhos { get; set; }
    }

    public class DOCCOB_Cabecalho
    {
        public DOCCOB_Cabecalho()
        {
            Transportadora = new DOCCOB_Transportadora();
        }
        public string Id_Documento { get; set; }
        public DOCCOB_Transportadora Transportadora { get; set; }
    }

    public class DOCCOB_Transportadora
    {
        public DOCCOB_Transportadora()
        {
            Cobrancas = new List<DOCCOB_Cobranca>();
        }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public List<DOCCOB_Cobranca> Cobrancas { get; set; }
    }

    public class DOCCOB_Cobranca
    {
        public DOCCOB_Cobranca()
        {
            Conhecimentos = new List<DOCCOB_Conhecimento>();
            NotasFiscais = new List<DOCCOB_NotaFiscal>();
        }
        public string FilialEmissora { get; set; }
        public int Tipo { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorTotal { get; set; }
        public string TipoCobranca { get; set; }
        public string CFOP { get; set; }
        public string CodigoAcessoNFe { get; set; }
        public string ChaveAcessoNFe { get; set; }
        public string ProtocoloNFe { get; set; }

        public List<DOCCOB_Conhecimento> Conhecimentos { get; set; }
        public List<DOCCOB_NotaFiscal> NotasFiscais { get; set; }
    }

    public class DOCCOB_Conhecimento
    {
        public int Id { get; set; }
        public string Filial { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public decimal ValorFrete { get; set; }
        public DateTime DataEmissao { get; set; }
        public string DocumentoRemetente { get; set; }
        public string DocumentoDestinatario { get; set; }
        public string DocumentoEmissor { get; set; }
        public string UfEmbarcadora { get; set; }
        public string UfDestinataria { get; set; }
        public string UfEmissora { get; set; }
        public string CodigoIVA { get; set; }
        public string Devolucao { get; set; }
    }

    public class DOCCOB_NotaFiscal
    {
        public int Id { get; set; }
        public string NotaSerie { get; set; }
        public string NotaNumero { get; set; }
        public DateTime DataEmissao { get; set; }
        public decimal Peso { get; set; }
        public decimal Valor { get; set; }
        public string DocumentoEmissor { get; set; }
        public string NumeroRomaneio { get; set; }
        public string NumeroSAP1 { get; set; }
        public string NumeroSAP2 { get; set; }
        public string NumeroSAP3 { get; set; }
        public bool Devolucao { get; set; }
    }

    public enum EnumDOCCOBTipo
    {
        DOCCOB50 = 1,
        DOCCOB30 = 2
    }
}
