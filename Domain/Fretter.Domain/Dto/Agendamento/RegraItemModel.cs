using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Agendamento
{
    public class RegraItemModel
    {
		public RegraItemModel(int cd_Id, int id_Regra, int id_RegraGrupoItem, int id_RegraTipoItem, int id_RegraTipoOperador, string ds_Valor, string ds_ValorInicial, string ds_ValorFinal)
        {
            Cd_Id = cd_Id;
            Id_Regra = id_Regra;
            Id_RegraGrupoItem = id_RegraGrupoItem;
            Id_RegraTipoItem = id_RegraTipoItem;
            Id_RegraTipoOperador = id_RegraTipoOperador;
            Ds_Valor = ds_Valor;
            Ds_ValorInicial = ds_ValorInicial;
            Ds_ValorFinal = ds_ValorFinal;
        }

        public int Cd_Id { get; set; }
		public int Id_Regra { get; set; }
		public int Id_RegraGrupoItem { get; set; }
		public int Id_RegraTipoItem { get; set;  }
        public int Id_RegraTipoOperador { get; set; }
        public string Ds_Valor { get; set; }
		public string Ds_ValorInicial { get; set; }
		public string Ds_ValorFinal { get; set; }
    }
}
