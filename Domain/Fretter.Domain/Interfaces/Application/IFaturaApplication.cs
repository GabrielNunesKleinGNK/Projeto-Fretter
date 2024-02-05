using Fretter.Api.Models;
using Fretter.Domain.Dto.Fatura;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IFaturaApplication<TContext> : IApplicationBase<TContext, Fatura>
        where TContext : IUnitOfWork<TContext>
    {
        List<Fatura> GetFaturasDaEmpresa(FaturaFiltro filtro);
        byte[] GetFaturasDemonstrativo(FaturaFiltro filtro);
        void RealizarAcao(FaturaAcaoDto acao);
        Task<List<EntregaDemonstrativoDTO>> GetEntregaPorDoccob(List<IFormFile> file);

        List<EntregaDemonstrativoDTO> GetEntregaPorPeriodo(FaturaPeriodoFiltro filtro);
        Task<byte[]> ProcessarFaturaManual(List<EntregaDemonstrativoDTO> entregaProcessamento);
        Task<int> ProcessarFaturaAprovacao(List<EntregaDemonstrativoDTO> entregaProcessamento);
    }
}
