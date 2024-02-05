using System;
using System.Collections.Generic;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class FaturaItemViewModel : IViewModel<FaturaItem>
    {
		public int Id { get; set; }
        public decimal Valor { get; protected set; }
        public string Descricao { get; protected set; }
        public int FaturaId { get; protected set; }
        public bool? Ativo { get; set; }
        public int? UsuarioCadastro { get; set; }
        public int? UsuarioAlteracao { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public FaturaItem Model()
		{
			return new FaturaItem(Id, Valor, Descricao, FaturaId);
		}
    }
}      
