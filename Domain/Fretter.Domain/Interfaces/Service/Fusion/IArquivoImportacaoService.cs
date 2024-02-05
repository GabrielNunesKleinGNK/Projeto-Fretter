using Fretter.Domain.Dto.ArquivoImportacaoLog;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IArquivoImportacaoService<TContext> : IServiceBase<TContext, ArquivoImportacao>
        where TContext : IUnitOfWork<TContext>
    {
        public List<ArquivoImportacaoLogDTO> GetLista(ArquivoImportacaoLogFiltro arquivoImportacaoLogFiltro);
    }
}	
