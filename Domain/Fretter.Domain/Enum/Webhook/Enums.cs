using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Fretter.Domain.Enum.Webhook
{
    public static class Enums
    {
        public enum Permissao : int
        {
            Novo = 1,
            Editar = 2,
            Visualizar = 3,
            Importar = 4,
            AtivarDesativar = 5,
            Atualizar = 6
        }

        public enum AtendimentoStatus : int
        {
            Aberto = 1,
            EmAndamento = 2,
            Fechado = 3
        }

        public enum AtendimentoTipo : int
        {
            Chamado = 3,
            Históricodechamado = 2
        }

        public enum ChaveArquivoOcorrencia : short
        {
            Transportador = 341,
            Ocorrencia = 342
        }

        public enum Responsavel : byte
        {
            Indefinido = 0,
            Embarcador = 1,
            Transportador = 2,
            MarketPlace = 3
        }

        public enum EntregaAtraso : byte
        {
            Todas = 0,
            AtrasoAberto = 1,
            AtrasoFinalizado = 2
        }

        public enum TipoStatusEntrega : byte
        {
            Todas = 0,
            Abertas = 1,
            Finalizadas = 2,
            SemOcorrencia = 3,
            EntregasEstagnadas = 4
        }

        public enum AcaoPI : byte
        {
            AjustaPrazo = 1,
            Roubo = 2,//Retorno PI
            Avaria = 3,//Retorno PI
            Extravio = 4,//Retorno PI

            Entregue = 5,

            Manual = 6,

            PIAberta = 7,
            PIRecusada = 8,
            Aguardando = 9,

            ManualSolicitacao = 10,


            SolicitacaoRecusada = 11,
            Atraso = 12,
            PIAbertaCliente = 13,
            AjustaPrazoResposta = 14,
            SolicitarNovamente = 15,
            AnalisarAnterior = 16
        }
        public enum EntregaContatoTipo : byte
        {
            Email = 1,
            Telefone1 = 2,
            Telefone2 = 3,
            Telefone3 = 4
        }

        public static class EntregaParametroTipo
        {
            public enum Bling_12 : byte
            {
                Id_Servico = 1, // Tipo Conexão 12 - Bling
                StatusNF = 2, // Tipo Conexão 12 - Bling
                TransportadorNF = 3, // Tipo Conexão 12 - Bling
            }

            public enum BSeller_12 : byte
            {
                SituacaoNF = 5, // Tipo Conexão 13 - BSeller 
                TipoDestinatario = 6
            }
        }

        public enum PIRespostaTipo : byte
        {
            Aceita = 1,
            Recusada = 2,
            Aguardar = 3
        }

        public enum PIRespostaOrigem : byte
        {
            Imap = 1,
            Ftp = 2,
            ImapLote = 3,
            Validação = 4,
            Email = 5,
            EmailLote = 6
        }

        public enum OrigemImportacao : byte
        {
            Web = 1,
            Ws = 2,
            Ftp = 3,
            Email = 4,
            RasCorreio = 5,
            RasDHL = 6,
            RasFedex = 7,
            RasTransp = 8,
            Mobile = 9,
            WebHook = 10,
            ServicoImp = 11,
            RasJadlog = 12,
            RasDirect = 13,
            RasMotoboy = 14,
            RasMelhorEnvio = 15,
            RasTransfolha = 16,
            RasLinx = 17,
            RasTranspoFrete = 18,
            RasToyMania = 19,
            RasMandae = 20,
            RasJamef = 21,
            RasConfirmaFacil = 22,
            RasSSW = 23,
            RasTNT = 24,
            RasPatrus = 25,
            RasPneuBest = 26,
            RasOlist = 27,
            RasPlimor = 28,
            RasMMA = 29,
            RasAlfa = 30,
            RasExata = 31,
            RasAgrotama = 32,
            RasASAPLog = 33,
            FreteFacil = 34,
            Sequoia = 35,
            EuEntrego = 36,
            Loggi = 37
        }

        public enum TipoConexao : byte
        {
            [Display(Name = "FTP")]
            Ftp = 1,
            [Display(Name = "WS")]
            WebService = 2,
            [Display(Name = "FRONT")]
            Front = 3,
            [Display(Name = "E-mail")]
            Email = 4,
            [Display(Name = "SMS")]
            Sms = 5,
            [Display(Name = "E-mail Destinatário")]
            EmailDestinatario = 6,
            [Display(Name = "Pasta")]
            Pasta = 7,
            [Display(Name = "Imap")]
            Imap = 8,
            [Display(Name = "SFTP")]
            Sftp = 9,
            [Display(Name = "URA")]
            URA = 10,
            [Display(Name = "Query")]
            Query = 11,
            [Display(Name = "Bling")]
            Bling = 12,
            [Display(Name = "BSeller")]
            BSeller = 13,
            [Display(Name = "Tiny")]
            Tiny = 14,
            [Display(Name = "SMS Amazon")]
            AmazonSms = 15,
            [Display(Name = "Carrefour")]
            Carrefour = 16,
            [Display(Name = "Carrefour - Status")]
            CarrefourStatus = 17,
            [Display(Name = "AnyMarket")]
            AnyMarket = 18,
            [Display(Name = "VNDA")]
            VNDA = 19,
            [Display(Name = "Protheus")]
            Protheus = 21
        }

        public enum ExpedicaoEnvioTipo : short
        {
            Gadle = 1,
            PLP = 2,
            Email = 3,
            Sigep = 4,
            Ftp = 5,
            Direct = 6,
            SSW = 7
        }

        public enum ExpedicaoEnvioParametroTipo : short
        {
            CodigoClienteDirect = 1,
            FusoHorario = 2,
            NotfisUnificado = 3,
            QtdDiasExpurgoNotfis = 4,
            CampoPedido = 5,
            ListaCodigoCliente = 6,
            EnumeraSroComMultiplosVolumes = 7
        }

        public enum ExpedicaoEnvioArquivo : short
        {
            Gadle = 1,
            PLP = 2,
            Notfis = 3,
            Sigep = 4,
            Direct = 5,
            SSW = 6
        }

        public enum TipoInterface : byte
        {
            ViaVarejo = 1,
            Skyhub = 2,
            Anymarket = 3,
            Carrefour = 4,
            RicardoEletro = 5,
            MercadoLivre = 6,
            Magalu = 7,
            VVLog = 8,
            Obter = 9,
            ObterMultiCanal = 10,
            Kabum = 11,
            VVFretes = 12,
            MadeiraMadeira = 13,
            Tray = 14
        }

        public enum ImportacaoConfigArquivo : byte
        {
            Fusion__CSV = 1,
            MaquinaDeVendas__XML = 2,
            MarketplaceVV__CSV = 3,
            Notfis__TXT = 4,
            NFe__XML = 5,
            Bling__Json = 6,
            MidiaSara__TXT = 7,
            BSeller__Json = 8,
            Tiny__Json = 9,
            Carrefour__Json = 10,
            AnyMarket__Json = 11
        }

        public enum RastreamentoConfigArquivo : byte
        {
            Fusion__TXT = 1,
            Fusion__XLS = 2,
            Fusion__XLSX = 3,
            Fusion__TXT_5_0 = 4,
            FusionSemRoteiro__CSV = 5
        }

        public enum ImportacaoConfigParametroTipo : byte
        {
            Porta = 1,
            EmailsAutorizados = 2,
            Query = 3,
            Cnpj_Transportador = 4,
            Cnpj_Canal = 5,
            Origem_Copia = 6,
            Limite_Data_Copia = 7,
            Ds_CanalVenda = 8,
            Filtro_Extensoes_Arquivos = 9,
            DiasRetroativo = 10,
            ConsultaTransportadorNfe = 11,
            IdLojistas = 12,
            CampoDataPrevistaEntregaBSeller = 13,
            FusoHorario = 14,
            RaizTransportadores = 15,
            DataCorteNotaTransportador = 16,
            IdLojistasImplantados = 17,
            DimensaoPadrao = 18,
            RecotacaoFrete = 19,
            ComplementoTiny = 20,
            InterfacesBSeller = 21,
            MecanicaNovaBSeller = 22,
            CampoAdicionalDanfe = 23,
            CampoAdicionalDataEmissaoNF = 24,
            CampoAdicionalEnderecoReferencia = 25,
            CampoAdicionalTelefoneDestinatario = 26,
            SerieDropShipping = 27,
            CodEstabelecimentoCrossDocking = 28,
            CodEstabelecimentoFisico = 29
        }

        public enum ImportacaoConfigTipo : byte
        {
            Entrega = 1,
            Pedido = 2,
            Produto = 3,
            Estoque = 4
        }

        public enum RastreamentoConfigTipo : byte
        {
            Correio = 1,
            Fedex = 2,
            DHL = 3,
            Passivo = 4,
            Jadlog = 5,
            Motoboy = 6,
            MelhorEnvio = 7,
            FusionMobile = 8,
            Transfolha = 9,
            Direct = 10,
            Linx = 11,
            TranspoFrete = 12,
            ToyMania = 13,
            Mandae = 14,
            WebhookSync = 15,
            Jamef = 16,
            ConfirmaFacil = 17,
            SSW = 18,
            TNT = 19,
            Patrus = 20,
            PneuBest = 21,
            Olist = 22,
            Plimor = 23,
            MMA = 24,
            Alfa = 25,
            Exata = 26,
            Agrotama = 27,
            ASAPLog = 28
        }

        public enum EmpresaConfigPITipoAbertura : byte
        {
            WebService = 1, //(Somente com Contrato)
            FTP = 2,//    (Somente com Contrato)
            Email = 3, //(Somente com Contrato)
            WebSite = 4
        }

        public enum EmpresaConfigPIParametroTipo : byte
        {
            CodigoEmpresa = 1,
            SenhaWSPrazoPreco = 2,
            FTPPastaRaiz = 3,
            FTPPastaEntrada = 4,
            FTPPastaRetorno = 5,
            FTPPastaReativacao = 6,
            FTPPastaRetornoReativacao = 7,
            FTPPastaCobrança = 8,
            FTPPastaRetornoCobranca = 9,
            TemplatedeNomeDeArquivoEntrada = 10,
            FiltroArquivoRetornoEntrada = 11,
            TemplateDeNomeDeArquivoReativação = 12,
            FiltroArquivoRetornoReativação = 13,
            TemplateDeNomeDeArquivoCobrança = 14,
            FiltroArquivoRetornoCobranca = 15,
            FTPUsuário = 16,
            FTPSenha = 17,
            EMailDeRecebimentoDeResposta = 20,
            EmailUsuário = 21,
            EmailSenha = 22,
            EMailImapServer = 23,
            EMailDeAberturaCorreios = 24
        }

        public enum EmpresaParametroTipo : byte
        {
            LogoEmpresa = 1,
            AuxiliarTrackingUrl = 2,
            OcorrenciaComplementarTracking = 3,
            FinalizaOcoAutomaticamente = 4,
            FusoHorario = 5,
            Mostra_Ds_CanalVenda_No_Tracking = 6,
            IgnoraLstTrackingQueContenhaNmSigla = 7,
            MensagemPadraoTracking = 8,
            CriarOcoImportacao = 9,
        }

        public enum TipoPI : byte
        {
            [Display(Name = "Entrega com ocorrência de roubo no tracking")]
            EntregaComOcorrenciaDeRouboNoTracking = 1,
            [Display(Name = "Entrega com ocorrência de extravio no tracking")]
            EntregComOcorrenciaDeExtravioNoTracking = 2,
            [Display(Name = "Entrega com ocorrência de avaria no tracking")]
            EntregaComOcorrenciaDeAvariaNoTracking = 3,
            [Display(Name = "Entrega em aberto e atrasada")]
            EntregaEmAbertoEAtrasada = 4,
            [Display(Name = "Entrega finalizada e atrasada")]
            EntregaFinalizadaEAtrasada = 5,
            [Display(Name = "Entrega finalizada com reclamação do cliente (Extravio)")]
            EntregaFinalizadaComReclamacaoDoCliente = 6,
            [Display(Name = "Entrega finalizada com reclamação do cliente (Avaria)")]
            EntregaFinalizadaComReclamacaoDoClienteAvaria = 7,
            [Display(Name = "Aguardando Retorno Correio")]
            AguardandoRetornoCorreio = 8
        }

        public enum SolicitacaoStatus : byte
        {
            [Display(Name = "Aguardando solicitação")]
            AguardandoSolicitacao = 1,

            [Display(Name = "Solicitada")]
            Solicitada = 2,

            [Display(Name = "Solicitação recusada")]
            SolicitacaoRecusada = 3,

            [Display(Name = "Solicitação cancelada")]
            SolicitacaoCancelada = 4,

            [Display(Name = "Solicitar novamente")]
            SolicitarNovamente = 5,

            [Display(Name = "Aguardando Finalização PI")]
            AguardandoFinalizacaoPI = 6,

            [Display(Name = "PI Aberta")]
            PIAberta = 7,

            [Display(Name = "PI Respondida")]
            PIRespondida = 8,

            [Display(Name = "PI Aceita")]
            PIAceita = 9,

            [Display(Name = "PI Recusada")]
            PIRecusada = 10,

            [Display(Name = "PI Paga Conciliada")]
            PIPagaConciliada = 11,

            [Display(Name = "PI Paga Não Conciliada")]
            PIPagaNaoConciliada = 12,

            [Display(Name = "PI Aberta Cliente")]
            PIAbertaCliente = 13,

            [Display(Name = "Solicitação Validada")]
            SolicitacaoValidada = 14

        }

        public enum StatusFatura : byte
        {
            EmProcessamento = 1,
            Faturada = 2,
            NFEmitida = 3,
            Paga = 4,
            Vencida = 5,
            Cancelada = 6
        }

        public enum EnvioConfigParametroTipo : byte
        {
            CopiaOculta = 1,
            EnabledSsl = 2,
            UrlTracking = 3,
            ListaCamposArquivo = 4,
            PortaSMTP = 5,
            Copia = 6,
            VozURA = 8,
            NomeEmpresa = 9,
            IdServicoPadrao = 10,
            CabecalhoCsv = 11,
            TituloEmail = 12,
            CorpoEmail = 13,
            HorarioExecucao = 14,
            DataPrevistaEmail = 15,
            EmailAtendimento = 16,
            EmailCabecalho = 17,
            EmailTextoInicial = 18,
            EmailTextoFinal = 19,
            EmailInfoTransportador = 20,
            EmailTracking = 21,
            EmailQtdTracking = 22,
            EmailTrackingRodape = 23,
            Tls12 = 24,
            EmailTelefoneContato = 25,
            TelSac = 26,
            EmailOcorrenciaComplementar = 27,
            EmailDescricaoAtendimento = 28,
            RegiaoTimezone = 29,
            IdFila = 30,
            DominiosIgnorados = 32,
            FusoHorario = 33,
            AgruparEmail = 34,
            UsarSendGrid = 35,
            EsquentarEndpoint = 36,
            EmailTrackingAgrupado = 37,
            StatusObjetoColetado = 38,
            StatusObjetoEmTransferencia = 39,
            StatusObjetoEmRotaEntrega = 40,
            StatusObjetoEntregue = 41,
            HtmlTrackingAgrupado = 42,
            Query = 43,
            NomeArquivo = 44
        }

        public enum EnvioConfigTemplateTipo : byte
        {
            Padrao = 1,
            Finalizado = 2
        }

        public enum TabelaStatus : byte
        {
            Nova = 1,
            Copia = 2,
            EmAprovacao = 3,
            Reprovada = 4,
            Ativa = 5,
            Desativada = 6,
            Excluida = 7
        }

        public enum RastreamentoConfigParametroTipo : byte
        {
            AccountNumber = 1,
            MeterNumber = 2,
            Senha = 3,
            Usuario = 4,
            Lingua = 5,
            SiglaPaizLingua = 6,
            AtualizaData = 7,
            FiltroExtensoesArquivos = 8,
            TipoPesquisaDirect = 9,
            FusoHorario = 10,
            EnderecoCopiaFTP = 11,
            UsuarioCopiaFTP = 12,
            SenhaCopiaFTP = 13,
            Porta = 14,
            EmailsAutorizados = 15,
            PrefixoPastas = 16,
            AtualizaTransportadorAlias = 17,
            MoveArquivos = 18
        }

        public enum EntregaAcaoTipo : byte
        {
            CalculaPrazoInicial = 1,
            AdicionaDias = 2,
            AtualizaPrazoCliente = 3,
            CalculaPrazoDevolucao = 4,
            AdicionaDiasDevolucao = 5,
            CalculaPrazoRetirada = 6,
            AtualizaTrackingElastic = 11
        }

        public enum TipoPrazoCliente : byte
        {
            Transportador = 1,
            Postagem = 2
        }

        public enum ServicoWindows : byte
        {
            Rastreio_Transportadores = 1,
            Rastreio_Correio = 2,
            Rastreio_DHL = 3,
            Rastreio_Fedex = 4,
            Rastreio_Jadlog = 5,
            Rastreio_Motoboy = 6,
            Importacao = 7,
            Integracao_Destinatario = 8,
            Integracao_Arquivo = 9,
            Integracao_Bling = 10,
            Integracao_Front = 11,
            PI = 12,
            PI_Leitura = 13,
            Billing = 14,
            Relatorio = 15,
            Ajustes = 16,
            Descoberta_Jadlog = 17,
            Descoberta_Motoboy = 18,
            Integracao_BSeller = 19,
            Rastreio_MelhorEnvio = 20,
            Integracao_Tiny = 21,
            Integracao_AmazonSms = 22,
            Rastreio_Transfolha = 23,
            Integracao_Carrefour = 24,
            Integracao_CarrefourStatus = 25,
            Rastreio_Direct = 26,
            Expedicao_Importacao = 27,
            Expedicao_Atualizacao = 28,
            Expedicao_Automatica = 29,
            Ajustes_Transportadores = 30,
            Ajustes_EntregasDetalhe = 31,
            Job_ExecucaoProcedures = 32,
            Rastreio_Linx = 33,
            Rastreio_TranspoFrete = 34,
            Descoberta_Linx = 35,
            Descoberta_TranspoFrete = 36,
            Rastreio_ToyMania = 37,
            Descoberta_ToyMania = 38,
            Descoberta_Mandae = 39,
            Rastreio_Mandae = 40,
            Rastreio_WebhookSync = 41,
            Ajustes_ArquivosElastic = 42,
            Rastreio_Jamef = 43,
            Ajustes_TrackingElastic = 44,
            PrazoDinamico_Atualiza = 45,
            PrazoDinamico_Implantacao = 46,
            PrazoDinamico_Entrega = 47,
            Integracao_AnyMarket = 48,
            Integracao_VNDA = 49,
            Ajustes_BaseCEPElastic = 50,
            Stage_Importacao = 51,
            Stage_Postagem = 52,
            Descoberta_ConfirmaFacil = 53,
            Rastreio_ConfirmaFacil = 54,
            Ajustes_EntregasElastic = 55,
            Rastreio_SSW = 56,
            Descoberta_SSW = 57,
            Rastreio_TNT = 58,
            Descoberta_TNT = 59,
            Rastreio_Patrus = 60,
            Descoberta_Patrus = 61,
            Integracao_ArquivoQuery = 62,
            Integracao_Unificada = 63,
            Integracao_Elastic = 64,
            Descoberta_Jamef = 65,
            Rastreio_PneuBest = 66,
            Descoberta_PneuBest = 67,
            Rastreio_Olist = 68,
            Descoberta_Olist = 69,
            Rastreio_Plimor = 70,
            Descoberta_Plimor = 71,
            Rastreio_MMA = 72,
            Descoberta_MMA = 73,
            Rastreio_Alfa = 74,
            Descoberta_Alfa = 75,
            Rastreio_Exata = 76,
            Descoberta_Exata = 77,
            StageEtiqueta_Geracao = 78,
            Integracao_Protheus = 79,
            Rastreio_Agrotama = 80,
            Descoberta_Agrotama = 81,
            Rastreio_ASAPLog = 82,
            PrazoDinamico_BaseInstalada = 83,
            Expedicao_Limpeza = 84
        }

        public enum RomaneioEnvioOrigem : byte
        {
            ExpedicaoManual = 1,
            ExpedicaoAuto = 2,
            TelaReenvio = 3
        }

        public enum TipoFeriado : int
        {
            Nacional = 1,
            Estadual = 2

        }

        public enum Mes : int
        {
            Janeiro = 1,
            Fevereiro = 2,
            Março = 3,
            Abril = 4,
            Maio = 5,
            Junho = 6,
            Julho = 7,
            Agosto = 8,
            Setembro = 9,
            Outubro = 10,
            Novembro = 11,
            Dezembro = 12
        }

        public enum EmpresaTokenTipo : byte
        {
            MenuFrete = 1
        }

        public enum EConfigEtiquetaTipo : byte
        {
            Correio = 1,
            VVLOG = 2,
            ASAPLOG = 3
        }

        public enum TamanhosCamposCorreios : int
        {
            Cep = 8,
            ComplementoBairroCidade = 30,
            NomeDaRuaOuPessoa = 50,
        }

        public enum TransportadoresOlist : long
        {
            Loggi = 488
        }

        public enum ExpedicaoEnvioCorreiosTipo : int
        {
            PAC = 1,
            Sedex = 2,
        }
        public enum OrigemLog : byte
        {
            Stage = 1,
            EtiquetaStage = 2,
            Cancelamento = 3
        }

        public enum TipoOcorrencia : int
        {
            [Display(Name = "Nenhum")]
            Nenhum = 0,
            [Display(Name = "Objeto Coletado")]
            Coletado,
            [Display(Name = "Em Transferência")]
            EmTransferencia,
            [Display(Name = "Em Rota")]
            EmRota,
            [Display(Name = "Entregue")]
            Entregue,
            [Display(Name = "Atenção")]
            Atencao
        }

    }
}
