using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Fretter.Domain.Dto.Conciliacao;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Data;
using Fretter.Domain.Dto.Fatura;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.Spreadsheet;
using Fretter.Domain.Dto.Fretter.Conciliacao;

namespace Fretter.Repository.Repositories
{
    public class ConciliacaoRepository<TContext> : RepositoryBase<TContext, Conciliacao>, IConciliacaoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<Conciliacao> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<Conciliacao>();

        public ConciliacaoRepository(IUnitOfWork<TContext> context) : base(context) { }
        public void ProcessaConciliacao()
        {
            ExecuteStoredProcedureWithParamNonQuery<int>("Fretter.ProcessaConciliacao", null, null, 180);
            ExecuteStoredProcedureWithParamNonQuery<int>("Fretter.ProcessaConciliacaoOcorrencia", null, null, 180);
        }

        public void ProcessaFatura()
        {
            ExecuteStoredProcedureWithParamNonQuery<int>("Fretter.ProcessaFaturaPeriodo", null);
            ExecuteStoredProcedureWithParamNonQuery<int>("Fretter.ProcessaFatura", null);
            ExecuteStoredProcedureWithParamNonQuery<int>("Fretter.ProcessaEntrega", null);
        }

        public List<EntregaConciliacaoDTO> ProcessaConciliacaoControle()
        {
            return ExecuteStoredProcedureWithParam<EntregaConciliacaoDTO>("Fretter.ProcessaConciliacaoEntrega", null, _transaction);
        }
        public void ProcessaConciliacaoControle(Int64 idControleProcesso)
        {
            var watch = new Stopwatch();
            watch.Start();

            SqlParameter[] parameters =
            {
                new SqlParameter("@ControleProcessoConciliacaoId", idControleProcesso)
                {
                    SqlDbType = SqlDbType.BigInt
                }
            };

            ExecuteNonQuery("Fretter.ProcessaConciliacaoEntrega", parameters, CommandType.StoredProcedure, null);
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Confirmando lote de conciliacao ProcessaConciliacaoEntrega idControleProcesso: {idControleProcesso} Tempo: {watch.ElapsedMilliseconds} ms");
        }

        public List<EntregaDemonstrativoDTO> GetDemonstrativoEntregaConciliacao(DataTable filtroDoccob, int empresaId, bool filtroPeriodo, DateTime? dataInicio = null, DateTime? dataTermino = null, int? transportadorId = null,
            int? statusConciliacaoId = null)
        {
            try
            {
                var listParameters = new List<SqlParameter>();

                listParameters.Add(new SqlParameter("@filtroDoccob", filtroDoccob)
                {
                    TypeName = "Fretter.Tp_FiltroEntregaConciliacao",
                    SqlDbType = SqlDbType.Structured,
                });

                listParameters.Add(new SqlParameter("@empresaId", empresaId)
                {
                    SqlDbType = SqlDbType.Int,
                });

                listParameters.Add(new SqlParameter("@filtroPeriodo", filtroPeriodo)
                {
                    SqlDbType = SqlDbType.Bit,
                });

                if (dataInicio != null && dataTermino != null)
                {
                    listParameters.Add(new SqlParameter("@dataInicio", dataInicio)
                    {
                        SqlDbType = SqlDbType.DateTime,
                    });
                    listParameters.Add(new SqlParameter("@dataTermino", dataTermino)
                    {
                        SqlDbType = SqlDbType.DateTime,
                    });
                }
                else
                {
                    listParameters.Add(new SqlParameter("@dataInicio", DBNull.Value)
                    {
                        SqlDbType = SqlDbType.DateTime,
                    });
                    listParameters.Add(new SqlParameter("@dataTermino", DBNull.Value)
                    {
                        SqlDbType = SqlDbType.DateTime,
                    });
                }

                if (transportadorId != null)
                    listParameters.Add(new SqlParameter("@transportadorId", transportadorId)
                    {
                        SqlDbType = SqlDbType.Int,
                    });
                else
                    listParameters.Add(new SqlParameter("@transportadorId", DBNull.Value)
                    {
                        SqlDbType = SqlDbType.Int,
                    });

                if (statusConciliacaoId > 0)
                    listParameters.Add(new SqlParameter("@statusConciliacaoId", statusConciliacaoId)
                    {
                        SqlDbType = SqlDbType.Int,
                    });
                else
                    listParameters.Add(new SqlParameter("@statusConciliacaoId", DBNull.Value)
                    {
                        SqlDbType = SqlDbType.Int,
                    });


                var retorno = ExecuteStoredProcedureWithParam<EntregaDemonstrativoDTO>("[Fretter].[GetEntregaConciliacao]", listParameters.ToArray(), null, 360);
                return retorno;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERRO Buscando EntregaConciliacao - Erro: {e.Message}");
            }
            return null;
        }
        public List<EntregaConciliacaoRecotacaoDTO> ObterConciliacaoRecotacao()
        {
            return ExecuteStoredProcedureWithParam<EntregaConciliacaoRecotacaoDTO>("Fretter.GetConciliacaoRecotacao", null, _transaction, 360);
        }

