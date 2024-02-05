using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Fretter.Domain.Interfaces.Repository;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IEntregaTransporteTipoRepository<TContext> : IRepositoryBase<TContext, EntregaTransporteTipo>
        where TContext : IUnitOfWork<TContext>
    {

        List<EntregaTransporteTipo> BuscaTipoTransporteAtivo();
    }
}

