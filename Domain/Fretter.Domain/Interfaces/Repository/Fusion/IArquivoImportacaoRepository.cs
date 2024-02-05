using Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IArquivoImportacaoRepository<TContext> : IRepositoryBase<TContext, ArquivoImportacao>
        where TContext : IUnitOfWork<TContext>
    {

        public List<ArquivoImportacao> GetArquivoImportacao(DateTime dtInicio, DateTime dtFim, string processType);
        int InserirArquivo(ArquivoImportacao arquivo);
    }
}

