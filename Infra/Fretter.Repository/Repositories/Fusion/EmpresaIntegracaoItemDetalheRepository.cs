using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Data;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Dto.EmpresaIntegracao.EmpresaIntegracaoItemDetalhe;

namespace Fretter.Repository.Repositories
{
    public class EmpresaIntegracaoItemDetalheRepository<TContext> : RepositoryBase<TContext, EmpresaIntegracaoItemDetalhe>, IEmpresaIntegracaoItemDetalheRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EmpresaIntegracaoItemDetalhe> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EmpresaIntegracaoItemDetalhe>();

        public EmpresaIntegracaoItemDetalheRepository(IUnitOfWork<TContext> context) : base(context) { }

        public List<EmpresaIntegracaoItemDetalheDto> GetDados(EmpresaIntegracaoItemDetalheFiltro filter)
        {
            try
            {
                SqlParameter[] parameters =
                {
                     new SqlParameter("@dataInicio", filter.DataEnvioInicio)
                    {
                        SqlDbType = SqlDbType.DateTime,
                    },
                     new SqlParameter("@dataFim", filter.DataEnvioFim)
                    {
                        SqlDbType = SqlDbType.DateTime,
                    },
                     new SqlParameter("@canalId", filter.CanalId)
                    {
                        SqlDbType = SqlDbType.Int,
                    },
                     new SqlParameter("@transportadorId", filter.TransportadorId)
                    {
                        SqlDbType = SqlDbType.Int,
                    },
                     new SqlParameter("@empresaId", filter.EmpresaId)
                    {
                        SqlDbType = SqlDbType.Int,
                    },
                     new SqlParameter("@pg", filter.Pagina)
                    {
                        SqlDbType = SqlDbType.Int,
                    },
                     new SqlParameter("@qtdePg", filter.PaginaLimite)
                    {
                         SqlDbType = SqlDbType.Int,
                    },
                     new SqlParameter("@descricao", filter.Descricao)
                    {
                        SqlDbType = SqlDbType.VarChar
                    },
                    new SqlParameter("@sigla", filter.Sigla)
                    {
                        SqlDbType = SqlDbType.VarChar,
                    },
                     new SqlParameter("@ocorrenciaId", filter.OcorrenciaId)
                    {
                        SqlDbType = SqlDbType.Int,
                    },
                     new SqlParameter("@dataOcorrenciaInicio", filter.DataOcorrenciaInicio)
                    {
                        SqlDbType = SqlDbType.DateTime,
                    },
                    new SqlParameter("@dataOcorrenciaFim", filter.DataOcorrenciaFim)
                    {
                        SqlDbType = SqlDbType.DateTime,
                    },
                    new SqlParameter("@sucesso", filter.Sucesso)
                    {
                        SqlDbType = SqlDbType.Bit,
                    }
                };

                var retorno = ExecuteStoredProcedureWithParam<EmpresaIntegracaoItemDetalheDto>("dbo.Pr_Edi_EntregaOCorrencia_Obter", parameters);
                return retorno.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERRO Buscando EntregaPedido - Erro: {e.Message}");
            }
            return null;
        }
        public bool InserirOcorrenciasParaReprocessamento(DataTable ids) 
        {
            try
            {
                SqlParameter[] parameters =
                {
                     new SqlParameter("@Itens", ids)
                    {
                        TypeName = "Tp_Int",
                        SqlDbType = SqlDbType.Structured,
                    } 
                };

                var retorno = ExecuteStoredProcedureWithParam<int>("dbo.Pr_Edi_ReprocessamentoOcorrenciaMassiva", parameters);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERRO Buscando EntregaPedido - Erro: {e.Message}");
                return false;
            }
        }
    }
}
