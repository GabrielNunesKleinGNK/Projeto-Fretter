using Fretter.Domain.Dto.Fatura;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Helpers;
using Fretter.Domain.Helpers.Proceda;
using Fretter.Domain.Helpers.Proceda.Entidades;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretter.Domain.Services
{

    public class FaturaService<TContext> : ServiceBase<TContext, Fatura>, IFaturaService<TContext> 
        where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        public new readonly IArquivoCobrancaService<TContext> _arquivoCobrancaService;
        public new readonly IFaturaRepository<TContext> _repository;
        public new readonly IConciliacaoRepository<TContext> _repositoryConciliacao;
        public new readonly IFaturaHistoricoRepository<TContext> _repositoryHistorico;
        public new readonly IFaturaStatusAcaoRepository<TContext> _repositoryStatusAcao;
        public new readonly IFaturaArquivoRepository<TContext> _repositoryArquivo;


        public FaturaService
        (
            IArquivoCobrancaService<TContext> arquivoCobrancaService, 
            IFaturaRepository<TContext> repository,
            IFaturaHistoricoRepository<TContext> repositoryHistorico,
            IConciliacaoRepository<TContext> repositoryConciliacao,
            IFaturaStatusAcaoRepository<TContext> repositoryStatusAcao,
            IFaturaArquivoRepository<TContext> repositoryArquivo,
            IUnitOfWork<TContext> unitOfWork, IUsuarioHelper user
        )
            : base(repository, unitOfWork, user)
        {
            _user = user;
            _arquivoCobrancaService = arquivoCobrancaService;
            _repository = repository;
            _repositoryHistorico = repositoryHistorico;
            _repositoryConciliacao = repositoryConciliacao;
            _repositoryStatusAcao = repositoryStatusAcao;
            _repositoryArquivo = repositoryArquivo;
        }

        public async Task<List<Fatura>> GetFaturasDaEmpresa(FaturaFiltro filtro)
        {
            return await _repository.GetFaturasDaEmpresa(_user.UsuarioLogado.EmpresaId, filtro);
        }

        public async Task<byte[]> GetFaturasDemonstrativo(FaturaFiltro filtro)
        {
            var listGenerica = new List<object>();
            try
            {
                var listFatura = await _repository.GetFaturasDaEmpresa(_user.UsuarioLogado.EmpresaId, filtro);

                if (listFatura.Any())
                {
                    var item = listFatura.Select(s => new Item() { Id = s.Id }).ToList();

                    var result = _repository.GetDemonstrativoPorFatura(CollectionHelper.ConvertTo(item));

                    if (result.Any())
                    {
                        listFatura.ForEach(fatura =>
                        {
                            var listDemonstrativo = result.Where(w => w.FaturaId == fatura.Id).ToList();
                            listDemonstrativo.ForEach(item => listGenerica.Add(new FaturaDemonstrativoDTO(fatura, item)));
                        });
                    }
                    else
                        listFatura.ForEach(fatura => listGenerica.Add(new FaturaDemonstrativoDTO(fatura, null)));
                }
                else
                    listGenerica.Add(new FaturaDemonstrativoDTO());
            }
            catch (Exception e)
            {

            }
            return listGenerica.ConvertToXlsx("fatura", true, FaturaDemonstrativoDTO.GetCustomColor(), true);
        }

        public void RealizarAcao(FaturaAcaoDto acao)
        {
            var fatura = _repository.Get(acao.FaturaId);
            var statusParaAtualizar = _repositoryStatusAcao.GetAll(x => x.FaturaAcaoId == acao.AcaoId
                                                        && x.FaturaStatusId == fatura.FaturaStatusId).ToList();

            if (statusParaAtualizar == null)
                throw new Exception("Não existe um novo status para essa ação executar.");

            int statusFinal = (int)statusParaAtualizar?.FirstOrDefault()?.FaturaStatusResultadoId;
            FaturaHistorico faturaHistorico = new FaturaHistorico
                                                  (
                                                      fatura,
                                                      _user.UsuarioLogado.Id,
                                                      $"Realizada ação. {(string.IsNullOrEmpty(acao.Motivo) ? string.Empty : $"Motivo: {acao.Motivo}")}",
                                                      statusFinal
                                                  );

            fatura.AtualizarStatusId(statusFinal);

            _repository.Update(fatura);
            _repositoryHistorico.Save(faturaHistorico);
        }

        public Fatura Update(Fatura entidade)
        {
            var fatura = _repository.Get(entidade.Id);

            FaturaHistorico faturaHistorico = new FaturaHistorico(entidade, _user.UsuarioLogado.Id, "Atualização de status", fatura?.FaturaStatusId);
            entidade.AtualizarUsuarioAlteracao(_user.UsuarioLogado.Id);
            entidade.AtualizarDataAlteracao();
            entidade.Validate();

            _repositoryHistorico.Save(faturaHistorico);
            _repository.Update(entidade);

            return entidade;
        }

        public async Task<List<EntregaDemonstrativoDTO>> GetEntregaPorDoccob(List<IFormFile> files)
        {
            var retorno = new List<EntregaDemonstrativoDTO>();
            var listDoccob = new List<RetornoLeituraDoccob>();
            RetornoLeituraDoccob retornoLeituraDoccob;
            bool enumDOCCOBTipo;

            if (files?.FirstOrDefault() != null)
                files.ForEach(file =>
                {
                    var doccob = new DOCCOB();
                    retornoLeituraDoccob = new RetornoLeituraDoccob();

                    switch (PROCEDAHelper.IdentificaDOCCOB(file))
                    {
                        case EnumDOCCOBTipo.DOCCOB50:
                            retornoLeituraDoccob = PROCEDAHelper.ObterDOCCOB50(file);
                            break;
                        case EnumDOCCOBTipo.DOCCOB30:
                            retornoLeituraDoccob = PROCEDAHelper.ObterDOCCOB30(file);
                            break;
                        default:
                            break;
                    }

                    listDoccob.Add(retornoLeituraDoccob);
                });

            if (listDoccob.Any())
            {
                var criticas = listDoccob?.FirstOrDefault(x => x.Criticas.Count > 0)?.Criticas ?? new List<FaturaArquivoCriticaDTO>();

                if (criticas.Count > 0)
                {
                    retorno.Add(new EntregaDemonstrativoDTO { Criticas = criticas.OrderBy(x => x.Linha).ToList() });
                }
                else
                {
                    var doccobFiltro = new List<DoccobFiltro>();
                    listDoccob.ForEach(item =>
                    {
                        item.Doccob.Cabecalhos.ForEach(cabecalho =>
                        {
                            var cnpj = cabecalho.Transportadora.CNPJ;

                            cabecalho.Transportadora.Cobrancas.ForEach(cobranca =>
                            {
                                cobranca.NotasFiscais.ForEach(nota =>
                                {
                                    var docFiltro = new DoccobFiltro();
                                    var conhecimento = cobranca.Conhecimentos.Where(t => t.Id == (nota.Id - 1)).FirstOrDefault();
                                    docFiltro.Id = nota.Id;
                                    docFiltro.CNPJ = cnpj;
                                    docFiltro.CNPJFilial = nota.DocumentoEmissor.RemoveNonNumeric();
                                    docFiltro.NotaFiscal = nota.NotaNumero;
                                    docFiltro.Serie = nota.NotaSerie;
                                    docFiltro.DataEmissao = nota.DataEmissao;
                                    docFiltro.ValorFrete = conhecimento != null ? conhecimento.ValorFrete : 0;
                                    docFiltro.ConhecimentoNumero = conhecimento != null ? conhecimento.Numero : null;
                                    docFiltro.ConhecimentoSerie = conhecimento != null ? conhecimento.Serie : null;
                                    doccobFiltro.Add(docFiltro);
                                });
                            });
                        });
                    });

                    if (doccobFiltro.Count > 0)
                    {
                        doccobFiltro = doccobFiltro.Where(t => !string.IsNullOrEmpty(t.NotaFiscal)).ToList();
                        try
                        {
                            var dataI = doccobFiltro.Where(t => t.DataEmissao != null && t.DataEmissao > DateTime.MinValue).OrderBy(t => t.DataEmissao).FirstOrDefault().DataEmissao;
                            var dataF = doccobFiltro.Where(t => t.DataEmissao != null).OrderByDescending(t => t.DataEmissao).FirstOrDefault().DataEmissao;
                            retorno = _repositoryConciliacao.GetDemonstrativoEntregaConciliacao(CollectionHelper.ConvertTo(doccobFiltro), _user.UsuarioLogado.EmpresaId, false, dataI, dataF);

                            var notasDivergentes = retorno?.Where(x => x.NotaDivergente) ?? new List<EntregaDemonstrativoDTO>();

                            if (notasDivergentes.Count() > 0)
                            {
                                FaturaArquivoCriticaDTO critica;
                                List<FaturaArquivoCriticaDTO> faturaArquivoCriticaDTO = new List<FaturaArquivoCriticaDTO>();

                                int posicao = listDoccob.Any(x => x.EnumDOCCOBTipo == EnumDOCCOBTipo.DOCCOB30) ? 15 : 16;

                                foreach (var item in notasDivergentes)
                                {
                                    critica = new FaturaArquivoCriticaDTO();

                                    critica.Linha = item.LinhaNotaFiscal;
                                    critica.Posicao = posicao;
                                    critica.Descricao = $"Numero de CT-e informada não foi conciliada com NF Número: {item.NotaFiscal} Série: {item.Serie} ";


                                    faturaArquivoCriticaDTO.Add(critica);
                                }

                                retorno.Clear();
                                retorno.Add(new EntregaDemonstrativoDTO { Criticas = faturaArquivoCriticaDTO.OrderBy(x => x.Linha).ToList() });
                            }
                        }
                        catch
                        {
                            throw new ApplicationException("Erro ao ler arquivo DOCCOB, Erro na data Emissão da Nota Fical. Linha: 556 | Posição Inicial: 16 | 8 Dígitos");
                        }
                    }
                }

                int faturaArquivoId = await GravarFaturaArquivo(listDoccob, files.FirstOrDefault());

                retorno?.ForEach(x => x.FaturaArquivoId = faturaArquivoId);
            }

            return retorno;
        }

        private async Task<int> GravarFaturaArquivo(List<RetornoLeituraDoccob> listDoccob, IFormFile arquivo)
        {
            FaturaArquivoDTO faturaArquivoDTO = new FaturaArquivoDTO();
            var docCobLido = listDoccob[0].Doccob;
            var criticas = listDoccob[0].Criticas;

            var lstFaturaArquivo = new List<FaturaArquivoDTO>();

            string url = await _arquivoCobrancaService.RealizarUploadDoccob(arquivo);

            docCobLido.Cabecalhos.ForEach(cabecalho => 
            {
                faturaArquivoDTO.EmpresaId = _user.UsuarioLogado.EmpresaId;
                faturaArquivoDTO.TransportadorCnpj = cabecalho.Transportadora.CNPJ.ToString();
                faturaArquivoDTO.NomeArquivo = arquivo.FileName;
                faturaArquivoDTO.QtdeRegistros = cabecalho.Transportadora.Cobrancas[0].NotasFiscais.Count;
                faturaArquivoDTO.QtdeCriticas = listDoccob[0].Criticas.Count();
                faturaArquivoDTO.ValorTotal = cabecalho.Transportadora.Cobrancas.Sum(x => x.ValorTotal);
                faturaArquivoDTO.UsuarioCadastro = _user.UsuarioLogado.Id;
                faturaArquivoDTO.Faturado = false;
                faturaArquivoDTO.UrlBlobStorage = url;
            });

            lstFaturaArquivo.Add(faturaArquivoDTO);

            listDoccob[0].Criticas.ForEach(critica => critica.UsuarioCriacao = _user.UsuarioLogado.Id);

            int faturaArquivoId = _repositoryArquivo.GravarFaturaArquivo(CollectionHelper.ConvertTo(lstFaturaArquivo), CollectionHelper.ConvertTo(listDoccob[0].Criticas));

            return faturaArquivoId;
        }

        public List<EntregaDemonstrativoDTO> GetEntregaPorPeriodo(FaturaPeriodoFiltro filtro)
        {
            return _repositoryConciliacao.GetDemonstrativoEntregaConciliacao(CollectionHelper.ConvertTo(new List<DoccobFiltro>()),
                _user.UsuarioLogado.EmpresaId, true, filtro.DataInicio.Date.ToLocalTime().Date, filtro.DataTermino.ToLocalTime().Date, filtro.TransportadorId, filtro.StatusConciliacaoId);
        }

        public async Task<byte[]> ProcessarFaturaManual(List<EntregaDemonstrativoDTO> entregaProcessamento)
        {
            var listGenerica = new List<object>();

            var resultFaturaProcessada = _repository.ProcessarFaturaManual(CollectionHelper.ConvertTo(entregaProcessamento), _user.UsuarioLogado.Id);

            var listFatura = await _repository.GetFaturasPorId(resultFaturaProcessada.Select(s => s.Id).ToList());

            if (listFatura.Any())
            {
                var item = resultFaturaProcessada.Select(s => new Item() { Id = s.Id }).ToList();
                var result = _repository.GetDemonstrativoPorFatura(CollectionHelper.ConvertTo(item));

                if (result.Any())
                {
                    listFatura.ForEach(fatura =>
                    {
                        var listDemonstrativo = result.Where(w => w.FaturaId == fatura.Id).ToList();
                        listDemonstrativo.ForEach(item => listGenerica.Add(new FaturaDemonstrativoDTO(fatura, item)));
                    });
                }
                else
                    listFatura.ForEach(fatura => listGenerica.Add(new FaturaDemonstrativoDTO(fatura, null)));
            }
            else
                listGenerica.Add(new FaturaDemonstrativoDTO());


            return listGenerica.ConvertToXlsx("fatura", true, FaturaDemonstrativoDTO.GetCustomColor(), true);
        }

        public async Task<int> ProcessarFaturaAprovacao(List<EntregaDemonstrativoDTO> entregaProcessamento)
        {
            int faturaArquivoId = entregaProcessamento[0].FaturaArquivoId;

            var listaConciliacao = entregaProcessamento.Select(x => new FaturaConciliacaoDTO()
            {
                ConciliacaoId = x.ConciliacaoId,
                TransportadorId = x.TransportadorId,
                NotaFiscal = x.NotaFiscal,
                Serie = x.Serie,
                Observacao = string.Empty,
                DataEmissao = x.DataEmissao,
                ValorAdicional = 0,
                ValorFrete = x.ValorFreteDoccob,
                Selecionado = x.Selecionado
            }).ToList();

            return _repository.ProcessarFaturaImportacao(CollectionHelper.ConvertTo(listaConciliacao), _user.UsuarioLogado.Id, faturaArquivoId).FirstOrDefault().Id;
        }
    }
}
