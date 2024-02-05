using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Menu
{
    public class UserMenu
    {
        public List<string> Paths { get; set; }
        public List<ItemMenu> MenuItens { get; set; }
    }
}
