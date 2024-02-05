using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IImportacaoConfiguracaoRepository<TContext> : IRepositoryBase<TContext, ImportacaoConfiguracao>
        where TContext : IUnitOfWork<TContext>
    {
    } 
}
