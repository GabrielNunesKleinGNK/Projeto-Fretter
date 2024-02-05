using Fretter.Api.Helpers.Atributes;
using Fretter.Domain.Config;
using Fretter.Domain.Dto.Carrefour;
using Fretter.Domain.Dto.EntregaDevolucao;
using Fretter.Domain.Dto.Fusion.EntregaOcorrencia;
using Fretter.Domain.Dto.LogisticaReversa;
using Fretter.Domain.Dto.LogisticaReversa.Enum;
using Fretter.Domain.Entities;
using Fretter.Domain.Enum;
using Fretter.Domain.Helpers;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static WSLogisticaReversa.logisticaReversaWSClient;

namespace Fretter.Domain.Services
{
    public class EntregaOcorrenciaService<TContext> : ServiceBase<TContext, EntregaOcorrencia>, IEntregaOcorrenciaService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        private readonly IEntregaOcorrenciaRepository<TContext> _repository;
        public readonly IBlobStorageService _blobStorageService;

        private readonly BlobStorageConfig _blobConfig;
        public EntregaOcorrenciaService(IEntregaOcorrenciaRepository<TContext> repository,
                                        IUnitOfWork<TContext> unitOfWork,
                                        IBlobStorageService blobStorageService,
                                        IOptions<BlobStorageConfig> blobConfig,
                                        IUsuarioHelper user) : base(repository, unitOfWork, user)
        {
            _repository = repository;
            _user = user;

            _blobConfig = blobConfig.Value;
            _blobStorageService = blobStorageService;
            _blobStorageService.InitBlob(_blobConfig.ConnectionString, _blobConfig.EmpresaContainerName);
        }

