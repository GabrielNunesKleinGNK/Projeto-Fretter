using Microsoft.EntityFrameworkCore;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Fretter.Domain.Dto.LogisticaReversa;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Data;
using Fretter.Repository.Util;

namespace Fretter.Repository.Repositories
{
    public class EntregaDevolucaoRepository<TContext> : RepositoryBase<TContext, EntregaDevolucao>, IEntregaDevolucaoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EntregaDevolucao> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EntregaDevolucao>();

        public readonly IUsuarioHelper _user;

        public EntregaDevolucaoRepository(IUnitOfWork<TContext> context, IUsuarioHelper user) : base(context)
        {
            _user = user;
        }

        public List<DevolucaoCorreio> BuscaEntregaDevolucaoPendente()
        {
            var watch = new Stopwatch();
            watch.Start();
            List<DevolucaoCorreio> listEntregas = new List<DevolucaoCorreio>();

            using (SqlConnection lconn = new SqlConnection(_connectionString))
            {
                lconn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = lconn;
                    cmd.CommandText = "dbo.Pr_Edi_EntregaDevolucao_Solicita";
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DevolucaoCorreio entrega = new DevolucaoCorreio();

                                try
                                {
                                    reader.MapDataToObject(entrega);
                                    listEntregas.Add(entrega);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"{ DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") } - ProcessaEntregaDevolucaoTask - Erro durante o Parse : {ex.Message}. Tempo (ms) - { watch.ElapsedMilliseconds}");
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"{ DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") } - ProcessaEntregaDevolucaoTask - Quantidade: {listEntregas.Count} Tempo (ms) - { watch.ElapsedMilliseconds}");

            return listEntregas;
        }
        public void SalvarEntregaDevolucaoProcessada(DataTable entregaDevolucao)
        {
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@Itens", entregaDevolucao)
                {
                    TypeName = "dbo.Tp_Edi_EntregaDevolucaoItem2",
                    SqlDbType = SqlDbType.Structured,
                }
            };

                ExecuteStoredProcedureWithParamNonQuery<int>("dbo.Pr_Edi_EntregaDevolucao_Retorno2", parameters);
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Salvando Entregas Devolucao- : {watch.ElapsedMilliseconds}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERRO Salvando Entregas Devolucao- : {watch.ElapsedMilliseconds} {ex.Message} {ex.InnerException}");
            }

        }

        public void SalvarEntregaDevolucaoOcorrencia(DataTable entregaDevolucaoOcorrencia)
        {
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@Itens", entregaDevolucaoOcorrencia)
                    {
                        TypeName = "Tp_Edi_EntregaDevolucaoOcorrenciaItem",
                        SqlDbType = SqlDbType.Structured,
                    }
                };

                ExecuteStoredProcedureWithParamNonQuery<int>("Pr_Edi_EntregaDevolucao_Ocorrencia", parameters);
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Salvando Ocorrencias Devolucao- : {watch.ElapsedMilliseconds}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERRO Salvando Ocorrencias Devolucao- : {watch.ElapsedMilliseconds} {ex.Message} {ex.InnerException}");
                throw ex;
            }
        }

        public List<DevolucaoCorreioRetorno> ProcessarEntregaDevolucaoOcorrencia(DataTable entregaDevolucaoOcorrencia)
        {
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                SqlParameter[] parameters =
                {
                new SqlParameter("@Itens", entregaDevolucaoOcorrencia)
                {
                    TypeName = "Tp_Edi_EntregaDevolucaoOcorrenciaItem",
                    SqlDbType = SqlDbType.Structured,
                }
            };

                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Processando Ocorrencias Devolucao - : {watch.ElapsedMilliseconds}");
                return ExecuteStoredProcedureWithParam<DevolucaoCorreioRetorno>("Pr_Edi_EntregaDevolucao_ProcessaOcorrencia", parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERRO Processando Ocorrencias Devolucao - : {watch.ElapsedMilliseconds} {ex.Message} {ex.InnerException}");
                return null;
            }

        }

        public List<DevolucaoCorreioCancela> BuscaEntregaDevolucaoCancela()
        {
            var watch = new Stopwatch();
            watch.Start();
            List<DevolucaoCorreioCancela> listEntregas = new List<DevolucaoCorreioCancela>();

            using (SqlConnection lconn = new SqlConnection(_connectionString))
            {
                lconn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = lconn;
                    cmd.CommandText = "dbo.Pr_Edi_EntregaDevolucao_Cancela";
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DevolucaoCorreioCancela entrega = new DevolucaoCorreioCancela();

                                try
                                {
                                    reader.MapDataToObject(entrega);
                                    listEntregas.Add(entrega);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"{ DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") } - ProcessaEntregaDevolucaoCancelaTask - Erro durante o Parse : {ex.Message}. Tempo (ms) - { watch.ElapsedMilliseconds}");
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"{ DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") } - ProcessaEntregaDevolucaoCancelaTask - Quantidade: {listEntregas.Count} Tempo (ms) - { watch.ElapsedMilliseconds}");

            return listEntregas;
        }

        public List<EntregaDevolucao> GetEntregasDevolucoes(Expression<Func<EntregaDevolucao, bool>> predicate = null)
        {
            return base.GetQueryable(predicate)
                .Where(x => x.Ativo)
                .Include(x => x.Status)
                .Include(x => x.Entrega).Where(t => t.Entrega.EmpresaMarketPlace == _user.UsuarioLogado.EmpresaId)
                .Include(x => x.Ocorrencias)
                .Include(x => x.EntregaReversa)
                    .ThenInclude(t => t.Empresa)
                .ToList();
        }

        public override IEnumerable<EntregaDevolucao> GetAll(Expression<Func<EntregaDevolucao, bool>> whereExp, params Expression<Func<EntregaDevolucao, object>>[] includeExps)
        {
            return base.GetAll(whereExp, includeExps);
        }

        public void InserirEntrega()
        {
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                ExecuteStoredProcedureWithParamNonQuery<int>("dbo.Pr_Edi_InserirDevolucaoNaEntrega", null, _transaction);
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - Criando Entregas da Devolucao- : {watch.ElapsedMilliseconds}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - ERRO Criando Entregas da Devolucao- : {watch.ElapsedMilliseconds} {ex.Message} {ex.InnerException}");
                throw ex;
            }
        }
    }
}