using System.Threading.Tasks;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class EntregaStageApplication<TContext> : ApplicationBase<TContext, EntregaStage>, IEntregaStageApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        private readonly IEntregaStageService<TContext> _service;

        public EntregaStageApplication(IUnitOfWork<TContext> context, IEntregaStageService<TContext> service) 
            : base(context, service)
        {
            _service = service;
        }

        public Task<int> PopulaFilaReclicagemEtiquetas()
        {
            return _service.PopulaFilaReclicagemEtiquetas();
        }
    }
}	
