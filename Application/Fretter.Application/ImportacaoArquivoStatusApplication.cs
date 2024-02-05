    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ImportacaoArquivoStatusApplication<TContext> : ApplicationBase<TContext, ImportacaoArquivoStatus>, IImportacaoArquivoStatusApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public ImportacaoArquivoStatusApplication(IUnitOfWork<TContext> context, IImportacaoArquivoStatusService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
