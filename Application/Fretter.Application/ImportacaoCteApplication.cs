    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ImportacaoCteApplication<TContext> : ApplicationBase<TContext, ImportacaoCte>, IImportacaoCteApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public ImportacaoCteApplication(IUnitOfWork<TContext> context, IImportacaoCteService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	