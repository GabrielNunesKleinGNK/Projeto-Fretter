using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fretter.Domain.Services
{
    public class ConfiguracaoCteTransportadorService<TContext> : ServiceBase<TContext, ConfiguracaoCteTransportador>, IConfiguracaoCteTransportadorService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IConfiguracaoCteTransportadorRepository<TContext> _Repository;

        public ConfiguracaoCteTransportadorService(IConfiguracaoCteTransportadorRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork, IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }

        public override IEnumerable<ConfiguracaoCteTransportador> GetAll(Expression<Func<ConfiguracaoCteTransportador, bool>> predicate = null)
        {
            return base.GetAll(predicate).Where(x => x.EmpresaId == _user.UsuarioLogado.EmpresaId);
        }

        public override ConfiguracaoCteTransportador Save(ConfiguracaoCteTransportador entidade)
        {
            entidade.AtualizarEmpresaId(_user.UsuarioLogado.EmpresaId);
            return base.Save(entidade);
        }

        public override ConfiguracaoCteTransportador Update(ConfiguracaoCteTransportador entidade)
        {
            entidade.AtualizarEmpresaId(_user.UsuarioLogado.EmpresaId);
            return base.Update(entidade);
        }
    }
}	
