using System;
using System.Collections.Generic;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fusion.EDI;

namespace Fretter.Api.Models
{
    public class MenuFretePeriodoViewModel : IViewModel<MenuFretePeriodo>
    {
		public int Id { get; set; }
        public string DsNome { get;  set; }
        public TimeSpan? HrInicio { get;  set; }
        public TimeSpan? HrTermino { get;  set; }

        public MenuFretePeriodo Model()
		{
			return new MenuFretePeriodo(Id, DsNome, HrInicio, HrTermino);
		}
    }
}      
