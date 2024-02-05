    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ImportacaoArquivoCategoriaApplication<TContext> : ApplicationBase<TContext, ImportacaoArquivoCategoria>, IImportacaoArquivoCategoriaApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public ImportacaoArquivoCategoriaApplication(IUnitOfWork<TContext> context, IImportacaoArquivoCategoriaService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
