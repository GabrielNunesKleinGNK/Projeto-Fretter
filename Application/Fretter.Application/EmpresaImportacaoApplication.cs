using Fretter.Domain.Dto.Fusion;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fretter.Application
{
    public class EmpresaImportacaoApplication<TContext> : ApplicationBase<TContext, EmpresaImportacaoArquivo>, IEmpresaImportacaoApplication<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        public new readonly IEmpresaImportacaoService<TContext> _service;
        public EmpresaImportacaoApplication(IUnitOfWork<TContext> context, IEmpresaImportacaoService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }
        public IEnumerable<EmpresaImportacaoArquivo> GetEmpresaImportacaoArquivoPorEmpresa(EmpresaImportacaoFiltro filtro)
        {
            return _service.GetEmpresaImportacaoArquivoPorEmpresa(filtro);
        }
    }
}

