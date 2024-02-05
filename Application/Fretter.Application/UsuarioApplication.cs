using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Applications;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Services;
using System.Collections.Generic;

namespace Fretter.Application.ServiceApplication
{
    public class UsuarioApplication<TContext> : ApplicationBase<TContext, Usuario>, IUsuarioApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        private new readonly IUsuarioService<TContext> _service;

        public UsuarioApplication(IUnitOfWork<TContext> context, IUsuarioService<TContext> service)
            : base(context, service)
        {
            this._service = service;
        }
        public Usuario ObterUsuarioPorLogin(string login) => _service.ObterUsuarioPorLogin(login);
        public void AlterarSenha(int usuarioId, string senhaAtual, string senhaNova)
        {
            _service.AlterarSenha(usuarioId, senhaAtual, senhaNova);
            _unitOfWork.Commit();
        }

        public void RecuperarSenha(string login)
        {
            var usuario = _service.RecuperarSenha(login);
            _unitOfWork.Commit();
        }

        public void NovaSenha(string hash, string senha)
        {
            _service.NovaSenha(hash, senha);
            _unitOfWork.Commit();
        }

        public override Usuario Save(Usuario entity)
        {
            _service.Save(entity);
            _unitOfWork.Commit();

            return entity;
        }

        public void SavePermissoes(int usuarioId, int[] menus)
        {
            _service.RemoverPermissoes(usuarioId);
            _service.CadastrarPermissoes(usuarioId, menus);
            _unitOfWork.Commit();
        }

        public IEnumerable<SistemaMenu> GetMenus(int usuarioId) {

            return _service.GetMenus(usuarioId);
        }
        public IEnumerable<Fretter.Domain.Dto.Dashboard.Canal> GetCanaisUsuario()
        {
            return _service.GetCanaisUsuario();
        }

        //public void EnviarEmailUsuarioCriado(string emailUsuario, string loginUsuario, string nomeUsuario, string senha)
        //{
        //    var template = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates/UsuarioCadastrado.html"); ;
        //    var corpo = TextProvider.BuildParaEmail(template, new Dictionary<string, string>
        //    {
        //        {"SiteUrl",_urls.Site },
        //        {"Nome",nomeUsuario },
        //        {"Login",loginUsuario },
        //        {"Senha",senha }
        //    });
        //    var email = new Email(emailUsuario, "Usuário Criado", corpo);
        //    _envioEmail.EnviarEmail(email);
        //}
        //public void EnviarEmailRecuperarSenha(Usuario usuario)
        //{
        //    var template = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates/RecuperarSenha.html"); ;
        //    var corpo = TextProvider.BuildParaEmail(template, new Dictionary<string, string>
        //    {
        //        {"SiteUrl", $"{_urls.RecuperarSenha}?hash={usuario.HashAlterarSenha}" },
        //        {"Nome", usuario.Nome }
        //    });

        //    var email = new Email(usuario.Email, "Recuperar senha", corpo);
        //    _envioEmail.EnviarEmail(email);
        //}
    }
}
