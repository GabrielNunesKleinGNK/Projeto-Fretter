
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
    public class FaturaApplication<TContext> : ApplicationBase<TContext, Fatura>, IFaturaApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public new readonly IFaturaService<TContext> _service;
        public FaturaApplication(IUnitOfWork<TContext> context, IFaturaService<TContext> service) 
            : base(context, service)
        {
            _service = service;
        }

        public List<Fatura> GetFaturasDaEmpresa(FaturaFiltro filtro)
        {
            return _service.GetFaturasDaEmpresa(filtro).Result;
        }

        public byte[] GetFaturasDemonstrativo(FaturaFiltro filtro)
        {
            return _service.GetFaturasDemonstrativo(filtro).Result; 
        }

        public Task<List<EntregaDemonstrativoDTO>> GetEntregaPorDoccob(List<IFormFile> files)
        {
            return _service.GetEntregaPorDoccob(files);
        }

        public List<EntregaDemonstrativoDTO> GetEntregaPorPeriodo(FaturaPeriodoFiltro filtro)
        {
            return _service.GetEntregaPorPeriodo(filtro)?? new List<EntregaDemonstrativoDTO>();
        }

        public Task<byte[]> ProcessarFaturaManual(List<EntregaDemonstrativoDTO> entregaProcessamento)
        {
            return _service.ProcessarFaturaManual(entregaProcessamento);
        }
        public Task<int> ProcessarFaturaAprovacao(List<EntregaDemonstrativoDTO> entregaProcessamento)
        {
            return _service.ProcessarFaturaAprovacao(entregaProcessamento);
        }

        public void RealizarAcao(FaturaAcaoDto acao)
        {
            _service.RealizarAcao(acao);
            _unitOfWork.Commit();
        }
    }
}	