        public void ProcessaConciliacaoRecotacao(DataTable dataTable)
        {

            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@Itens", dataTable)
            {
                TypeName = "Fretter.Tp_ConciliacaoRecotacaoRetorno",
                SqlDbType = SqlDbType.Structured,
            });

            ExecuteNonQuery("Fretter.ProcessaConciliacaoRecotacao", parameters.ToArray(), CommandType.StoredProcedure, null, 360);
        }

        public ImportacaoArquivo GetArquivoPorConciliacao(int conciliacaoId)
        {
            return _dbSet.Where(p => p.Id == conciliacaoId)
                    .Include(e => e.ImportacaoCte)
                    .ThenInclude(t => t.ImportacaoArquivo)
                    .Select(o => o.ImportacaoCte.ImportacaoArquivo)
                    .FirstOrDefault();
        }

        public void EnviarParaRecalculoFrete(DataTable listaConciliacoes, string parametrosJson, int usuarioId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@ListaConciliacoesId", listaConciliacoes)
                {
                    TypeName = "Tp_Int",
                    SqlDbType = SqlDbType.Structured,
                },
                new SqlParameter("@ParametrosJson", parametrosJson),
                new SqlParameter("@UsuarioId", usuarioId),
            };

            ExecuteStoredProcedureWithParam<int>("Fretter.EnviaConciliacaoRecalculoFrete", parameters.ToArray());
        }

        public void EnviarParaRecalculoFreteMassivo(RelatorioDetalhadoFiltroDTO filtro, string parametrosJson, int usuarioId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@DataInicial", filtro.DataInicial),
                new SqlParameter("@DataFinal", filtro.DataFinal),
                new SqlParameter("@FaturaId", filtro.FaturaId),
                new SqlParameter("@TransportadorId", filtro.TransportadorId),
                new SqlParameter("@StatusId", filtro.StatusId),
                new SqlParameter("@CodigoEntrega", filtro.CodigoEntrega),
                new SqlParameter("@CodigoPedido", filtro.CodigoPedido),
                new SqlParameter("@CodigoDanfe", filtro.CodigoDanfe),
                new SqlParameter("@EmpresaId", filtro.EmpresaId),
                new SqlParameter("@ListSize", filtro.ListSize),
                new SqlParameter("@ParametrosJson", parametrosJson),
                new SqlParameter("@UsuarioId", usuarioId)
            };

            ExecuteStoredProcedureWithParam<int>("Fretter.EnviaConciliacaoRecalculoFreteMassivo", parameters.ToArray());
        }

        public List<ConciliacaoRecotacaoDTO> ListarRecotacoesPorIds(DataTable conciliacoes)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@ListaConciliacoesId", conciliacoes)
                {
                    TypeName = "Tp_Int",
                    SqlDbType = SqlDbType.Structured,
                }
            };

            return ExecuteStoredProcedureWithParam<ConciliacaoRecotacaoDTO>("Fretter.GetDetalhesConciliacaoRecotacao", parameters.ToArray());
        }
    }
}
