using Fretter.Domain.Helpers;
using System;

namespace Fretter.Domain.Entities
{
    public abstract class EntityBase : ValidatableObject
    {
        public EntityBase()
        {
            this.Ativar();
            this.DataCadastro = DateTime.Now;
        }

        public int Id { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public int? UsuarioCadastro { get; set; }
        public int? UsuarioAlteracao { get; set; }

        #region Metados
        public virtual void AtualizarId(int id)
        {
            if (id == 0)
                AddException(nameof(EntityBase), nameof(this.Id), "campoObrigatorio", "id");

            if (string.IsNullOrEmpty(id.ToString()))
                AddException(nameof(EntityBase), nameof(this.Id), "campoObrigatorio", "id");

            this.Id = id;
        }
        public virtual void AtualizarUsuarioCriacao(int usuarioId)
        {
            if (usuarioId <= 0)
                AddException(nameof(EntityBase), nameof(this.UsuarioCadastro), "campoObrigatorioId", "usuario");

            this.UsuarioCadastro = usuarioId;
        }
        public virtual void AtualizarUsuarioAlteracao(int? usuarioId)
        {
            if (usuarioId <= 0)
                AddException(nameof(EntityBase), nameof(this.UsuarioAlteracao), "campoObrigatorioId", "usuario");
            this.UsuarioAlteracao = usuarioId;
        }
        public virtual void AtualizarDataCriacao(DateTime? data = null) => this.DataCadastro = data.HasValue ? data.GetValueOrDefault() : DateTime.Now;
        public virtual void AtualizarDataAlteracao(DateTime? data = null) => this.DataAlteracao = data.HasValue ? data.GetValueOrDefault() : DateTime.Now;
        public virtual void Ativar() => this.Ativo = true;
        public virtual void Inativar() => this.Ativo = false;
        #endregion
    }
}
