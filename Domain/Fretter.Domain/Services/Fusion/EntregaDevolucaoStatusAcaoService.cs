using Fretter.Domain.Dto.EntregaDevolucao;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fretter.Domain.Helpers;
using Newtonsoft.Json;
using System.Dynamic;
using System;
using System.Reflection;

namespace Fretter.Domain.Services
{
    public class EntregaDevolucaoStatusAcaoService<TContext> : ServiceBase<TContext, EntregaDevolucaoStatusAcao>, IEntregaDevolucaoStatusAcaoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEntregaDevolucaoStatusAcaoRepository<TContext> _Repository;

        public EntregaDevolucaoStatusAcaoService(IEntregaDevolucaoStatusAcaoRepository<TContext> Repository, IUnitOfWork<TContext> unitOfWork, IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
        }
    }
}
