using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Dto.Conciliacao;
using System.Data;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IConciliacaoTransportadorRepository<TContext> : IRepositoryBase<TContext, EntregaConciliacao>
        where TContext : IUnitOfWork<TContext>
    {
        int ProcessaConciliacaoTransportador(DataTable entregas);
    }
}

