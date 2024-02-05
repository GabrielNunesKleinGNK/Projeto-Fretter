using Microsoft.EntityFrameworkCore;

using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Fretter.Domain.Entities;
using System.Data;
using System.Diagnostics;
using Fretter.Repository.Util;
using Fretter.Domain.Dto.Carrefour;

namespace Fretter.Repository.Repositories
{
    public class EntregaStageRepository<TContext> : RepositoryBase<TContext, EntregaStage>, IEntregaStageRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<EntregaStage> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<EntregaStage>();

        public EntregaStageRepository(IUnitOfWork<TContext> context) : base(context) { }

        IEnumerable<StageEtiquetaReciclagemFilaDTO> IEntregaStageRepository<TContext>.ObterEtiquetasParaReciclagem(DateTime dataAtual, DateTime dataInicialParaBusca)
        {
            SqlParameter[] parameters =
           {
                new SqlParameter("@dataAtual", dataAtual)
                {
                    SqlDbType = SqlDbType.DateTime,
                },
                new SqlParameter("@dataInicialParaBusca", dataInicialParaBusca)
                {
                    SqlDbType = SqlDbType.DateTime,
                }
            };

            List<StageEtiquetaReciclagemFilaDTO> lstEntregas = ExecuteStoredProcedureWithParam<StageEtiquetaReciclagemFilaDTO>("Pr_Edi_ObterEtiquetasReciclagem_ShipN", parameters);
            return lstEntregas;

        }
    }
}	
