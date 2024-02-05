using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class MenuFreteTabelaArquivoApplication<TContext> : ApplicationBase<TContext, TabelaArquivo>, IMenuFreteTabelaArquivoApplication<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        public new readonly IMenuFreteTabelaArquivoService<TContext> _service;
        public MenuFreteTabelaArquivoApplication(IUnitOfWork<TContext> context, IMenuFreteTabelaArquivoService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }
        public int ProcessaTabelaArquivo()
        {
            return this._service.ProcessarTabelaArquivo().Result;
        }
    }
}
