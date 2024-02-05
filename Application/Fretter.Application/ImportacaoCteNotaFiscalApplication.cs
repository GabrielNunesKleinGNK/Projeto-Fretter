    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ImportacaoCteNotaFiscalApplication<TContext> : ApplicationBase<TContext, ImportacaoCteNotaFiscal>, IImportacaoCteNotaFiscalApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public ImportacaoCteNotaFiscalApplication(IUnitOfWork<TContext> context, IImportacaoCteNotaFiscalService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
