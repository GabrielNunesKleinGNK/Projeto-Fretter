using System;

namespace Fretter.Domain.Entities
{
    public class Entrega : EntityBase
    {
        #region "Construtores"
        public Entrega(int Id, DateTime Importacao, int Canal, int EmpresaTransportador, DateTime? Finalizado, string Sro,
            string CodigoEntrega, string CodigoPedido, string NotaFiscal, string Serie, char CepOrigem, char? CepDestino,
            char? CepPostagem, DateTime? DataPostagem, DateTime? PrazoTransportador, DateTime? PrevistaEntrega, DateTime? DataPesquisa,
            string Contrato, string Postagem, string LocalPostagem, string Cidade, string Uf, int? CanalVenda,
            string CategoriaEntrega, int TransportadorCnpj, string PI, DateTime? DataAbertura, string ServicoCorreios,
            decimal? Frete, int? Arquivo, int? PrazoDias, bool? EntregaSabado, bool? ServicoCorreiosVerificado, int IsNULL,
            int EmpresaId, int? Transportador, int? EmpresaMarketPlace, byte TipoServico, DateTime? Devolucao,
            DateTime? PrazoDevolucao, string Ocorrencia, DateTime? DataOcorrencia, string Sigla, int? EntregaOcorrencia,
            bool? DtDevolucaoVerificada, DateTime? ImportacaoEmpresa, DateTime? ImportacaoMarketplace,
            DateTime? ImportacaoTransportador, string CnpjTerceiro, string NotaTerceiro, string SerieTerceiro,
            int? DtOcorrencia, int? FinalizadoNr, DateTime? Alteracao, string NfSerie, int? MicroServico, string AWB,
            int? Tabela, DateTime? EmissaoNF, string Danfe, DateTime? PostagemDATE, int TransportadorPadrao, decimal? CustoFrete,
            DateTime? DataDin, int? Din, string SroUnico, int? SroComprimento, bool? Rastreiar, string SroTratado, string Tempos,
            DateTime? ImportacaoDATE, bool? EnviadoElastic, int? TransportadorAlias, int? Volumes, bool? DropShipping,
            int? EmpresaDRS, int? CanalDRS, string NotaFiscalDRS, string SerieDRS, string DanfeDRS, string NfSerieDRS,
            int? CargaCliente)
        {
            this.Ativar();
            this.Id = Id;
            this.Importacao = Importacao;
            this.Canal = Canal;
            this.EmpresaTransportador = EmpresaTransportador;
            this.Finalizado = Finalizado;
            this.Sro = Sro;
            this.CodigoEntrega = CodigoEntrega;
            this.CodigoPedido = CodigoPedido;
            this.NotaFiscal = NotaFiscal;
            this.Serie = Serie;
            this.CepOrigem = CepOrigem;
            this.CepDestino = CepDestino;
            this.CepPostagem = CepPostagem;
            this.DataPostagem = DataPostagem;
            this.PrazoTransportador = PrazoTransportador;
            this.PrevistaEntrega = PrevistaEntrega;
            this.DataPesquisa = DataPesquisa;
            this.Contrato = Contrato;
            this.Postagem = Postagem;
            this.LocalPostagem = LocalPostagem;
            this.Cidade = Cidade;
            this.Uf = Uf;
            this.CanalVenda = CanalVenda;
            this.CategoriaEntrega = CategoriaEntrega;
            this.TransportadorCnpj = TransportadorCnpj;
            this.PI = PI;
            this.DataAbertura = DataAbertura;
            this.ServicoCorreios = ServicoCorreios;
            this.Frete = Frete;
            this.Arquivo = Arquivo;
            this.PrazoDias = PrazoDias;
            this.EntregaSabado = EntregaSabado;
            this.ServicoCorreiosVerificado = ServicoCorreiosVerificado;
            this.IsNULL = IsNULL;
            this.EmpresaId = EmpresaId;
            this.Transportador = Transportador;
            this.EmpresaMarketPlace = EmpresaMarketPlace;
            this.TipoServico = TipoServico;
            this.Devolucao = Devolucao;
            this.PrazoDevolucao = PrazoDevolucao;
            this.Ocorrencia = Ocorrencia;
            this.DataOcorrencia = DataOcorrencia;
            this.Sigla = Sigla;
            this.EntregaOcorrencia = EntregaOcorrencia;
            this.DtDevolucaoVerificada = DtDevolucaoVerificada;
            this.ImportacaoEmpresa = ImportacaoEmpresa;
            this.ImportacaoMarketplace = ImportacaoMarketplace;
            this.ImportacaoTransportador = ImportacaoTransportador;
            this.CnpjTerceiro = CnpjTerceiro;
            this.NotaTerceiro = NotaTerceiro;
            this.SerieTerceiro = SerieTerceiro;
            this.DtOcorrencia = DtOcorrencia;
            this.FinalizadoNr = FinalizadoNr;
            this.Alteracao = Alteracao;
            this.NfSerie = NfSerie;
            this.MicroServico = MicroServico;
            this.AWB = AWB;
            this.Tabela = Tabela;
            this.EmissaoNF = EmissaoNF;
            this.Danfe = Danfe;
            this.PostagemDATE = PostagemDATE;
            this.TransportadorPadrao = TransportadorPadrao;
            this.CustoFrete = CustoFrete;
            this.DataDin = DataDin;
            this.Din = Din;
            this.SroUnico = SroUnico;
            this.SroComprimento = SroComprimento;
            this.Rastreiar = Rastreiar;
            this.SroTratado = SroTratado;
            this.Tempos = Tempos;
            this.ImportacaoDATE = ImportacaoDATE;
            this.EnviadoElastic = EnviadoElastic;
            this.TransportadorAlias = TransportadorAlias;
            this.Volumes = Volumes;
            this.DropShipping = DropShipping;
            this.EmpresaDRS = EmpresaDRS;
            this.CanalDRS = CanalDRS;
            this.NotaFiscalDRS = NotaFiscalDRS;
            this.SerieDRS = SerieDRS;
            this.DanfeDRS = DanfeDRS;
            this.NfSerieDRS = NfSerieDRS;
            this.CargaCliente = CargaCliente;
        }
        #endregion

