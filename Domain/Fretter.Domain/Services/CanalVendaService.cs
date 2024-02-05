using Fretter.Domain.Config;
using Fretter.Domain.Dto.Fusion;
using Fretter.Domain.Entities;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Fretter.Domain.Services
{
    public class CanalVendaService<TContext> : ServiceBase<TContext, CanalVenda>, ICanalVendaService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly FretterConfig _fretterConfig;
        private readonly ICanalVendaRepository<TContext> _repository;
        private readonly IEmpresaRepository<TContext> _empresaRepository;
        private readonly ICanalRepository<TContext> _empresaCanalRepository;
        private readonly ICanalVendaEntradaRepository<TContext> _canalVendaEntradaRepository;
        private readonly ITabelaTipoRepository<TContext> _tabelaTipoRepository;
        private readonly ITabelaTipoCanalVendaRepository<TContext> _tabelaTipoCanalVendaRepository;
        private readonly IEmpresaTokenRepository<TContext> _empresaTokenRepository;
        private readonly ICanalVendaInterfaceRepository<TContext> _canalVendaInterfaceRepository;
        private readonly IMicroServicoRepository<TContext> _microServicoRepository;
        private readonly ITabelaCorreioCanalRepository<TContext> _tabelaCorreioCanalRepository;

        public CanalVendaService(IOptions<FretterConfig> fretterConfig,
            ICanalVendaRepository<TContext> repository,
            IEmpresaRepository<TContext> empresaRepository,
            ICanalRepository<TContext> empresaCanalRepository,
            ICanalVendaEntradaRepository<TContext> canalVendaEntradaRepository,
            ITabelaTipoRepository<TContext> tabelaTipoRepository,
            ITabelaTipoCanalVendaRepository<TContext> tabelaTipoCanalVendaRepository,
            IEmpresaTokenRepository<TContext> empresaTokenRepository,
            ICanalVendaInterfaceRepository<TContext> canalVendaInterfaceRepository,
            IMicroServicoRepository<TContext> microServicoRepository,
            ITabelaCorreioCanalRepository<TContext> tabelaCorreioCanalRepository,
            IUnitOfWork<TContext> unitOfWork,
            IUsuarioHelper user) : base(repository, unitOfWork, user)
        {
            _fretterConfig = fretterConfig.Value;
            _repository = repository;
            _empresaRepository = empresaRepository;
            _empresaCanalRepository = empresaCanalRepository;
            _canalVendaEntradaRepository = canalVendaEntradaRepository;
            _tabelaTipoRepository = tabelaTipoRepository;
            _tabelaTipoCanalVendaRepository = tabelaTipoCanalVendaRepository;
            _empresaTokenRepository = empresaTokenRepository;
            _canalVendaInterfaceRepository = canalVendaInterfaceRepository;
            _microServicoRepository = microServicoRepository;
            _tabelaCorreioCanalRepository = tabelaCorreioCanalRepository;
        }
        public Dto.Fusion.EmpresaCanalVenda ConfiguraCanalVenda(EmpresaCanalVenda dto)
        {
            string tokenResultado = string.Empty;
            try
            {
                var empresa = _empresaRepository.ObterEmpresaPeloCanalPorCnpj(dto.Cnpj);
                if (empresa == null)
                    empresa = _empresaRepository.GetAll(t => t.Cnpj == dto.Cnpj && t.Ativo).FirstOrDefault();

                var canal = _empresaCanalRepository.GetAll(t => t.Cnpj == dto.Cnpj).FirstOrDefault();

                if (empresa != null)
                {
                    var canalVenda = _repository.GetQueryable(t => t.EmpresaId == empresa.Id && t.CanalVendaNome == dto.CanalVendaNome).FirstOrDefault();
                    if (canalVenda == null)
                    {
                        canalVenda = new CanalVenda(0, dto.CanalVendaNome, false, empresa.Id);
                        //Add CanalVenda Entrada
                        canalVenda.CanalVendaEntradas.Add(new CanalVendaEntrada(0, dto.CanalVendaNome, empresa.Id, canalVenda.Id));
                        canalVenda.CanalVendaConfigs.Add(new CanalVendaConfig(0, false, 0, empresa.Id, false, true, true));
                        canalVenda = _repository.Save(canalVenda);
                        _unitOfWork.Commit();
                    }

                    //Insere Tabela Frete Padrao
                    var tabelaFretePadrao = _tabelaTipoRepository.GetAll(t => (t.Tipo.ToLower() == "padrão" || t.Tipo.ToLower() == "padrao") && t.EmpresaId == empresa.Id).FirstOrDefault();
                    int modalidadeDefault = 0;
                    if (tabelaFretePadrao == null)
                    {
                        tabelaFretePadrao = new TabelaTipo(0, "Padrao", empresa.Id, DateTime.Now);
                        tabelaFretePadrao = _tabelaTipoRepository.Save(tabelaFretePadrao);
                        _unitOfWork.Commit();
                    }

                    modalidadeDefault = tabelaFretePadrao.Id;

                    var tabelaFreteExpressa = _tabelaTipoRepository.GetAll(t => t.Tipo.ToLower() == "expressa" && t.EmpresaId == empresa.Id).FirstOrDefault();
                    if (tabelaFreteExpressa == null)
                    {
                        tabelaFreteExpressa = new TabelaTipo(0, "Expressa", empresa.Id, DateTime.Now);
                        tabelaFreteExpressa = _tabelaTipoRepository.Save(tabelaFreteExpressa);
                    }

                    var tabelaFreteEconomica = _tabelaTipoRepository.GetAll(t => (t.Tipo.ToLower() == "econômica" || t.Tipo.ToLower() == "economica") && t.EmpresaId == empresa.Id).FirstOrDefault();
                    if (tabelaFreteEconomica == null)
                    {
                        tabelaFreteEconomica = new TabelaTipo(0, "Economica", empresa.Id, DateTime.Now);
                        tabelaFreteEconomica = _tabelaTipoRepository.Save(tabelaFreteEconomica);
                    }

                    //Modalidade Padrao x CanalVenda 
                    var modalidadeCanal = _tabelaTipoCanalVendaRepository.GetQueryable(t => t.Id == modalidadeDefault && t.CanalVendaId == canalVenda.Id).FirstOrDefault();
                    if (modalidadeCanal == null)
                    {
                        modalidadeCanal = new TabelaTipoCanalVenda(modalidadeDefault, false, false, canalVenda.Id);
                        modalidadeCanal = _tabelaTipoCanalVendaRepository.Save(modalidadeCanal);
                    }
                    else
                    {
                        modalidadeCanal.AtualizarPriorizaPrazo(false);
                        modalidadeCanal = _tabelaTipoCanalVendaRepository.Update(modalidadeCanal);
                    }
                    /*ANALISAR UNIQUE DA PROBLEMA NO UPDATE*/

                    //Token
                    var empresaToken = _empresaTokenRepository.GetQueryable(t => t.EmpresaId == empresa.Id && t.EmpresaTokenTipoId == 1 && t.Padrao == true).FirstOrDefault();
                    if (empresaToken == null)
                    {
                        var tokenData = Guid.NewGuid();
                        empresaToken = new EmpresaToken(0, empresa.Id, 1, dto.TokenNome, tokenData, true, 1, DateTime.Now, 1);
                        empresaToken = _empresaTokenRepository.Save(empresaToken);
                        tokenResultado = tokenData.ToString();
                    }
                    else
                    {
                        empresaToken.Ativar();
                        tokenResultado = empresaToken.Token.ToString();
                        empresaToken = _empresaTokenRepository.Update(empresaToken);
                    }

                    //Canal Venda Interface
                    var canalVendaInterface = _canalVendaInterfaceRepository.GetQueryable(t => t.CanalVendaId == canalVenda.Id && t.EmpresaId == empresa.Id && t.TipoInterface == dto.TipoInterfaceId).FirstOrDefault();
                    if (canalVendaInterface == null)
                    {
                        empresaToken.CanalVendaInterfaces.Add(new CanalVendaInterface(0, canalVenda.Id, Convert.ToInt16(dto.TipoInterfaceId), 0, 2300, empresaToken.Id, empresa.Id));

                        if (empresaToken.Id > 0) empresaToken = _empresaTokenRepository.Update(empresaToken);
                        else empresaToken = _empresaTokenRepository.Save(empresaToken);
                        //canalVendaInterface = _canalVendaInterfaceRepository.Save(canalVendaInterface);
                    }
                    else
                    {
                        canalVendaInterface.AtualizarEmpresaToken(empresaToken.Id);
                        _canalVendaInterfaceRepository.Update(canalVendaInterface);
                    }

                    //ASSOCIA BALCAO SEDEX AO MICROSERVICO NA MODALIDADE PADRAO
                    if (dto.CorreioBalcao)
                    {
                        var microServico = _microServicoRepository.ObterMicroServicoPorEmpresa(empresa.Id, _fretterConfig.CorreioTransportadorId);

                        if (microServico != null && canal.Id > 0)
                        {
                            var tabelaCorreioCanal = _tabelaCorreioCanalRepository.GetQueryable(t => t.CanalId == canal.Id && t.TabelaCorreio == _fretterConfig.CorreioTabelaId).FirstOrDefault();
                            if (tabelaCorreioCanal == null)
                            {
                                tabelaCorreioCanal = new TabelaCorreioCanal(0, _fretterConfig.CorreioTabelaId, canal.Id, new DateTime(2020, 11, 01), new DateTime(2050, 11, 02), DateTime.Now, microServico.Id, dto.Cep);
                                tabelaCorreioCanal.TabelaCorreioCanalTabelaTipos.Add(new TabelaCorreioCanalTabelaTipo(0, tabelaCorreioCanal.Id, modalidadeDefault, DateTime.Now));
                                tabelaCorreioCanal = _tabelaCorreioCanalRepository.Save(tabelaCorreioCanal);
                            }
                        }
                    }

                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                dto.MensagemRetorno = $"Houve um erro ao processar o Canal. Erro: {ex.Message} {ex.InnerException}";
                throw;
            }

            dto.TokenRetorno = tokenResultado;
            return dto;
        }
    }
}
