using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System.Threading.Tasks;

namespace Fretter.Application
{
    public class RecotacaoFreteApplication<TContext> : ApplicationBase<TContext, RecotacaoFrete>, IRecotacaoFreteApplication<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        public new readonly IRecotacaoFreteService<TContext> _service;
        public RecotacaoFreteApplication(IUnitOfWork<TContext> context, IRecotacaoFreteService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }
        public int ProcessarArquivoRecotacaoAsync()
        {
            return this._service.ProcessarRecotacaoFrete().GetAwaiter().GetResult();
        }
    }
}
