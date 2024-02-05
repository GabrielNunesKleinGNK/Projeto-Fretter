using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Fretter.Api.Models;
using Fretter.Api.Helpers;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Applications;
using Fretter.Repository.Contexts;

namespace Fretter.Api.Controllers
{
    public class UsuarioController : ControllerBase<FretterContext, Usuario, UsuarioViewModel>
    {
        private new readonly IUsuarioApplication<FretterContext> _application;
        private readonly ISistemaMenuApplication<FretterContext> _sistemaMenuApplication;
        private readonly IUsuarioHelper _user;

        public UsuarioController(IUsuarioApplication<FretterContext> application,
                                ISistemaMenuApplication<FretterContext> sistemaMenuApplication,
                                IUsuarioHelper user) : base(application)
        {
            _application = application;
            _sistemaMenuApplication = sistemaMenuApplication;
            _user = user;
            
        }

        [HttpGet]
        [Route("/api/usuario/perfil")]
        public Task<IActionResult> GetUsuarioPerfil()
        {
            //var result = _mapper.Map<UsuarioViewModel>(_application.Get(_user.UsuarioLogado.Id));
            //return Task.FromResult<IActionResult>(Ok(result));
            return null;
        }

        [HttpPost]
        [Route("/api/usuario/alterarSenha")]
        public Task<IActionResult> AlterarSenha(UsuarioSenhaViewModel usuarioSenha)
        {
            _application.AlterarSenha(_user.UsuarioLogado.Id, usuarioSenha.SenhaAtual, usuarioSenha.Senha);
            return Task.FromResult<IActionResult>(Ok(new { mensagem = "Senha Alterada com sucesso." }));
        }

        [HttpPost("{usuarioId}/Permissoes")]
        public IActionResult CadastrarPermissoes(int usuarioId, [FromBody] UsuarioPermissaoViewModel model)
        {
            _application.SavePermissoes(usuarioId, model.Menus);

            return Ok();
        }

        [HttpGet("{usuarioId}/Menus")]
        public IActionResult GetMenusUsuario(int usuarioId)
        {
            var menusPermissao = _application.GetMenus(usuarioId);

            var menus = _sistemaMenuApplication.GetAll(p => p.Ativo).ToList();

            var lista = new List<UsuarioPermissaoEditViewModel>();

            var menusPai = menus.Where(p => p.ParentId == null).ToList();

            foreach (var pai in menusPai)
            {
                var viewModel = new UsuarioPermissaoEditViewModel();

                viewModel.Id = pai.Id;
                viewModel.Name = pai.Descricao;
                viewModel.Name = pai.Descricao;
                viewModel.Icon = pai.Icone;

                var permissao = menusPermissao.Where(p => p.Id == pai.Id).FirstOrDefault();

                if (permissao != null)
                {
                    viewModel.HasPermission = true;
                }

                var menusFilho = menus.Where(p => p.ParentId == pai.Id).ToList();

                if (menusFilho.Count > 0)
                {
                    viewModel.HasSubMenu = true;
                    viewModel.SubMenus = new List<UsuarioPermissaoEditViewModel>();

                    foreach (var filho in menusFilho)
                    {
                        var subMenu = new UsuarioPermissaoEditViewModel();
                        subMenu.Id = filho.Id;
                        subMenu.Name = filho.Descricao;
                        subMenu.Name = filho.Descricao;
                        subMenu.Icon = filho.Icone;

                        var permissaoFilho = menusPermissao.Where(p => p.Id == filho.Id).FirstOrDefault();

                        if (permissaoFilho != null)
                        {
                            subMenu.HasPermission = true;
                        }

                        viewModel.SubMenus.Add(subMenu);
                    }
                }

                lista.Add(viewModel);
            }

            return Ok(lista);
        }
        
        [HttpGet("/api/usuario/Canais")]
        public IActionResult GetCanaisUsuario()
        {
            return Ok(_application.GetCanaisUsuario());
        }
    }
}
