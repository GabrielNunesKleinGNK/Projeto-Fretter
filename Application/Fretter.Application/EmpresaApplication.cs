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
    public class EmpresaApplication<TContext> : ApplicationBase<TContext, Empresa>, IEmpresaApplication<TContext>
       where TContext : IUnitOfWork<TContext>
    {
        public new readonly IEmpresaService<TContext> _service;
        public EmpresaApplication(IUnitOfWork<TContext> context, IEmpresaService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }
        public async Task ProcessarUploadEmpresa(IFormFile file)
        {
            await _service.ProcessarUploadEmpresa(file);            
        }
        public byte[] DownloadArquivo(int arquivoId)
        {
            return _service.DownloadArquivo(arquivoId);
        }
    }
}

