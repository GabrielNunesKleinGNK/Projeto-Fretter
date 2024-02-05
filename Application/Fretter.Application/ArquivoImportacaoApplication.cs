
using System.Collections.Generic;
using System.Threading.Tasks;
using Fretter.Domain.Dto.ArquivoImportacaoLog;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;

namespace Fretter.Application
{
    public class ArquivoImportacaoApplication<TContext> : ApplicationBase<TContext, ArquivoImportacao>, IArquivoImportacaoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public new readonly IArquivoImportacaoService<TContext> _service;
        public ArquivoImportacaoApplication(IUnitOfWork<TContext> context, IArquivoImportacaoService<TContext> service) 
            : base(context, service)
        {
            _service = service;
        }

        public Task<List<ArquivoImportacaoLogDTO>> GetArquivoImportacaoLog(ArquivoImportacaoLogFiltro arquivoImportacaoLogFiltro)
        {
            return Task.FromResult(_service.GetLista(arquivoImportacaoLogFiltro));
        }
    }
}	
