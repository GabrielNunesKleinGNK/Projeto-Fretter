using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Linq;
using Fretter.Domain.Interfaces.Repository.Fusion;
using Fretter.Domain.Entities.Fusion;
using Fretter.Domain.Entities.Fusion.SKU;
using System.Linq.Expressions;
using System;
using Fretter.Domain.Interfaces;
using System.Data;
using Fretter.Domain.Dto.RegraEstoque;

namespace Fretter.Repository.Repositories.Fusion
{
    public class RegraEstoqueRepository<TContext> : RepositoryBase<TContext, RegraEstoque>, IRegraEstoqueRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<RegraEstoque> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<RegraEstoque>();
        public readonly IUsuarioHelper _user;

        public RegraEstoqueRepository(IUnitOfWork<TContext> context,
                                      IUsuarioHelper user) : base(context) {
            _user = user;
        }

        public override IEnumerable<RegraEstoque> GetAll(Expression<Func<RegraEstoque, bool>> predicate = null)
        {
            var list = base.GetQueryable()
                        .Where(x => x.Ativo && x.EmpresaId == _user.UsuarioLogado.EmpresaId)
                        .Include(x => x.Empresa)
                        .Include(x => x.Grupo)
                        .Include(x => x.CanalDestino)
                        .Include(x => x.CanalOrigem)
                        .ToList();
            return list;
        }

        public RegraEstoqueDTO SaveWithProcedure(RegraEstoqueDTO regraEstoque)
        {
            var listaRegraEstoque = CollectionHelper.ConvertTo<RegraEstoqueDTO>(new List<RegraEstoqueDTO> { regraEstoque });

            Microsoft.Data.SqlClient.SqlParameter[] parameters =
               {
                    new Microsoft.Data.SqlClient.SqlParameter("@regraEstoque", listaRegraEstoque)
                    {
                        TypeName = "Tp_SKU_RegraEstoque",
                        SqlDbType = SqlDbType.Structured,                        
                    }
                };

            var retorno = ExecuteStoredProcedureWithParam<RegraEstoqueDTO>("[dbo].[Pr_SKU_RegraEstoque_Insert]", parameters);
            return retorno?.FirstOrDefault();
        }

        public RegraEstoque GetByCanalDestino(int idCanalDestino)
        {
            var regraEstoque = base.GetQueryable()
                        .Where(x => x.Ativo && x.EmpresaId == _user.UsuarioLogado.EmpresaId && x.CanalIdDestino == idCanalDestino)
                        .Include(x => x.Empresa)
                        .Include(x => x.Grupo)
                        .Include(x => x.CanalDestino)
                        .Include(x => x.CanalOrigem)
                        .FirstOrDefault();
            return regraEstoque;
        }
    }
}	
