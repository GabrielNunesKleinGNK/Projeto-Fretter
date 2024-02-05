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

namespace Fretter.Repository.Repositories
{
    public class ArquivoCobrancaRepository<TContext> : RepositoryBase<TContext, ArquivoCobranca>, IArquivoCobrancaRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        private DbSet<ArquivoCobranca> _dbSet => ((Contexts.FretterContext)UnitOfWork).Set<ArquivoCobranca>();

        public ArquivoCobrancaRepository(IUnitOfWork<TContext> context) : base(context)
        {

        }

        public List<ArquivoCobranca> ObterArquivoCobrancas(int faturaId)
        {
            var result = _dbSet.Where(x => x.Ativo && x.FaturaId == faturaId)
                .Include(x => x.ArquivoCobrancaDocumentos)
                .ToList();
            return result;
        }
    }
}
