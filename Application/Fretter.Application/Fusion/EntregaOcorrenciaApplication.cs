using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Fretter.Api.Models;
using Fretter.Domain.Dto.EntregaDevolucao;
using Fretter.Domain.Dto.Fusion.EntregaOcorrencia;
using Fretter.Domain.Dto.LogisticaReversa;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Fretter.Domain.Interfaces.Service.Fusion;
using Microsoft.AspNetCore.Http;

namespace Fretter.Application
{
    public class EntregaOcorrenciaApplication<TContext> : ApplicationBase<TContext, EntregaOcorrencia>, IEntregaOcorrenciaApplication<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        public new readonly IEntregaOcorrenciaService<TContext> _service;
        public new readonly Domain.Interfaces.Service.Webhook.IEntregaService<TContext> _serviceEntrega;
        public EntregaOcorrenciaApplication(IUnitOfWork<TContext> context,
            IEntregaOcorrenciaService<TContext> service)
            : base(context, service)
        {
            _service = service;
        }

        public Task<List<EntregaEmAbertoDto>> ObterEntregasEmAberto(EntregaEmAbertoFiltro filtro) => _service.ObterEntregasEmAberto(filtro);
        public Task<List<EntregaOcorrencia>> Inserir(List<EntregaOcorrencia> ocorrencia) 
        { 
            var retorno = _service.Inserir(ocorrencia); 
            //_unitOfWork.Commit();

            return retorno;
        }
        public Task<bool> UploadArquivoMassivo(IFormFile file) => _service.UploadArquivoMassivo(file);
        public Task<List<OcorrenciaEmbarcadorDto>> ObterDePara() => _service.ObterDePara();

        public Task<byte[]> DownloadArquivo(bool comEntregas, EntregaEmAbertoFiltro filtro)
        {
            return _service.DownloadArquivoEntregaEmAberto(comEntregas, filtro);
        }
    }
}
