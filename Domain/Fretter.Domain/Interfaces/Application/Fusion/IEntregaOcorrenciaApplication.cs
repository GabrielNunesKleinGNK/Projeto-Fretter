using Fretter.Domain.Dto.EntregaDevolucao;
using Fretter.Domain.Dto.Fusion.EntregaOcorrencia;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
	public interface IEntregaOcorrenciaApplication<TContext> : IApplicationBase<TContext, EntregaOcorrencia>
		where TContext : IUnitOfWork<TContext>
	{
		Task<List<EntregaOcorrencia>> Inserir(List<EntregaOcorrencia> ocorrencia);
		Task<bool> UploadArquivoMassivo(IFormFile file);
		Task<List<EntregaEmAbertoDto>> ObterEntregasEmAberto(EntregaEmAbertoFiltro filtro);
		Task<List<OcorrenciaEmbarcadorDto>>ObterDePara();
		Task<byte[]> DownloadArquivo(bool comEntregas, EntregaEmAbertoFiltro filtro);
	}
}
	