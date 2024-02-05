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
using Fretter.Domain.Entities.Fusion;

namespace Fretter.Repository.Repositories
{
    public class StageConfgRepository<TContext> : RepositoryBase<TContext, StageConfig>, IStageConfigRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<StageConfig> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<StageConfig>();

        public StageConfgRepository(IUnitOfWork<TContext> context) : base(context) { }

        public List<StageConfig> GetStageConfig()
        {
            List<StageConfig> lstStageConfig = ExecuteStoredProcedureWithParam<StageConfig>("Pr_Adm_GetStageConfig", null);
            return lstStageConfig;
        }

    }
}	
