using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;
using Microsoft.Data.SqlClient;
using System.Data;
using Fretter.Domain.Dto.RecotacaoFrete;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using Fretter.Domain.Enum;

namespace Fretter.Repository.Repositories
{
    public class RecotacaoFreteRepository<TContext> : RepositoryBase<TContext, RecotacaoFrete>, IRecotacaoFreteRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<RecotacaoFrete> _dbSet => ((Contexts.CommandContext)UnitOfWork).Set<RecotacaoFrete>();

        public RecotacaoFreteRepository(IUnitOfWork<TContext> context) : base(context) { }


        public void AtualizarRecotacaoFrete(int idRecotacaoFrete, EnumRecotacaoFreteStatus recotacaoFreteStatus, int qtAdvertencia = 0,int qtErros =0, string objRetorno = "")
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@IdRecotacaoFrete", idRecotacaoFrete)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@IdRecotacaoFreteStatus", (int)recotacaoFreteStatus)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@QtAdvertencia", qtAdvertencia)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@QtErros", qtErros)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@ObJsonRetorno", objRetorno)
                {
                    SqlDbType = SqlDbType.VarChar,
                },
                new SqlParameter("@DtAtualizacao", DateTime.Now)
                {
                    SqlDbType = SqlDbType.DateTime,
                },
            };

            ExecuteStoredProcedureWithParamNonQuery<int>("Pr_MF_AtualizarRecotacaoFrete", parameters);

        }

        public void InserirRecotacaoFreteItem(DataTable itens)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@itens", itens)
                {
                    TypeName = "Tp_MF_RecotacaoFreteItem",
                    SqlDbType = SqlDbType.Structured,
                }
            };

            ExecuteStoredProcedureWithParamNonQuery<int>("Pr_MF_RecotacaoFreteItemInserir", parameters);

        }

        public List<MenuFreteCotacao> BuscarDadosPedido(DataTable itens)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@itens", itens)
                {
                    TypeName = "Tp_MF_RecotacaoFreteItem",
                    SqlDbType = SqlDbType.Structured,
                }
            };

            return ExecuteStoredProcedureWithParam<MenuFreteCotacao>("Pr_MF_RecotacaoFreteBuscarPedido", parameters);
        }
    }
}
