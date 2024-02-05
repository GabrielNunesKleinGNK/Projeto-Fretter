using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using System;

namespace Fretter.Api.Models.Fusion
{
    public class CanalVendaViewModel : IViewModel<CanalVenda>
    {
        public int Id { get; set; }
        public bool Default { get;  set; }
        public int EmpresaId { get;  set; }
        public string CanalVendaUnico { get;  set; }
        public int DefaultUnico { get;  set; }
        public string CanalVendaNome { get;  set; }
        public string CanalVendaCodigo { get;  set; }
        public DateTime? UltAtualizacaoProduto { get;  set; }
        public byte? TipoIntegracao { get;  set; }
        public bool? EmbalagemUnicaMF { get;  set; }
        public byte? OrigemImportacao { get;  set; }

        public CanalVenda Model()
        {
            return new CanalVenda(Id, CanalVendaNome, Default, EmpresaId, CanalVendaUnico, DefaultUnico, UltAtualizacaoProduto,
                CanalVendaCodigo, TipoIntegracao, EmbalagemUnicaMF, OrigemImportacao);
        }
    }
}
