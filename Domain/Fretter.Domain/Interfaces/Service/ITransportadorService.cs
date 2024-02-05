
using Fretter.Domain.Entities.Fusion;
using Fretter.Domain.Interfaces.Repository;
using System.Collections.Generic;

namespace Fretter.Domain.Interfaces.Service
{
    public interface ITransportadorService<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        List<Dto.Dashboard.Transportador> GetTransportadorPorEmpresa();
        List<TransportadorCnpj> GetTransportadorCNPJ(int transportadorId);
        List<Transportador> GetTransportador();
    }
}
