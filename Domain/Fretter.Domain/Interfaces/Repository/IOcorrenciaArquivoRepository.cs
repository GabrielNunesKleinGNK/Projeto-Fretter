using Fretter.Domain.Dto.TabelaArquivo;
using Fretter.Domain.Entities;
using Fretter.Domain.Enum;
using System.Collections.Generic;
using System.Data;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IOcorrenciaArquivoRepository<TContext> : IRepositoryBase<TContext, OcorrenciaArquivo>
        where TContext : IUnitOfWork<TContext>
    {
        List<Dto.OcorrenciaArquivo.OcorrenciaArquivoDto> GetOcorrenciaArquivoProcessamentos();
        void AtualizarOcorrenciaArquivo(int idTabelArquivo, EnumTabelaArquivoStatus enumTabelaArquivoStatus,
           string objRetorno = null, int? qtAdvertencia = null, int? qtErros = null, int? qtRegistros = null, int? nrPercentualAtualizacao = null);
    }
}

