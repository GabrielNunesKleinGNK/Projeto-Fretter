
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fretter.Api.Models;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Http;

namespace Fretter.Application
{
    public class AtualizacaoTabelasCorreiosApplication<TContext> : ApplicationBase<TContext, TabelasCorreiosArquivo>, IAtualizacaoTabelasCorreiosApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public new readonly IAtualizacaoTabelasCorreiosService<TContext> _service;
        public AtualizacaoTabelasCorreiosApplication(IUnitOfWork<TContext> context, IAtualizacaoTabelasCorreiosService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }

        public async Task<TabelasCorreiosArquivo> UploadArquivo(IFormFile uploadModel)
        {
            var arquivo = await _service.UploadArquivo(uploadModel);
            _unitOfWork.Commit();

            return arquivo;
        }

        public async Task<int> ProcessarTabelasArquivo()
        {
            await _service.ProcessarTabelasArquivo();
            return 0;
        }
    }
}
