using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fusion.SKU;
using Fretter.Domain.Interfaces.Repository;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IAnymarketService<TContext> : IServiceBase<TContext, EntregaConfiguracao>
        where TContext : IUnitOfWork<TContext>
    {
        Task ImportarSku();
    }
}	
