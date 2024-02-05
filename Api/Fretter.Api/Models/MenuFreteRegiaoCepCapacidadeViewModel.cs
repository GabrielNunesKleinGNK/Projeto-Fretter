using System;
using System.Collections.Generic;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fusion.EDI;

namespace Fretter.Api.Models
{
    public class MenuFreteRegiaoCepCapacidadeViewModel : IViewModel<MenuFreteRegiaoCepCapacidade>
    {
		public int Id { get; set; }
        public int IdRegiaoCEP { get;  set; }
        public int IdPeriodo { get;  set; }
        public byte NrDia { get;  set; }
        public int VlQuantidade { get;  set; }
        public int VlQuantidadeDisponivel { get;  set; }
        public decimal NrValor { get;  set; }

        public MenuFreteRegiaoCepCapacidade Model()
		{
			return new MenuFreteRegiaoCepCapacidade(Id, IdRegiaoCEP, IdPeriodo, NrDia, VlQuantidade, VlQuantidadeDisponivel, NrValor);
		}
    }
}      
