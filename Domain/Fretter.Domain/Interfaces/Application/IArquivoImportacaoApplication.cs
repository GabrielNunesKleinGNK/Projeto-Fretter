using Fretter.Domain.Dto.ArquivoImportacaoLog;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IArquivoImportacaoApplication<TContext> : IApplicationBase<TContext, ArquivoImportacao>
		where TContext : IUnitOfWork<TContext>
	{

		public Task<List<ArquivoImportacaoLogDTO>> GetArquivoImportacaoLog(ArquivoImportacaoLogFiltro arquivoImportacaoLogFiltro);
       
	}
}
	