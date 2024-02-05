using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Service;
using System.Linq;
using System.Collections.Generic;
using Fretter.Domain.Dto.TabelaArquivo;
using System;
using System.IO;
using System.Threading.Tasks;
using Fretter.Domain.Config;
using Microsoft.Extensions.Options;
using static Fretter.Domain.Helpers.XmlHelper;
using Newtonsoft.Json;
using Fretter.Domain.Interfaces.Repository.Fusion;
using ExcelDataReader;
using System.Data;
using Fretter.Domain.Extensions;
using Fretter.Domain.Helpers;
using Fretter.Domain.Enum;
using Fretter.Domain.Entities.Fusion;
using Fretter.Domain.Dto.EntregaDevolucao;
using Fretter.Api.Helpers.Atributes;
using Fretter.Domain.Dto.OcorrenciaArquivo;

namespace Fretter.Domain.Services
{
    public class OcorrenciaArquivoService<TContext> : ServiceBase<TContext, OcorrenciaArquivo>, IOcorrenciaArquivoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IOcorrenciaArquivoRepository<TContext> _repository;
        public readonly IBlobStorageService _blobStorageService;
        public readonly IEntregaOcorrenciaService<TContext> _entregaOcorrenciaService;
        private readonly BlobStorageConfig _blobConfig;
        private Stream FileStream { get; set; }

        public OcorrenciaArquivoService(IOcorrenciaArquivoRepository<TContext> repository,
            IEntregaOcorrenciaService<TContext> entregaOcorrenciaService,
            IBlobStorageService blobStorageService,
            IOptions<BlobStorageConfig> blobConfig,
            IUnitOfWork<TContext> unitOfWork, IUsuarioHelper user) : base(repository, unitOfWork, user)
        {
            _repository = repository;
            _entregaOcorrenciaService = entregaOcorrenciaService;
            _blobConfig = blobConfig.Value;
            _blobStorageService = blobStorageService;
            _blobStorageService.InitBlob(_blobConfig.ConnectionString, _blobConfig.EmpresaContainerName);
        }

        public async Task<int> ProcessarOcorrenciaArquivo()
        {
            var listaArquivos = _repository.GetOcorrenciaArquivoProcessamentos();

            if (listaArquivos?.FirstOrDefault() == null)
                return 0;

            var retMsg = new ListImportacaoExcelMsg();
            var arquivo = listaArquivos.First();

            try
            {
                _repository.AtualizarOcorrenciaArquivo(arquivo.Id, Enum.EnumTabelaArquivoStatus.Processando);

                FileStream = await _blobStorageService.GetFile(arquivo.Url);

                await ProcessarArquivoOcorrencia(arquivo, retMsg);

                var status = retMsg.Any(c => c.Error) ? Enum.EnumTabelaArquivoStatus.Erro : Enum.EnumTabelaArquivoStatus.Processado;

                _repository.AtualizarOcorrenciaArquivo(arquivo.Id, status, null,
                    retMsg.Count(c => !c.Error), retMsg.Count(c => c.Error));
            }
            catch (Exception e)
            {
                retMsg.Add(e.Message, true, null);
                _repository.AtualizarOcorrenciaArquivo(arquivo.Id, Enum.EnumTabelaArquivoStatus.Erro, string.Join(',', retMsg),
                    retMsg.Count(c => !c.Error), retMsg.Count(c => c.Error));
            }
            finally
            {
                FileStream.Dispose();
            }
            return 1;
        }

        private async Task<bool> ProcessarArquivoOcorrencia(OcorrenciaArquivoDto ocorrenciaArquivo, ListImportacaoExcelMsg retMsg)
        {
            var listaDeParaEmpresa = await _entregaOcorrenciaService.ObterDePara(ocorrenciaArquivo.EmpresaId);
            var listaOcorrenciasEmpresaEnvio = new List<EntregaOcorrencia>();

            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(FileStream))
            {
                int qtdTotalRegistros = reader.RowCount;
                Console.WriteLine($"Quantidade Total de Registros: {qtdTotalRegistros}");

                int linha = 1;
                int nextPercent = 0;
                while (reader.Read())
                {
                    if (linha > 1)
                    {
                        //Somente pra printar a Evolução em % processado
                        var currentPercent = ((100 * linha) / qtdTotalRegistros);
                        if (currentPercent >= nextPercent)
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm")} - Quantidade PROCESSADA de Registros: {linha} = {currentPercent}%");
                            nextPercent = currentPercent - (currentPercent % 1) + 10;

                            _repository.AtualizarOcorrenciaArquivo(ocorrenciaArquivo.Id, 0, null, null, null, qtdTotalRegistros, currentPercent);
                        }

                        var codigoOcorrencia = reader.GetValue(7).ToString();
                        DateTime.TryParse(reader.GetValue(8).ToString(), out DateTime dataTratada);
                        var idEntrega = reader.GetValue(0).ToString().ExcelDataToInt(linha, 0, retMsg);

                        var deParaAtual = listaDeParaEmpresa?.FirstOrDefault(x => x.Codigo.Trim() == codigoOcorrencia.Trim());

                        var ocorrencia = new EntregaOcorrencia(
                                                Id: -1,
                                                OcorrenciaId: deParaAtual?.Id,
                                                Ocorrencia: deParaAtual?.Descricao,
                                                DataOcorrencia: dataTratada,
                                                EntregaId: idEntrega
                                                );

                        listaOcorrenciasEmpresaEnvio.Add(ocorrencia);
                    }
                    linha++;
                }

                var retorno = await _entregaOcorrenciaService.Inserir(listaOcorrenciasEmpresaEnvio, ocorrenciaArquivo.EmpresaId);

                if (retorno.Count > 0)
                {

                    foreach (var alertas in retorno)
                        retMsg.Add($"Entrega:{alertas.EntregaId}-{alertas.RetornoValidacao}", false, null, null);

                    var arquivoUrl = await MontarESalvarArquivoDeAlertas(retorno, ocorrenciaArquivo.EmpresaId);
                    _repository.AtualizarOcorrenciaArquivo(ocorrenciaArquivo.Id, 0, arquivoUrl, null, null, null, null);
                }
            }

            return true;
        }

        private async Task<string> MontarESalvarArquivoDeAlertas(List<EntregaOcorrencia> ocorrencias, int empresaId)
        {
            var listGenerica = new List<object>();
            foreach (var ocorrencia in ocorrencias)
            {
                listGenerica.Add(new
                {
                    EntregaFusion = ocorrencia.EntregaId,
                    Ocorrencia = ocorrencia.Ocorrencia,
                    Sigla = ocorrencia.Sigla
                });
            }

            var arquivo = listGenerica.ConvertToXlsx("Alertas", true);
            string urlFile = await _blobStorageService.UploadFileToBlobAsync($"{empresaId.ToString().PadLeft(6, '0')}_{DateTime.Now.ToString()}", "Arquivo alertas", arquivo, MimeTypes.Application.Xlsx);

            return urlFile;
        }
    }
}