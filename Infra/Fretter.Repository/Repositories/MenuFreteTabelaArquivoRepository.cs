using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;
using System.Collections.Generic;
using Fretter.Domain.Dto.TabelaArquivo;
using System;
using Microsoft.Data.SqlClient;
using System.Data;
using Fretter.Domain.Enum;
using Fretter.Domain.Config;
using Microsoft.Extensions.Options;

namespace Fretter.Repository.Repositories
{
    public class MenuFreteTabelaArquivoRepository<TContext> : RepositoryBase<TContext, TabelaArquivo>, IMenuFreteTabelaArquivoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private readonly TimeoutConfig _timeoutConfig;
        private DbSet<TabelaArquivo> _dbSet => ((Contexts.CommandContext)UnitOfWork).Set<TabelaArquivo>();

        public MenuFreteTabelaArquivoRepository(IUnitOfWork<TContext> context,
            IOptions<TimeoutConfig> timeoutConfig) : base(context)
        {
            _timeoutConfig = timeoutConfig?.Value;
        }

        public List<TabelaArquivoProcessamento> GetTabelArquivoProcessamento()
        {
            var listParameter = new List<SqlParameter>();
            return ExecuteStoredProcedureWithParam<TabelaArquivoProcessamento>("dbo.GetTabelaArquivoProcessamento", listParameter.ToArray());
        }

        public List<RegiaoTipo> GetRegiaoTipo(int? idEmpresa, int? idTransportador)
        {
            var filtro = new
            {
                IdEmpresa = idEmpresa,
                IdTransportador = idTransportador
            };
            return ExecuteStoredProcedure<RegiaoTipo, dynamic>(filtro, "[dbo].[GetRegioMF]");
        }

        public List<RegiaoCep> GetRegiaoCep(int idTabela)
        {
            var filtro = new
            {
                IdTabela = idTabela,
            };
            return ExecuteStoredProcedure<RegiaoCep, dynamic>(filtro, "[dbo].[GetRegioCEP]");
        }

