using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;

namespace Fretter.Api.Models
{
    public class UsuarioSenhaViewModel
    {
        public string SenhaAtual { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }
    }
}
