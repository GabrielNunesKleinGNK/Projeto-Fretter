using ExcelDataReader;
using Fretter.Api.Helpers.Atributes;
using Fretter.Domain.Config;
using Fretter.Domain.Entities;
using Fretter.Domain.Helpers;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fretter.Domain.Services
{
    public class EmpresaService<TContext> : ServiceBase<TContext, Empresa>, IEmpresaService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly FretterConfig _fretterConfig;
        private readonly IEmpresaRepository<TContext> _repository;
        private readonly IEmpresaConfigRepository<TContext> _empresaConfigRepository;
        private readonly IEmpresaSegmentoRepository<TContext> _empresaSegmentoRepository;
        private readonly ICanalRepository<TContext> _empresaCanalRepository;
        private readonly ICnpjDetalheRepository<TContext> _cnpjDetalheRepository;
        private readonly ICanalConfigRepository<TContext> _canalConfigRepository;
        private readonly IEmpresaTransportadorConfigRepository<TContext> _empresaTransportadorRepository;
        private readonly IMicroServicoRepository<TContext> _microServicoRepository;
        private readonly IEmpresaImportacaoRepository<TContext> _empresaImportacaoRepository;
        private readonly IRepositoryBase<TContext, EmpresaImportacaoDetalhe> _empresaImportacaoDetalheRepository;
        private readonly ICanalVendaService<TContext> _canalVendaService;
        public readonly IBlobStorageService _blobStorageService;
        private readonly BlobStorageConfig _blobConfig;

        public EmpresaService(IOptions<FretterConfig> fretterConfig,
            IEmpresaRepository<TContext> Repository,
            IEmpresaConfigRepository<TContext> empresaConfigRepository,
            IEmpresaSegmentoRepository<TContext> empresaSegmentoRepository,
            ICanalRepository<TContext> empresaCanalRepository,
            ICnpjDetalheRepository<TContext> cnpjDetalheRepository,
            ICanalConfigRepository<TContext> canalConfigRepository,
            IEmpresaTransportadorConfigRepository<TContext> empresaTransportadorRepository,
            IMicroServicoRepository<TContext> microServicoRepository,
            IEmpresaImportacaoRepository<TContext> empresaImportacaoRepository,
            IRepositoryBase<TContext, EmpresaImportacaoDetalhe> empresaImportacaoDetalheRepository,
            ICanalVendaService<TContext> canalVendaService,
            IBlobStorageService blobStorageService,
            IOptions<BlobStorageConfig> blobConfig,
            IUnitOfWork<TContext> unitOfWork,
            IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _fretterConfig = fretterConfig.Value;
            _repository = Repository;
            _empresaConfigRepository = empresaConfigRepository;
            _empresaSegmentoRepository = empresaSegmentoRepository;
            _empresaCanalRepository = empresaCanalRepository;
            _cnpjDetalheRepository = cnpjDetalheRepository;
            _canalConfigRepository = canalConfigRepository;
            _empresaTransportadorRepository = empresaTransportadorRepository;
            _microServicoRepository = microServicoRepository;
            _empresaImportacaoRepository = empresaImportacaoRepository;
            _empresaImportacaoDetalheRepository = empresaImportacaoDetalheRepository;
            _canalVendaService = canalVendaService;

            _blobConfig = blobConfig.Value;
            _blobStorageService = blobStorageService;
            _blobStorageService.InitBlob(_blobConfig.ConnectionString, _blobConfig.EmpresaContainerName);
        }

        public byte[] DownloadArquivo(int arquivoId)
        {
            var empresaArquivo = _empresaImportacaoRepository.GetEmpresaImportacaoDetalhePorArquivo(arquivoId);

            var listGenerica = new List<object>();

            foreach (var empresa in empresaArquivo.Detalhes)
                listGenerica.Add(new { Nome = empresa.Nome, Cnpj = empresa.Cnpj, Cep = empresa.CEP, CepPostagem = empresa.CEP, UF = empresa.UF, Email = empresa.Email, Token = empresa.Token });

            return listGenerica.ConvertToXlsx(empresaArquivo.Nome, true);
        }
        public async Task ProcessarUploadEmpresa(IFormFile file)
        {
            string fileName = $"{Guid.NewGuid().ToString("N")}{Path.GetExtension(file.FileName)}";
            byte[] fileBytes = ObterBytes(file);
            string urlFile = await _blobStorageService.UploadFileToBlobAsync($"{this._fretterConfig.EmpresaImportacaoReferenciaId.ToString().PadLeft(6, '0')}", fileName, fileBytes, MimeTypes.Application.Xlsx);

            var empresaImportacao = new EmpresaImportacaoArquivo(0, file.FileName, string.Empty, this._fretterConfig.EmpresaImportacaoReferenciaId, urlFile);
            empresaImportacao.AtualizarUsuarioCriacao(this._user.UsuarioLogado.Id);
            empresaImportacao.AtualizarDataCriacao(DateTime.Now);
            empresaImportacao.AtualizarProcessado(false);

            empresaImportacao = _empresaImportacaoRepository.Save(empresaImportacao);
            _unitOfWork.Commit();

            List<Dto.Fusion.EmpresaImportacao> listEmpresas = ConverterArquivoDto(file);
            try
            {
                foreach (var emp in listEmpresas)
                {
                    string mensagemProcessamento = string.Empty;
                    var empresaImportacaoDetalhe = new EmpresaImportacaoDetalhe(0, empresaImportacao.Id, null, string.Empty, emp.Email, emp.NomeFantasia, emp.Cnpj, emp.CEP, emp.UF, emp.SedexBalcao, emp.ApiFrete); ;
                    empresaImportacaoDetalhe.AtualizarUsuarioCriacao(this._user.UsuarioLogado.Id);
                    empresaImportacaoDetalhe = _empresaImportacaoDetalheRepository.Save(empresaImportacaoDetalhe);
                    _unitOfWork.Commit();

                    try
                    {
                        emp.ImportacaoDetalheId = empresaImportacaoDetalhe.Id;
                        emp.Cnpj = Regex.Replace(emp.Cnpj.Trim(), "[^0-9]+", "", RegexOptions.Compiled);
                        emp.Cnpj = Convert.ToDecimal(emp.Cnpj).ToString("00000000000000");

                        emp.CEPPostagem = Regex.Replace(emp.CEPPostagem.Trim(), "[^0-9]+", "", RegexOptions.Compiled);
                        emp.CEP = Regex.Replace(emp.CEP.Trim(), "[^0-9]+", "", RegexOptions.Compiled);

                        if (!Domain.Helpers.Validations.ValidateData.DocumentoValido(emp.Cnpj))
                            mensagemProcessamento += "Cnpj informado e invalido/inexistente. ";

                        if (!Domain.Helpers.Validations.ValidateData.EmailValido(emp.Email))
                            mensagemProcessamento += "Email informado e inválido. ";

                        if (string.IsNullOrEmpty(emp.CEPPostagem) || string.IsNullOrEmpty(emp.CEP))
                            mensagemProcessamento += "CEP/CEP Postagem informado e invalido. ";

                        if (emp.CEPPostagem.Length > 8 || emp.CEP.Length > 8)
                            mensagemProcessamento += "CEP/CEP Postagem possui quantidade de caracteres incorreta. ";

                        if (string.IsNullOrEmpty(emp.UF))
                            mensagemProcessamento += "UF informado e invalido. ";

                        if (string.IsNullOrEmpty(emp.NomeFantasia))
                            mensagemProcessamento += "Nome Fantasia informado e invalido. ";

                        if (!string.IsNullOrEmpty(mensagemProcessamento))
                        {
                            empresaImportacaoDetalhe.AtualizarDescricao(mensagemProcessamento);
                            empresaImportacaoDetalhe = _empresaImportacaoDetalheRepository.Update(empresaImportacaoDetalhe);
                            _unitOfWork.Commit();
                            continue;
                        }

                        var empresa = _repository.ObterEmpresaPeloCanalPorCnpj(emp.Cnpj);

                        if (empresa == null)
                            empresa = _repository.GetAll(t => t.Cnpj == emp.Cnpj && t.Ativo).FirstOrDefault();

                        if (empresa == null)
                        {
                            empresa = new Empresa(0, DateTime.Now, emp.NomeFantasia, emp.NomeFantasia, emp.Cnpj, Guid.NewGuid(), Guid.NewGuid(), false, Guid.NewGuid(), false, false, _fretterConfig.OrigemImportacaoId, false, false);
                            //Segmento
                            empresa.AtualizarSegmento(new EmpresaSegmento(0, DateTime.Now, emp.NomeFantasia, _fretterConfig.OrigemImportacaoId, empresa.Id, true));
                            //Canal
                            empresa.EmpresaSegmento.AtualizarCanal(new Canal(0, DateTime.Now, emp.NomeFantasia, emp.NomeFantasia, emp.Cnpj, emp.NomeFantasia, Convert.ToInt16(_fretterConfig.OrigemImportacaoId), empresa.EmpresaSegmento, empresa));
                            //Transportador
                            empresa.EmpresaTransportadorConfigs.Add(new EmpresaTransportadorConfig(0, empresa.Id, 128, 5, 1, 1, Convert.ToInt16(_fretterConfig.OrigemImportacaoId), 1, 0, 5, 1, DateTime.Now));
                            empresa.EmpresaTransportadorConfigs.Add(new EmpresaTransportadorConfig(0, empresa.Id, 3, 5, 1, 1, Convert.ToInt16(_fretterConfig.OrigemImportacaoId), 1, 0, 5, 1, DateTime.Now));
                            //Micro Servico
                            var transpPadrao = empresa.EmpresaTransportadorConfigs.Where(t => t.TransportadorId == 128).FirstOrDefault();
                            if (transpPadrao != null)
                                transpPadrao.MicroServicos.Add(new MicroServico(0, transpPadrao.Id, "FSN", 3, "Padrao FUSION", false, false));

                            var transpCorreio = empresa.EmpresaTransportadorConfigs.Where(t => t.TransportadorId == 3).FirstOrDefault();
                            if (transpCorreio != null)
                            {
                                transpCorreio.MicroServicos.Add(new MicroServico(0, transpCorreio.Id, "SDX", 3, "Sedex", false, false, false, "SEDEX"));
                                transpCorreio.MicroServicos.Add(new MicroServico(0, transpCorreio.Id, "PAC", 3, "PAC", false, false, false, "PAC"));
                            }
                            //EmpresaConfig
                            empresa.EmpresaConfigs.Add(new EmpresaConfig(empresa.Id, false, DateTime.Now, false, false, _fretterConfig.EmpresaQuantidadeTabela));
                            //CanalConfig
                            empresa.EmpresaSegmento.Canal.AtualizarCanalConfig(new CanalConfig(empresa.EmpresaSegmento.Canal.Id, false, empresa));

                            empresa = _repository.Save(empresa);
                            _unitOfWork.Commit();
                            emp.Sucesso = true;
                        }
                        else
                        {
                            var empresaConfig = _empresaConfigRepository.GetAll(false, t => t.Id == empresa.Id).FirstOrDefault();
                            if (empresaConfig == null)
                            {
                                empresaConfig = new EmpresaConfig(empresa.Id, false, DateTime.Now, false, false, _fretterConfig.EmpresaQuantidadeTabela);
                                empresaConfig = _empresaConfigRepository.Save(empresaConfig);
                            }
                            else
                            {
                                empresaConfig.AtualizarNaoUsaEndpointExterno(false);
                                empresaConfig.AtualizarQtdTabelas(_fretterConfig.EmpresaQuantidadeTabela);
                                empresaConfig.Ativar();
                                empresaConfig = _empresaConfigRepository.Update(empresaConfig);
                            }

                            var empresaSegmento = _empresaSegmentoRepository.GetAll(t => t.EmpresaId == empresa.Id).FirstOrDefault();
                            if (empresaSegmento == null)
                            {
                                empresaSegmento = new EmpresaSegmento(0, DateTime.Now, emp.NomeFantasia, _fretterConfig.OrigemImportacaoId, empresa.Id, true);
                                empresaSegmento = _empresaSegmentoRepository.Save(empresaSegmento);
                                _unitOfWork.Commit();
                            }
                            else
                            {
                                empresaSegmento.AtualizarOrigemImportacao(_fretterConfig.OrigemImportacaoId);
                                empresaSegmento = _empresaSegmentoRepository.Update(empresaSegmento);
                                _unitOfWork.Commit();
                            }

                            var empresaCanal = _empresaCanalRepository.GetAll(t => t.Cnpj == emp.Cnpj && t.Ativo).FirstOrDefault();
                            if (empresaCanal == null)
                            {
                                empresaCanal = new Canal(0, DateTime.Now, emp.NomeFantasia, emp.NomeFantasia, emp.Cnpj, emp.NomeFantasia, empresaSegmento.Id, Convert.ToInt16(_fretterConfig.OrigemImportacaoId), empresa.Id);
                                empresaCanal = _empresaCanalRepository.Save(empresaCanal);
                                _unitOfWork.Commit();
                            }
                            else
                            {
                                if (empresaCanal.Id == (empresa?.Canal?.Id ?? 0))
                                {
                                    empresa.Canal.AtualizarSegmento(empresaSegmento.Id);
                                    empresaCanal = _empresaCanalRepository.Update(empresa.Canal);
                                }
                                else
                                {
                                    empresaCanal.AtualizarSegmento(empresaSegmento.Id);
                                    empresaCanal.AtualizarEmpresa(empresa.Id);
                                    empresaCanal = _empresaCanalRepository.Update(empresaCanal);
                                }
                                _unitOfWork.Commit();
                            }

                            var canalConfig = _canalConfigRepository.GetQueryable(t => t.EmpresaId == empresa.Id && t.Id == empresaCanal.Id).FirstOrDefault();
                            if (canalConfig == null)
                            {
                                canalConfig = new CanalConfig(empresaCanal.Id, false, empresa.Id);
                                canalConfig = _canalConfigRepository.Save(canalConfig);
                            }

                            var transportadorConfig = _empresaTransportadorRepository.GetQueryable(t => t.EmpresaId == empresa.Id && (t.TransportadorId == 128 || t.TransportadorId == 3));
                            List<EmpresaTransportadorConfig> listTranspConfig = new List<EmpresaTransportadorConfig>();

                            var transportadorConfigPadrao = transportadorConfig.Where(t => t.TransportadorId == 128).FirstOrDefault();
                            var transportadorConfigCorreio = transportadorConfig.Where(t => t.TransportadorId == 3).FirstOrDefault();

                            if (transportadorConfigPadrao == null)
                            {
                                transportadorConfigPadrao = new EmpresaTransportadorConfig(0, empresa.Id, 128, 5, 1, 1, Convert.ToInt16(_fretterConfig.OrigemImportacaoId), 1, 0, 5, 1, DateTime.Now);
                                listTranspConfig.Add(transportadorConfigPadrao);
                            }

                            if (transportadorConfigCorreio == null)
                            {
                                transportadorConfigCorreio = new EmpresaTransportadorConfig(0, empresa.Id, 3, 5, 1, 1, Convert.ToInt16(_fretterConfig.OrigemImportacaoId), 1, 0, 5, 1, DateTime.Now);
                                listTranspConfig.Add(transportadorConfigCorreio);
                            }

                            if (listTranspConfig.Count > 0)
                                _empresaTransportadorRepository.SaveRange(listTranspConfig);

                            List<MicroServico> listMicroServico = new List<MicroServico>();

                            var microServicoPadrao = _microServicoRepository.GetQueryable(t => t.EmpresaTransportadorId == transportadorConfigPadrao.Id).FirstOrDefault();
                            if (microServicoPadrao == null)
                                listMicroServico.Add(new MicroServico(0, transportadorConfigPadrao.Id, "FSN", 3, "Padrao FUSION", false, false));

                            var microServicoCorreio = _microServicoRepository.GetQueryable(t => t.EmpresaTransportadorId == transportadorConfigCorreio.Id).ToList();
                            if (microServicoCorreio.Where(t => t.ServicoCodigo == "SDX").Count() <= 0)
                                listMicroServico.Add(new MicroServico(0, transportadorConfigCorreio.Id, "SDX", 3, "Sedex", false, false, false, "SEDEX"));

                            if (microServicoCorreio.Where(t => t.ServicoCodigo == "PAC").Count() <= 0)
                                listMicroServico.Add(new MicroServico(0, transportadorConfigCorreio.Id, "PAC", 3, "PAC", false, false, false, "PAC"));


                            if (listMicroServico.Count > 0)
                                _microServicoRepository.SaveRange(listMicroServico);

                            _unitOfWork.Commit();
                            emp.Sucesso = true;
                        }

                        var empresaCnpjDetalhe = _cnpjDetalheRepository.GetQueryable(t => t.Cnpj == emp.Cnpj).FirstOrDefault();
                        if (empresaCnpjDetalhe == null)
                        {
                            empresaCnpjDetalhe = new CnpjDetalhe(0, emp.Cnpj, "MATRIZ", emp.CEP, emp.UF);
                            empresaCnpjDetalhe = _cnpjDetalheRepository.Save(empresaCnpjDetalhe);
                            _unitOfWork.Commit();
                        }

                        if (empresa.Id > 0)
                            empresaImportacaoDetalhe.AtualizarEmpresa(empresa.Id);

                        empresaImportacaoDetalhe.AtualizarSucessoEmpresa(emp.Sucesso);
                        empresaImportacaoDetalhe = _empresaImportacaoDetalheRepository.Update(empresaImportacaoDetalhe);
                    }
                    catch (Exception ex)
                    {
                        empresaImportacaoDetalhe.AtualizarSucessoEmpresa(false);
                        empresaImportacaoDetalhe.AtualizarDescricao(($"Houve um erro processar Empresa.{ex.Message}{ex.InnerException}").Truncate(256));
                        empresaImportacaoDetalhe = _empresaImportacaoDetalheRepository.Update(empresaImportacaoDetalhe);
                    }
                }

                int empresasProcessadasComSucesso = listEmpresas.Where(t => t.Sucesso).ToList().Count;
                int empresasTotal = listEmpresas.Count;
                empresaImportacao.AtualizarQuantidadeEmpresa(listEmpresas.Count);
                empresaImportacao.AtualizarProcessado(true);
                empresaImportacao.AtualizarSucesso(empresasProcessadasComSucesso > 0);
                empresaImportacao.AtualizarDescricao(empresasProcessadasComSucesso < empresasTotal ? "Processado PARCIAL. Veja os detalhes." : "Processamento realizado com Sucesso.");
            }
            catch (Exception ex)
            {
                empresaImportacao.AtualizarQuantidadeEmpresa(0);
                empresaImportacao.AtualizarProcessado(true);
                empresaImportacao.AtualizarSucesso(false);
                empresaImportacao.AtualizarDescricao(($"Arquivo processado com Erro. Erro:{ex.Message} {ex.InnerException}").Truncate(2040));
            }

            empresaImportacao = _empresaImportacaoRepository.Update(empresaImportacao);
            _unitOfWork.Commit();

            var empresasProcessadas = listEmpresas.Where(t => t.Sucesso).ToList();
            if (empresasProcessadas.Count > 0)
            {
                foreach (var empresa in empresasProcessadas)
                {
                    var canalVendaDto = new Dto.Fusion.EmpresaCanalVenda()
                    {
                        Cnpj = empresa.Cnpj,
                        CanalVendaNome = _fretterConfig.CanalVendaNome,
                        TipoInterfaceId = _fretterConfig.TipoInterfaceId,
                        Modalidades = "1",
                        CorreioBalcao = empresa.SedexBalcao,
                        TokenNome = _fretterConfig.TokenNome,
                        CodigoIntegracao = empresa.ImportacaoDetalheId,
                        Cep = empresa.CEPPostagem.ToInt(),
                    };

                    canalVendaDto = _canalVendaService.ConfiguraCanalVenda(canalVendaDto);
                    var importacaoDetalhe = _empresaImportacaoDetalheRepository.Get(canalVendaDto.CodigoIntegracao);

                    if (string.IsNullOrEmpty(canalVendaDto.MensagemRetorno))
                    {
                        if (!string.IsNullOrEmpty(canalVendaDto.TokenRetorno))
                        {
                            importacaoDetalhe.AtualizarToken(canalVendaDto.TokenRetorno);
                            importacaoDetalhe.AtualizarSucessoCanal(true);

                            //Define o TipoPermissao a Empresa vai Consumir a Api de Frete
                            var retornoPermissao = _repository.ProcessaPermissaoEmpresa(empresa.Email, empresa.Cnpj, (empresa.ApiFrete ? 2 : 1));

                            if (retornoPermissao)
                            {
                                importacaoDetalhe.AtualizarSucessoPermissao(true);
                                importacaoDetalhe.AtualizarDescricao("Empresa processada com Sucesso");
                            }
                            else importacaoDetalhe.AtualizarDescricao("Houve um erro ao processar a permissão da Empresa.");

                            _empresaImportacaoDetalheRepository.Update(importacaoDetalhe);
                            _unitOfWork.Commit();
                        }
                    }
                    else
                    {
                        importacaoDetalhe.AtualizarDescricao(($"Houve um erro ao processar o Canal.Erro: {canalVendaDto.MensagemRetorno}").Truncate(256));
                        _empresaImportacaoDetalheRepository.Update(importacaoDetalhe);
                        _unitOfWork.Commit();
                    }
                }
            }
        }

        #region Metodos Auxiliares       
        private List<Dto.Fusion.EmpresaImportacao> ConverterArquivoDto(IFormFile file)
        {
            List<Dto.Fusion.EmpresaImportacao> listEmpresas = new List<Dto.Fusion.EmpresaImportacao>();
            using (var reader = ExcelReaderFactory.CreateReader(file.OpenReadStream()))
            {
                int contadorLinhas = 0;
                do
                {
                    while (reader.Read()) //Each ROW    
                    {
                        var empresaObjeto = new Dto.Fusion.EmpresaImportacao();
                        if (contadorLinhas > 0) //Header
                        {
                            empresaObjeto.NomeFantasia = reader.GetValue(0).ToString();
                            empresaObjeto.Cnpj = reader.GetValue(1).ToString();
                            empresaObjeto.CEP = reader.GetValue(2).ToString();
                            empresaObjeto.CEPPostagem = reader.GetValue(3).ToString();
                            empresaObjeto.UF = reader.GetValue(4).ToString();
                            empresaObjeto.Email = reader.GetValue(5).ToString();
                            empresaObjeto.SedexBalcao = ConverteCondicionalBoolean(reader.GetValue(6).ToString());
                            empresaObjeto.ApiFrete = ConverteCondicionalBoolean(reader.GetValue(7).ToString());

                            listEmpresas.Add(empresaObjeto);
                        }
                        contadorLinhas++;
                    }
                } while (reader.NextResult()); //Move to NEXT SHEET
            }

            return listEmpresas;

        }
        private bool ConverteCondicionalBoolean(string condicao)
        {
            condicao = Regex.Replace(condicao.ToLower().Trim(), "[^a-zã]+", "", RegexOptions.Compiled);
            switch (condicao)
            {
                case "não":
                case "nao":
                    return false;
                default:
                    return true;
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

        #endregion
    }
}
