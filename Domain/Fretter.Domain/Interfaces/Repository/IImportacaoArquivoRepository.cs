using  Fretter.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Dto.ImportacaoArquivo;

namespace  Fretter.Domain.Interfaces.Repository
{
    public interface IImportacaoArquivoRepository<TContext> : IRepositoryBase<TContext, ImportacaoArquivo> 
        where TContext : IUnitOfWork<TContext>
    {
        List<ImportacaoArquivo> ObterArquivosPendentesConemb();
        List<ImportacaoArquivo> ObterArquivosPendentesCte();
        List<ImportacaoArquivo> ObterArquivosPendentes();
        ImportacaoArquivoResumo ObterImportacaoArquivoResumo(ImportacaoArquivoFiltro importacaoArquivoFiltro);
    }
}	
	
