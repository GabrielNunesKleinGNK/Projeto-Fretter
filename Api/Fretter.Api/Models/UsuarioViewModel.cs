using System;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class UsuarioViewModel : IViewModel<Usuario>
    {
		public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Avatar { get; set; }
        public int UsuarioTipoId { get; set; }

        public bool? Ativo { get; set; }
        public int? UsuarioCadastro { get; set; }
        public int? UsuarioAlteracao { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public bool SenhaAlterada { get; private set; }
        public bool Bloqueado { get; private set; }
        public string Telefone { get; private set; }
        public string Cargo { get; private set; }
        public UsuarioTipo UsuarioTipo { get; set; }
        public Usuario Model()
		{
			return new Usuario(Id,Nome, Login, Email,Avatar, Senha, SenhaAlterada, Bloqueado, Telefone, Cargo, UsuarioTipoId );
		}
    }
}      
