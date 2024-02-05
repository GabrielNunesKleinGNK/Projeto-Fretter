using ExcelDataReader;
using Fretter.Api.Helpers.Atributes;
using Fretter.Api.Models;
using Fretter.Domain.Config;
using Fretter.Domain.Dto.CTe;
using Fretter.Domain.Dto.Fusion;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fusion;
using Fretter.Domain.Enum;
using Fretter.Domain.Exceptions;
using Fretter.Domain.Extensions;
using Fretter.Domain.Helpers.Proceda;
using Fretter.Domain.Helpers.Proceda.Entidades;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Repository.Fusion;
using Fretter.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Options;
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
    public class AtualizacaoTabelasCorreiosService<TContext> : ServiceBase<TContext, TabelasCorreiosArquivo>, IAtualizacaoTabelasCorreiosService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        public new readonly IAtualizacaoTabelasCorreiosRepository<TContext> _repository;
        public readonly IBlobStorageService _blobStorageService;
        private readonly BlobStorageConfig _blobConfig;
        public AtualizacaoTabelasCorreiosService(IAtualizacaoTabelasCorreiosRepository<TContext> repository,
                                        IUnitOfWork<TContext> unitOfWork, IUsuarioHelper user,
                                        IBlobStorageService blobStorageService,
                                        IOptions<BlobStorageConfig> blobConfig) : base(repository, unitOfWork, user)
        {
            _blobConfig = blobConfig.Value;
            _blobStorageService = blobStorageService;
            _blobStorageService.InitBlob(_blobConfig.ConnectionString, _blobConfig.TabelasCorreiosName);
            _repository = repository;
        }

        public override IPagedList<TabelasCorreiosArquivo> GetPaginated(QueryFilter filter, int start, int limit, bool orderByDescending)
        {
            filter = null;
            return _repository.GetPaginated(filter, start, limit, orderByDescending);
        }

        public async Task<TabelasCorreiosArquivo> UploadArquivo(IFormFile uploadModel)
        {
            string novoGuid = Guid.NewGuid().ToString("N");
            string fileName = $"{novoGuid}{Path.GetExtension(uploadModel.FileName)}";
            byte[] fileBytes = ObterBytes(uploadModel);

            var arquivo = await ImportarArquivo(fileBytes, fileName);
            return arquivo;
        }
        public async Task<TabelasCorreiosArquivo> ImportarArquivo(byte[] bytes, string fileName)
        {
            try
            {
                EnumTabelaArquivoStatus status = EnumTabelaArquivoStatus.Aguardando;
                string mimeType = GetMimeType(fileName);
                string urlFile = await _blobStorageService.UploadFileToBlobAsync($"TabelasCorreios", fileName, bytes, mimeType);
                TabelasCorreiosArquivo importacaoArquivo = new TabelasCorreiosArquivo(0, status.GetHashCode(), fileName, urlFile);

                importacaoArquivo.Validate();
                return  _repository.Save(importacaoArquivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task ProcessarTabelasArquivo()
        {
            string erro = string.Empty;
            TabelasCorreiosArquivo arquivo = _repository.GetAll(x => x.TabelaArquivoStatusId == EnumTabelaArquivoStatus.Aguardando.GetHashCode()).FirstOrDefault();
            

            if (arquivo != null)
            {
                UpdateStatus:
                {
                    arquivo.AtualizarStatus(EnumTabelaArquivoStatus.Processando.GetHashCode());
                    _repository.Update(arquivo);
                    _unitOfWork.Commit();
                }
                
                if (arquivo.TabelaArquivoStatusId == EnumTabelaArquivoStatus.Aguardando.GetHashCode())
                    goto UpdateStatus;

                var file = await _blobStorageService.GetFile(arquivo.Url);
                string passoEmExecucao = string.Empty;
                Dto.Fusion.AtualizacaoTabelasCorreios atualizacaoTabelasCorreios = new Dto.Fusion.AtualizacaoTabelasCorreios();
                try
                {
                    using (var reader = ExcelReaderFactory.CreateReader(file))
                    {
                        int contadorLinhas = 0, contadorGuias = 1;
                        string area = string.Empty, uf = string.Empty;
                        do
                        {
                            while (reader.Read()) //Each ROW
                            {
                                var capitalObjeto = new Dto.Fusion.ImportacaoCapitais();
                                if (contadorLinhas > 3 && reader.GetValue(2) != null) //Valida se é o Header e se o registro de leitura continua está em branco.
                                {
                                    passoEmExecucao = $"Leitura das Capitais, linha {contadorLinhas + 1}";

                                    area = reader.GetValue(0) != null ? reader.GetValue(0).ToString() : area;
                                    uf = reader.GetValue(1) != null ? reader.GetValue(1).ToString() : uf;

                                    capitalObjeto.cidadesdaAreadeinfluência = area;
                                    capitalObjeto.uF = uf.ToLettersOnly();
                                    capitalObjeto.cidade = reader.GetValue(2).ToString();
                                    capitalObjeto.cEPInicial = reader.GetValue(3).ToString();
                                    capitalObjeto.cEPFinal = reader.GetValue(4).ToString();
                                    capitalObjeto.column6 = reader.GetValue(5)?.ToString();

                                    atualizacaoTabelasCorreios.listCapitais.Add(capitalObjeto);
                                }
                                contadorLinhas++;
                            }

                            reader.NextResult();//Move to NEXT SHEET
                            contadorLinhas = 0;
                            contadorGuias++;

                            while (reader.Read()) //Each ROW
                            {
                                var localObjeto = new Dto.Fusion.ImportacaoLocal();
                                if (contadorLinhas > 4 && reader.GetValue(0) != null) //Valida se é o Header e se o registro de leitura continua está em branco.
                                {
                                    passoEmExecucao = $"Leitura dos Locais, linha {contadorLinhas + 1}";

                                    localObjeto.ufOrigem = reader.GetValue(0).ToString().ToLettersOnly();
                                    localObjeto.municípioOrigem = reader.GetValue(1).ToString();
                                    localObjeto.codigoIBGEOrigem = reader.GetValue(2).ToString();
                                    localObjeto.cepOrigemInicio = reader.GetValue(3).ToString();
                                    localObjeto.cepOrigemFim = reader.GetValue(4).ToString();
                                    localObjeto.ufDestino = reader.GetValue(5).ToString().ToLettersOnly();
                                    localObjeto.municípioDestino = reader.GetValue(6).ToString();
                                    localObjeto.codigoIBGEDestino = reader.GetValue(7).ToString();
                                    localObjeto.cepDestinoInicio = reader.GetValue(8).ToString();
                                    localObjeto.cepDestinoFim = reader.GetValue(9).ToString();
                                    localObjeto.nivel = reader.GetValue(10).ToString();

                                    atualizacaoTabelasCorreios.listLocais.Add(localObjeto);
                                }

                                contadorLinhas++;
                            }

                            reader.NextResult();//Move to NEXT SHEET
                            contadorLinhas = 0;
                            contadorGuias++;

                            while (reader.Read()) //Each ROW
                            {
                                var divisaObjeto = new Dto.Fusion.ImportacaoDivisa();
                                if (contadorLinhas > 4 && reader.GetValue(0) != null) //Valida se é o Header e se o registro de leitura continua está em branco.
                                {
                                    passoEmExecucao = $"Leitura das Divisas, linha {contadorLinhas + 1}";

                                    divisaObjeto.ufOrigem = reader.GetValue(0).ToString().ToLettersOnly();
                                    divisaObjeto.municípioOrigem = reader.GetValue(1).ToString();
                                    divisaObjeto.codigoIBGEOrigem = reader.GetValue(2).ToString();
                                    divisaObjeto.cepOrigemInicio = reader.GetValue(3).ToString();
                                    divisaObjeto.cepOrigemFim = reader.GetValue(4).ToString();
                                    divisaObjeto.ufDestino = reader.GetValue(5).ToString().ToLettersOnly();
                                    divisaObjeto.municípioDestino = reader.GetValue(6).ToString();
                                    divisaObjeto.codigoIBGEDestino = reader.GetValue(7).ToString();
                                    divisaObjeto.cepDestinoInicio = reader.GetValue(8).ToString();
                                    divisaObjeto.cepDestinoFim = reader.GetValue(9).ToString();
                                    divisaObjeto.nivel = reader.GetValue(10).ToString();

                                    atualizacaoTabelasCorreios.listDivisas.Add(divisaObjeto);
                                }
                                contadorLinhas++;
                            }

                            reader.NextResult();//Move to NEXT SHEET
                            contadorLinhas = 0;
                            contadorGuias++;

                            while (reader.Read()) //Each ROW
                            {
                                var estadualObjeto = new Dto.Fusion.ImportacaoEstadual();
                                if (contadorLinhas > 4 && reader.GetValue(0) != null) //Valida se é o Header e se o registro de leitura continua está em branco.
                                {
                                    passoEmExecucao = $"Leitura dos Estaduais, linha {contadorLinhas + 1}";

                                    estadualObjeto.ufOrigem = reader.GetValue(0).ToString().ToLettersOnly();
                                    estadualObjeto.municípioOrigem = reader.GetValue(1).ToString();
                                    estadualObjeto.codigoIBGEOrigem = reader.GetValue(2).ToString();
                                    estadualObjeto.cepOrigemInicio = reader.GetValue(3).ToString();
                                    estadualObjeto.cepOrigemFim = reader.GetValue(4).ToString();
                                    estadualObjeto.ufDestino = reader.GetValue(5).ToString().ToLettersOnly();
                                    estadualObjeto.municípioDestino = reader.GetValue(6).ToString();
                                    estadualObjeto.codigoIBGEDestino = reader.GetValue(7).ToString();
                                    estadualObjeto.cepDestinoInicio = reader.GetValue(8).ToString();
                                    estadualObjeto.cepDestinoFim = reader.GetValue(9).ToString();
                                    estadualObjeto.nivel = reader.GetValue(10).ToString();

                                    atualizacaoTabelasCorreios.listEstaduais.Add(estadualObjeto);
                                }
                                contadorLinhas++;
                            }

                            reader.NextResult();//Move to NEXT SHEET
                            contadorLinhas = 0;
                            contadorGuias++;

                            while (reader.Read()) //Each ROW
                            {
                                var interiorObjeto = new Dto.Fusion.ImportacaoInterior();
                                if (contadorLinhas > 4 && reader.GetValue(0) != null) //Valida se é o Header e se o registro de leitura continua está em branco.
                                {
                                    passoEmExecucao = $"Leitura dos Interiores, linha {contadorLinhas + 1}";

                                    interiorObjeto.UF_Origem = reader.GetValue(0).ToString().ToLettersOnly();
                                    interiorObjeto.Município_Origem = reader.GetValue(1).ToString();
                                    interiorObjeto.CodigoIBGE_Origem = reader.GetValue(2).ToString();
                                    interiorObjeto.CepOrigemInicio = reader.GetValue(3).ToString();
                                    interiorObjeto.CepOrigemFim = reader.GetValue(4).ToString();
                                    interiorObjeto.UF_Destino = reader.GetValue(5).ToString().ToLettersOnly();
                                    interiorObjeto.Município_Destino = reader.GetValue(6).ToString();
                                    interiorObjeto.CodigoIBGE_Destino = reader.GetValue(7).ToString();
                                    interiorObjeto.CepDestinoInicio = reader.GetValue(8).ToString();
                                    interiorObjeto.CepDestinoFim = reader.GetValue(9).ToString();
                                    interiorObjeto.Nivel = reader.GetValue(10).ToString();

                                    atualizacaoTabelasCorreios.listInteriores.Add(interiorObjeto);
                                }
                                contadorLinhas++;
                            }

                            reader.NextResult(); //Move to NEXT SHEET
                            contadorLinhas = 0;
                            contadorGuias++;

                            while (reader.Read()) //Each ROW
                            {
                                var matrizObjeto = new Dto.Fusion.ImportacaoMatriz();
                                if (contadorLinhas > 2 && reader.GetValue(1) != null) //Valida se é o Header e se o registro de leitura continua está em branco.
                                {
                                    passoEmExecucao = $"Leitura das Matrizes, linha {contadorLinhas + 1}";

                                    matrizObjeto.id = contadorLinhas - 2;
                                    matrizObjeto.uf = reader.GetValue(1).ToString().ToLettersOnly();
                                    matrizObjeto.ac = reader.GetValue(2).ToString();
                                    matrizObjeto.al = reader.GetValue(3).ToString();
                                    matrizObjeto.am = reader.GetValue(4).ToString();
                                    matrizObjeto.ap = reader.GetValue(5).ToString();
                                    matrizObjeto.ba = reader.GetValue(6).ToString();
                                    matrizObjeto.ce = reader.GetValue(7).ToString();
                                    matrizObjeto.df = reader.GetValue(8).ToString();
                                    matrizObjeto.es = reader.GetValue(9).ToString();
                                    matrizObjeto.go = reader.GetValue(10).ToString();
                                    matrizObjeto.ma = reader.GetValue(11).ToString();
                                    matrizObjeto.mg = reader.GetValue(12).ToString();
                                    matrizObjeto.ms = reader.GetValue(13).ToString();
                                    matrizObjeto.mt = reader.GetValue(14).ToString();
                                    matrizObjeto.pa = reader.GetValue(15).ToString();
                                    matrizObjeto.pb = reader.GetValue(16).ToString();
                                    matrizObjeto.pe = reader.GetValue(17).ToString();
                                    matrizObjeto.pi = reader.GetValue(18).ToString();
                                    matrizObjeto.pr = reader.GetValue(19).ToString();
                                    matrizObjeto.rj = reader.GetValue(20).ToString();
                                    matrizObjeto.rn = reader.GetValue(21).ToString();
                                    matrizObjeto.ro = reader.GetValue(22).ToString();
                                    matrizObjeto.rr = reader.GetValue(23).ToString();
                                    matrizObjeto.rs = reader.GetValue(24).ToString();
                                    matrizObjeto.sc = reader.GetValue(25).ToString();
                                    matrizObjeto.se = reader.GetValue(26).ToString();
                                    matrizObjeto.sp = reader.GetValue(27).ToString();
                                    matrizObjeto.to = reader.GetValue(28).ToString();

                                    atualizacaoTabelasCorreios.listMatrizes.Add(matrizObjeto);
                                }
                                contadorLinhas++;
                            }

                        } while (reader.ResultsCount < contadorGuias);

                        reader.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    erro = $"Erro na leitura do arquivo. {passoEmExecucao}. Message: { ex.Message}";
                    arquivo.AtualizarStatus(EnumTabelaArquivoStatus.Erro.GetHashCode());
                    arquivo.AtualizarErro(erro.Truncate(4096));
                }

                if (string.IsNullOrEmpty(erro))
                {
                    DateTime? importacao = null, atualizacao = null;
                    bool sucessoImportacao = false, sucessoAtualizacao = false;
                    _repository.BeginTransaction();
                    try
                    {
                        sucessoImportacao = _repository.ImportarDadosTabelasTemps(
                             CollectionHelper.ConvertTo(atualizacaoTabelasCorreios.listCapitais),
                             CollectionHelper.ConvertTo(atualizacaoTabelasCorreios.listLocais),
                             CollectionHelper.ConvertTo(atualizacaoTabelasCorreios.listDivisas),
                             CollectionHelper.ConvertTo(atualizacaoTabelasCorreios.listEstaduais),
                             CollectionHelper.ConvertTo(atualizacaoTabelasCorreios.listInteriores),
                             CollectionHelper.ConvertTo(atualizacaoTabelasCorreios.listMatrizes)
                             );

                        importacao = DateTime.Now;
                    }
                    catch (Exception ex)
                    {
                        _repository.RollbackTransaction();

                        erro = $"Erro ao salvar dados importados nas tabelas temporárias. Message: {ex.Message}";
                        arquivo.AtualizarStatus(EnumTabelaArquivoStatus.Erro.GetHashCode());
                        arquivo.AtualizarErro(erro.Truncate(4096));
                    }

                    if (sucessoImportacao)
                        try
                        {
                            sucessoAtualizacao = _repository.AtualizarTabelasFinais();
                            atualizacao = DateTime.Now;
                        }
                        catch (Exception ex)
                        {
                            _repository.RollbackTransaction();

                            erro = $"Erro ao atualizar tabelas finais. Message: {ex.Message}";
                            arquivo.AtualizarStatus(EnumTabelaArquivoStatus.Erro.GetHashCode());
                            arquivo.AtualizarErro(erro.Truncate(4096));
                        }

                    if (sucessoImportacao && sucessoAtualizacao)
                    {
                        _repository.CommitTransaction();
                        arquivo.AtualizarStatus(EnumTabelaArquivoStatus.Processado.GetHashCode());
                    }


                    arquivo.AtualizarDataImportacaoDados(importacao);
                    arquivo.AtualizarDataAtualizacaoTabelas(atualizacao);
                    _repository.Update(arquivo);
                    _unitOfWork.Commit();
                }
                else
                {
                    _repository.Update(arquivo);
                    _unitOfWork.Commit();

                    throw new Exception(erro);
                }
            }
            
        }

        #region Metodos Auxiliares

        private string GetMimeType(string fileName)
        {
            var extension = Path.GetExtension(fileName);

            switch (extension.ToLower())
            {
                case ".xlsx": return MimeTypes.Application.Xlsx;
                case ".xls": return MimeTypes.Application.Xls;
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
        #endregion

    }
}
