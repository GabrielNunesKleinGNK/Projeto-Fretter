using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Dto.Menu
{
    public class ItemMenu
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public bool Root { get; set; }
        public string Icon { get; set; }
        public string Page { get; set; }

        public List<ItemMenu> Submenu { get; set; }
    }
}
