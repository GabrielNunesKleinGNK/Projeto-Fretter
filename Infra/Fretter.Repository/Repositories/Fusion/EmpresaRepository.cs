using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Fretter.Repository.Repositories
{
    public class EmpresaRepository<TContext> : RepositoryBase<TContext, Empresa>, IEmpresaRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<Empresa> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<Empresa>();

        public EmpresaRepository(IUnitOfWork<TContext> context) : base(context) { }

        public bool ProcessaPermissaoEmpresa(string email, string cnpj, int tipoPermissao)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Email", email)
                {
                    SqlDbType = SqlDbType.VarChar,
                },
                new SqlParameter("@Tipo_permissao", tipoPermissao)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@Cd_Cnpj", cnpj)
                {
                    SqlDbType = SqlDbType.VarChar,
                }
            };

            var retornoPermissao = ExecuteStoredProcedureWithParamScalar<int>("Fretter.ProcessaEmpresaPermissao", parameters);

            if (retornoPermissao > 0) return true;
            else return false;
        }

        public Empresa ObterEmpresaPeloCanalPorCnpj(string cnpj)
        {
            return _dbSet
               .Include(e => e.Canal)
               .Where(t => t.Canal.Cnpj == cnpj && t.Canal.Ativo == true && t.Ativo == true)
               .OrderByDescending(t => t.Id)
               .FirstOrDefault();
        }

    }
}
