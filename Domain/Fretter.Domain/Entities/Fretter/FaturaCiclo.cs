using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class FaturaCiclo : EntityBase
    {
		#region "Construtores"
		public FaturaCiclo(int id, int faturaCicloTipoId, short diaFechamento, short diaVencimento)
		{
			this.Ativar();
			this.Id = id;
			this.FaturaCicloTipoId = faturaCicloTipoId;
			this.DiaFechamento = diaFechamento;
			this.DiaVencimento = diaVencimento;

		}
		#endregion

		#region "Propriedades"
		public int FaturaCicloTipoId { get; protected set; }
		public short DiaFechamento { get; protected set; }
		public short DiaVencimento { get; protected set; }
		#endregion

		#region "Referencias"
		public virtual List<ContratoTransportador> ContratoTransportadores { get; set; }
		#endregion

		#region "Métodos"
		public void AtualizarDiaFechamento(short dia) => this.DiaFechamento = dia;
		#endregion
    }
}      
