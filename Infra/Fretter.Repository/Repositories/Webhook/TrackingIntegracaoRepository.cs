using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Repository.Webhook;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fretter.Repository.Repositories.Webhook
{
    public class TrackingIntegracaoRepository<TContext> : ITrackingIntegracaoRepository<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        protected IUnitOfWork<TContext> UnitOfWork { get; }
        public readonly DbContext _context;
        public readonly string _connectionString = string.Empty;

        public TrackingIntegracaoRepository(IUnitOfWork<TContext> unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
            _context = ((DbContext)unitOfWork);
            _connectionString = _context.Database.GetConnectionString();
        }

    }
}
