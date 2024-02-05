using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Entities
{
    public class SistemaMenuPermissao : EntityBase
    {
        public int SistemaMenuId { get; protected set; }
        public int UsuarioId { get; protected set; }

        public virtual SistemaMenu SistemaMenu { get; set; }
        public virtual Usuario Usuario { get; set; }

        public SistemaMenuPermissao(int sistemaMenuId, int usuarioId)
        {
            SistemaMenuId = sistemaMenuId;
            UsuarioId = usuarioId;
        }
    }
}
