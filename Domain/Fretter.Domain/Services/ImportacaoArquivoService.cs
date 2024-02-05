using Azure;
using Fretter.Api.Helpers.Atributes;
using Fretter.Api.Models;
using Fretter.Domain.Config;
using Fretter.Domain.Dto.CTe;
using Fretter.Domain.Dto.ImportacaoArquivo;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Entities.Fusion;
using Fretter.Domain.Enum;
using Fretter.Domain.Exceptions;
using Fretter.Domain.Helpers.Proceda;
using Fretter.Domain.Helpers.Proceda.Entidades;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Repository.Fusion;
using Fretter.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Fretter.Domain.Services
{
    public class ImportacaoArquivoService<TContext> : ServiceBase<TContext, ImportacaoArquivo>, IImportacaoArquivoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        public List<ConfiguracaoCteTransportador> _configuracoesCTe;
        public List<TransportadorCnpj> _transportadores;

        public IEnumerable<Canal> _canais;
        public IEnumerable<ImportacaoConfiguracao> _configuracoesAtivas;
        public ICanalRepository<TContext> _canalRepository;
        public ITransportadorCnpjRepository<TContext> _transportadorCnpjRepository;
        public IConfiguracaoCteTransportadorRepository<TContext> _configCteTransportadorRepository;
        public IImportacaoConfiguracaoRepository<TContext> _configuracaoRepository;
        private readonly IContratoTransportadorArquivoTipoRepository<TContext> _contratoTransportadorArquivoTipoRepository;

        public IRepositoryBase<TContext, ImportacaoArquivoCritica> _importacaoArquivoCriticaRepository;

        public new readonly IImportacaoArquivoRepository<TContext> _repository;
        public readonly IImportacaoCteRepository<TContext> _importacaoCteRepository;
        public readonly IBlobStorageService _blobStorageService;
        private readonly BlobStorageConfig _blobConfig;
        public ImportacaoArquivoService
        (
            IUsuarioHelper user,
            IUnitOfWork<TContext> unitOfWork, 
            IBlobStorageService blobStorageService,
            IOptions<BlobStorageConfig> blobConfig,
            IImportacaoArquivoRepository<TContext> repository,
            IImportacaoCteRepository<TContext> importacaoCteRepository,
            ICanalRepository<TContext> canalRepository,
            IImportacaoConfiguracaoRepository<TContext> configuracaoRepository,
            IConfiguracaoCteTransportadorRepository<TContext> configCteTransportadorRepository,
            ITransportadorCnpjRepository<TContext> transportadorCnpjRepository,
            IContratoTransportadorArquivoTipoRepository<TContext> contratoTransportadorArquivoTipoRepository,
            IRepositoryBase<TContext, ImportacaoArquivoCritica> importacaoArquivoCriticaRepository
        ) 
            : base(repository, unitOfWork, user)
        {
            _blobConfig = blobConfig.Value;
            _blobStorageService = blobStorageService;
            _blobStorageService.InitBlob(_blobConfig.ConnectionString, _blobConfig.ConciliacaoContainerName);
            _importacaoCteRepository = importacaoCteRepository;
            _repository = repository;
            _transportadorCnpjRepository = transportadorCnpjRepository;
            _canalRepository = canalRepository;
            _configCteTransportadorRepository = configCteTransportadorRepository;
            _configuracaoRepository = configuracaoRepository;
            _importacaoArquivoCriticaRepository = importacaoArquivoCriticaRepository;
            _contratoTransportadorArquivoTipoRepository = contratoTransportadorArquivoTipoRepository;
        }

        public override IEnumerable<ImportacaoArquivo> GetAll(Expression<Func<ImportacaoArquivo, bool>> predicate = null)
        {
            predicate = x => (x.EmpresaId == _user.UsuarioLogado.EmpresaId);
            return base.GetAll(predicate);
        }

        public ImportacaoArquivoResumo ObterImportacaoArquivoResumo(ImportacaoArquivoFiltro importacaoArquivoFiltro)
        {
            importacaoArquivoFiltro.EmpresaId = _user.UsuarioLogado.EmpresaId;

            return _repository.ObterImportacaoArquivoResumo(importacaoArquivoFiltro);
        }

        public async Task UploadArquivo(UploadModel uploadModel, int empresaId)
        {
            foreach (var file in uploadModel?.files)
            {
                string novoGuid = Guid.NewGuid().ToString("N");
                string fileName = $"{novoGuid}{Path.GetExtension(file.FileName)}";
                byte[] fileBytes = ObterBytes(file);
                await ImportarArquivo(fileBytes, file.FileName, empresaId, 0);
            }
        }
        public async Task ImportarArquivo(byte[] bytes, string fileName, int empresaId, int configuracaoId)
        {
            EnumImportacaoArquivoTipo enumTipo = (EnumImportacaoArquivoTipo)ObterExtensaoArquivo(fileName);
            string mimeType = GetMimeType(enumTipo);
            string urlFile = await _blobStorageService.UploadFileToBlobAsync($"{empresaId.ToString().PadLeft(6, '0')}", fileName, bytes, mimeType);
            ImportacaoArquivo importacaoArquivo = new ImportacaoArquivo(fileName, empresaId, enumTipo, fileName, urlFile, configuracaoId);
            importacaoArquivo.Validate();
            _repository.Save(importacaoArquivo);
        }


        public string DownloadArquivo(UploadModel uploadModel)
        {
            using (var clientToken = new HttpClient())
            {
                var responseResult = clientToken.GetAsync(uploadModel.url).Result;
                return responseResult.Content.ReadAsStringAsync().Result;
            }
        }
        public void ProcessarArquivosCTe()
        {
            List<ImportacaoArquivo> arquivosCte = _repository.ObterArquivosPendentesCte();
            foreach (ImportacaoArquivo arquivo in arquivosCte)
            {
                arquivo.AtualizarStatusId(EnumImportacaoArquivoStatus.Processando);
                arquivo.AtualizarDataProcessamento(DateTime.Now);
                _repository.Update(arquivo);
                _unitOfWork.Commit();

                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(arquivo.Diretorio);
                    XmlElement root = doc.DocumentElement;
                    enviCTe response = null;
                    XmlSerializer serializer = new XmlSerializer(typeof(enviCTe));

                    using (XmlReader reader = new XmlNodeReader(root))
                    {
                        response = (enviCTe)serializer.Deserialize(reader);
                        var configs = ObterConfigs(response.CTe.InfCte.Emitente.CNPJ);
                        var arquivoExiste = _importacaoCteRepository
                                                        .GetAll(x => x.Chave == response.CTe.InfCte.Id)
                                                        .FirstOrDefault();

                        if (arquivoExiste != null)
                            throw new ApplicationException($"XML duplicado, a chave {response.CTe.InfCte.Id} já foi importada (ImportacaoCteId: {arquivoExiste.Id})");
                        else
                        {
                            ImportacaoCte importacaoCte = new ImportacaoCte(response.CTe, response.ProtCTe, arquivo.Id, configs);
                            _importacaoCteRepository.Save(importacaoCte);
                        }
                    }
                    arquivo.AtualizarStatusId(EnumImportacaoArquivoStatus.Concluido);
                    arquivo.AtualizarMensagem($"ArquivoId: {arquivo.Id} - Sucesso: Arquivo importado com exito");
                }
                catch (Exception ex)
                {
                    arquivo.AtualizarStatusId(EnumImportacaoArquivoStatus.Falha);
                    arquivo.AtualizarMensagem($"ArquivoId: {arquivo.Id} - Error: {ex.Message}");
                }
                finally
                {
                    _repository.Update(arquivo);
                    _unitOfWork.Commit();
                }
            }
        }
        public void ProcessarArquivos()
        {
            List<ImportacaoArquivo> arquivosCte = _repository.ObterArquivosPendentes();
            foreach (ImportacaoArquivo arquivo in arquivosCte)
            {
                arquivo.AtualizarStatusId(EnumImportacaoArquivoStatus.Processando);
                arquivo.AtualizarDataProcessamento(DateTime.Now);
                int? empresaId = null;
                _repository.Update(arquivo);
                _unitOfWork.Commit();

                try
                {
                    switch ((EnumImportacaoArquivoTipo)arquivo.ImportacaoArquivoTipoId)
                    {
                        case EnumImportacaoArquivoTipo.CTe:
                            empresaId = ProcessarArquivoCTe(arquivo);
                            arquivo.AtualizarEmpresaId(empresaId);
                            break;
                        case EnumImportacaoArquivoTipo.CONEMB:
                            ProcessarArquivoCONEMB(arquivo);
                            break;
                        default:
                            break;
                    }
                    arquivo.AtualizarStatusId(EnumImportacaoArquivoStatus.Concluido);
                    arquivo.AtualizarMensagem($"ArquivoId: {arquivo.Id} - Sucesso: Arquivo importado com exito");
                }
                catch (ArgumentException ex)
                {
                    arquivo.AtualizarStatusId(EnumImportacaoArquivoStatus.NaoClassificado);
                    arquivo.AtualizarMensagem($"ArquivoId: {arquivo.Id} - Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    arquivo.AtualizarStatusId(EnumImportacaoArquivoStatus.Falha);
                    arquivo.AtualizarMensagem($"ArquivoId: {arquivo.Id} - Error: {ex.Message}");
                }
                finally
                {
                    _repository.Update(arquivo);
                    _unitOfWork.Commit();
                }
            }
        }
        public int? ProcessarArquivoCTe(ImportacaoArquivo arquivo)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(arquivo.Diretorio);
            XmlElement root = doc.DocumentElement;
            enviCTe response = null;
            XmlSerializer serializer = new XmlSerializer(typeof(enviCTe));
            int? empresaId = null;

            if (_canais == null)
                _canais = _canalRepository.GetAll().ToList();

            if (_configuracoesAtivas == null)
                _configuracoesAtivas = _configuracaoRepository.GetAll().ToList();

            using (XmlReader reader = new XmlNodeReader(root))
            {
                response = (enviCTe)serializer.Deserialize(reader);

                var configs = ObterConfigs(response.CTe.InfCte.Emitente.CNPJ);
                var arquivoExiste = _importacaoCteRepository
                                                .GetAll(x => x.Chave == response.CTe.InfCte.Id)
                                                .FirstOrDefault();

                if (arquivoExiste != null)
                    throw new ApplicationException($"XML duplicado, a chave {response.CTe.InfCte.Id} já foi importada (ImportacaoCteId: {arquivoExiste.Id})");
                else
                {
                    var enumImportacaoArquivoTipoItem = ClassificaArquivo(response.CTe.InfCte);

                    ImportacaoCte importacaoCte = new ImportacaoCte(response.CTe, response.ProtCTe, arquivo.Id, configs);

                    try
                    {
                        importacaoCte.Validate();
                    }
                    catch (DomainSummaryException ex)
                    {
                        SalvarListaImportacaoArquivoCriticas(arquivo.Id, EnumImportacaoArquivoTipo.CTe, ex);
                    }

                    if (importacaoCte?.CNPJEmissor == null)
                        throw new ApplicationException($"Erro na CTe, remetente inválido.");

                    if (enumImportacaoArquivoTipoItem == EnumImportacaoArquivoTipoItem.Devolucao)
                    {
                        empresaId = _canais.Where(t => t.Cnpj == importacaoCte?.CNPJTomador.RemoveNonNumeric()).FirstOrDefault()?.EmpresaId;
                    }
                    else
                    {
                        empresaId = _canais.Where(t => t.Cnpj == importacaoCte?.CNPJEmissor.RemoveNonNumeric()).FirstOrDefault()?.EmpresaId;
                    }

                    arquivo.AtualizarImportacaoArquivoTipoItemId(enumImportacaoArquivoTipoItem);

                    PreencherTransportadorIdArquivo(arquivo, importacaoCte.CNPJTransportador);

                    if (_configuracoesAtivas.Any(p => p.EmpresaId == empresaId) && empresaId > 0)
                        _importacaoCteRepository.Save(importacaoCte);
                    else
                        throw new ApplicationException($"Nao existe ImportacaoConfiguracao parametrizada para a Empresa: {importacaoCte?.CNPJEmissor}");
                }
            }

            return empresaId;
        }

        public void ProcessarArquivoCONEMB(ImportacaoArquivo arquivo)
        {
            CONEMB conemb = null;
            var webClient = new WebClient();
            byte[] file = webClient.DownloadData(arquivo.Diretorio);
            conemb = PROCEDAHelper.ObterCONEMB(file);

            foreach (var cabecalho in conemb?.Cabecalhos)
            {
                var configs = ObterConfigs(cabecalho.Transportadora.CNPJ);
                foreach (var conhecimento in cabecalho.Transportadora.Conhecimentos)
                {

                    var arquivoExiste = _importacaoCteRepository
                                                .GetAll(x => x.Chave == cabecalho.Id_Documento)
                                                .FirstOrDefault();

                    if (arquivoExiste != null)
                        throw new ApplicationException($"CONEMB duplicado, a chave {cabecalho.Id_Documento} já foi importada (ImportacaoCteId: {arquivoExiste.Id})");
                    else
                    {
                        ImportacaoCte importacaoCte = new ImportacaoCte(conhecimento, arquivo.Id, cabecalho.Transportadora.CNPJ, configs);

                        try
                        {
                            importacaoCte.Validate();
                        }
                        catch (DomainSummaryException ex)
                        {
                            SalvarListaImportacaoArquivoCriticas(arquivo.Id, EnumImportacaoArquivoTipo.CONEMB, ex, conhecimento.Linha);
                        }

                        PreencherTransportadorIdArquivo(arquivo, importacaoCte.CNPJTransportador);

                        _importacaoCteRepository.Save(importacaoCte);
                    }
                }
            }

        }
        public void ProcessarArquivosConemb()
        {
            throw new NotImplementedException();
        }

        #region Metodos Auxiliares
        private IFormFile ReturnFormFile(FileStream result)
        {
            var ms = new MemoryStream();
            try
            {
                result.CopyTo(ms);
                return new FormFile(ms, 0, ms.Length, "file", "file");
            }
            catch (Exception e)
            {
                ms.Dispose();
                throw;
            }
            finally
            {
                ms.Dispose();
            }
        }
        private string GetMimeType(EnumImportacaoArquivoTipo tipoArquivo)
        {
            string mimeType = null;
            if (tipoArquivo == EnumImportacaoArquivoTipo.CTe)
                mimeType = MimeTypes.Application.Xml;
            if (tipoArquivo == EnumImportacaoArquivoTipo.CONEMB)
                mimeType = MimeTypes.Text.Plain;

            return mimeType;
        }
        private void ValidarExtensaoArquivo(string nomeArquivo, EnumImportacaoArquivoTipo tipo)
        {
            var extension = Path.GetExtension(nomeArquivo);

            if (string.IsNullOrEmpty(extension))
                throw new ApplicationException($"Extens�o de arquivo nulo ou inv�lido: {extension}");

            switch (tipo)
            {
                case EnumImportacaoArquivoTipo.CTe:
                    {
                        if (extension != ".xml")
                            throw new ApplicationException($"Erro: Extens�o do arquivo divergente do informado, extens�o esperada: '.xml'. Recebido: {extension}");
                        break;
                    }
                case EnumImportacaoArquivoTipo.CONEMB:
                    {
                        if (extension != ".txt")
                            throw new ApplicationException($"Erro: Extens�o do arquivo divergente do informado, extens�o esperada: '.txt'. Recebido: {extension}");
                        break;
                    }
                default:
                    throw new ApplicationException("Erro: Necessario informar o tipo do arquivo.");
            }

        }
        private EnumImportacaoArquivoTipo? ObterExtensaoArquivo(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            switch (extension)
            {
                case ".xml": return EnumImportacaoArquivoTipo.CTe;
                case ".txt": return EnumImportacaoArquivoTipo.CONEMB;
                default:
                    {
                        new ApplicationException($"Erro: Extensão do arquivo inválido: {extension}.");
                        return null;
                    }
            }
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
        private List<ConfiguracaoCteTransportador> ObterConfigs(string cnpj)
        {
            if (_transportadores == null)
                _transportadores = _transportadorCnpjRepository.ObterTransportadorCnpj(0);

            if (_configuracoesCTe == null)
                _configuracoesCTe = _configCteTransportadorRepository.GetAll().ToList();

            List<ConfiguracaoCteTransportador> list = new List<ConfiguracaoCteTransportador>();
            var transportador = _transportadores.Where(x => x.CNPJ == cnpj).FirstOrDefault();
            if (transportador != null)
                list.AddRange(_configuracoesCTe.Where(x => x.TransportadorCnpjId == transportador.Id));
            return list;
        }

        private EnumImportacaoArquivoTipoItem ClassificaArquivo(InfCte cte)
        {
            string caracteristica = cte.Complemento?.Caracteristica;

            if (string.IsNullOrEmpty(caracteristica))
                return EnumImportacaoArquivoTipoItem.Entrega;

            var transporador = _transportadores
                .Where(transportador => transportador.CNPJ.Equals(cte.Emitente.CNPJ))
                .FirstOrDefault() ?? new TransportadorCnpj();

            var lstContratoTransportadorArquivoTipo = _contratoTransportadorArquivoTipoRepository
                .GetAll(contratoArquivo => contratoArquivo.TransportadorId == transporador.TransportadorId);

            var arquivoTipoItem = lstContratoTransportadorArquivoTipo
                .FirstOrDefault(contratoTranpostadorArquivoTipo => contratoTranpostadorArquivoTipo.Alias.Equals(caracteristica))?.ImportacaoArquivoTipoItemId ?? default;

            if (arquivoTipoItem == default)
            {
                //throw new ArgumentException($"Nenhum alias compativel com a classificacao: {caracteristica}");
                return EnumImportacaoArquivoTipoItem.Entrega;                
            }

            return (EnumImportacaoArquivoTipoItem)arquivoTipoItem;
        }

        private void SalvarListaImportacaoArquivoCriticas(int arquivoId, EnumImportacaoArquivoTipo enumImportacaoArquivoTipo, DomainSummaryException ex, int? linha = null)
        {
            List<ImportacaoArquivoCritica> lstImportacaoArquivoCritica = new List<ImportacaoArquivoCritica>();

            switch (enumImportacaoArquivoTipo)
            {
                case EnumImportacaoArquivoTipo.CTe:
                    lstImportacaoArquivoCritica = ex.Exceptions.Select(excecao => new ImportacaoArquivoCritica(arquivoId, excecao.Message, linha, excecao.Arguments[0].ToString())).ToList();
                    break;
                case EnumImportacaoArquivoTipo.CONEMB:
                    lstImportacaoArquivoCritica = ex.Exceptions.Select(excecao => new ImportacaoArquivoCritica(arquivoId, excecao.Message, linha, excecao.Reference)).ToList();
                    break;
            }


            _importacaoArquivoCriticaRepository.SaveRange(lstImportacaoArquivoCritica);
        }

        private void PreencherTransportadorIdArquivo(ImportacaoArquivo arquivo, string cnpjTransportador)
        {
            var transportador = _transportadores.Where(x => x.CNPJ == cnpjTransportador).FirstOrDefault();

            arquivo.AtualizarTransportadorId(transportador?.TransportadorId);
        }

        #endregion

    }
}
