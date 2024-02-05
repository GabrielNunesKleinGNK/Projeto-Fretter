using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fretter.Repository.Repositories
{
    public class EmpresaTransporteConfiguracaoRepository<TContext> : RepositoryBase<TContext, EmpresaTransporteConfiguracao>, IEmpresaTransporteConfiguracaoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EmpresaTransporteConfiguracao> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EmpresaTransporteConfiguracao>();
        public readonly IUsuarioHelper _user;

        public EmpresaTransporteConfiguracaoRepository(IUnitOfWork<TContext> context,
                                      IUsuarioHelper user) : base(context)
        {
            _user = user;
        }
        public override EmpresaTransporteConfiguracao Update(EmpresaTransporteConfiguracao entity)
        {
            entity.AtualizarEmpresaId(_user.UsuarioLogado.EmpresaId);
            DbSet.Update(entity);
            return entity;
        }
        public override EmpresaTransporteConfiguracao Save(EmpresaTransporteConfiguracao entity)
        {
            entity.AtualizarEmpresaId(_user.UsuarioLogado.EmpresaId);
            DbSet.Add(entity);
            return entity;
        }
        public override IEnumerable<EmpresaTransporteConfiguracao> GetAll(Expression<Func<EmpresaTransporteConfiguracao, bool>> predicate = null)
        {
            if (predicate == null)
                return DbSet
                    .Where(T => T.Ativo)
                    .Where(T => T.EmpresaId == _user.UsuarioLogado.EmpresaId)
                    .Include(x => x.EmpresaTransporteConfiguracaoItems.Where(x => x.Ativo))
                    .Include(t => t.EmpresaTransporteTipoItem)
                        .ThenInclude(p => p.Transportador)
                    .AsNoTracking()
                    .ToList();
            else
                return DbSet
                    .Where(predicate)
                    .Where(T => T.Ativo)
                    .Where(T => T.EmpresaId == _user.UsuarioLogado.EmpresaId)
                    .Include(x => x.EmpresaTransporteConfiguracaoItems.Where(x => x.Ativo))
                    .Include(t => t.EmpresaTransporteTipoItem)
                        .ThenInclude(p => p.Transportador)
                    .AsNoTracking()
                    .ToList();
        }

        public override EmpresaTransporteConfiguracao Get(int id)
        {
            return DbSet.Include(t => t.EmpresaTransporteTipoItem).AsNoTracking().FirstOrDefault(p => p.Id == id);
        }
    }
}
