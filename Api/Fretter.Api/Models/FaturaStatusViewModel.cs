using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class FaturaStatusViewModel : IViewModel<FaturaStatus>
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Icon { get; set; }
        public string IconColor { get; set; }


        public FaturaStatus Model()
        {
            return new FaturaStatus(Id, Descricao, Icon, IconColor);
        }
    }
}
