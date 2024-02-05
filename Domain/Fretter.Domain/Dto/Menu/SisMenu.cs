using System;
namespace Fretter.Domain.Dto.Menu
{
    public class SisMenu
    {
        public SisMenu()
        {
        }

        public int IdMenu { get; set; }
        public string DsMenu { get; set; }
        public int? IdPai { get; set; }
        public string DsLink { get; set; }
        public int NrOrdem { get; set; }
        public string Icone {get;set;}

    }
}
