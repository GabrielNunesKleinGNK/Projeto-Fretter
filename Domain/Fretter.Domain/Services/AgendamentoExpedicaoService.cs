using Fretter.Domain.Dto.Fretter.Conciliacao;
using Fretter.Domain.Entities.Fretter;
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
    public class AgendamentoExpedicaoService<TContext> : ServiceBase<TContext, AgendamentoExpedicao>, IAgendamentoExpedicaoService<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        private readonly IAgendamentoExpedicaoRepository<TContext> _repository;
        public AgendamentoExpedicaoService(IAgendamentoExpedicaoRepository<TContext> repository, IUnitOfWork<TContext> unitOfWork, IUsuarioHelper user) : base(repository, unitOfWork, user)
        {
            _user = user;
            _repository = repository;
        }

        public override AgendamentoExpedicao Update(AgendamentoExpedicao entidade)
        {
            VerificaDuplicidade(entidade);
            VerificaCadastroCredenciais(_user.UsuarioLogado.EmpresaId, entidade.TransportadorId.ToString());

            entidade.AtualizarEmpresaId(_user.UsuarioLogado.EmpresaId);
            entidade.AtualizarDataAlteracao(DateTime.Now);

            return base.Update(entidade);
        }
        public override AgendamentoExpedicao Save(AgendamentoExpedicao entidade)
        {
            VerificaDuplicidade(entidade);
            VerificaCadastroCredenciais(_user.UsuarioLogado.EmpresaId, entidade.TransportadorId.ToString());

            entidade.AtualizarEmpresaId(_user.UsuarioLogado.EmpresaId);
            entidade.AtualizarUsuarioCriacao(_user.UsuarioLogado.Id);
            
            return base.Save(entidade);
        }

        private void VerificaDuplicidade(AgendamentoExpedicao entidade)
        {
            var dbEntities = _repository
                .GetAll(x => 
                    x.EmpresaId == _user.UsuarioLogado.EmpresaId && 
                    x.CanalId == entidade.CanalId && 
                    x.TransportadorId == entidade.TransportadorId && 
                    x.TransportadorCnpjId == entidade.TransportadorCnpjId && 
                    x.Id != entidade.Id)
                ?.ToList() ?? new List<AgendamentoExpedicao>();

            if (dbEntities.Any())
                throw new DomainException(nameof(AgendamentoExpedicao), "Update", $"Já existe uma configuração para esta filial com este mesmo transportador e transportador filial");
        }

        private void VerificaCadastroCredenciais(int idEmpresa, string idTransportador)
        {
            var validacao = _repository.VerificaCadastroCredenciais(idEmpresa, idTransportador);
            if (validacao != null && !validacao.Any())
                throw new DomainException(nameof(AgendamentoExpedicao), "VerificaCadastroCredenciais", $"Não existe credenciais cadastradas para essa empresa e transportadora na tabela Tb_Adm_EmpresaIntegracao");
        }

    }
}
