using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface ICanalVendaService<TContext> : IServiceBase<TContext, CanalVenda>
        where TContext : IUnitOfWork<TContext>
    {
        Dto.Fusion.EmpresaCanalVenda ConfiguraCanalVenda(Dto.Fusion.EmpresaCanalVenda dto);
    }
}	
