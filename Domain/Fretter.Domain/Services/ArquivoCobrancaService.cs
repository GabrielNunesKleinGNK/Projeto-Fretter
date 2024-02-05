using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Service;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Fretter.Domain.Helpers.Proceda;
using Fretter.Domain.Helpers.Proceda.Entidades;
using Microsoft.Extensions.Options;
using Fretter.Domain.Config;
using System;
using System.IO;
using Fretter.Api.Helpers.Atributes;
using System.Threading.Tasks;
using Fretter.Domain.Dto.Fatura;

namespace Fretter.Domain.Services
{
    public class ArquivoCobrancaService<TContext> : ServiceBase<TContext, ArquivoCobranca>, IArquivoCobrancaService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IArquivoCobrancaRepository<TContext> _repository;
        public readonly IBlobStorageService _blobStorageService;
        private readonly BlobStorageConfig _blobConfig;
        public ArquivoCobrancaService(IArquivoCobrancaRepository<TContext> repository, 
                                      IUnitOfWork<TContext> unitOfWork,
                                      IUsuarioHelper user,
                                      IOptions<BlobStorageConfig> blobConfig,
                                      IBlobStorageService blobStorageService) : base(repository, unitOfWork, user)
        {
            _repository = repository;
            _blobConfig = blobConfig.Value;
            _blobStorageService = blobStorageService;
            _blobStorageService.InitBlob(_blobConfig.ConnectionString, _blobConfig.ConciliacaoContainerName);
        }

        public List<ArquivoCobranca> ObterArquivosCobranca(int faturaId)
        {
            return _repository.ObterArquivoCobrancas(faturaId);
        }

        public async Task SalvarUploadDoccob(IFormFile file, int faturaId)
        {
            RetornoLeituraDoccob dto = PROCEDAHelper.ObterDOCCOB50(file);
            string novoGuid = Guid.NewGuid().ToString("N");
            string fileName = $"{novoGuid}{Path.GetExtension(file.FileName)}";
            byte[] fileBytes = ObterBytes(file);
            string pastaEmpresa = _user.UsuarioLogado.EmpresaId.ToString().PadLeft(6, '0');
            string urlFile = await _blobStorageService.UploadFileToBlobAsync($"{pastaEmpresa}/ArquivoCobranca", fileName, fileBytes, MimeTypes.Text.Plain);
            ArquivoCobranca arquivoCobranca = new ArquivoCobranca(dto.Doccob, faturaId, _user.UsuarioLogado.EmpresaId, urlFile);
            _repository.Save(arquivoCobranca);
        }

        public async Task<string> RealizarUploadDoccob(IFormFile file)
        {
            byte[] fileBytes = ObterBytes(file);
            string pastaEmpresa = _user.UsuarioLogado.EmpresaId.ToString().PadLeft(6, '0');
            string urlFile = await _blobStorageService.UploadFileToBlobAsync($"{pastaEmpresa}", "ArquivoCobranca", file.FileName, fileBytes, MimeTypes.Text.Plain);

            return urlFile;
        }

        private byte[] ObterBytes(IFormFile file)
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                byte[] fileBytes = ms.ToArray();
                return fileBytes;
            }
        }
    }
}	
