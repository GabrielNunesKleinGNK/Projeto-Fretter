using Fretter.Domain.Dto.EntregaDevolucao;
using Fretter.Domain.Dto.Fusion.EntregaOcorrencia;
using Fretter.Domain.Dto.LogisticaReversa;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Service
{
    public interface IEntregaOcorrenciaService<TContext> : IServiceBase<TContext, EntregaOcorrencia>
        where TContext : IUnitOfWork<TContext>
    {
        Task<List<EntregaOcorrencia>> Inserir(List<EntregaOcorrencia> ocorrencia, int? empresaId = null);
        Task<bool> UploadArquivoMassivo(IFormFile file);
        Task<List<EntregaEmAbertoDto>> ObterEntregasEmAberto(EntregaEmAbertoFiltro filtro);
        Task<List<OcorrenciaEmbarcadorDto>> ObterDePara(int? empresaId = null);
        Task<byte[]> DownloadArquivoEntregaEmAberto(bool comEntregas, EntregaEmAbertoFiltro filtro);
    }
}