        #region "Propriedades"
        public DateTime Importacao { get; protected set; }
        public int Canal { get; protected set; }
        public int EmpresaTransportador { get; protected set; }
        public DateTime? Finalizado { get; protected set; }
        public string Sro { get; protected set; }
        public string CodigoEntrega { get; protected set; }
        public string CodigoPedido { get; protected set; }
        public string NotaFiscal { get; protected set; }
        public string Serie { get; protected set; }
        public char CepOrigem { get; protected set; }
        public char? CepDestino { get; protected set; }
        public char? CepPostagem { get; protected set; }
        public DateTime? DataPostagem { get; protected set; }
        public DateTime? PrazoTransportador { get; protected set; }
        public DateTime? PrevistaEntrega { get; protected set; }
        public DateTime? DataPesquisa { get; protected set; }
        public string Contrato { get; protected set; }
        public string Postagem { get; protected set; }
        public string LocalPostagem { get; protected set; }
        public string Cidade { get; protected set; }
        public string Uf { get; protected set; }
        public int? CanalVenda { get; protected set; }
        public string CategoriaEntrega { get; protected set; }
        public int TransportadorCnpj { get; protected set; }
        public string PI { get; protected set; }
        public DateTime? DataAbertura { get; protected set; }
        public string ServicoCorreios { get; protected set; }
        public decimal? Frete { get; protected set; }
        public int? Arquivo { get; protected set; }
        public int? PrazoDias { get; protected set; }
        public bool? EntregaSabado { get; protected set; }
        public bool? ServicoCorreiosVerificado { get; protected set; }
        public int IsNULL { get; protected set; }
        public int EmpresaId { get; protected set; }
        public int? Transportador { get; protected set; }
        public int? EmpresaMarketPlace { get; protected set; }
        public byte TipoServico { get; protected set; }
        public DateTime? Devolucao { get; protected set; }
        public DateTime? PrazoDevolucao { get; protected set; }
        public string Ocorrencia { get; protected set; }
        public DateTime? DataOcorrencia { get; protected set; }
        public string Sigla { get; protected set; }
        public int? EntregaOcorrencia { get; protected set; }
        public bool? DtDevolucaoVerificada { get; protected set; }
        public DateTime? ImportacaoEmpresa { get; protected set; }
        public DateTime? ImportacaoMarketplace { get; protected set; }
        public DateTime? ImportacaoTransportador { get; protected set; }
        public string CnpjTerceiro { get; protected set; }
        public string NotaTerceiro { get; protected set; }
        public string SerieTerceiro { get; protected set; }
        public int? DtOcorrencia { get; protected set; }
        public int? FinalizadoNr { get; protected set; }
        public DateTime? Alteracao { get; protected set; }
        public string NfSerie { get; protected set; }
        public int? MicroServico { get; protected set; }
        public string AWB { get; protected set; }
        public int? Tabela { get; protected set; }
        public DateTime? EmissaoNF { get; protected set; }
        public string Danfe { get; protected set; }
        public DateTime? PostagemDATE { get; protected set; }
        public int TransportadorPadrao { get; protected set; }
        public decimal? CustoFrete { get; protected set; }
        public DateTime? DataDin { get; protected set; }
        public int? Din { get; protected set; }
        public string SroUnico { get; protected set; }
        public int? SroComprimento { get; protected set; }
        public bool? Rastreiar { get; protected set; }
        public string SroTratado { get; protected set; }
        public string Tempos { get; protected set; }
        public DateTime? ImportacaoDATE { get; protected set; }
        public bool? EnviadoElastic { get; protected set; }
        public int? TransportadorAlias { get; protected set; }
        public int? Volumes { get; protected set; }
        public bool? DropShipping { get; protected set; }
        public int? EmpresaDRS { get; protected set; }
        public int? CanalDRS { get; protected set; }
        public string NotaFiscalDRS { get; protected set; }
        public string SerieDRS { get; protected set; }
        public string DanfeDRS { get; protected set; }
        public string NfSerieDRS { get; protected set; }
        public int? CargaCliente { get; protected set; }
        #endregion

