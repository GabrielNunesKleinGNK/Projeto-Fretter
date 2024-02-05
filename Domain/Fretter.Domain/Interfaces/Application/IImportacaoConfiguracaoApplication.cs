using Fretter.Api.Models;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IImportacaoConfiguracaoApplication<TContext> : IApplicationBase<TContext, ImportacaoConfiguracao>
        where TContext : IUnitOfWork<TContext>
    {
        int ProcessarImportacaoConfiguracao();
    }
}
