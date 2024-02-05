
using System.Collections.Generic;
using System.Linq;
using Fretter.Domain.Dto.Menu;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class SisMenuApplication<TContext> : ApplicationBase<TContext, Domain.Entities.SisMenu>, ISisMenuApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        private new readonly ISisMenuService<TContext> _service;

        public SisMenuApplication(IUnitOfWork<TContext> context, ISisMenuService<TContext> service) 
            : base(context, service)
        {
            _service = service;
        }


        public UserMenu GetUserMenu()
        {
            var menus = _service.GetSisMenu();
         
            var result = new UserMenu() {
                Paths = menus.Where(w => !string.IsNullOrEmpty(w.DsLink)).Select(s => s.DsLink).ToList(),
                MenuItens = GeItemMenu(menus, null)
            };

            return result;
        }

        private static List<ItemMenu> GeItemMenu(IEnumerable<SisMenu> menus, int? idPai)
        {
            var lista = new List<ItemMenu>();

            menus.Where(w => w.IdPai == idPai).OrderBy(o => o.NrOrdem).ToList().ForEach(menu =>
            {
                var viewModel = new ItemMenu
                {
                    Title = menu.DsMenu,
                    Desc = menu.DsMenu,
                    Root = true,
                    Icon = menu.Icone,
                    Page = menu.DsLink
                };

                if (menus.Any(a => a.IdPai == menu.IdMenu))
                    viewModel.Submenu = GeItemMenu(menus, menu.IdMenu);

                lista.Add(viewModel);
            });

            return lista;
        }
    }
}	
