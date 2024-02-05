using Fretter.Api.Helpers;
using Fretter.Domain.Entities.Fusion.SKU;
using System;

namespace Fretter.Api.Models.Fusion
{
    public class RegraEstoqueViewModel : IViewModel<RegraEstoque>
    {
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public int CanalIdOrigem { get; set; }
        public int CanalIdDestino { get; set; }
        public int? GrupoId { get; set; }
        public string Skus { get; set; }
        public DateTime DataCadastro { get; set; }
        public int UsuarioCadastro { get; set; }
        public DateTime DataAlteracao { get; set; }
        public int UsuarioAlteracao { get; set; }

        public EmpresaViewModel Empresa { get; set; }
        public CanalViewModel CanalOrigem { get; set; }
        public CanalViewModel CanalDestino { get; set; }
        public GrupoViewModel Grupo { get; set; }

        public RegraEstoque Model()
        {
            return new RegraEstoque(Id, EmpresaId, CanalIdOrigem, CanalIdDestino, GrupoId, Skus);
        }
    }
}
