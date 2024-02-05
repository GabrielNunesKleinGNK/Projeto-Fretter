using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.Api.Models
{
    public class UsuarioPermissaoViewModel
    {
        public int[] Menus { get; set; }
    }

    public class UsuarioPermissaoEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public bool HasSubMenu { get; set; }
        public bool HasPermission { get; set; }
        public List<UsuarioPermissaoEditViewModel> SubMenus { get; set; }
    }
}
