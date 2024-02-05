using System;
using System.Collections.Generic;

namespace Fretter.Domain.Entities
{
    public class FaturaStatus : EntityBase
    {
		#region "Construtores"
		public FaturaStatus(int id,string descricao, string icon, string iconColor)
		{
			this.Ativar();
			this.AtualizarId(id);
			this.AtualizarDescricao(descricao);
			this.AtualizarDataCriacao();
			this.AtualizarIcon(icon);
			this.AtualizarIconColor(iconColor);
		}
		#endregion

		#region "Propriedades"
        public string Descricao { get; protected set; }
		public string Icon { get; protected set; }
		public string IconColor { get; protected set; }
		#endregion

		#region "Referencias"
		public virtual List<Fatura> Fatura { get; set; }
		public virtual List<FaturaHistorico> FaturaHistorico { get; set; }
		#endregion

		#region "Métodos"
		public void AtualizarDescricao(string descricao) => this.Descricao = descricao;
		public void AtualizarIcon(string icon) => this.Icon = icon;
		public void AtualizarIconColor(string iconColor) => this.IconColor = iconColor;
		#endregion
	}
}      
