using Fretter.Domain.Dto.Fusion;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretter.Domain.Interfaces.Application
{
    public interface IEmpresaImportacaoApplication<TContext> : IApplicationBase<TContext, EmpresaImportacaoArquivo>
        where TContext : IUnitOfWork<TContext>
    {
        IEnumerable<EmpresaImportacaoArquivo> GetEmpresaImportacaoArquivoPorEmpresa(EmpresaImportacaoFiltro filtro);
    }
}