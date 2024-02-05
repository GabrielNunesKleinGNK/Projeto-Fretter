using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;

namespace Fretter.Repository.Repositories
{
    public class AgendamentoExpedicaoRepository<TContext> : RepositoryBase<TContext, AgendamentoExpedicao>, IAgendamentoExpedicaoRepository<TContext> where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        private DbSet<ContratoTransportador> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<ContratoTransportador>();

        public AgendamentoExpedicaoRepository(IUnitOfWork<TContext> context, IUsuarioHelper user) : base(context)
        {
            _user = user;
        }

        public override IPagedList<AgendamentoExpedicao> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true, params Expression<Func<AgendamentoExpedicao, object>>[] includes)
        {
            filter.Filters.RemoveAll(x => x.Value == null || x.Value.ToString() == string.Empty);

            var configuracoes = new List<Expression<Func<AgendamentoExpedicao, object>>>()
            {
                x => x.Canal,
                x => x.Transportador,
                x => x.TransportadorCnpj
            };
            return base.GetPaginated(filter, start, limit, orderByDescending, configuracoes.ToArray());
        }

        public List<dynamic> VerificaCadastroCredenciais(int idEmpresa, string idTranportador)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@EmpresaId", idEmpresa)
                    {
                        SqlDbType = SqlDbType.Int,
                    },
                    new SqlParameter("@TranspordorId", idTranportador)
                    {
                        SqlDbType = SqlDbType.VarChar,
                    }
                };

               
                var retorno = ExecuteStoredProcedureWithParam<dynamic>("dbo.Pr_Adm_ValidaCredenciaisIntegracao", parameters, null, 1800);
                return retorno;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERRO Validando credenciais - Erro: {e.Message}");
                return null;
            }
        }
    }
}
