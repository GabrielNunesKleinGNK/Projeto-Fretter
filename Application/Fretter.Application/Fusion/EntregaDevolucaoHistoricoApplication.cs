using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System.Collections.Generic;

namespace Fretter.Application
{
    public class EntregaDevolucaoHistoricoApplication<TContext> : ApplicationBase<TContext, EntregaDevolucaoHistorico>, IEntregaDevolucaoHistoricoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public new readonly IEntregaDevolucaoHistoricoService<TContext> _service;
        public EntregaDevolucaoHistoricoApplication(IUnitOfWork<TContext> context, IEntregaDevolucaoHistoricoService<TContext> service) 
            : base(context, service)
        {
            _service = service;
        }

        public List<EntregaDevolucaoHistorico> ObterHistoricoEntregaDevolucao(int entregaDevolucaoId)
        {
            return _service.ObterHistoricoEntregaDevolucao(entregaDevolucaoId).Result;
        }
    }
}	
