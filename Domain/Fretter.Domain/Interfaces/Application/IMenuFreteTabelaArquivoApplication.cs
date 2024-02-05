using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IMenuFreteTabelaArquivoApplication<TContext> : IApplicationBase<TContext, TabelaArquivo>
        where TContext : IUnitOfWork<TContext>
    {
        int ProcessaTabelaArquivo();
    }
}
	