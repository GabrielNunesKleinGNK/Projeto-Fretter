using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Entities;
using Fretter.Domain.Exceptions;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fretter.Domain.Services
{
    public class ContratoTransportadorArquivoTipoService<TContext> : ServiceBase<TContext, ContratoTransportadorArquivoTipo>, IContratoTransportadorArquivoTipoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        private readonly IContratoTransportadorArquivoTipoRepository<TContext> _repository;
        private readonly IContratoTransportadorRegraRepository<TContext> _contratoRegraRepository;

        public ContratoTransportadorArquivoTipoService(IContratoTransportadorArquivoTipoRepository<TContext> repository,
                                            IUnitOfWork<TContext> unitOfWork,
                                            IUsuarioHelper user)
            : base(repository, unitOfWork, user)
        {
            _repository = repository;
            _user = user;
        }

        public bool Save(List<ContratoTransportadorArquivoTipo> model)
        {
            model.ForEach(m =>
            {
                m.AtualizarEmpresaId(_user.UsuarioLogado.EmpresaId);
            });
            
            _repository.SaveRange(model);
            
            return true;
        }

        public override IPagedList<ContratoTransportadorArquivoTipo> GetPaginated(QueryFilter filter, int start, int limit, bool orderByDescending)
        {
            //Filtro empresaId
            var empresaId = _user.UsuarioLogado.EmpresaId;
            filter.AddFilter(nameof(empresaId), empresaId);

            return _repository.GetPaginated(filter, start, limit, orderByDescending, x => x.ImportacaoArquivoTipoItem);
        }
    }
}
