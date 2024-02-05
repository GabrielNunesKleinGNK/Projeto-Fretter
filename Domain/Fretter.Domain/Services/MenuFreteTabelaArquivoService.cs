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

namespace Fretter.Domain.Services
{
    public class MenuFreteTabelaArquivoService<TContext> : ServiceBase<TContext, TabelaArquivo>, IMenuFreteTabelaArquivoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IMenuFreteTabelaArquivoRepository<TContext> _repository;
        public readonly IBlobStorageService _blobStorageService;
        public readonly ITransportadorCnpjRepository<TContext> _transportadorCnpjRepository;
        private readonly BlobStorageConfig _blobConfig;
        private Stream FileStream { get; set; }
        private List<RegiaoTipo> ListRegiaoTipo { get; set; }

        public MenuFreteTabelaArquivoService(IMenuFreteTabelaArquivoRepository<TContext> repository,
            ITransportadorCnpjRepository<TContext> transportadorCnpjRepository,
            IBlobStorageService blobStorageService,
            IOptions<BlobStorageConfig> blobConfig,
            IUnitOfWork<TContext> unitOfWork, IUsuarioHelper user) : base(repository, unitOfWork, user)
        {
            _repository = repository;
            _transportadorCnpjRepository = transportadorCnpjRepository;
            _blobConfig = blobConfig.Value;
            _blobStorageService = blobStorageService;
            _blobStorageService.InitBlob(_blobConfig.ConnectionString, _blobConfig.MenuFreteContainerName);
        }

        public async Task<int> ProcessarTabelaArquivo()
        {
            var listaArquivos = _repository.GetTabelArquivoProcessamento();

            if (listaArquivos?.FirstOrDefault() == null)
                return 0;

            var retMsg = new ListImportacaoExcelMsg();
            var arquivo = listaArquivos.First();

            try
            {
                _repository.AtualizarTabelaArquivo(arquivo.Id, Enum.EnumTabelaArquivoStatus.Processando);

                FileStream = await _blobStorageService.GetFile(arquivo.DsUrl);

                switch (arquivo.DsModelo)
                {
                    case "novo":
                        ListRegiaoTipo = _repository.GetRegiaoTipo(arquivo.IdEmpresa, arquivo.IdTransportador);
                        ProcessarArquivoNovo(arquivo, retMsg);
                        break;
                    case "antigo":
                        ListRegiaoTipo = _repository.GetRegiaoTipo(arquivo.IdEmpresa, arquivo.IdTransportador);
                        ProcessarArquivoAntigo(arquivo, retMsg);
                        break;
                    case "vtex":
                        ProcessarArquivoVtex(arquivo, retMsg);
                        break;
                    case "correios":
                        ProcessarArquivoCorreios(arquivo, retMsg);
                        break;
                    case "agendamento":
                        ListRegiaoTipo = _repository.GetRegiaoTipo(arquivo.IdEmpresa, arquivo.IdTransportador);
                        ProcessarArquivoAgendamento(arquivo, retMsg);
                        break;
                }

                var status = retMsg.Any(c => c.Error) ? Enum.EnumTabelaArquivoStatus.Erro : Enum.EnumTabelaArquivoStatus.Processado;

                _repository.AtualizarTabelaArquivo(arquivo.Id, status, JsonConvert.SerializeObject(retMsg),
                    retMsg.Count(c => !c.Error), retMsg.Count(c => c.Error));
            }
            catch (Exception e)
            {
                retMsg.Add(e.Message, true, null);
                _repository.AtualizarTabelaArquivo(arquivo.Id, Enum.EnumTabelaArquivoStatus.Erro, JsonConvert.SerializeObject(retMsg),
                    retMsg.Count(c => !c.Error), retMsg.Count(c => c.Error));
            }
            finally
            {
                FileStream.Dispose();
            }
            return 1;
        }

        private string ObterCNPJTransportadorModeloAntigo(IExcelDataReader reader)
        {
            reader.AdvanceLines(4);

            return reader.GetString(1);
        }

