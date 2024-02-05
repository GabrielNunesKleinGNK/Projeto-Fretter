using Fretter.Domain.Entities.Fretter;
using System.Data;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IFaturaArquivoRepository<TContext> : IRepositoryBase<TContext, FaturaArquivo>
        where TContext : IUnitOfWork<TContext>
    {
        int GravarFaturaArquivo(DataTable faturaArquivo, DataTable criticas);
    }
}
