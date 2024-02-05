using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Fretter.Repository.Repositories
{
    public class ArquivoImportacaoRepository<TContext> : RepositoryBase<TContext, ArquivoImportacao>, IArquivoImportacaoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<ArquivoImportacao> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<ArquivoImportacao>();

        public ArquivoImportacaoRepository(IUnitOfWork<TContext> context) : base(context) { }

        public List<ArquivoImportacao> GetArquivoImportacao(DateTime dtInicio, DateTime dtTermino, string processType)
        {
            var type = "";
            if (processType == "1")
                type = "n_lote";
            if (processType == "2")
                type = "entrega";

            return _dbSet.Where(t => t.DtImportacao >= dtInicio && t.DtImportacao <= dtTermino
                        //&& t.DsNome.Contains("bseller") 
                        //&& (string.IsNullOrEmpty(type) || t.DsNome.Contains(type))
                        ).ToList();
        }

        public List<ArquivoImportacao> BuscarArquivoImportacao(DateTime dtInicio, DateTime dtTermino)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@dataInicio", dtInicio)
                    {
                        SqlDbType = SqlDbType.DateTime,
                    },
                    new SqlParameter("@dataTermino", dtTermino)
                    {
                        SqlDbType = SqlDbType.DateTime,
                    }
                };

                var retorno = ExecuteStoredProcedureWithParam<ArquivoImportacao>("[Fretter].[GetPedidoEntrega]", parameters);
                return retorno;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERRO Buscando EntregaPedido - Erro: {e.Message}");
            }
            return null;
        }

        public override IEnumerable<ArquivoImportacao> GetAll(Expression<Func<ArquivoImportacao, bool>> predicate = null)
        {
            return base.GetQueryable(predicate)
                .ToList();
        }

        public int InserirArquivo(ArquivoImportacao arquivo)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@Ds_Nome", arquivo.DsNome )
                    {
                        SqlDbType = SqlDbType.VarChar,
                    },
                    new SqlParameter("@Ds_Extensao", arquivo.DsExtensao )
                    {
                        SqlDbType = SqlDbType.VarChar,
                    },
                    new SqlParameter("@Origem_Id", arquivo.OrigemId)
                    {
                        SqlDbType = SqlDbType.Int,
                    },
                    new SqlParameter("@Ds_Arquivo", arquivo.DsArquivo )
                    {
                        SqlDbType = SqlDbType.VarBinary,
                    },
                    new SqlParameter("@Id_ArquivoImportacao_Pai", arquivo.IdArquivoImportacaoPai )
                    {
                        SqlDbType = SqlDbType.Int,
                    }
                };

                var retorno = ExecuteStoredProcedureWithParamScalar<int>("Pr_Edi_InserirArquivoImportacao", parameters.ToArray());

                return retorno;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERRO ao salvar arquivo - Erro: {e.Message}");
                return 0;
            }
        }
    }
}
