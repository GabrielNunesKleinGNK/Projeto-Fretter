using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class OcorrenciaArquivoApplication<TContext> : ApplicationBase<TContext, OcorrenciaArquivo>, IOcorrenciaArquivoApplication<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        public new readonly IOcorrenciaArquivoService<TContext> _service;
        public OcorrenciaArquivoApplication(IUnitOfWork<TContext> context, IOcorrenciaArquivoService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }
        public int ProcessarOcorrenciaArquivo()
        {
            return this._service.ProcessarOcorrenciaArquivo().Result;
        }
    }
}
