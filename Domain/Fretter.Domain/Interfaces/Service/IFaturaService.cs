using Fretter.Api.Models;
using Fretter.Domain.Dto.Fatura;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IFaturaService<TContext> : IServiceBase<TContext, Fatura>
        where TContext : IUnitOfWork<TContext>
    {
        Task<List<Fatura>> GetFaturasDaEmpresa(FaturaFiltro filtro);
        Task<byte[]> GetFaturasDemonstrativo(FaturaFiltro filtro);
        void RealizarAcao(FaturaAcaoDto acao);

        Task<List<EntregaDemonstrativoDTO>> GetEntregaPorDoccob(List<IFormFile> files);

        List<EntregaDemonstrativoDTO> GetEntregaPorPeriodo(FaturaPeriodoFiltro filtro);
        Task<byte[]> ProcessarFaturaManual(List<EntregaDemonstrativoDTO> entregaProcessamento);
        Task<int> ProcessarFaturaAprovacao(List<EntregaDemonstrativoDTO> entregaProcessamento);
    }
}
