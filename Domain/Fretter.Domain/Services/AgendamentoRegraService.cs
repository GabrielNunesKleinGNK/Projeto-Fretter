using Fretter.Domain.Dto.Agendamento;
using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Enum;
using Fretter.Domain.Exceptions;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fretter.Domain.Services
{
    public class AgendamentoRegraService<TContext> : ServiceBase<TContext, AgendamentoRegra>, IAgendamentoRegraService<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        private readonly IAgendamentoRegraRepository<TContext> _repository;
        private readonly IRepositoryBase<TContext, RegraTipoOperador> _tipoOperadorRepository;
        private readonly IRepositoryBase<TContext, RegraTipoItem> _tipoItemRepository;
        private readonly IRepositoryBase<TContext, RegraTipo> _regraTipoRepository;

        public AgendamentoRegraService
        (
            IAgendamentoRegraRepository<TContext> repository,
            IRepositoryBase<TContext, RegraTipoOperador> tipoOperadorRepository,
            IRepositoryBase<TContext, RegraTipoItem> tipoItemRepository,
            IRepositoryBase<TContext, RegraTipo> regraTipoRepository,
            IUnitOfWork<TContext> unitOfWork, 
            IUsuarioHelper user
        )
            : base(repository, unitOfWork, user)
        {
            _user = user;
            _repository = repository;
            _tipoOperadorRepository = tipoOperadorRepository;
            _tipoItemRepository = tipoItemRepository;
            _regraTipoRepository = regraTipoRepository;
        }

        public List<RegraTipoOperador> ObtemRegraTiposOperadores()
        {
            return _tipoOperadorRepository.GetAll()?.ToList();
        }

        public List<RegraTipoItem> ObtemRegraTipoItem()
        {
            return _tipoItemRepository.GetAll()?.ToList();
        }

        public List<RegraTipo> ObtemRegraTipo()
        {
            return _regraTipoRepository.GetAll()?.ToList();
        }
        
        public int GravaRegra(AgendamentoRegra entidade)
        {
            List<RegraItemModel> lstRegraItensModel = new List<RegraItemModel>();

            entidade.UsuarioCadastro = _user.UsuarioLogado.Id;
            entidade.UsuarioAlteracao = entidade.UsuarioCadastro;
            entidade.EmpresaId = _user.UsuarioLogado.EmpresaId;
            entidade.EmpresaTransportadorId = null;
            entidade.RegraStatusId = (int)EnumRegraStatus.Ativo;
            entidade.RegraTipoId = (int)EnumRegraTipo.Bloqueio;

            var regraGrupo = entidade.RegraItens.FirstOrDefault().RegraGrupoItem;

            foreach (var item in entidade.RegraItens)
            {
                lstRegraItensModel.Add(new RegraItemModel
                (
                    item.Id, 
                    item.RegraId, 
                    item.RegraGrupoItemId, 
                    item.RegraTipoItemId, 
                    item.RegraTipoOperadorId,
                    item.Valor, 
                    item.ValorInicial, 
                    item.ValorFinal
                ));
            }

            return _repository.GravaRegra(entidade, regraGrupo, CollectionHelper.ConvertTo(lstRegraItensModel));
        }

        public int InativarRegra(int id)
        {
            return _repository.InativarRegra(id, _user.UsuarioLogado.Id);
        }
    }
}
