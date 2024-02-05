    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ImportacaoConfiguracaoTipoApplication<TContext> : ApplicationBase<TContext, ImportacaoConfiguracaoTipo>, IImportacaoConfiguracaoTipoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public ImportacaoConfiguracaoTipoApplication(IUnitOfWork<TContext> context, IImportacaoConfiguracaoTipoService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
