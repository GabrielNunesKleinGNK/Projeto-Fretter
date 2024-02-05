using Fretter.Domain.Dto.Conciliacao;
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
    public class ContratoTransportadorService<TContext> : ServiceBase<TContext, ContratoTransportador>, IContratoTransportadorService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        private readonly IContratoTransportadorRepository<TContext> _repository;
        private readonly IRepositoryBase<TContext, ContratoTransportadorHistorico> _historicoRepository;
        private readonly IContratoTransportadorRegraRepository<TContext> _contratoRegraRepository;

        public ContratoTransportadorService(IContratoTransportadorRepository<TContext> repository,
                                            IUnitOfWork<TContext> unitOfWork,
                                            IUsuarioHelper user,
                                            IRepositoryBase<TContext, ContratoTransportadorHistorico> historicoRepository,
                                            IContratoTransportadorRegraRepository<TContext> contratoTransportadorRegraRepository)
            : base(repository, unitOfWork, user)
        {
            _user = user;
            _repository = repository;
            _historicoRepository = historicoRepository;
            _contratoRegraRepository = contratoTransportadorRegraRepository;
        }
        public override ContratoTransportador Update(ContratoTransportador entidade)
        {
            VerificaDuplicidade(entidade);
            var dbEntity = _repository.GetNoTracking(entidade.Id);
            var historico = new ContratoTransportadorHistorico(dbEntity);

            int usuarioAlteracaoId = (int)(dbEntity.UsuarioAlteracao ?? _user.UsuarioLogado.Id);

            historico.AtualizarUsuarioCriacao(usuarioAlteracaoId);
            _historicoRepository.Save(historico);

            entidade.AtualizarEmpresaId(_user.UsuarioLogado.EmpresaId);
            return base.Update(entidade);
        }
        public override ContratoTransportador Save(ContratoTransportador entidade)
        {
            VerificaDuplicidade(entidade);
            entidade.AtualizarEmpresaId(_user.UsuarioLogado.EmpresaId);
            entidade.AtualizarUsuarioCriacao(_user.UsuarioLogado.Id);
            return base.Save(entidade);
        }

        public List<MicroServicosDTO> ObterMicroServicoPorEmpresa()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@EmpresaId", _user.UsuarioLogado.EmpresaId));
            var result = _repository.ExecuteStoredProcedureWithParam<MicroServicosDTO>("Fretter.GetMicroServicoPorEmpresa", parameters.ToArray());
            return result;
        }

        public List<OcorrenciasDTO> ObterOcorrenciasPorEmpresa()
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@EmpresaId", _user.UsuarioLogado.EmpresaId));
            var result = _repository.ExecuteStoredProcedureWithParam<OcorrenciasDTO>("Fretter.GetOcorrenciasPorEmpresa", parameters.ToArray());
            return result;
        }

        public int ProcessaContratoTransportadorRegra(List<ContratoTransportadorRegra> contratoTransportadorRegras)
        {
            foreach (var contratoTransportadorRegra in contratoTransportadorRegras)
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@OcorrenciaEmpresaItemId", contratoTransportadorRegra.OcorrenciaId));
                parameters.Add(new SqlParameter("@Ativo", contratoTransportadorRegra.Ativo));
                parameters.Add(new SqlParameter("@EmpresaId", _user.UsuarioLogado.EmpresaId));
                parameters.Add(new SqlParameter("@TransportadorId", contratoTransportadorRegra.TransportadorId));
                parameters.Add(new SqlParameter("@ContratoTransportadorRegraTipoId", contratoTransportadorRegra.TipoCondicao));
                parameters.Add(new SqlParameter("@Acrescimo", contratoTransportadorRegra.Operacao));
                parameters.Add(new SqlParameter("@UsuarioAlteracao", _user.UsuarioLogado.Id));
                parameters.Add(new SqlParameter("@Valor", contratoTransportadorRegra.Valor));
                parameters.Add(new SqlParameter("@ConciliacaoTipoId", contratoTransportadorRegra.ConciliacaoTipoId));                
                var result = _repository.ExecuteStoredProcedureWithParam<int>("Fretter.SetContratoTransportadorRegra", parameters.ToArray()).FirstOrDefault();
            }

            return 0;
        }

        public List<ContratoTransportadorRegra> ObterContratoTransportadorRegra(ContratoTransportadorRegraFiltroDTO regra)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@EmpresaId", _user.UsuarioLogado.EmpresaId));
            parameters.Add(new SqlParameter("@TransportadorId", regra.TransportadorId));
            parameters.Add(new SqlParameter("@OcorrenciaEmpresaItemId", regra.OcorrenciaEmpresaItemId));
            parameters.Add(new SqlParameter("@ConciliacaoTipoId", regra.ConciliacaoTipoId));

            return _repository.ExecuteStoredProcedureWithParam<ContratoTransportadorRegra>("Fretter.GetContratoTransportadorRegraPorEmpresa", parameters.ToArray());
        }

        private void VerificaDuplicidade(ContratoTransportador entidade)
        {
            var dbEntities = _repository.GetAll(x => x.EmpresaId == _user.UsuarioLogado.EmpresaId).ToList();
            var duplicado = dbEntities.FirstOrDefault(x => x.TransportadorCnpjId == entidade.TransportadorCnpjId
                                                        && x.MicroServicoId == entidade.MicroServicoId
                                                        && x.Id != entidade.Id);
            if (duplicado != null)
                throw new DomainException(nameof(ContratoTransportador), "Update", $"Já existe um contrato vigente para o mesmo transportador e micro serviço. ( Contrato: {duplicado.Id} )");
        }
    }
}
