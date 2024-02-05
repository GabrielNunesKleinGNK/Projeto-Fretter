    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ImportacaoCteCargaApplication<TContext> : ApplicationBase<TContext, ImportacaoCteCarga>, IImportacaoCteCargaApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public ImportacaoCteCargaApplication(IUnitOfWork<TContext> context, IImportacaoCteCargaService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
