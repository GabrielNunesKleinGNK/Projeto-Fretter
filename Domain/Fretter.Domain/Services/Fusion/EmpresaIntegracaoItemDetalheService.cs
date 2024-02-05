using Fretter.Domain.Dto.EmpresaIntegracao.EmpresaIntegracaoItemDetalhe;
using Fretter.Domain.Dto.Fusion;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fretter.Domain.Services
{
    public class EmpresaIntegracaoItemDetalheService<TContext> : ServiceBase<TContext, EmpresaIntegracaoItemDetalhe>, IEmpresaIntegracaoItemDetalheService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        private readonly IEmpresaIntegracaoItemDetalheRepository<TContext> _repository;
        private readonly IEmpresaIntegracaoItemRepository<TContext> _repositoryItem;

        public EmpresaIntegracaoItemDetalheService(
                IEmpresaIntegracaoItemDetalheRepository<TContext> repository,
                IEmpresaIntegracaoItemRepository<TContext> repositoryItem,
                IUnitOfWork<TContext> unitOfWork,
                IUsuarioHelper user) : base(repository, unitOfWork, user)
        {
            _user = user;
            _repository = repository;
            _repositoryItem = repositoryItem;
        }

        public bool ReprocessarLote(List<long> ids)
        {
            var list = ids.Select(x => new { Id = x });
            var retorno = _repository.InserirOcorrenciasParaReprocessamento(CollectionHelper.ConvertTo(list));
            return retorno;
        }

        public EmpresaIntegracaoItemDetalhe ObterStatusReprocessamento(int id)
        {
            return null;
        }

        public List<EmpresaIntegracaoItemDetalheDto> ObterDados(EmpresaIntegracaoItemDetalheFiltro filtro)
        {
            filtro.EmpresaId = _user.UsuarioLogado.EmpresaId;
            filtro.DataEnvioInicio = new DateTime(filtro.DataEnvioInicio.Year, filtro.DataEnvioInicio.Month, filtro.DataEnvioInicio.Day, 00, 00, 00);
            filtro.DataEnvioFim = new DateTime(filtro.DataEnvioFim.Year, filtro.DataEnvioFim.Month, filtro.DataEnvioFim.Day, 23, 59, 59);

            var diasDiferenca = (filtro.DataEnvioInicio - filtro.DataEnvioFim).TotalDays;
            diasDiferenca = diasDiferenca < 0 ? diasDiferenca * (-1) : diasDiferenca;
            if (diasDiferenca > 30)
                throw new Exception("O filtro de 'data de envio' de não deve ser maior que 30 dias.");

            if(filtro.DataOcorrencia.HasValue)
            {
                var dataOco = filtro.DataOcorrencia.Value;
                filtro.DataOcorrencia= new DateTime(dataOco.Year, dataOco.Month, dataOco.Day, 00, 00, 00);
                filtro.DataOcorrencia = new DateTime(dataOco.Year, dataOco.Month, dataOco.Day, 23, 59, 59);
            }

            return _repository.GetDados(filtro);
        }
    }
}