        public async Task<List<EntregaOcorrencia>> Inserir(List<EntregaOcorrencia> ocorrencias, int? empresaId = null)
        {
            try
            {
                //remove duplicidade de entregas que estão duplicadas devido a ter mais de uma entrega saida.
                //Nesse caso a entrega Id é a mesma e a ocorrencia é amarrada ao entrega Id e não a entrega saida.
                var lstOcorrenciaSemDuplicidade = ocorrencias.GroupBy(x => new { x.OcorrenciaId, x.DataOcorrencia, x.EntregaId}).ToList();

                foreach (var ocoDuplicada in lstOcorrenciaSemDuplicidade.Where(a => a.Count() > 1))
                {
                    while (ocorrencias.Where(e => e.EntregaId == ocoDuplicada.Key.EntregaId 
                                                  && e.OcorrenciaId == ocoDuplicada.Key.OcorrenciaId 
                                                  && e.DataOcorrencia == ocoDuplicada.Key.DataOcorrencia).Count() > 1)
                    {
                        ocorrencias.Remove(ocorrencias.FirstOrDefault(e => e.EntregaId == ocoDuplicada.Key.EntregaId
                                                                           && e.OcorrenciaId == ocoDuplicada.Key.OcorrenciaId
                                                                           && e.DataOcorrencia == ocoDuplicada.Key.DataOcorrencia));
                    }
                }

                foreach (var ocorrencia in ocorrencias)
                    ValidaTrataOcorrencia(ocorrencia, empresaId);

                var ocorrenciasValidas = ocorrencias.Where(x => x.OcorrenciaStatusValidacao == true).ToList();
                var ocorrenciasNaoValidas = ocorrencias.Where(x => x.OcorrenciaStatusValidacao == false).ToList();

                if (ocorrenciasValidas.Any())
                    GravarOcorrencias(ocorrenciasValidas);

                return ocorrenciasNaoValidas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> UploadArquivoMassivo(IFormFile file)
        {
            try
            {
                string fileName = $"{Guid.NewGuid().ToString("N")}{Path.GetExtension(file.FileName)}";
                byte[] fileBytes = ObterBytes(file);
                string urlFile = await _blobStorageService.UploadFileToBlobAsync($"{_user.UsuarioLogado.EmpresaId.ToString().PadLeft(6, '0')}", fileName, fileBytes, MimeTypes.Application.Xlsx);

                _repository.InserirArquivoMassivo(fileName, urlFile, _user.UsuarioLogado.Email, _user.UsuarioLogado.EmpresaId);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<EntregaEmAbertoDto>> ObterEntregasEmAberto(EntregaEmAbertoFiltro filtro)
        {
            TrataFiltro(filtro);
            var entregas = _repository.ObterEntregasEmAberto(filtro);
            return entregas;
        }
        public async Task<List<OcorrenciaEmbarcadorDto>> ObterDePara(int? empresaId = null)
        {
            var depara = _repository.ObterDePara(empresaId ?? _user.UsuarioLogado.EmpresaId);
            return depara;
        }
        public async Task<byte[]> DownloadArquivoEntregaEmAberto(bool comEntregas, EntregaEmAbertoFiltro filtro)
        {
            filtro.EmpresaId = _user.UsuarioLogado.EmpresaId;
            var listGenerica = new List<object>();
            try
            {
                if (comEntregas)
                {
                    TrataFiltro(filtro);

                    var entregas = _repository.ObterEntregasEmAberto(filtro);
                    if (entregas.Any())
                        foreach (var entrega in entregas)
                            listGenerica.Add(new
                            {
                                EntrgaFusion = entrega.Id,
                                CodigoEntrega = $@"'{entrega.CodigoEntrega}'",
                                Transportador = entrega.Transportador,
                                NF = entrega.Nota,
                                Serie = entrega.Serie,
                                UltimaOcorrencia = entrega.UltimaOcorrencia,
                                DataUltimaOcorrencia = entrega.DataUltimaOcorrencia,
                                CodigoNovaOcorrencia = "",
                                DataNovaOcorrencia = "",
                                Observacao = "",
                            });
                    else
                        listGenerica.Add(new
                        {
                            EntrgaFusion = "",
                            CodigoEntrega = "",
                            Transportador = "",
                            NF = "",
                            Serie = "",
                            UltimaOcorrencia = "",
                            DataUltimaOcorrencia = "",
                            CodigoNovaOcorrencia = "",
                            DataNovaOcorrencia = "",
                            Observacao = "",
                        });
                }
                else
                {
                    listGenerica.Add(new
                    {
                        EntrgaFusion = "",
                        CodigoEntrega = "",
                        Transportador = "",
                        NF = "",
                        Serie = "",
                        UltimaOcorrencia = "",
                        DataUltimaOcorrencia = "",
                        CodigoNovaOcorrencia = "",
                        DataNovaOcorrencia = "",
                        Observacao = "",
                    });
                }

                return listGenerica.ConvertToXlsx("Entregas", true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private async void ValidaTrataOcorrencia(EntregaOcorrencia ocorrencia, int? empresaId = null)
        {
            var ocorrenciasInseridas = _repository.GetAll(false, o => o.EntregaId == ocorrencia.EntregaId);
            var ocorrenciasEmbarcador = await ObterDePara(empresaId);

            if (ocorrenciasInseridas.Any(x => (x.Finalizar ?? false) == true))
            {
                ocorrencia.AtualizarOcorrenciaStatusValidacao(false);
                ocorrencia.AtualizarRetornoValidacao($"A entrega (Fusion) {ocorrencia.EntregaId} ja está finalida. Não é possível realizar a inserção da oco {ocorrencia.Ocorrencia}.");
                return;
            }

            if (ocorrenciasInseridas.Any(x => x.OcorrenciaId == ocorrencia.OcorrenciaId && x.DataInclusao == ocorrencia.DataInclusao))
            {
                ocorrencia.AtualizarOcorrenciaStatusValidacao(false);
                ocorrencia.AtualizarRetornoValidacao($"Já existe a oco {ocorrencia.Ocorrencia} com a data de inserção {ocorrencia.DataInclusao} para a entrega (Fusion) {ocorrencia.EntregaId}.");
                return;
            }

            if (ocorrencia.OcorrenciaId == null || !ocorrenciasEmbarcador.Any(x => x.Id == ocorrencia.OcorrenciaId))
            {
                ocorrencia.AtualizarOcorrenciaStatusValidacao(false);
                ocorrencia.AtualizarRetornoValidacao($"O código de ocorrência inserido não pertence a empresa logada.");
                return;
            }

            var ultimaOcorrencia = ocorrenciasInseridas.OrderByDescending(x => x.Id).FirstOrDefault();

            ocorrencia.AtualizarDataOriginal(ocorrencia.DataInclusao);
            ocorrencia.AtualizarDataInclusaoAnterior(ultimaOcorrencia?.DataInclusao ?? DateTime.Now);
            ocorrencia.AtualizarDataOcorrenciaAnterior(ultimaOcorrencia?.DataOcorrencia ?? DateTime.Now);
            ocorrencia.AtualizarUsuarioInclusao(("Imp. Manual. " + _user?.UsuarioLogado?.Email ?? "Processado pela task.").Truncate(50));
            ocorrencia.AtualizarArquivoImportacao("Importação Manual");
            ocorrencia.AtualizarOcorrenciaValidada(true);
        }
        private void GravarOcorrencias(List<EntregaOcorrencia> ocorrencias)
        {
            string observacao = string.Empty;
            List<OcorrenciaEnvioDto> lstEnvio = new List<OcorrenciaEnvioDto>();
            foreach (var ocorrencia in ocorrencias)
            {
                if (string.IsNullOrEmpty(observacao))
                    observacao = ocorrencia.Complementar;

                lstEnvio.Add(new OcorrenciaEnvioDto()
                {
                    Cd_Id = -1,
                    Id_Entrega = ocorrencia.EntregaId,
                    Id_Ocorrencia = ocorrencia.OcorrenciaId,
                    Ds_Ocorrencia = ocorrencia.Ocorrencia,
                    Dt_Ocorrencia = ocorrencia.DataOcorrencia,
                    Dt_Inclusao = ocorrencia.DataInclusao,
                    Dt_Original = ocorrencia.DataOriginal,
                    Flg_OcorrenciaValidada = ocorrencia.OcorrenciaValidada,
                    Ds_ArquivoImportacao = ocorrencia.ArquivoImportacao,
                    Ds_UsuarioInclusao = ocorrencia.UsuarioInclusao,
                    Flg_OcorrenciaValidadaDe = true,
                    Id_OcorrenciaDe = ocorrencia.OcorrenciaDeId,
                    Id_Transportador = ocorrencia.TransportadorId,
                    Id_OrigemImportacao = ocorrencia.OrigemImportacaoId,
                    Dt_InclusaoAnterior = ocorrencia.DataInclusaoAnterior,
                    Dt_OcorrenciaAnterior = ocorrencia.DataOcorrenciaAnterior
                });
            }

            _repository.Inserir(CollectionHelper.ConvertTo<OcorrenciaEnvioDto>(lstEnvio), _user?.UsuarioLogado?.Id ?? 0, observacao);
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
        private EntregaEmAbertoFiltro TrataFiltro(EntregaEmAbertoFiltro filtro)
        {
            //força a data selecionada ser entre as 00:00 e 23:59 do dia 
            var dataInicio = filtro.DataInicio.Value;
            var dataFim = filtro.DataFim.Value;
            filtro.DataInicio = new DateTime(dataInicio.Year, dataInicio.Month, dataInicio.Day, 00, 00, 00);
            filtro.DataFim = new DateTime(dataFim.Year, dataFim.Month, dataFim.Day, 23, 59, 59);

            //força o filtro de data ser de 30 dias no máximo
            var diasDiferenca = ((DateTime)filtro.DataInicio - (DateTime)filtro.DataFim).TotalDays;
            diasDiferenca = diasDiferenca < 0 ? diasDiferenca * (-1) : diasDiferenca;
            if (diasDiferenca > 31)
                throw new DataException("O filtro de 'data inicio x data fim' não deve ser maior que 31 dias.");

            //valida se a visualização é das entregas mkt dessa empresa
            if (filtro.EntregasMarketplace)
                filtro.EmpresaMarketplaceId = _user.UsuarioLogado.EmpresaId;

            //Caso exista o filtro de data da ultima ocorrencia, ele força a ser da 00:00 as 23:59 dessa data
            if (filtro.DataUltimaOcorrencia.HasValue)
            {
                var dataOco = filtro.DataUltimaOcorrencia.Value;
                filtro.DataUltimaOcorrencia = new DateTime(dataOco.Year, dataOco.Month, dataOco.Day, 00, 00, 00);
                filtro.DataUltimaOcorrenciaFim = new DateTime(dataOco.Year, dataOco.Month, dataOco.Day, 23, 59, 59);
            }

            //Separa os pedidos por , para poder realizar o IN na proc
            if (!string.IsNullOrEmpty(filtro.Pedidos))
            {
                var pedidosTratados = string.Empty;
                int count = 1;
                var listaPedidos = filtro.Pedidos.Split(';').Where(x => !string.IsNullOrEmpty(x));
                foreach (var pedido in listaPedidos)
                {
                    pedidosTratados += "'" + pedido.Trim() + (count != listaPedidos.Count() ? "'," : "'");
                    count++;
                }

                filtro.Pedidos = pedidosTratados;
            }
            else
                filtro.Pedidos = null;

            filtro.EmpresaId = _user.UsuarioLogado.EmpresaId;

            return filtro;
        }
    }
}
