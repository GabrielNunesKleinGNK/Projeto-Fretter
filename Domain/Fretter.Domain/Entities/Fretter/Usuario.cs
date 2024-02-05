using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using Fretter.Domain.Enum;

namespace Fretter.Domain.Entities
{
    public class Usuario : EntityBase
    {
        public Usuario()
        {

        }

        public Usuario(int id, string nome, string login, string email, int usuarioTipoId, int empresaId = 0, int? idUsuarioReal=null, bool isAdmin = true)
        {
            Id = id;
            Nome = nome;
            Login = login;
            Email = email;
            UsuarioTipoId = usuarioTipoId;
            EmpresaId = empresaId;
            if (Avatar == null)
                Avatar = "https://egergizerstorage.blob.core.windows.net/usuario-perfil/user.png";
            IdUsuarioReal = idUsuarioReal;
            IsAdmin = isAdmin;
        }

        public Usuario(int id, string nome, string login, string email, string avatar, string senha, bool senhaAlterada, bool bloqueado, string telefone, string cargo, int usuarioTipoId, int empresaId = 0, bool isAdmin = true)
        {
            Id = id;
            Nome = nome;
            Login = login;
            Email = email;
            Avatar = avatar;
            Senha = senha;
            SenhaAlterada = senhaAlterada;
            Bloqueado = bloqueado;
            Telefone = telefone;
            Cargo = cargo;
            UsuarioTipoId = usuarioTipoId;
            EmpresaId = empresaId;
            if (Avatar == null)
                Avatar = "https://egergizerstorage.blob.core.windows.net/usuario-perfil/user.png";
            IsAdmin = isAdmin;
        }

        public string Login { get; private set; }
        public string Senha { get; private set; }
        public string Email { get; private set; }
        public string Nome { get; private set; }
        public string Avatar { get; private set; }
        public bool ForcarTrocaSenha { get; set; }
        public bool SenhaAlterada { get; set; }
        public int? UsuarioTipoId { get; set; }
        public int? EmpresaId { get; set; }
        public string Telefone { get; set; }
        public string Cargo { get; set; }
        public bool Bloqueado { get; set; }
        public void AlterarAvarar(string avatar) => this.Avatar = avatar;
        public string ApiKey { get; private set; }
        public virtual EnumUsuarioTipo UsuarioTipo { get; set; }

        public int? IdUsuarioReal { get; set; }

        public bool IsAdmin { get;set; }

        [NotMapped]
        public bool OrigemFusion { get; private set; }

        public void AlterarSenha(string senhaNova)
        {
            this.Senha = senhaNova;
        }

        public void AlterarOrigemFusion(bool origemFusion) => this.OrigemFusion = origemFusion;
    }
}
