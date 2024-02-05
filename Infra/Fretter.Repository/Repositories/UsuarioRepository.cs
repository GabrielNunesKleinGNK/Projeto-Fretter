using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repositories;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Fretter.Repository.Repositories
{
    public class UsuarioRepository<TContext> : RepositoryBase<TContext, Usuario>, IUsuarioRepository<TContext>
      where TContext : IUnitOfWork<TContext>
    {
        public UsuarioRepository(IUnitOfWork<TContext> unitOfWork) : base(unitOfWork) { }

        public override Usuario Get(int id)
        {
            //usuario fake para bypass da tela
            return new Usuario(1, "Admin", "admin", "admin", "", "admin", false, false, "999999999", "admin", 1, 17870);
            //return DbSet.FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<Usuario> GetAll(Expression<Func<Usuario, bool>> predicate = null)
        {
            return DbSet
                .Include(e => e.UsuarioTipo)
                .Where(T => T.Ativo)
                .ToList();
        }

        public override Usuario Update(Usuario entity)
        {
            var result = DbSet.Attach(entity);
            result.State = EntityState.Modified;

            result.Property(x => x.DataCadastro).IsModified = false;
            result.Property(x => x.UsuarioCadastro).IsModified = false;
            result.Property(x => x.Senha).IsModified = entity.SenhaAlterada;
            return entity;
        }

        public bool ExisteUsuario(long id, string login) => DbSet.Any(x => x.Id != id && x.Login.ToLower() == login.ToLower());

        public Usuario ObterUsuarioPorLogin(string login)
        {
            //usuario fake para bypass da tela
            return new Usuario(1, "Admin", "admin", "admin", "", "admin", false, false, "999999999", "admin", 1, 63);
            //DbSet.FirstOrDefault(x => x.Login ==login && x.Ativo);
        }


        public Usuario ObterUsuarioPorApiKey(string apiKey) =>
             DbSet.FirstOrDefault(x => x.ApiKey == apiKey && x.Ativo);
        public IEnumerable<Fretter.Domain.Dto.Dashboard.Canal> GetCanaisUsuario(int userId, int empresaId)
        {
            SqlParameter[] parameters = { 
                new SqlParameter("@userId", userId){ SqlDbType = SqlDbType.Int },
                new SqlParameter("@empresaId", empresaId){ SqlDbType = SqlDbType.Int },
            };

            var retorno = ExecuteStoredProcedureWithParam<Fretter.Domain.Dto.Dashboard.Canal>("[Fretter].[GetCanaisUsuario]", parameters);
            return retorno;
        }
    }
}
