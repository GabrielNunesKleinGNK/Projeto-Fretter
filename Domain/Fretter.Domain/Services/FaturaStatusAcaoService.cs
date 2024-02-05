using Fretter.Domain.Dto.Fatura;
using Fretter.Domain.Entities;
using Fretter.Domain.Helpers;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.Domain.Services
{

    public class FaturaStatusAcaoService<TContext> : ServiceBase<TContext, FaturaStatusAcao>, IFaturaStatusAcaoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        public new readonly IFaturaStatusAcaoRepository<TContext> _repository;
  

        public FaturaStatusAcaoService(IFaturaStatusAcaoRepository<TContext> repository, 
                                       IUnitOfWork<TContext> unitOfWork, 
                                       IUsuarioHelper user) 
                                       : base(repository, unitOfWork, user)
        {
            _user = user;
            _repository = repository;
        }
    }
}
