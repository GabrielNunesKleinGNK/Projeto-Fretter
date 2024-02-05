using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Entities;
using Fretter.Domain.Exceptions;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace Fretter.Domain.Services
{
    public class ContratoTransportadorHistoricoService<TContext> : ServiceBase<TContext, ContratoTransportadorHistorico>, IContratoTransportadorHistoricoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        private readonly IContratoTransportadorHistoricoRepository<TContext> _repository;
        private readonly IRepositoryBase<TContext, ContratoTransportadorHistorico> _historicoRepository;

        public ContratoTransportadorHistoricoService(IContratoTransportadorHistoricoRepository<TContext> repository, 
                                            IUnitOfWork<TContext> unitOfWork,
                                            IUsuarioHelper user,
                                            IRepositoryBase<TContext, ContratoTransportadorHistorico> historicoRepository) 
            : base(repository, unitOfWork, user)
        {
            _user = user;
            _repository = repository;
            _historicoRepository = historicoRepository;
        }
    }
}	
