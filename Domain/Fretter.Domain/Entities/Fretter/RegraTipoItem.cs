using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Entities.Fretter
{
    public class RegraTipoItem : EntityBase
    {
		public RegraTipoItem(int id, int regraTipoId, string nome, int dadoTipo, bool range)
		{
            Id = id;
            RegraTipoId = regraTipoId;
            Nome = nome;
            DadoTipo = dadoTipo;
            Range = range;
        }

        public int RegraTipoId {get; set;}
	    public string Nome {get; set;}
	    public int DadoTipo {get; set;}
	    public bool Range { get; set; }
    }
}
