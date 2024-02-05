using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Linq;
using System.Collections.Generic;
using Fretter.Domain.Dto.EntregaDevolucao;
using Microsoft.Data.SqlClient;
using System.Data;
using Fretter.Domain.Dto.Fusion.EntregaOcorrencia;
using Fretter.Domain.Interfaces;
using System.Linq.Expressions;
using System;

namespace Fretter.Repository.Repositories
{
    public class EntregaOcorrenciaRepository<TContext> : RepositoryBase<TContext, EntregaOcorrencia>, IEntregaOcorrenciaRepository<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        private DbSet<Entrega> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<Entrega>();
        public EntregaOcorrenciaRepository(IUnitOfWork<TContext> context) : base(context) { }

        public dynamic Inserir(DataTable ocorrencias, int usuarioId, string obs)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@id_usuario", usuarioId)
                    {
                        SqlDbType = SqlDbType.Int,
                    },
                    new SqlParameter("@Obs", obs )
                    {
                        SqlDbType = SqlDbType.VarChar,
                    },
                    new SqlParameter("@Entregas_Ocorrencias", ocorrencias)
                    {
                        TypeName = "dbo.Tp_Edi_EntregaOcorrencia",
                        SqlDbType = SqlDbType.Structured,
                    }
                };

            return ExecuteStoredProcedureWithParam<int>("[dbo].[Pr_Edi_EntregaOcorrencia_InsereLista]", parameters);
        }

        public void InserirArquivoMassivo(string nomeArquivo, string url, string usuario, int empresaId)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@NomeArquivo", nomeArquivo)
                    {
                        SqlDbType = SqlDbType.VarChar,
                    },
                    new SqlParameter("@Url", url )
                    {
                        SqlDbType = SqlDbType.VarChar,
                    },
                    new SqlParameter("@usuario", usuario)
                    {
                        SqlDbType = SqlDbType.VarChar,
                    },
                    new SqlParameter("@empresaId", empresaId)
                    {
                        SqlDbType = SqlDbType.Int,
                    }
                };

            ExecuteStoredProcedureWithParam<int>("[dbo].[Pr_Edi_IncluirOcorrenciaArquivo]", parameters);
        }

        public List<EntregaEmAbertoDto> ObterEntregasEmAberto(EntregaEmAbertoFiltro filtro)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@EmpresaId", filtro.EmpresaId)
                {
                    SqlDbType = SqlDbType.Int
                },
                new SqlParameter("@TransportadorId", filtro.TransportadorId)
                {
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true
                },
                new SqlParameter("@EmpresaMarketplaceId", filtro.EmpresaMarketplaceId)
                {
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true
                },
                new SqlParameter("@OcorrenciaId", filtro.OcorrenciaId)
                {
                    SqlDbType = SqlDbType.Int,
                    IsNullable = true
                },
                new SqlParameter("@Pedidos", filtro.Pedidos)
                {
                    SqlDbType = SqlDbType.VarChar,
                    IsNullable = true
                },
                new SqlParameter("@DataUltimaOcorrencia", filtro.DataUltimaOcorrencia)
                {
                    SqlDbType = SqlDbType.DateTime,
                    IsNullable = true
                }
                ,
                new SqlParameter("@DataUltimaOcorrenciaFim", filtro.DataUltimaOcorrenciaFim)
                {
                    SqlDbType = SqlDbType.DateTime,
                    IsNullable = true
                },
                new SqlParameter("@DataInicio", filtro.DataInicio)
                {
                    SqlDbType = SqlDbType.DateTime,
                    IsNullable = true
                },
                new SqlParameter("@DataFim", filtro.DataFim)
                {
                    SqlDbType = SqlDbType.DateTime,
                    IsNullable = true
                },
                new SqlParameter("@Pagina", filtro.Pagina)
                {
                    SqlDbType = SqlDbType.Int
                },
                new SqlParameter("@PaginaLimite", filtro.PaginaLimite)
                {
                    SqlDbType = SqlDbType.Int
                }
            };
            try
            {
                List<EntregaEmAbertoDto> lstEntregas = ExecuteStoredProcedureWithParam<EntregaEmAbertoDto>("Tb_Edi_ObterEntregasEmAberto", parameters, null, 360);
                return lstEntregas;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<OcorrenciaEmbarcadorDto> ObterDePara(int empresaId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@EmpresaId", empresaId)
                {
                    SqlDbType = SqlDbType.Int
                }
            };
            List<OcorrenciaEmbarcadorDto> lstOcorrencias = ExecuteStoredProcedureWithParam<OcorrenciaEmbarcadorDto>("Pr_Edi_ObterDeParaEmpresa", parameters);
            return lstOcorrencias;
        }

        public override IPagedList<EntregaOcorrencia> GetPaginated(QueryFilter filter, int start = 0, int limit = 10, bool orderByDescending = true, params Expression<Func<EntregaOcorrencia, object>>[] includes)
        {
            var result = DbSet.AsQueryable();

            if (includes != null)
                foreach (var include in includes)
                    result = result.Include(include);

            return GetPagedList(result, filter, start, limit, orderByDescending);
        }
    }
}
