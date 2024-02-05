
using Fretter.Domain.Entities.Fusion.SKU;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class RegraEstoqueApplication<TContext> : ApplicationBase<TContext, RegraEstoque>, IRegraEstoqueApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public RegraEstoqueApplication(IUnitOfWork<TContext> context, IRegraEstoqueService<TContext> service)
            : base(context, service)
        { }

        public override void Delete(int chave)
        {
            _service.Delete(chave);
        }
    }
}
