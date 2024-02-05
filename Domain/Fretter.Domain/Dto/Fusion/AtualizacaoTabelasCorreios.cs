using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Fusion
{
    public class AtualizacaoTabelasCorreios
    {
        public List<ImportacaoCapitais> listCapitais { get; set; } = new List<ImportacaoCapitais>();
        public List<ImportacaoLocal> listLocais { get; set; } = new List<ImportacaoLocal>();
        public List<ImportacaoDivisa> listDivisas { get; set; } = new List<ImportacaoDivisa>();
        public List<ImportacaoEstadual> listEstaduais { get; set; } = new List<ImportacaoEstadual>();
        public List<ImportacaoInterior> listInteriores { get; set; } = new List<ImportacaoInterior>();
        public List<ImportacaoMatriz> listMatrizes { get; set; } = new List<ImportacaoMatriz>();
    }
    public class ImportacaoCapitais
    {
        public string cidadesdaAreadeinfluência { get; set; }
        public string uF { get; set; }
        public string cidade { get; set; }
        public string cEPInicial { get; set; }
        public string cEPFinal { get; set; }
        public string column6 { get; set; }
        public string situação { get; set; } = $"Incluída em {DateTime.Now.ToString("dd/MM/yyyy")}";
    }
    public class ImportacaoLocal
    {
        public string ufOrigem { get; set; }
        public string municípioOrigem { get; set; }
        public string codigoIBGEOrigem { get; set; }
        public string cepOrigemInicio { get; set; }
        public string cepOrigemFim { get; set; }
        public string ufDestino { get; set; }
        public string municípioDestino { get; set; }
        public string codigoIBGEDestino { get; set; }
        public string cepDestinoInicio { get; set; }
        public string cepDestinoFim { get; set; }
        public string nivel { get; set; }
        public string situacao { get; set; } = $"Incluido em {DateTime.Now.ToString("dd/MM/yyyy")}";
    }
    public class ImportacaoDivisa
    {
        public string ufOrigem { get; set; }
        public string municípioOrigem { get; set; }
        public string codigoIBGEOrigem { get; set; }
        public string cepOrigemInicio { get; set; }
        public string cepOrigemFim { get; set; }
        public string ufDestino { get; set; }
        public string municípioDestino { get; set; }
        public string codigoIBGEDestino { get; set; }
        public string cepDestinoInicio { get; set; }
        public string cepDestinoFim { get; set; }
        public string nivel { get; set; }
        public string situacao { get; set; } = $"Incluido em {DateTime.Now.ToString("dd/MM/yyyy")}";
    }
    public class ImportacaoEstadual
    {
        public string ufOrigem { get; set; }
        public string municípioOrigem { get; set; }
        public string codigoIBGEOrigem { get; set; }
        public string cepOrigemInicio { get; set; }
        public string cepOrigemFim { get; set; }
        public string ufDestino { get; set; }
        public string municípioDestino { get; set; }
        public string codigoIBGEDestino { get; set; }
        public string cepDestinoInicio { get; set; }
        public string cepDestinoFim { get; set; }
        public string nivel { get; set; }
        public string situacao { get; set; } = $"Incluido em {DateTime.Now.ToString("dd/MM/yyyy")}";
    }
    public class ImportacaoInterior
    {
        public string UF_Origem { get; set; }
        public string Município_Origem { get; set; }
        public string CodigoIBGE_Origem { get; set; }
        public string CepOrigemInicio { get; set; }
        public string CepOrigemFim { get; set; }
        public string UF_Destino { get; set; }
        public string Município_Destino { get; set; }
        public string CodigoIBGE_Destino { get; set; }
        public string CepDestinoInicio { get; set; }
        public string CepDestinoFim { get; set; }
        public string Nivel { get; set; }
        public string Situacao { get; set; } = $"Incluido em {DateTime.Now.ToString("dd/MM/yyyy")}";
    }
    public class ImportacaoMatriz
    {
        public int id { get; set; }
        public string uf { get; set; }
        public string ac { get; set; }
        public string al { get; set; }
        public string am { get; set; }
        public string ap { get; set; }
        public string ba { get; set; }
        public string ce { get; set; }
        public string df { get; set; }
        public string es { get; set; }
        public string go { get; set; }
        public string ma { get; set; }
        public string mg { get; set; }
        public string ms { get; set; }
        public string mt { get; set; }
        public string pa { get; set; }
        public string pb { get; set; }
        public string pe { get; set; }
        public string pi { get; set; }
        public string pr { get; set; }
        public string rj { get; set; }
        public string rn { get; set; }
        public string ro { get; set; }
        public string rr { get; set; }
        public string rs { get; set; }
        public string sc { get; set; }
        public string se { get; set; }
        public string sp { get; set; }
        public string to { get; set; }
    }
}
