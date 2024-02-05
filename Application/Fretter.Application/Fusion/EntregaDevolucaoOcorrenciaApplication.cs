    
using System.Linq;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class EntregaDevolucaoOcorrenciaApplication<TContext> : ApplicationBase<TContext, EntregaDevolucaoOcorrencia>, IEntregaDevolucaoOcorrenciaApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public EntregaDevolucaoOcorrenciaApplication(IUnitOfWork<TContext> context, IEntregaDevolucaoOcorrenciaService<TContext> service) 
            : base(context, service)
        {
        }
    }
}	
