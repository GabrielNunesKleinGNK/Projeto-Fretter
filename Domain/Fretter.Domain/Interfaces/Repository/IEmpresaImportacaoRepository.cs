using Fretter.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fretter.Domain.Dto.Fusion;

namespace Fretter.Domain.Interfaces.Repository
{
    public interface IEmpresaImportacaoRepository<TContext> : IRepositoryBase<TContext, EmpresaImportacaoArquivo>
        where TContext : IUnitOfWork<TContext>
    {
        List<EmpresaImportacaoArquivo> GetEmpresaImportacaoArquivoPorEmpresa(int empresaId, EmpresaImportacaoFiltro filtro);
        EmpresaImportacaoArquivo GetEmpresaImportacaoDetalhePorArquivo(int arquivoId);
    }
}