        private async void ProcessarArquivoAntigo(TabelaArquivoProcessamento tabelaArquivo, ListImportacaoExcelMsg retMsg)
        {
            var listFaixasPeso = new List<TabelaPesoModel>();
            var listRegs = new List<RegiaoModel>();
            var listCEP = new List<RegiaoCEPCompletoModel>();
            var listPrecos = new List<TabelaPrecoModel>();

            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(FileStream))
            {
                string cnpj = ObterCNPJTransportadorModeloAntigo(reader);

                if (!cnpj.ValidateExcelString(4, 1, reader.Name, retMsg, new[] { ".", "/", "-" }, 14))
                {
                    retMsg.Add("CNPJ informado inválido", true, 4, 1, cnpj);
                    return;
                }

                cnpj = cnpj.ReplaceChars(new[] { ".", "/", "-" }, string.Empty);

                var transportadorCnpj = _transportadorCnpjRepository.ObterTransportadorCnpj(tabelaArquivo.IdTransportador);

                if (transportadorCnpj?.FirstOrDefault(f => f.CNPJ == cnpj) == null)
                {
                    retMsg.Add("Planilha não pertence ao transportador da tabela", true, 4, 1, cnpj);
                    return;
                }

                reader.Reset();

                reader.AdvanceLines(18);

                int linhaFaixaPeso = 19;
                while (reader.Read() && !string.IsNullOrEmpty(reader.GetString(2)))
                {
                    var fp = new TabelaPesoModel();

                    fp.Nr_PesoFim = reader.GetValue(3).ToString().ExcelDataToDecimal(linhaFaixaPeso, 3, retMsg);

                    fp.Flg_Ativo = reader.GetValue(2)?.ToString().ExcelDataToBoolean(linhaFaixaPeso, 2, retMsg) ?? false;

                    fp.Cd_Id = -Convert.ToInt32(fp.Nr_PesoFim * 100);


                    listFaixasPeso.Add(fp);

                    linhaFaixaPeso++;
                }

                reader.Reset();
                reader.AdvanceLines(2);

                int coluna = 4;
                while (!string.IsNullOrEmpty(reader.GetValue(coluna)?.ToString()) && coluna <= reader.FieldCount)
                {
                    RegiaoModel regiao = new RegiaoModel();

                    regiao.Ds_Regiao = reader.GetValue(coluna)?.ToString();

                    regiao.Cd_Uf = reader.AdvanceLines(1).GetValue(coluna)?.ToString().ToUpper().Substring(0, 2);

                    string descricaoTipo = reader.AdvanceLines(1).GetValue(coluna)?.ToString();

                    int tipoId = ListRegiaoTipo
                        .OrderBy(a => string.Equals(a.DsTipo, descricaoTipo, StringComparison.InvariantCultureIgnoreCase) ? 1 : 2)
                        .FirstOrDefault(a => string.Equals(a.DsTipo, descricaoTipo, StringComparison.InvariantCultureIgnoreCase))?.Id ?? 0;

                    if (tipoId == 0 && string.IsNullOrEmpty(descricaoTipo))
                        retMsg.Add("Existe região nova sem tipo", true, 2, coluna, regiao.Ds_Regiao);


                    regiao.Cd_Id = -coluna + 1;
                    regiao.Id_Transportador = tabelaArquivo.IdTransportador;
                    regiao.Id_Tabela = tabelaArquivo.IdTabela;
                    regiao.Id_RegiaoTipo = tipoId;
                    regiao.Ds_Tipo = tipoId == 0 ? descricaoTipo : null;

                    regiao.Flg_Ativo = reader.AdvanceLines(1).GetValue(coluna).ToString().ExcelDataToBoolean(5, coluna, retMsg);
                    regiao.Nr_Prazo = (byte)(reader.AdvanceLines(1).GetValue(coluna)?.ToString().ExcelDataToByte(6, coluna, retMsg));
                    regiao.Flg_Generica = false;

                    reader.AdvanceLines(1);

                    regiao.Nr_PesoExcedente = reader.AdvanceLines(1).GetValue(coluna).ToString().ExcelDataToDecimal(8, coluna, retMsg);
                    regiao.Nr_PedagioFixo = reader.AdvanceLines(1).GetValue(coluna).ToString().ExcelDataToDecimal(9, coluna, retMsg);
                    regiao.Nr_PedagioVariavel = reader.AdvanceLines(1).GetValue(coluna).ToString().ExcelDataToDecimal(10, coluna, retMsg);
                    regiao.Nr_FracaoPesoPedagio = reader.AdvanceLines(1).GetValue(coluna).ToString().ExcelDataToDecimal(11, coluna, retMsg);
                    regiao.Nr_PercentualGRIS = reader.AdvanceLines(1).GetValue(coluna).ToString().ExcelDataToDecimal(12, coluna, retMsg);
                    regiao.Nr_PercentualAdValorem = reader.AdvanceLines(1).GetValue(coluna).ToString().ExcelDataToDecimal(13, coluna, retMsg);
                    regiao.Nr_PercentualBalsa = Math.Round(reader.AdvanceLines(1).GetValue(coluna).ToString().ExcelDataToDecimal(14, coluna, retMsg), 4);
                    regiao.Nr_PercentualRedespachoFluvial = reader.AdvanceLines(1).GetValue(coluna).ToString().ExcelDataToDecimal(15, coluna, retMsg);
                    regiao.Nr_PesoMaximo = reader.AdvanceLines(1).GetValue(coluna).ToString().ExcelDataToDecimal(16, coluna, retMsg);
                    regiao.Nr_FatorCubagem = reader.AdvanceLines(1).GetValue(coluna)?.ToString().ExcelDataToInt(17, coluna, retMsg);

                    listRegs.Add(regiao);

                    // Avança da linha 18 para a linha 19
                    reader.AdvanceLines(1);

                    int linhaPrecoFaixaPeso = 19;
                    while (ValidaRangeFaixaPeso(reader, coluna))
                    {
                        var tp = new TabelaPrecoModel
                        {
                            Id_Tabela = tabelaArquivo.IdTabela,
                            Id_Regiao = regiao.Cd_Id
                        };

                        tp.Nr_Valor = reader.GetValue(coluna).ToString().ExcelDataToDecimal(linhaPrecoFaixaPeso, coluna, retMsg, false);


                        if (tp.Id_TabelaPeso == default)
                        {
                            decimal peso = reader.GetValue(3).ToString().ExcelDataToDecimal(linhaPrecoFaixaPeso, 3, retMsg, false);

                            tp.Id_TabelaPeso = -Convert.ToInt32(peso * 100);
                        }

                        if (tp.Id_TabelaPeso != default)
                            listPrecos.Add(tp);

                        linhaPrecoFaixaPeso++;
                    }

                    reader.Read();

					string ts = string.Empty;
                    int linhaFaixaCep = linhaPrecoFaixaPeso + 3;
                    while (ValidaRangeFaixaCep(reader, coluna))
                    {
                        ts = reader.GetString(coluna);

                        var arr = ts?.Replace(" ", "").Split('|');
                        if (arr.Length != 2)
                            throw new Exception($"Problemas na celula {retMsg.GeraLetraColunaExcel(coluna) + linhaFaixaCep}");

                        var rc = new RegiaoCEPCompletoModel
                        {
                            Id_Regiao = regiao.Cd_Id,
                            Cd_CepInicio = arr[0]?.Trim()?.Replace("-", ""),
                            Cd_CepFim = arr[1]?.Trim()?.Replace("-", "")
                        };
                        if (!string.IsNullOrEmpty(rc.Cd_CepInicio) && !string.IsNullOrEmpty(rc.Cd_CepFim) && rc.Cd_CepInicio != "Inicio")
                            listCEP.Add(rc);

                        linhaFaixaCep++;
                    }
                    coluna++;

                    reader.Reset();
                    reader.AdvanceLines(2);
                }

                if (listFaixasPeso.Count == 0)
                {
                    retMsg.Add(new ImportacaoExcelMsg { Msg = "Nenhuma faixa de peso foi lida.", Error = true, Cel = string.Empty, Vlr = string.Empty });
                }
                if (listRegs.Count == 0)
                {
                    retMsg.Add(new ImportacaoExcelMsg { Msg = "Nenhuma região foi lida.", Error = true, Cel = string.Empty, Vlr = string.Empty });
                }

                var tt = 0;
                foreach (var f in listFaixasPeso)
                {
                    var t = listPrecos.Count(a => a.Id_TabelaPeso == f.Cd_Id);
                    if (tt == default)
                        tt = t;
                    else if (tt != t)
                        retMsg.Add(new ImportacaoExcelMsg { Msg = "O número de faixas peso x preço estão inconsistentes", Error = true, Cel = string.Empty, Vlr = f.Nr_PesoFim.ToString() });
                }
                tt = 0;
                foreach (var f in listRegs)
                {
                    if (f.Nr_Prazo == default)
                        retMsg.Add(new ImportacaoExcelMsg { Msg = "Existe região com prazo zerado", Error = true, Cel = string.Empty, Vlr = f.Ds_Regiao });

                    var t = listPrecos.Where(a => a.Id_Regiao == f.Cd_Id).OrderBy(a => a.Nr_PesoFim).ToList();
                    if (tt == default)
                        tt = t.Count;
                    else if (tt != t.Count)
                        retMsg.Add(new ImportacaoExcelMsg { Msg = "O número de faixas regiões x preço estão inconsistentes", Error = true, Cel = string.Empty, Vlr = f.Ds_Regiao });

                    decimal vlr = 0;
                    foreach (var p in t)
                    {
                        if (p.Nr_Valor <= vlr)
                            retMsg.Add(new ImportacaoExcelMsg { Msg = $"Valor da faixa [{p.Nr_PesoFim}] menor que o da anteior {vlr} -> {p.Nr_Valor}", Error = true, Cel = string.Empty, Vlr = f.Ds_Regiao });
                        vlr = p.Nr_Valor;
                    }

                    var r = listCEP.Where(a => a.Id_Regiao == f.Cd_Id).OrderBy(a => a.CepFim).ToList();
                    var lastCepFim = 0;
                    foreach (var c in r)
                    {
                        if (lastCepFim != 0 && lastCepFim != c.CepInicio)
                            retMsg.Add(new ImportacaoExcelMsg { Msg = $"Existe furo de cep na região CEP Fim [ {lastCepFim - 1} ] e CEP Inicio [ {c.CepInicio} ]", Error = false, Cel = string.Empty, Vlr = f.Ds_Regiao });
                        lastCepFim = c.CepFim + 1;
                    }
                }
            }