        public void AtualizarTabelaArquivo(int idTabelArquivo, EnumTabelaArquivoStatus enumTabelaArquivoStatus,
            string objRetorno = null, int? qtAdvertencia = null, int? qtErros = null, int? qtRegistros = null, int? nrPercentualAtualizacao = null)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@IdTabelaArquivo", idTabelArquivo)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@IdTabelaArquivoStatus", (int)enumTabelaArquivoStatus)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@JsonRetorno", objRetorno ?? string.Empty)
                {
                    SqlDbType = SqlDbType.VarChar,
                    IsNullable = true
                },
                new SqlParameter("@QtAdvertencia", qtAdvertencia ?? 0)
                {
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true
                },
                new SqlParameter("@QtErros", qtErros ?? 0)
                {
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true
                },
                new SqlParameter("@QtRegistros", qtRegistros ?? 0)
                {
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true
                },
                new SqlParameter("@NrPercentualAtualizacao", nrPercentualAtualizacao ?? 0)
                {
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true
                },
                new SqlParameter("@DataAtualizacao", DateTime.Now)
                {
                    SqlDbType = SqlDbType.DateTime
                },
            };

            ExecuteStoredProcedureWithParamNonQuery<int>("Pr_MF_AtualizarTabelaArquivo", parameters, commandTimeout: _timeoutConfig?.DatabaseCommandTimeout);

        }

        #region Processar

        public void InserirLista(int idTabela, int idEmpresa, int idTransportador, bool novo, DataTable lstFaixas,
            DataTable lstRegiao, DataTable lstValor, DataTable lstRegiaoCEP)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Cd_Id", idTabela)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@Id_Empresa", idEmpresa)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@Id_Transportador", idTransportador)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@novo", novo)
                {
                    SqlDbType = SqlDbType.Bit,
                },
                new SqlParameter("@faixas", lstFaixas)
                {
                    TypeName = "Tp_MF_TabelaPeso",
                    SqlDbType = SqlDbType.Structured,
                },
                new SqlParameter("@regioes", lstRegiao)
                {
                    TypeName = "Tp_MF_Regiao",
                    SqlDbType = SqlDbType.Structured,
                },
                new SqlParameter("@valores", lstValor)
                {
                    TypeName = "Tp_MF_TabelaPreco",
                    SqlDbType = SqlDbType.Structured,
                },
                new SqlParameter("@regiaoCEP", lstRegiaoCEP)
                {
                    TypeName = "Tp_MF_RegiaoCEP3",
                    SqlDbType = SqlDbType.Structured,
                },
            };

            ExecuteStoredProcedureWithParamNonQuery<int>("Pr_MF_Tabela_InserirListaArquivo", parameters, commandTimeout: _timeoutConfig?.DatabaseCommandTimeout);
        }

        public void InserirListaCorreios(int idTabela, int idEmpresa, int idTransportador, bool novo, DataTable lstFaixas,
            DataTable lstRegiao, DataTable lstValor)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Cd_Id", idTabela)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@Id_Empresa", idEmpresa)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@Id_Transportador", idTransportador)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@novo", novo)
                {
                    SqlDbType = SqlDbType.Bit,
                },
                new SqlParameter("@faixas", lstFaixas)
                {
                    TypeName = "Tp_MF_TabelaPeso",
                    SqlDbType = SqlDbType.Structured,
                },
                new SqlParameter("@regioes", lstRegiao)
                {
                    TypeName = "Tp_MF_RegiaoCorreios",
                    SqlDbType = SqlDbType.Structured,
                },
                new SqlParameter("@valores", lstValor)
                {
                    TypeName = "Tp_MF_TabelaPreco",
                    SqlDbType = SqlDbType.Structured,
                }
            };

            ExecuteStoredProcedureWithParamNonQuery<int>("Pr_MF_Tabela_InserirListaCorreios", parameters, commandTimeout: _timeoutConfig?.DatabaseCommandTimeout);
        }

        public void InserirListaVtex(int idTabela, bool novo, DataTable registrosVtex)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Cd_Id", idTabela)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@novo", novo)
                {
                    SqlDbType = SqlDbType.Bit,
                },
                new SqlParameter("@registrosVtex", registrosVtex)
                {
                    TypeName = "Tp_MF_TabelaVtex",
                    SqlDbType = SqlDbType.Structured,
                },
            };

            ExecuteStoredProcedureWithParamNonQuery<int>("Pr_MF_Tabela_InserirListaVtex", parameters, commandTimeout: _timeoutConfig?.DatabaseCommandTimeout);
        }

        public void InserirParametroCep(int idTabela, int idEmpresa, DataTable lstRegiaoCEP)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Cd_Id", idTabela)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@Id_Empresa", idEmpresa)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@ceps", lstRegiaoCEP)
                {
                    TypeName = "Tp_MF_TabelaCep",
                    SqlDbType = SqlDbType.Structured,
                },
            };


            ExecuteStoredProcedureWithParamNonQuery<int>("Pr_MF_TabelaCep_AlterarLista", parameters, commandTimeout: _timeoutConfig?.DatabaseCommandTimeout);
        }

        public void AtualizarLista(int idTabela, int idEmpresa, int idTransportador, DataTable lstFaixas,
            DataTable lstRegiao, DataTable lstValor, DataTable lstRegiaoCEP)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Cd_Id", idTabela)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@Id_Empresa", idEmpresa)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@Id_Transportador", idTransportador)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@faixas", lstFaixas)
                {
                    TypeName = "Tp_MF_TabelaPeso",
                    SqlDbType = SqlDbType.Structured,
                },
                new SqlParameter("@regioes", lstRegiao)
                {
                    TypeName = "Tp_MF_Regiao",
                    SqlDbType = SqlDbType.Structured,
                },
                new SqlParameter("@valores", lstValor)
                {
                    TypeName = "Tp_MF_TabelaPreco",
                    SqlDbType = SqlDbType.Structured,
                },
                new SqlParameter("@regiaoCEP", lstRegiaoCEP)
                {
                    TypeName = "Tp_MF_RegiaoCEP",
                    SqlDbType = SqlDbType.Structured,
                },
            };

            ExecuteStoredProcedureWithParamNonQuery<int>("Pr_MF_Tabela_AtualizaLista", parameters, commandTimeout: _timeoutConfig?.DatabaseCommandTimeout);
        }

        public void InserirListaAgendamento(int idTabela, int idEmpresa, int idTransportador, DataTable lstRegiaoCEPCapacidade, DataTable lstRegiaoCEP, DataTable lstRegiao)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@Cd_Id", idTabela)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@Id_Empresa", idEmpresa)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@Id_Transportador", idTransportador)
                {
                    SqlDbType = SqlDbType.Int,
                },
                new SqlParameter("@Regioes", lstRegiao)
                {
                    TypeName = "Tp_MF_Regiao",
                    SqlDbType = SqlDbType.Structured,
                },
                new SqlParameter("@RegioesCEP", lstRegiaoCEP)
                {
                    TypeName = "Tp_MF_RegiaoCEP2",
                    SqlDbType = SqlDbType.Structured,
                },
                new SqlParameter("@RegioesCEPCapacidade", lstRegiaoCEPCapacidade)
                {
                    TypeName = "Tp_MF_RegiaoCEPCapacidade",
                    SqlDbType = SqlDbType.Structured,
                }
            };

            ExecuteStoredProcedureWithParamNonQuery<int>("Pr_MF_Tabela_InserirListaAgendamento", parameters, commandTimeout: _timeoutConfig?.DatabaseCommandTimeout);
        }

        #endregion

    }
}
