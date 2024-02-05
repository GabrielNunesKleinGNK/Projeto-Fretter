
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fretter.Api.Models;
using Fretter.Domain.Dto.Fatura;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Http;

namespace Fretter.Application
{
    public class FaturaStatusAcaoApplication<TContext> : ApplicationBase<TContext, FaturaStatusAcao>, IFaturaStatusAcaoApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public new readonly IFaturaStatusAcaoService<TContext> _service;
        public FaturaStatusAcaoApplication(IUnitOfWork<TContext> context, IFaturaStatusAcaoService<TContext> service) 
            : base(context, service)
        {
            _service = service;
        }
    }
}	
