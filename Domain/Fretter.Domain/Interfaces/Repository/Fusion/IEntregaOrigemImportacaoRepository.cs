using Fretter.Domain.Dto.EmpresaIntegracao;
using Fretter.Domain.Dto.Fusion;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IEntregaOrigemImportacaoRepository<TContext> : IRepositoryBase<TContext, EntregaOrigemImportacao>
        where TContext : IUnitOfWork<TContext>
    {
    }
}
