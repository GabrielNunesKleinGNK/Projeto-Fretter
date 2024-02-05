using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Dto.LogisticaReversa;
using System.Data;
using Fretter.Domain.Dto.EntregaDevolucao;
using Fretter.Domain.Dto.Fusion.EntregaOcorrencia;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IEntregaOcorrenciaRepository<TContext> : IRepositoryBase<TContext, EntregaOcorrencia>
        where TContext : IUnitOfWork<TContext>
    {
        dynamic Inserir(DataTable ocorrencias, int usuarioId, string obs);
        List<EntregaEmAbertoDto> ObterEntregasEmAberto(EntregaEmAbertoFiltro filtro);
        List<OcorrenciaEmbarcadorDto> ObterDePara(int empresaId);
        void InserirArquivoMassivo(string nomeArquivo, string url, string usuario, int empresaId);
    }
}

