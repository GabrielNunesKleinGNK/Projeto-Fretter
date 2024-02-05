    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ImportacaoArquivoTipoApplication<TContext> : ApplicationBase<TContext, ImportacaoArquivoTipo>, IImportacaoArquivoTipoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public ImportacaoArquivoTipoApplication(IUnitOfWork<TContext> context, IImportacaoArquivoTipoService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
