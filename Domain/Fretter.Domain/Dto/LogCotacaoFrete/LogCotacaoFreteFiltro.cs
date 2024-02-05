using System;
namespace Fretter.Domain.Dto.Dashboard
{
    public class LogCotacaoFreteFiltro
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }
        public string Application { get; set; }
        public string Instancia { get; set; }
        public int? EmpresaId { get; set; }
        public string Level { get; set; }
        public bool Exists_CotacaoCepInvalido
        {
            get { return true; }
        }
    }
}
