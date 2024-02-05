using Fretter.Domain.Entities;
using Fretter.Domain.Exceptions;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Services;
using Fretter.Domain.Interfaces.Repositories;
using Fretter.Domain.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Fretter.Domain.Enum;

namespace Fretter.Domain.Services
{
    public class UsuarioService<TContext> : ServiceBase<TContext, Usuario>, IUsuarioService<TContext>
      where TContext : IUnitOfWork<TContext>
    {
        private new readonly IUsuarioRepository<TContext> _repository;
        private readonly ISistemaMenuPermissaoRepository<TContext> _sistemaMenuPermissaoRepository;
        private readonly ISistemaMenuRepository<TContext> _sistemaMenuRepository;
        private readonly IUsuarioHelper _user;

        public UsuarioService(IUsuarioRepository<TContext> repository,
                             IUnitOfWork<TContext> unitOfWork,
                             IUsuarioHelper user,
                             ISistemaMenuPermissaoRepository<TContext> sistemaMenuPermissaoRepository,
                             ISistemaMenuRepository<TContext> sistemaMenuRepository) : base(repository, unitOfWork, user)
        {
            this._repository = repository;
            this._user = user;
            this._sistemaMenuPermissaoRepository = sistemaMenuPermissaoRepository;
            this._sistemaMenuRepository = sistemaMenuRepository;
        }

        public override Usuario Save(Usuario usuario)
        {
            if (_repository.ExisteUsuario(usuario.Id, usuario.Login))
                throw new DomainException(nameof(UsuarioService<TContext>), nameof(Save), "registroJaCadastrado", nameof(usuario));

            return base.Save(usuario);
        }

        public override Usuario Update(Usuario usuario)
        {
            if (_repository.ExisteUsuario(usuario.Id, usuario.Login))
                throw new DomainException(nameof(UsuarioService<TContext>), nameof(Update), "registroJaCadastrado", nameof(usuario));

            if (usuario.SenhaAlterada)
                usuario.ForcarTrocaSenha = true;

            usuario.AtualizarUsuarioAlteracao(_user.UsuarioLogado.Id);
            usuario.AtualizarDataAlteracao();
            usuario.Validate();

            _repository.Update(usuario);

            return usuario;
        }

        public void AlterarSenha(int usuarioId, string senhaAtual, string senhaNova)
        {
            var usuario = Get(usuarioId);

            if (usuario == null || usuario.Senha != senhaAtual)
                throw new DomainException(nameof(IUsuarioService<TContext>), nameof(AlterarSenha), "senha inválida", nameof(senhaAtual));

            usuario.AlterarSenha(senhaNova);
            usuario.SenhaAlterada = true;
            usuario.ForcarTrocaSenha = false;
            usuario.Validate();

            _repository.Update(usuario);
        }

        public Usuario RecuperarSenha(string login)
        {
            var usuario = _repository.ObterUsuarioPorLogin(login);

            if (usuario == null)
                throw new DomainException(nameof(IUsuarioService<TContext>), nameof(RecuperarSenha), "senha inválida", nameof(login));

            //usuario.CriarHash();
            usuario.Validate();

            _repository.Update(usuario);

            return usuario;
        }

        public Usuario NovaSenha(string hash, string senha)
        {
            //var usuario = _repository.ObterUsuarioPorHash(hash);

            //if (usuario == null)
            //    throw new DomainException(nameof(IUsuarioService<TContext>), nameof(NovaSenha), "campoInvalido", nameof(usuario));

            //usuario.LimparHash();
            //usuario.AlterarSenha(senha, true);
            //usuario.Validate();

            return null;
        }

        private Usuario ValidaUsuarioSenha(string login, string senha)
        {
            var usuario = _repository.ObterUsuarioPorLogin(login);
            //var hash = SHA.Encrypt(SHA.Algorithm.SHA512, senha);

            if (usuario == null || usuario.Senha != senha)
                throw new DomainException(nameof(IUsuarioService<TContext>), nameof(AlterarSenha), "campoInvalido", nameof(senha));

            return usuario;
        }

        public Usuario ObterUsuarioPorLogin(string login)
        {
            return _repository.ObterUsuarioPorLogin(login);
        }


        public void RemoverPermissoes(int usuarioId)
        {
            var permissoes = this._sistemaMenuPermissaoRepository.GetAll().Where(p => p.UsuarioId == usuarioId).ToList();

            foreach (var item in permissoes)
            {
                _sistemaMenuPermissaoRepository.Delete(item.Id);
            }

        }
        public void CadastrarPermissoes(int usuarioId, int[] menus)
        {
            foreach (var item in menus)
            {
                var permissao = new SistemaMenuPermissao(item, usuarioId);
                permissao.AtualizarUsuarioCriacao(_user.UsuarioLogado.Id);
                _sistemaMenuPermissaoRepository.Save(permissao);
            }

        }


        public void CadastrarPermissoesUsuarioAdmin(int usuarioId)
        {
            var usuario = Get(usuarioId);

            if (usuario.UsuarioTipoId == EnumUsuarioTipo.Padrao.GetHashCode())
            {
                var menus = _sistemaMenuRepository.GetAll().Where(p => p.Ativo).Select(p => p.Id).ToArray();

                CadastrarPermissoes(usuario.Id, menus);
            }

            _unitOfWork.Commit();
        }

        public IEnumerable<SistemaMenu> GetMenus(int usuarioId)
        {
            return _sistemaMenuPermissaoRepository.GetMenus(usuarioId);
        }

        public IEnumerable<Fretter.Domain.Dto.Dashboard.Canal> GetCanaisUsuario()
        {
            return _repository.GetCanaisUsuario(_user.UsuarioLogado.Id, _user.UsuarioLogado.EmpresaId);
        }
    }
}
