using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Service.Helper
{
    public class UsuarioHelper: IUsuarioHelper
    {
        public UsuarioHelper() { }

        public UsuarioIdentity UsuarioLogado
        {
            get
            {
                return null;
            }
        }
    }
}
