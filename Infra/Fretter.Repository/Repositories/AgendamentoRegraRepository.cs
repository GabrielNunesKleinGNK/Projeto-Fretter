using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.VariantTypes;
using Fretter.Domain.Config;
using Fretter.Domain.Dto.Webhook.Tracking.Response;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Enum;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Fretter.Repository.Repositories
{
    public class AgendamentoRegraRepository<TContext> : RepositoryBase<TContext, AgendamentoRegra>, IAgendamentoRegraRepository<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;

        public AgendamentoRegraRepository(IUnitOfWork<TContext> context, IUsuarioHelper user) : base(context)
        {
            _user = user;
        }

        public override IPagedList<AgendamentoRegra> GetPaginated
        (
            QueryFilter filter,
            int start = 0,
            int limit = 10,
            bool orderByDescending = true,
            params Expression<Func<AgendamentoRegra, object>>[] includes
        )
        {
            filter.Filters.RemoveAll(x => x.Value == null || x.Value.ToString() == string.Empty);

            var result = DbSet
                .Include(regra => regra.Canal)
                .Include(regra => regra.Transportador)
                .Include(regra => regra.TransportadorCnpj)
                .Include(regra => regra.RegraItens.Where(x => x.Ativo))
                .ThenInclude(grupo => grupo.RegraGrupoItem)
                .AsQueryable()
                .Where(x => x.Ativo);

            return base.GetPagedList(result, filter, start, limit, orderByDescending);
        }

        public int GravaRegra(AgendamentoRegra agendamentoRegra, RegraGrupoItem regraGrupoItem, DataTable regraItens)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Id_Regra", agendamentoRegra.Id)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@Id_EmpresaTransportador", agendamentoRegra.EmpresaTransportadorId)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@Id_RegraStatus", agendamentoRegra.RegraStatusId)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@Id_RegraTipo", agendamentoRegra.RegraTipoId)
                {
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true
                },
                new SqlParameter("@Id_Empresa",  agendamentoRegra.EmpresaId)
                {
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true
                },
                new SqlParameter("@Id_Canal",  agendamentoRegra.CanalId)
                {
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true
                },
                new SqlParameter("@Id_Transportador",  agendamentoRegra.TransportadorId)
                {
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true
                },
                new SqlParameter("@Id_TransportadorCnpj",  agendamentoRegra.TransportadorCnpjId)
                {
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true
                },
                new SqlParameter("@Ds_Nome",  agendamentoRegra.Nome)
                {
                    SqlDbType = SqlDbType.VarChar,
                    IsNullable = true
                },
                new SqlParameter("@Dt_Inicio", agendamentoRegra.DataInicio)
                {
                    SqlDbType = SqlDbType.DateTime,
                    IsNullable = true
                },
                new SqlParameter("@Dt_Termino", agendamentoRegra.DataTermino)
                {
                    SqlDbType = SqlDbType.DateTime,
                    IsNullable = true
                },
                new SqlParameter("@Us_Inclusao", agendamentoRegra.UsuarioCadastro)
                {
                    SqlDbType = SqlDbType.Int
                },
                new SqlParameter("@Us_Alteracao", agendamentoRegra.UsuarioAlteracao)
                {
                    SqlDbType = SqlDbType.Int
                },
                new SqlParameter("@Id_RegraGrupoItem", regraGrupoItem.Id)
                {
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true
                },
                new SqlParameter("@Ds_NomeGrupo", regraGrupoItem.Nome)
                {
                    SqlDbType = SqlDbType.VarChar,
                    IsNullable = true
                },
                new SqlParameter("@Ds_Tipo", regraGrupoItem.Tipo)
                {
                    SqlDbType = SqlDbType.VarChar
                },
                new SqlParameter("@RegrasItem", regraItens)
                {
                    SqlDbType = SqlDbType.Structured
                },
            };

            return ExecuteStoredProcedureWithParam<int>("Pr_Age_GravaRegra", parameters).FirstOrDefault();
        }

        public int InativarRegra(int id, int usuarioAlteracao)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Id_Regra", id)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@Us_Alteracao", usuarioAlteracao)
                {
                    SqlDbType = SqlDbType.Int,
                }
            };

            return ExecuteStoredProcedureWithParam<int>("Pr_Age_InativaRegra", parameters).FirstOrDefault();
        }
    }
}
