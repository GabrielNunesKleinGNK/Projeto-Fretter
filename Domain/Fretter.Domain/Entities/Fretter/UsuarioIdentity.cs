using Fretter.Util;

namespace Fretter.Domain.Entities
{
    public class UsuarioIdentity
    {
        public int Id { get; }
        public string Email { get; }
        public int UsuarioRealId { get; }
        public int EmpresaId { get; }
        public string UsuarioCodigo { get; set; }
        public int Perfil { get; set; }
        public bool Administrador { get; set; }

        public UsuarioIdentity(int id, string email, int usuarioRealId, int empresaId, int perfil, bool administrador)
        {
            //Throw.IfLessThanOrEqZero(id);
            Throw.IfIsNullOrWhiteSpace(email);
            //Throw.IfLessThanOrEqZero(usuarioRealId);
            //Throw.IfLessThanOrEqZero(empresaId);
            this.Id = id;
            this.Email = email;
            this.UsuarioRealId = usuarioRealId;
            this.EmpresaId = empresaId;
            this.Perfil = perfil;
            this.Administrador = administrador;
        }
    }
}
