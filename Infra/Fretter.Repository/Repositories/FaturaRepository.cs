using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Fretter.Domain.Enum;
using Fretter.Domain.Dto.Fatura;
using System.Threading.Tasks;
using Fretter.Domain.Dto.Fretter.Conciliacao;
using System.Data;
using Microsoft.Data.SqlClient;
using Fretter.Domain.Entities.Fretter;

namespace Fretter.Repository.Repositories
{
    public class FaturaRepository<TContext> : RepositoryBase<TContext, Fatura>, IFaturaRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<Fatura> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<Fatura>();
        public FaturaRepository(IUnitOfWork<TContext> context) : base(context)
        {

        }
        public async Task<List<Fatura>> GetFaturasDaEmpresa(int empresaId, FaturaFiltro filtro)
        {

            Expression<Func<Fatura, bool>> filtros = x =>
                    (filtro.TransportadorId == default ? x.EmpresaId == empresaId : x.TransportadorId == filtro.TransportadorId)
                    && (filtro.FaturaStatusId == default ? x.EmpresaId == empresaId : x.FaturaStatusId == filtro.FaturaStatusId)
                    && ((filtro.DataInicio == default && filtro.DataTermino == default)
                                        ? x.EmpresaId == empresaId
                                        : x.DataCadastro >= filtro.DataInicio && x.DataCadastro <= filtro.DataTermino);

            return _dbSet.
                 Include(e => e.FaturaStatus)
                .Include(e => e.FaturaPeriodo)
                .Include(e => e.Transportador)
                .Include(e => e.ArquivoCobrancas.Where(x => x.Ativo))
                .Where(x => x.EmpresaId == empresaId)
                .Where(filtros)
                .ToListAsync().Result;
        }

        public async Task<List<Fatura>> GetFaturasPorId(List<int> faturas)
        {
            Expression<Func<Fatura, bool>> filtros = x => faturas.Contains(x.Id);

            return _dbSet.
                 Include(e => e.FaturaStatus)
                .Include(e => e.FaturaPeriodo)
                .Include(e => e.Transportador)
                .Include(e => e.ArquivoCobrancas.Where(x => x.Ativo))
                .Where(filtros)
                .ToListAsync().Result;
        }
        public List<DemonstrativoRetorno> GetDemonstrativoPorFatura(DataTable listFaturaId)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@Itens", listFaturaId)
                    {
                        TypeName = "Tp_Int",
                        SqlDbType = SqlDbType.Structured,
                    }
                };

                var retorno = ExecuteStoredProcedureWithParam<DemonstrativoRetorno>("[Fretter].[GetDemonstrativoPorFatura]", parameters);
                return retorno;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERRO Buscando EntregaPedido - Erro: {e.Message}");
            }
            return null;
        }

        public List<Item> ProcessarFaturaManual(DataTable entregaConciliacao, int userId)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@entregaConciliacao", entregaConciliacao)
                    {
                        TypeName = "Fretter.Tp_EntregaConciliacaoFatura",
                        SqlDbType = SqlDbType.Structured,
                    },
                    new SqlParameter("@userId", userId)
                    {
                        SqlDbType = SqlDbType.Int,
                    },


                };

                var retorno = ExecuteStoredProcedureWithParam<Item>("[Fretter].[ProcessaFaturaManual]", parameters);
                return retorno;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERRO Processar Fatura Manual - Erro: {e.Message}");
            }
            return null;
        }
        public List<Item> ProcessarFaturaImportacao(DataTable entregaConciliacao, int usuarioId, int faturaArquivoId)
        {
            SqlParameter[] parameters =
            {
                    new SqlParameter("@entregaConciliacao", entregaConciliacao)
                    {
                        TypeName = "Fretter.Tp_FaturaConciliacao",
                        SqlDbType = SqlDbType.Structured,
                    },
                    new SqlParameter("@usuarioId", usuarioId)
                    {
                        SqlDbType = SqlDbType.Int,
                    },
                    new SqlParameter("@faturaArquivoId", faturaArquivoId)
                    {
                        SqlDbType = SqlDbType.Int,
                    },
                };

            var retorno = ExecuteStoredProcedureWithParam<Item>("[Fretter].[ProcessaFaturaImportacao]", parameters);
            return retorno;
        }

        public override IPagedList<Fatura> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true, params Expression<Func<Fatura, object>>[] includes)
        {
            List<Expression<Func<Fatura, object>>> faturaIncludes = new List<Expression<Func<Fatura, object>>>()
            {
                x => x.FaturaStatus,
                x => x.FaturaPeriodo,
                x => x.Transportador,
                x => x.ArquivoCobrancas.Where(x => x.Ativo)
            };

            return base.GetPaginated(filter, start, limit, orderByDescending, faturaIncludes.ToArray());
        }

        public List<FaturaArquivoCriticaDTO> ValidarDataEmissaoNF(DataTable lstDataEmissaoNF)
        {
            SqlParameter[] parameters =
{
                    new SqlParameter("@DatasEmissaoNF", lstDataEmissaoNF)
                    {
                        TypeName = "Fretter.Tp_FaturaArquivoCriticaDataEmissao",
                        SqlDbType = SqlDbType.Structured,
                    }
                };

            return ExecuteStoredProcedureWithParam<FaturaArquivoCriticaDTO>("[Fretter].[ProcessaValidacaoDataEmissaoNF]", parameters);
        }
    }
}