            if (!retMsg.Any(a => a.Error))
            {
                _repository.InserirLista(tabelaArquivo.IdTabela, tabelaArquivo.IdEmpresa, tabelaArquivo.IdTransportador, false,
                    CollectionHelper.ConvertTo(listFaixasPeso), CollectionHelper.ConvertTo(listRegs), CollectionHelper.ConvertTo(listPrecos),
                    CollectionHelper.ConvertTo(listCEP));
                if (!string.IsNullOrEmpty(tabelaArquivo.DsUrlParametroCep))
                    await ProcessarParametroCep(tabelaArquivo, listCEP, retMsg);
            }
        }

        private static bool ValidaRangeFaixaCep(IExcelDataReader reader, int coluna)
        {
            reader.Read();

            string valor = reader.GetValue(coluna)?.ToString();

            return !string.IsNullOrEmpty(valor);
        }

        private bool ValidaRangeFaixaPeso(IExcelDataReader reader, int coluna)
        {
            reader.Read();

            string valorPeso = reader.GetValue(coluna)?.ToString();
            string peso = reader.GetValue(3)?.ToString();

            return !string.IsNullOrEmpty(peso) && !string.IsNullOrEmpty(valorPeso) && valorPeso.Trim() != "Inicio | Fim";
        }

        private async Task ProcessarParametroCep(TabelaArquivoProcessamento tabelaArquivo, List<RegiaoCEPCompletoModel> listCepModel, ListImportacaoExcelMsg retMsg)
        {

            var listCep = _repository.GetRegiaoCep(tabelaArquivo.IdTabela);

            var parametroCep = await _blobStorageService.GetFile(tabelaArquivo.DsUrlParametroCep);
            parametroCep.Position = 0;
            var lstCEPs = new List<TabelaCepModel>();

            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(parametroCep))
            {
                int linha = 3;
                reader.AdvanceLines(2);
                while (reader.Read())
                {
                    var c = new TabelaCepModel();

                    var cepInicio = string.Empty;
                    cepInicio = reader.GetValue(4).ToString();

                    if (!cepInicio.ValidateExcelString(linha, 4, string.Empty, retMsg))
                        cepInicio = string.Empty;


                    var cepFim = string.Empty;
                    cepFim = reader.GetValue(5).ToString();

                    if (!cepFim.ValidateExcelString(linha, 5, string.Empty, retMsg))
                        cepFim = string.Empty;

                    var cep = listCep.FirstOrDefault(f => f.CepInicio.PadLeft(8, '0') == cepInicio?.Trim()?.Replace("-", "").PadLeft(8, '0') && f.CepFim.PadLeft(8, '0') == cepFim?.Trim()?.Replace("-", "").PadLeft(8, '0'));

                    if (cep == null)
                    {
                        retMsg.Add("Cep não encontrado na tabela base ", true, linha, 4, reader.GetValue(4).ToString());
                    }
                    else
                    {
                        c.Cd_Id = cep.Id;
                        c.Nr_PercentualGRIS = reader.GetValue(6).ToString().ExcelDataToDecimal(linha, 6, retMsg, false);
                        c.Nr_Prazo = reader.GetValue(7).ToString().ExcelDataToByte(linha, 7, retMsg, false);
                        c.Nr_FreteAdicional = reader.GetValue(8).ToString().ExcelDataToDecimal(linha, 8, retMsg, false);
                        c.Flg_ZonaDificuldade = reader.GetValue(9).ToString().ExcelDataToBoolean(linha, 9, retMsg);
                        lstCEPs.Add(c);
                    }
                    linha++;
                }

            }

            if (lstCEPs.Count == 0)
            {
                retMsg.Add(new ImportacaoExcelMsg { Msg = "Nenhuma faixa de peso foi lida.", Error = true, Cel = string.Empty, Vlr = "Parâmetro Cep" });
                return;
            }
            if (!retMsg.Any(a => a.Error))
                _repository.InserirParametroCep(tabelaArquivo.IdTabela, tabelaArquivo.IdEmpresa, CollectionHelper.ConvertTo(lstCEPs));
        }

        private void ProcessarArquivoAgendamento(TabelaArquivoProcessamento tabelaArquivo, ListImportacaoExcelMsg retMsg)
        {
            var lstRegioesCEPCapacidade = new List<RegiaoCEPCapacidadeModel>();
            var lstRegioesCEP = new List<RegiaoCEPModel>();
            var lstRegioes = new List<RegiaoModel>();

            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(FileStream))
            {
                RegiaoCEPCapacidadeModel regiaoCEPCapacidade;
                RegiaoCEPModel regiaoCepModel;
                RegiaoModel regiaoModel;

                int linha = 2;

                // Pula a linha de titulo das colunas
                reader.AdvanceLines(1);

                while (reader.Read())
                {
                    regiaoCEPCapacidade = new RegiaoCEPCapacidadeModel();
                    regiaoCepModel = new RegiaoCEPModel();
                    regiaoModel = new RegiaoModel();

                    regiaoModel.Cd_Id = -linha;

                    regiaoModel.Ds_Regiao = reader.GetValue(1)?.ToString();

                    if (!string.IsNullOrEmpty(regiaoModel.Ds_Regiao))
                    {
                        regiaoModel.Cd_Uf = reader.GetValue(2).ToString();

                        string descricaoTipoRegiao = reader.GetValue(3).ToString();

                        int regiaoTipoId = ObterRegiaoTipoIdPorDescricao(descricaoTipoRegiao);

                        if (regiaoTipoId == 0 && string.IsNullOrEmpty(descricaoTipoRegiao))
                            retMsg.Add("Existe regi�o nova sem tipo", true, 2, 3, regiaoModel.Ds_Regiao);

                        regiaoModel.Id_RegiaoTipo = regiaoTipoId;
                        regiaoModel.Ds_Tipo = regiaoModel.Id_RegiaoTipo == 0 ? descricaoTipoRegiao : null;

                        regiaoCepModel.Cd_Id = -linha;
                        regiaoCepModel.Id_Regiao = regiaoModel.Cd_Id;
                        regiaoCepModel.Cd_CepInicio = reader.GetValue(4).ToString().Replace("-", "");
                        regiaoCepModel.Cd_CepFim = reader.GetValue(5).ToString().Replace("-", "");

                        regiaoCEPCapacidade.Cd_Id = -linha;
                        regiaoCEPCapacidade.Id_RegiaoCEP = regiaoCepModel.Cd_Id;

                        string periodo = reader.GetValue(9).ToString();

                        regiaoCEPCapacidade.Nr_Dia = reader.GetValue(8).ToString().ExcelDataToByte(linha, 8, retMsg);
                        regiaoCEPCapacidade.Vl_Quantidade = reader.GetValue(10).ToString().ExcelDataToInt(linha, 10, retMsg);
                        regiaoCEPCapacidade.Vl_QuantidadeDisponivel = regiaoCEPCapacidade.Vl_Quantidade;
                        regiaoCEPCapacidade.Nr_Valor = reader.GetValue(11).ToString().ExcelDataToDecimal(linha, 11, retMsg);

                        if (regiaoCEPCapacidade.Nr_Dia < 1 || regiaoCEPCapacidade.Nr_Dia > 7)
                        {
                            retMsg.Add("O dia deve estar entre 1 e 7", true, linha, 8, regiaoCEPCapacidade.Nr_Dia.ToString());
                        }

                        periodo = periodo.Trim().ToLower().ReplaceSpecialChars();

                        switch (periodo)
                        {
                            case "manha":
                                regiaoCEPCapacidade.Id_Periodo = (int)EnumPeriodo.Manha;
                                break;
                            case "tarde":
                                regiaoCEPCapacidade.Id_Periodo = (int)EnumPeriodo.Tarde;
                                break;
                            case "noite":
                                regiaoCEPCapacidade.Id_Periodo = (int)EnumPeriodo.Noite;
                                break;
                            default:
                                retMsg.Add("O perido informado deve ser Manha | Tarde | Noite", true, linha, 9, periodo);
                                break;
                        }

                        lstRegioesCEPCapacidade.Add(regiaoCEPCapacidade);
                        lstRegioesCEP.Add(regiaoCepModel);
                        lstRegioes.Add(regiaoModel);
                    }

                    linha++;
                }

                if (lstRegioesCEPCapacidade.Count == 0)
                    retMsg.Add(new ImportacaoExcelMsg { Msg = "Nenhuma linha foi lida.", Error = true, Cel = string.Empty, Vlr = string.Empty });

                if (!retMsg.Any(a => a.Error))
                {
                    _repository.InserirListaAgendamento(tabelaArquivo.IdTabela, tabelaArquivo.IdEmpresa, tabelaArquivo.IdTransportador,
                    CollectionHelper.ConvertTo(lstRegioesCEPCapacidade),
                    CollectionHelper.ConvertTo(lstRegioesCEP),
                    CollectionHelper.ConvertTo(lstRegioes));
                }
            }
        }

        private int ObterRegiaoTipoIdPorDescricao(string descricaoTipo)
        {
            return ListRegiaoTipo
                        .OrderBy(a => string.Equals(a.DsTipo, descricaoTipo, StringComparison.InvariantCultureIgnoreCase) ? 1 : 2)
                        .FirstOrDefault(a => string.Equals(a.DsTipo, descricaoTipo, StringComparison.InvariantCultureIgnoreCase))?.Id ?? 0;
        }

        private void ProcessarArquivoNovo(TabelaArquivoProcessamento tabelaArquivo, ListImportacaoExcelMsg retMsg)
        {
            var listFaixasPeso = new List<TabelaPesoModel>();
            var listCEP = new List<RegiaoCEPCompletoModel>();
            var listRegs = new List<RegiaoModel>(1);
            var listPrecos = new List<TabelaPrecoModel>();

            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(FileStream))
            {
                int qtdTotalRegistros = reader.RowCount;
                Console.WriteLine($"Quantidade Total de Registros: {qtdTotalRegistros}");
                IEnumerable<RegiaoModel> listRegRanges;
                listRegs = new List<RegiaoModel>(qtdTotalRegistros);

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

                            _repository.AtualizarTabelaArquivo(tabelaArquivo.Id, 0, null, null, null, qtdTotalRegistros, currentPercent);
                        }

                        var tipo = reader.GetValue(3)?.ToString().ToLower();
                        var tipoId = ListRegiaoTipo
                                            .OrderBy(a => string.Equals(a.DsTipo, tipo, StringComparison.InvariantCultureIgnoreCase) ? 1 : 2)
                                            .FirstOrDefault(a => string.Equals(a.DsTipo, tipo, StringComparison.InvariantCultureIgnoreCase))?.Id;


                        if (tipoId == null && string.IsNullOrEmpty(reader.GetValue(3)?.ToString()))
                            retMsg.Add("Existe região nova sem tipo", true, linha, 1, reader.GetValue(1).ToString());


                        var reg = new RegiaoModel
                        {
                            Cd_Id = -linha,
                            Id_Empresa = tabelaArquivo.IdEmpresa,
                            Id_Transportador = tabelaArquivo.IdTransportador,
                            Id_RegiaoTipo = tipoId ?? 0,
                            Ds_Tipo = tipoId == null ? reader.GetValue(3)?.ToString() : null,
                            Ds_Regiao = reader.GetValue(1)?.ToString(),
                            Cd_Uf = reader.GetValue(2)?.ToString().ToUpper().Substring(0, 2)
                        };

                        reg.Flg_Ativo = reader.GetValue(6).ToString().ExcelDataToBoolean(linha, 6, retMsg);
                        reg.Nr_Prazo = reader.GetValue(7).ToString().ExcelDataToByte(linha, 7, retMsg);
                        reg.Flg_Generica = false;
                        reg.Nr_PesoExcedente = reader.GetValue(9).ToString().ExcelDataToDecimal(linha, 9, retMsg);
                        reg.Nr_PedagioFixo = reader.GetValue(10).ToString().ExcelDataToDecimal(linha, 10, retMsg);
                        reg.Nr_PedagioVariavel = reader.GetValue(11).ToString().ExcelDataToDecimal(linha, 11, retMsg);
                        reg.Nr_FracaoPesoPedagio = reader.GetValue(12).ToString().ExcelDataToDecimal(linha, 12, retMsg);
                        reg.Nr_PercentualAdValorem = reader.GetValue(15).ToString().ExcelDataToDecimal(linha, 15, retMsg);
                        reg.Nr_PercentualBalsa = reader.GetValue(17).ToString().ExcelDataToDecimal(linha, 17, retMsg);
                        reg.Nr_PercentualRedespachoFluvial = reader.GetValue(19).ToString().ExcelDataToDecimal(linha, 19, retMsg);
                        reg.Nr_PesoMaximo = reader.GetValue(20).ToString().ExcelDataToDecimal(linha, 20, retMsg);
                        reg.Nr_FatorCubagem = reader.GetValue(21).ToString().ExcelDataToInt(linha, 21, retMsg);

                        listRegs.Sort((r1, r2) => r1.Cd_Uf.CompareTo(r2.Cd_Uf));
                        listRegs.Sort((r1, r2) => r1.Id_RegiaoTipo.CompareTo(r2.Id_RegiaoTipo));

                        listRegRanges = listRegs.FindAll(a => a.Cd_Uf == reg.Cd_Uf && a.Id_RegiaoTipo == reg.Id_RegiaoTipo);
                        var reg2 = listRegRanges.FirstOrDefault(a => a.Ds_Regiao == reg.Ds_Regiao);

                        if (reg2 == null)
                        {
                            listRegs.Add(reg);
                        }
                        else
                        {

                            if (reg.Cd_Uf != reg2.Cd_Uf)
                                retMsg.Add("Existe duplicidade de região com [ UF ] diferentes", false, linha, 3, reader.GetValue(3).ToString());

                            if (reg.Id_RegiaoTipo != reg2.Id_RegiaoTipo || reg.Ds_Tipo != reg2.Ds_Tipo)
                                retMsg.Add("Existe duplicidade de região com [ Tipo de Região ] diferentes", false, linha, 4, reader.GetValue(4).ToString());

                            if (reg.Flg_Ativo != reg2.Flg_Ativo)
                                retMsg.Add("Existe duplicidade de região com [ Habilitado ] diferentes", false, linha, 6, reader.GetValue(6).ToString());

                            if (reg.Flg_Generica != reg2.Flg_Generica)
                                retMsg.Add("Existe duplicidade de região com [ Generica ] diferentes", false, linha, 8, reader.GetValue(8).ToString());

                            if (reg.Nr_PesoExcedente != reg2.Nr_PesoExcedente)
                                retMsg.Add("Existe duplicidade de região com [ Valor kg excedente ] diferentes", false, linha, 9, reader.GetValue(9).ToString());

                            if (reg.Nr_PedagioFixo != reg2.Nr_PedagioFixo)
                                retMsg.Add("Existe duplicidade de região com [ Pegádio Fixo ] diferentes", false, linha, 10, reader.GetValue(10).ToString());

                            if (reg.Nr_PedagioVariavel != reg2.Nr_PedagioVariavel)
                                retMsg.Add("Existe duplicidade de região com [ Pedágio Variável ] diferentes", false, linha, 11, reader.GetValue(11).ToString());

                            if (reg.Nr_FracaoPesoPedagio != reg2.Nr_FracaoPesoPedagio)
                                retMsg.Add("Existe duplicidade de região com [ Fração Peso Pedágio ] diferentes", false, linha, 12, reader.GetValue(12).ToString());

                            if (reg.Nr_PercentualAdValorem != reg2.Nr_PercentualAdValorem)
                                retMsg.Add("Existe duplicidade de região com [ AdValorem ] diferentes", false, linha, 15, reader.GetValue(15).ToString());

                            if (reg.Nr_PercentualBalsa != reg2.Nr_PercentualBalsa)
                                retMsg.Add("Existe duplicidade de região com [ Balsa ] diferentes", false, linha, 17, reader.GetValue(17).ToString());

                            if (reg.Nr_PercentualRedespachoFluvial != reg2.Nr_PercentualRedespachoFluvial)
                                retMsg.Add("Existe duplicidade de região com [ Redespacho Fluvial ] diferentes", false, linha, 19, reader.GetValue(19).ToString());

                            if (reg.Nr_PesoMaximo != reg2.Nr_PesoMaximo)
                                retMsg.Add("Existe duplicidade de região com [ Peso Máximo ] diferentes", false, linha, 20, reader.GetValue(20).ToString());

                            if (reg.Nr_FatorCubagem != reg2.Nr_FatorCubagem)
                                retMsg.Add("Existe duplicidade de região com [ Fator Cubagem ] diferentes", false, linha, 21, reader.GetValue(21).ToString());

                            reg = reg2;
                        }

                        var fp = new TabelaPesoModel { Flg_Ativo = true, Cd_Id = -linha };

                        fp.Nr_PesoFim = reader.GetValue(23).ToString().ExcelDataToDecimal(linha, 23, retMsg);

                        var fp2 = listFaixasPeso.FirstOrDefault(a => a.Nr_PesoFim == fp.Nr_PesoFim);

                        if (fp2 == null)
                        {
                            listFaixasPeso.Add(fp);
                        }
                        else
                        {
                            fp = fp2;
                        }

                        var tp = new TabelaPrecoModel
                        {
                            Id_Regiao = reg.Cd_Id,
                            Id_TabelaPeso = fp.Cd_Id
                        };

                        tp.Nr_Valor = reader.GetValue(24).ToString().ExcelDataToDecimal(linha, 24, retMsg);

                        var tp2 = listPrecos.FirstOrDefault(a => a.Id_Regiao == tp.Id_Regiao && a.Id_TabelaPeso == tp.Id_TabelaPeso);

                        if (tp2 == null)
                        {
                            listPrecos.Add(tp);
                        }
                        else
                        {
                            if (tp.Nr_Valor != tp2.Nr_Valor)
                                retMsg.Add("Existe duplicidade de região com [ Valor faixa peso ] diferentes", false, linha, 24, reader.GetValue(24).ToString());
                            tp = tp2;
                        }

                        var rc = new RegiaoCEPCompletoModel
                        {
                            Id_Regiao = reg.Cd_Id,
                            Cd_CepInicio = reader.GetValue(4)?.ToString().Trim()?.Replace("-", ""),
                            Cd_CepFim = reader.GetValue(5)?.ToString().Trim()?.Replace("-", "")
                        };

                        rc.Nr_Prazo = reader.GetValue(7).ToString().ExcelDataToByte(linha, 7, retMsg);

                        rc.Nr_ValorMinimoGRIS = reader.GetValue(14).ToString().ExcelDataToDecimal(linha, 14, retMsg);
                        rc.Nr_ValorMinimoAdvalorem = reader.GetValue(16).ToString().ExcelDataToDecimal(linha, 16, retMsg);
                        rc.Nr_ValorMinimoFluvial = reader.GetValue(18).ToString().ExcelDataToDecimal(linha, 18, retMsg);
                        rc.Nr_ValorSuframa = reader.GetValue(26).ToString().ExcelDataToDecimal(linha, 26, retMsg);
                        rc.Nr_ValorMinimoTRT = reader.GetValue(27).ToString().ExcelDataToDecimal(linha, 27, retMsg);
                        rc.Nr_PercentualTRT = reader.GetValue(28).ToString().ExcelDataToDecimal(linha, 28, retMsg);
                        rc.Nr_ValorMinimoTDE = reader.GetValue(29).ToString().ExcelDataToDecimal(linha, 29, retMsg);
                        rc.Nr_PercentualTDE = reader.GetValue(30).ToString().ExcelDataToDecimal(linha, 30, retMsg);
                        rc.Nr_ValorMinimoTDA = reader.GetValue(31).ToString().ExcelDataToDecimal(linha, 31, retMsg);
                        rc.Nr_PercentualTDA = reader.GetValue(32).ToString().ExcelDataToDecimal(linha, 32, retMsg);
                        rc.Nr_ValorCTE = reader.GetValue(33).ToString().ExcelDataToDecimal(linha, 33, retMsg);
                        rc.Nr_ValorTaxaRisco = reader.GetValue(34).ToString().ExcelDataToDecimal(linha, 34, retMsg);
                        rc.Nr_PercentualGRIS = reader.GetValue(13).ToString().ExcelDataToDecimal(linha, 13, retMsg);

                        rc.Nr_FreteAdicional = reader.GetValue(25).ToString().ExcelDataToDecimal(linha, 25, retMsg);

                        rc.Flg_ZonaDificuldade = reader.GetValue(35).ToString().ExcelDataToBoolean(linha, 35, retMsg);


                        if (string.IsNullOrEmpty(rc.Cd_CepInicio) ||
                            string.IsNullOrEmpty(rc.Cd_CepFim) ||
                            rc.Cd_CepInicio.Length != 8 ||
                            rc.Cd_CepFim.Length != 8)
                        {
                            if (string.IsNullOrEmpty(rc.Cd_CepInicio))
                                retMsg.Add("Existe CEP Inicio em branco", true, linha, 4, reader.GetValue(4).ToString());
                            else if (rc.Cd_CepInicio.Length != 8)
                                retMsg.Add("Existe CEP Inicio com quantidade de dígitos diferente de 8", true, linha, 4, reader.GetValue(4).ToString());

                            if (string.IsNullOrEmpty(rc.Cd_CepFim))
                                retMsg.Add("Existe CEP Fim em branco", true, linha, 5, reader.GetValue(5).ToString());
                            else if (rc.Cd_CepFim.Length != 8)
                                retMsg.Add("Existe CEP Fim com quantidade de dígitos diferente de 8", true, linha, 5, reader.GetValue(5).ToString());
                        }
                        else
                        {
                            var rc2 = listCEP.FirstOrDefault(a => a.CepInicio == rc.CepInicio && a.CepFim == rc.CepFim);
                            if (rc2 == null)
                            {
                                if (listCEP.Any(a =>
                                        (a.CepInicio <= rc.CepInicio && a.CepFim >= rc.CepInicio) ||
                                        (a.CepInicio <= rc.CepFim && a.CepFim >= rc.CepFim) ||
                                        (a.CepInicio >= rc.CepInicio && a.CepFim <= rc.CepFim))
                                    )
                                {
                                    retMsg.Add("Existe colisão de CEP", false, linha, new int[] { 4, 5 }, reader.GetValue(4).ToString() + " | " + reader.GetValue(5).ToString());
                                }
                                else
                                {
                                    var ok = true;

                                    if (ok)
                                    {
                                        listCEP.Add(rc);
                                    }
                                }
                            }
                            else
                            {
                                if (rc.Id_Regiao != rc2.Id_Regiao)
                                {
                                    retMsg.Add("Existe duplicidade de região com [ CEP ] diferentes", false, linha, new int[] { 4, 5 }, reader.GetValue(4).ToString() + " | " + reader.GetValue(5).ToString());
                                }
                                if (rc.Nr_Prazo != rc2.Nr_Prazo)
                                {
                                    retMsg.Add("Existe duplicidade de região com [ Prazo ] diferentes", false, linha, new int[] { 4, 5 }, reader.GetValue(4).ToString() + " | " + reader.GetValue(5).ToString());
                                }
                                if (rc.Nr_PercentualGRIS != rc2.Nr_PercentualGRIS)
                                {
                                    retMsg.Add("Existe duplicidade de região com [ GRIS ] diferentes", false, linha, new int[] { 4, 5 }, reader.GetValue(4).ToString() + " | " + reader.GetValue(5).ToString());
                                }
                            }
                        }

                    }
                    linha++;
                }

                if (listFaixasPeso.Count == 0)
                    retMsg.Add(new ImportacaoExcelMsg { Msg = "Nenhuma faixa de peso foi lida.", Error = true, Cel = string.Empty, Vlr = string.Empty });

                if (listRegs.Count == 0)
                    retMsg.Add(new ImportacaoExcelMsg { Msg = "Nenhuma região foi lida.", Error = true, Cel = string.Empty, Vlr = string.Empty });

                var tt = 0;
                foreach (var f in listFaixasPeso)
                {
                    var t = listPrecos.Count(a => a.Id_TabelaPeso == f.Cd_Id);
                    if (tt == default)
                        tt = t;
                    else if (tt != t)
                        retMsg.Add(new ImportacaoExcelMsg { Msg = "O número de faixas peso x preço estão inconsistentes", Error = true, Cel = string.Empty, Vlr = f.Nr_PesoFim.ToString() });
                }
                tt = 0;

                foreach (var f in listRegs)
                {
                    if (f.Nr_Prazo == default)
                        retMsg.Add(new ImportacaoExcelMsg { Msg = "Existe região com prazo zerado", Error = true, Cel = string.Empty, Vlr = f.Ds_Regiao });

                    if (f.Nr_PesoMaximo <= 0 || f.Nr_PesoMaximo == default)
                        f.Nr_PesoMaximo = 9999;

                    var t = listPrecos.Where(a => a.Id_Regiao == f.Cd_Id).OrderBy(a => a.Nr_PesoFim).ToList();
                    if (tt == default)
                        tt = t.Count;
                    else if (tt != t.Count)
                        retMsg.Add(new ImportacaoExcelMsg { Msg = "O número de faixas regiões x preço estão inconsistentes", Error = true, Cel = string.Empty, Vlr = f.Ds_Regiao });

                    decimal vlr = 0;
                    foreach (var p in t)
                    {
                        if (p.Nr_Valor <= vlr)
                            retMsg.Add(new ImportacaoExcelMsg { Msg = $"Valor da faixa [{p.Nr_PesoFim}] menor que o da anteior {vlr} -> {p.Nr_Valor}", Error = true, Cel = string.Empty, Vlr = f.Ds_Regiao });
                        vlr = p.Nr_Valor;
                    }

                    var cepFim = listCEP.Where(a => a.Id_Regiao == f.Cd_Id).OrderBy(a => a.CepFim).ToList();
                    var lastCepFim = 0;
                    foreach (var c in cepFim)
                    {
                        if (lastCepFim != 0 && lastCepFim != c.CepInicio)
                            retMsg.Add(new ImportacaoExcelMsg { Msg = $"Existe furo de cep na região CEP Fim [ {lastCepFim - 1} ] e CEP Inicio [ {c.CepInicio} ]", Error = false, Cel = string.Empty, Vlr = f.Ds_Regiao });
                        lastCepFim = c.CepFim + 1;
                    }
                }
            }

            if (!retMsg.Any(a => a.Error))
                _repository.InserirLista(tabelaArquivo.IdTabela, tabelaArquivo.IdEmpresa, tabelaArquivo.IdTransportador, false,
                    CollectionHelper.ConvertTo(listFaixasPeso), CollectionHelper.ConvertTo(listRegs), CollectionHelper.ConvertTo(listPrecos),
                    CollectionHelper.ConvertTo(listCEP));

        }

        private void ProcessarArquivoVtex(TabelaArquivoProcessamento tabelaArquivo, ListImportacaoExcelMsg retMsg)
        {
            var lstVtex = new List<VtexModel>();

            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(FileStream))
            {
                do
                {
                    int linha = 1;
                    while (reader.Read())
                    {
                        var dadosVtex = new VtexModel() { Flg_Ativo = true };

                        if (linha > 1)
                        {
                            dadosVtex.Id_Empresa = tabelaArquivo.IdEmpresa;
                            dadosVtex.Id_Transportador = tabelaArquivo.IdTransportador;
                            dadosVtex.Cd_CepInicio = reader.GetValue(0).ToString().Replace("{", "").Replace("}", "");
                            dadosVtex.Cd_CepFim = reader.GetValue(1).ToString().Replace("{", "").Replace("}", "");
                            dadosVtex.Ds_NomeGeolicazacao = reader.GetValue(2).ToString();

                            dadosVtex.Nr_PesoInicial = reader.GetValue(3).ToString().ExcelDataToDecimal(linha, 3, retMsg);
                            dadosVtex.Nr_PesoFim = reader.GetValue(4).ToString().ExcelDataToDecimal(linha, 4, retMsg);
                            dadosVtex.Nr_CustoAbsoluto = reader.GetValue(5).ToString().ExcelDataToDecimal(linha, 5, retMsg);
                            dadosVtex.Nr_ProcentagemDePrecoAdicional = reader.GetValue(6).ToString().ExcelDataToDecimal(linha, 6, retMsg);
                            dadosVtex.Nr_PrecoPesoExtra = reader.GetValue(7).ToString().ExcelDataToDecimal(linha, 7, retMsg);
                            dadosVtex.Nr_VolumeMaximo = reader.GetValue(8).ToString().ExcelDataToDecimal(linha, 8, retMsg);
                            dadosVtex.Ds_PrazoEntrega = reader.GetValue(9).ToString();
                            dadosVtex.Ds_Pais = reader.GetValue(10).ToString();
                            dadosVtex.Nr_TaxaAdicionalDeSeguro = reader.GetValue(11).ToString().ExcelDataToDecimal(linha, 11, retMsg);

                            lstVtex.Add(dadosVtex);
                        }

                        linha++;
                    }

                    if (lstVtex.Count == 0)
                        retMsg.Add(new ImportacaoExcelMsg { Msg = "Nenhuma faixa de peso foi lida.", Error = true, Cel = string.Empty, Vlr = string.Empty });

                } while (reader.NextResult());

                if (!retMsg.Any(a => a.Error))
                {
                    _repository.InserirListaVtex(tabelaArquivo.IdTabela, false, CollectionHelper.ConvertTo(lstVtex));
                }
            }
        }


        private void ProcessarArquivoCorreios(TabelaArquivoProcessamento tabelaArquivo, ListImportacaoExcelMsg retMsg)
        {
            var listFaixasPeso = new List<TabelaPesoModel>();
            var listRegs = new List<RegiaoCorreiosModel>();
            var listPrecos = new List<TabelaPrecoModel>();

            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(FileStream))
            {
                int coluna = 0;
                int colunaRegiao = 1;
                int linha = 1;

                while (reader.Read())
                {
                    if (linha == 1)
                    {
                        while (colunaRegiao <= reader.FieldCount - 1 && !string.IsNullOrEmpty(reader.GetValue(colunaRegiao)?.ToString()))
                        {
                            listRegs.Add(new RegiaoCorreiosModel()
                            {
                                Cd_Id = -colunaRegiao,
                                Ds_Regiao = reader.GetValue(colunaRegiao).ToString()
                            });
                            colunaRegiao++;
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(reader.GetValue(coluna)?.ToString()))
                            break;

                        decimal.TryParse(reader.GetValue(coluna).ToString(), out decimal peso);

                        var faixaPeso = new TabelaPesoModel()
                        {
                            Cd_Id = -(coluna - listFaixasPeso.Count()) + 1,
                            Nr_PesoFim = (peso / 1000),
                            Flg_Ativo = true
                        };

                        listRegs.ForEach(regiao =>
                        {
                            coluna++;
                            decimal.TryParse(reader.GetValue(coluna).ToString(), out decimal preco);

                            listPrecos.Add(new TabelaPrecoModel()
                            {
                                Id_Regiao = regiao.Cd_Id,
                                Id_TabelaPeso = faixaPeso.Cd_Id,
                                Nr_Valor = preco
                            });
                        });

                        listFaixasPeso.Add(faixaPeso);

                        coluna = 0;
                    }
                    linha++;
                }

                // Volta para a primeira linha e avança para a proxima planilha
                reader.Reset();
                reader.NextResult();

                linha = 1;
                while (reader.Read())
                {
                    if (linha > 1 && !string.IsNullOrEmpty(reader.GetValue(0)?.ToString()))
                    {
                        var regiaoTaxa = reader.GetValue(0)?.ToString();

                        if (listRegs.Any(a => a.Ds_Regiao == regiaoTaxa))
                        {
                            var regiao = listRegs.FirstOrDefault(a => a.Ds_Regiao == regiaoTaxa);

                            regiao.Nr_PesoExcedente = reader.GetValue(1).ToString().ExcelDataToDecimal(linha, 1, retMsg);
                            regiao.Nr_PedagioFixo = reader.GetValue(2).ToString().ExcelDataToDecimal(linha, 2, retMsg);
                            regiao.Nr_PedagioVariavel = reader.GetValue(3).ToString().ExcelDataToDecimal(linha, 3, retMsg);
                            regiao.Nr_FracaoPesoPedagio = reader.GetValue(4).ToString().ExcelDataToDecimal(linha, 4, retMsg);
                            regiao.Nr_PercentualGRIS = reader.GetValue(5).ToString().ExcelDataToDecimal(linha, 5, retMsg);
                            regiao.Nr_ValorMinimoGRIS = reader.GetValue(6).ToString().ExcelDataToDecimal(linha, 6, retMsg);
                            regiao.Nr_PercentualAdValorem = reader.GetValue(7).ToString().ExcelDataToDecimal(linha, 7, retMsg);
                            regiao.Nr_ValorMinimoAdvalorem = reader.GetValue(8).ToString().ExcelDataToDecimal(linha, 8, retMsg);
                            regiao.Nr_PercentualBalsa = reader.GetValue(9).ToString().ExcelDataToDecimal(linha, 9, retMsg);
                            regiao.Nr_ValorMinimoFluvial = reader.GetValue(10).ToString().ExcelDataToDecimal(linha, 10, retMsg);
                            regiao.Nr_PercentualRedespachoFluvial = reader.GetValue(11).ToString().ExcelDataToDecimal(linha, 11, retMsg);
                            regiao.Nr_FreteAdicional = reader.GetValue(12).ToString().ExcelDataToDecimal(linha, 12, retMsg);
                            regiao.Nr_ValorSuframa = reader.GetValue(13).ToString().ExcelDataToDecimal(linha, 13, retMsg);
                            regiao.Nr_ValorMinimoTRT = reader.GetValue(14).ToString().ExcelDataToDecimal(linha, 14, retMsg);
                            regiao.Nr_PercentualTRT = reader.GetValue(15).ToString().ExcelDataToDecimal(linha, 15, retMsg);
                            regiao.Nr_ValorMinimoTDE = reader.GetValue(16).ToString().ExcelDataToDecimal(linha, 16, retMsg);
                            regiao.Nr_PercentualTDE = reader.GetValue(17).ToString().ExcelDataToDecimal(linha, 17, retMsg);
                            regiao.Nr_ValorMinimoTDA = reader.GetValue(18).ToString().ExcelDataToDecimal(linha, 18, retMsg);
                            regiao.Nr_PercentualTDA = reader.GetValue(19).ToString().ExcelDataToDecimal(linha, 19, retMsg);
                            regiao.Nr_ValorCTE = reader.GetValue(20).ToString().ExcelDataToDecimal(linha, 20, retMsg);
                            regiao.Nr_ValorTaxaRisco = reader.GetValue(21).ToString().ExcelDataToDecimal(linha, 21, retMsg);
                        }
                    }
                    linha++;
                }
            }

            if (!retMsg.Any(a => a.Error))
                _repository.InserirListaCorreios(tabelaArquivo.IdTabela, tabelaArquivo.IdEmpresa, tabelaArquivo.IdTransportador, false,
                    CollectionHelper.ConvertTo(listFaixasPeso), CollectionHelper.ConvertTo(listRegs), CollectionHelper.ConvertTo(listPrecos));
        }
    }
}