        #region "Referencias"		
        public Empresa Empresa { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarImportacao(DateTime Importacao) => this.Importacao = Importacao;
        public void AtualizarCanal(int Canal) => this.Canal = Canal;
        public void AtualizarEmpresaTransportador(int EmpresaTransportador) => this.EmpresaTransportador = EmpresaTransportador;
        public void AtualizarFinalizado(DateTime? Finalizado) => this.Finalizado = Finalizado;
        public void AtualizarSro(string Sro) => this.Sro = Sro;
        public void AtualizarCodigoEntrega(string codigo) => this.CodigoEntrega = codigo;
        public void AtualizarCodigoPedido(string codigo) => this.CodigoPedido = codigo;
        public void AtualizarNotaFiscal(string NotaFiscal) => this.NotaFiscal = NotaFiscal;
        public void AtualizarSerie(string Serie) => this.Serie = Serie;
        public void AtualizarCepOrigem(char CepOrigem) => this.CepOrigem = CepOrigem;
        public void AtualizarCepDestino(char? CepDestino) => this.CepDestino = CepDestino;
        public void AtualizarCepPostagem(char? CepPostagem) => this.CepPostagem = CepPostagem;
        public void AtualizarDataPostagem(DateTime? data) => this.DataPostagem = data;
        public void AtualizarPrazoTransportador(DateTime? PrazoTransportador) => this.PrazoTransportador = PrazoTransportador;
        public void AtualizarPrevistaEntrega(DateTime? PrevistaEntrega) => this.PrevistaEntrega = PrevistaEntrega;
        public void AtualizarPesquisa(DateTime? Pesquisa) => this.DataPesquisa = Pesquisa;
        public void AtualizarContrato(string Contrato) => this.Contrato = Contrato;
        public void AtualizarPostagem(string Postagem) => this.Postagem = Postagem;
        public void AtualizarLocalPostagem(string LocalPostagem) => this.LocalPostagem = LocalPostagem;
        public void AtualizarCidade(string Cidade) => this.Cidade = Cidade;
        public void AtualizarUf(string Uf) => this.Uf = Uf;
        public void AtualizarCanalVenda(int? CanalVenda) => this.CanalVenda = CanalVenda;
        public void AtualizarCategoriaEntrega(string CategoriaEntrega) => this.CategoriaEntrega = CategoriaEntrega;
        public void AtualizarTransportadorCnpj(int TransportadorCnpj) => this.TransportadorCnpj = TransportadorCnpj;
        public void AtualizarPI(string PI) => this.PI = PI;
        public void AtualizarAbertura(DateTime? Abertura) => this.DataAbertura = Abertura;
        public void AtualizarServicoCorreios(string ServicoCorreios) => this.ServicoCorreios = ServicoCorreios;
        public void AtualizarFrete(decimal? Frete) => this.Frete = Frete;
        public void AtualizarArquivo(int? Arquivo) => this.Arquivo = Arquivo;
        public void AtualizarPrazoDias(int? PrazoDias) => this.PrazoDias = PrazoDias;
        public void AtualizarEntregaSabado(bool? EntregaSabado) => this.EntregaSabado = EntregaSabado;
        public void AtualizarServicoCorreiosVerificado(bool? ServicoCorreiosVerificado) => this.ServicoCorreiosVerificado = ServicoCorreiosVerificado;
        public void AtualizarIsNULL(int IsNULL) => this.IsNULL = IsNULL;
        public void AtualizarEmpresa(int EmpresaId) => this.EmpresaId = EmpresaId;
        public void AtualizarTransportador(int? Transportador) => this.Transportador = Transportador;
        public void AtualizarEmpresaMarketPlace(int? EmpresaMarketPlace) => this.EmpresaMarketPlace = EmpresaMarketPlace;
        public void AtualizarTipoServico(byte TipoServico) => this.TipoServico = TipoServico;
        public void AtualizarDevolucao(DateTime? Devolucao) => this.Devolucao = Devolucao;
        public void AtualizarPrazoDevolucao(DateTime? PrazoDevolucao) => this.PrazoDevolucao = PrazoDevolucao;
        public void AtualizarOcorrencia(string Ocorrencia) => this.Ocorrencia = Ocorrencia;
        public void AtualizarOcorrencia(DateTime? Ocorrencia) => this.DataOcorrencia = Ocorrencia;
        public void AtualizarSigla(string Sigla) => this.Sigla = Sigla;
        public void AtualizarEntregaOcorrencia(int? EntregaOcorrencia) => this.EntregaOcorrencia = EntregaOcorrencia;
        public void AtualizarDtDevolucaoVerificada(bool? DtDevolucaoVerificada) => this.DtDevolucaoVerificada = DtDevolucaoVerificada;
        public void AtualizarImportacaoEmpresa(DateTime? ImportacaoEmpresa) => this.ImportacaoEmpresa = ImportacaoEmpresa;
        public void AtualizarImportacaoMarketplace(DateTime? ImportacaoMarketplace) => this.ImportacaoMarketplace = ImportacaoMarketplace;
        public void AtualizarImportacaoTransportador(DateTime? ImportacaoTransportador) => this.ImportacaoTransportador = ImportacaoTransportador;
        public void AtualizarCnpjTerceiro(string CnpjTerceiro) => this.CnpjTerceiro = CnpjTerceiro;
        public void AtualizarNotaTerceiro(string NotaTerceiro) => this.NotaTerceiro = NotaTerceiro;
        public void AtualizarSerieTerceiro(string SerieTerceiro) => this.SerieTerceiro = SerieTerceiro;
        public void AtualizarDtOcorrencia(int? DtOcorrencia) => this.DtOcorrencia = DtOcorrencia;
        public void AtualizarFinalizadoNr(int? FinalizadoNr) => this.FinalizadoNr = FinalizadoNr;
        public void AtualizarAlteracao(DateTime? Alteracao) => this.Alteracao = Alteracao;
        public void AtualizarDescricaoSerie(string desc) => this.NfSerie = desc;
        public void AtualizarMicroServico(int? MicroServico) => this.MicroServico = MicroServico;
        public void AtualizarAWB(string AWB) => this.AWB = AWB;
        public void AtualizarTabela(int? Tabela) => this.Tabela = Tabela;
        public void AtualizarEmissaoNF(DateTime? EmissaoNF) => this.EmissaoNF = EmissaoNF;
        public void AtualizarDanfe(string Danfe) => this.Danfe = Danfe;
        public void AtualizarPostagemDATE(DateTime? PostagemDATE) => this.PostagemDATE = PostagemDATE;
        public void AtualizarTransportadorPadrao(int TransportadorPadrao) => this.TransportadorPadrao = TransportadorPadrao;
        public void AtualizarCustoFrete(decimal? CustoFrete) => this.CustoFrete = CustoFrete;
        public void AtualizarDin(DateTime? data) => this.DataDin = data;
        public void AtualizarDin(int? Din) => this.Din = Din;
        public void AtualizarSroUnico(string SroUnico) => this.SroUnico = SroUnico;
        public void AtualizarSroComprimento(int? SroComprimento) => this.SroComprimento = SroComprimento;
        public void AtualizarRastreiar(bool? Rastreiar) => this.Rastreiar = Rastreiar;
        public void AtualizarSroTratado(string SroTratado) => this.SroTratado = SroTratado;
        public void AtualizarTempos(string Tempos) => this.Tempos = Tempos;
        public void AtualizarImportacaoDATE(DateTime? ImportacaoDATE) => this.ImportacaoDATE = ImportacaoDATE;
        public void AtualizarEnviadoElastic(bool? EnviadoElastic) => this.EnviadoElastic = EnviadoElastic;
        public void AtualizarTransportadorAlias(int? TransportadorAlias) => this.TransportadorAlias = TransportadorAlias;
        public void AtualizarVolumes(int? Volumes) => this.Volumes = Volumes;
        public void AtualizarDropShipping(bool? DropShipping) => this.DropShipping = DropShipping;
        public void AtualizarEmpresaDRS(int? EmpresaDRS) => this.EmpresaDRS = EmpresaDRS;
        public void AtualizarCanalDRS(int? CanalDRS) => this.CanalDRS = CanalDRS;
        public void AtualizarNotaFiscalDRS(string NotaFiscalDRS) => this.NotaFiscalDRS = NotaFiscalDRS;
        public void AtualizarSerieDRS(string SerieDRS) => this.SerieDRS = SerieDRS;
        public void AtualizarDanfeDRS(string DanfeDRS) => this.DanfeDRS = DanfeDRS;
        public void AtualizarNFSerieDRS(string SerieDRS) => this.NfSerieDRS = SerieDRS;
        public void AtualizarCargaCliente(int? CargaCliente) => this.CargaCliente = CargaCliente;
        #endregion
    }
}
