using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.TabelaArquivo.Modelo
{
    public class ImportacaoRetorno
    {
        public List<TabelaPesoModel> ListFaixasPeso { get; set; }
        public List<RegiaoModel> ListReg { get; set; }
        public List<RegiaoCEPModel> ListCEP { get; set; }
        public List<TabelaPrecoModel> ListPrecos { get; set; }
    }
}
