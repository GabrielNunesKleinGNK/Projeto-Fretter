
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fretter.Api.Models;
using Fretter.Domain.Dto.Fatura;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Http;

namespace Fretter.Application
{
    public class ArquivoCobrancaApplication<TContext> : ApplicationBase<TContext, ArquivoCobranca>, IArquivoCobrancaApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public new readonly IArquivoCobrancaService<TContext> _service;
        public ArquivoCobrancaApplication(IUnitOfWork<TContext> context, IArquivoCobrancaService<TContext> service) 
            : base(context, service)
        {
            _service = service;
        }

        public List<ArquivoCobranca> ObterArquivosCobranca(int faturaId)
        {
            return _service.ObterArquivosCobranca(faturaId);
        }

        public async Task SalvarUploadDoccob(IFormFile file, int faturaId)
        {
            await _service.SalvarUploadDoccob(file, faturaId);
            _unitOfWork.Commit();
        }
    }
}	
