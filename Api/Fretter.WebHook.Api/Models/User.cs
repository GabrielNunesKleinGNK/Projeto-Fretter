using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.WebHook.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Nome { get; set; }
        public string ApiKey { get; set; }
        public int? UsuarioTipoId { get; set; }
        public int? ClienteId { get; set; }
    }
}
