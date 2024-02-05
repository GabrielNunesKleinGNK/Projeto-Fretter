using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IImportacaoArquivoCategoriaService<TContext> : IServiceBase<TContext, ImportacaoArquivoCategoria>
        where TContext : IUnitOfWork<TContext>
    {
    }
}	
