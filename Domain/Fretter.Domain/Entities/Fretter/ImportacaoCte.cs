using Fretter.Domain.Dto.CTe;
using Fretter.Domain.Enum;
using Fretter.Domain.Helpers;
using Fretter.Domain.Helpers.Proceda.Entidades;
using Fretter.Domain.Helpers.Validations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Fretter.Domain.Entities
{
    public class ImportacaoCte : EntityBase
    {
        #region "Construtores"
        public ImportacaoCte(int Id, int? TipoAmbiente, int? ImportacaoArquivoId, int? TipoCte, int? TipoServico, string Chave, string Codigo, string Numero, int? DigitoVerificador, string Serie, DateTime? DataEmissao, decimal? ValorPrestacaoServico, string JsonComposicaoValores)
        {
            this.Ativar();
            this.Id = Id;
            this.TipoAmbiente = TipoAmbiente;
            this.ImportacaoArquivoId = ImportacaoArquivoId;
            this.TipoCte = TipoCte;
            this.TipoServico = TipoServico;
            this.Chave = Chave;
            this.Codigo = Codigo;
            this.Numero = Numero;
            this.DigitoVerificador = DigitoVerificador;
            this.Serie = Serie;
            this.DataEmissao = DataEmissao;
            this.ValorPrestacaoServico = ValorPrestacaoServico;
            this.JsonComposicaoValores = JsonComposicaoValores;
        }
        public ImportacaoCte(CTe cte, ProtCTe protCte, int importacaoArquivoId, List<ConfiguracaoCteTransportador> configCteTransportadores)
        {
                var configCteTransportador = configCteTransportadores;

                this.Ativar();
                AtualizarValorPrestacaoServico((decimal)cte.InfCte.ValorPrestacao.ValorTotal);
                AtualizarImportacaoArquivoId(importacaoArquivoId);
                AtualizarSerie(cte.InfCte.Identificacao.Serie.ToString());
                AtualizarDataEmissao(cte.InfCte.Identificacao.DataEmissao.ToDateTime());
                AtualizarDigitoVerificador((int)cte.InfCte.Identificacao.DigitoVerificador);
                AtualizarNumero(cte.InfCte.Identificacao.Numero);
                AtualizarCodigo(cte.InfCte.Identificacao.Codigo);
                AtualizarChave(cte.InfCte.Id.ToString());
                AtualizarTipoAmbiente(cte.InfCte.Identificacao.TipoAmbiente);
                AtualizarTipoCte(cte.InfCte.Identificacao.TipoCte);
                AtualizarTipoServico(cte.InfCte.Identificacao.TipoServico);
                AtualizarCNPJTransportador(cte.InfCte.Emitente.CNPJ);
                AtualizarCNPJEmissor(cte.InfCte.Remetente);
                AtualizarCNPJTomadorCTe(cte.InfCte);
                AtualizarModal(cte.InfCte.Identificacao?.Modal);
                AtualizarCodigoMunicipioEnvio(cte.InfCte.Identificacao?.CodigoMunicipio);
                AtualizarMunicipioEnvio(cte.InfCte.Identificacao?.NomeMunicipio);
                AtualizarUFEnvio(cte.InfCte.Identificacao?.UFEnvio);
                AtualizarCodigoMunicipioInicio(cte.InfCte.Identificacao?.CodigoMunicipioInicio);
                AtualizarMunicipioInicio(cte.InfCte.Identificacao?.NomeMunicipioInicio);
                AtualizarUFInicio(cte.InfCte.Identificacao?.UFInicio);
                AtualizarCodigoMunicipioFim(cte.InfCte.Identificacao?.CodigoMunicipioFim);
                AtualizarMunicipioFim(cte.InfCte.Identificacao?.NomeMunicipioFim);
                AtualizarUFFim(cte.InfCte.Identificacao?.UFFim);
                AtualizarValorTributo(cte.InfCte?.Impostos?.ValorTotalTributos);
                AtualizarCFOP(cte.InfCte.Identificacao?.CFOP.ToString());
                AtualizarVersaoProcesso(cte.InfCte.Identificacao?.VersaoProcesso);
                AtualizarVersaoAplicacao(protCte?.InfProt?.VersaoAplicacao);
                AtualizarChaveCte(protCte?.InfProt?.ChaveCTe);
                AtualizarDigestValue(protCte?.InfProt?.DigestValue);
                AtualizarDataAutorizacao(protCte?.InfProt?.DataAutorizacao);
                AtualizarStatusAutorizacao(protCte?.InfProt?.StatusAutorizacao);
                AtualizarProtocoloAutorizacao(protCte?.InfProt?.ProtocoloAutorizacao);
                AtualizarMotivoAutorizacao(protCte?.InfProt?.MotivoAutorizacao);

                if (this.TipoCte == 0)
                {
                    foreach (var informacaoQuantidadeCarga in cte.InfCte.InformacaoCte?.InformacaoCarga?.InformacaoQuantidadeCarga)
                    {
                        string tipoCarga = informacaoQuantidadeCarga.TipoMedia;
                        decimal quantidade = (decimal)informacaoQuantidadeCarga.Quantidade;
                        string codigo = informacaoQuantidadeCarga.CodigoUnidadeMedida;
                        int? configTipo = ObterConfigTipo(configCteTransportador, tipoCarga);

                        ImportacaoCteCarga importacaoCteCarga = new ImportacaoCteCarga(0, this.Id, tipoCarga, codigo, quantidade, configTipo);
                        AdicionarImportacaoCarga(importacaoCteCarga);
                    }

                    string chave = cte.InfCte.InformacaoCte.InformacaoDocumento.InformacaoNFe.ChaveNFe;
                    DateTime? dataPrevistaEntrega = cte.InfCte.InformacaoCte?.InformacaoDocumento?.InformacaoNFe?.DataPrevista;
                    ImportacaoCteNotaFiscal importacaoCteNotaFiscal = new ImportacaoCteNotaFiscal(0, this.Id, chave, dataPrevistaEntrega == DateTime.MinValue ? null : dataPrevistaEntrega);
                    AdicionarImportacaoNFe(importacaoCteNotaFiscal);

                    if (cte.InfCte.Impostos != null)
                        AdicionarImpostos(cte.InfCte.Impostos);
                }
                else
                {
                    AtualizarChaveComplementar(cte.InfCte.InformacaoCteComplementar.Chave);
                }

                foreach (var componente in cte.InfCte.ValorPrestacao.Componentes)
                {
                    string nomeComponente = componente.NomeComponente;
                    decimal? valorComponente = componente.ValorComponente;
                    int? configTipo = ObterConfigTipo(configCteTransportador, componente.NomeComponente);

                    ImportacaoCteComposicao importacaoCteComposicao = new ImportacaoCteComposicao(0, this.Id, nomeComponente, valorComponente, configTipo);
                    AdicionarImportacaoComponente(importacaoCteComposicao);
                }

                AtualizarJsonComposicaoValores(configCteTransportadores);
        }
        public ImportacaoCte(CONEMB_Conhecimento conhecimento, int importacaoArquivoId, string cnpjTransportador, List<ConfiguracaoCteTransportador> configCteTransportadores)
        {
            this.Ativar();
            AtualizarValorPrestacaoServico((decimal)conhecimento.ValorTotalFrete);
            AtualizarImportacaoArquivoId(importacaoArquivoId);
            AtualizarSerie(conhecimento.Serie);
            AtualizarDataEmissao(conhecimento.DataEmissao);
            AtualizarDigitoVerificador(0);
            AtualizarNumero(conhecimento.Numero);
            AtualizarCodigo(null);
            AtualizarChave(conhecimento?.DANFE);
            AtualizarTipoAmbiente(0);
            AtualizarTipoCte(0);
            AtualizarTipoServico(0);
            AtualizarCNPJTransportador(cnpjTransportador);
            AtualizarCNPJEmissor(conhecimento.CNPJEmissorNota);
            AtualizarCNPJTomadorCONEMB(conhecimento.CNPJFilialEmissora);
            AtualizarCFOP(conhecimento.CFOP);
            AtualizarProtocoloAutorizacao(conhecimento.ProtocoloAutorizacao);

            var TipoDocumento = conhecimento.TipoDocumento;

            if (TipoDocumento == null || TipoDocumento == "" || TipoDocumento == "N")
            {
                AtualizarTipoCte(0);
            }
            else if (TipoDocumento == "A")
            {
                AtualizarTipoCte(1);
            }
            else
            {
                AtualizarTipoCte(2);
            }

            AtualizarUFEnvio(conhecimento.UFEnvio);
            AtualizarUFInicio(conhecimento.UFInicio);
            AtualizarUFFim(conhecimento.UFFim);

            ImportacaoCteCarga importacaoCteCarga = new ImportacaoCteCarga(0, this.Id, "PESO", null, conhecimento.PesoTransportado, 1);
            AdicionarImportacaoCarga(importacaoCteCarga);

            if(conhecimento.PesoTransportado <= 0)
                AddException(nameof(ImportacaoCte), nameof(conhecimento.PesoTransportado), "Valor menor ou igual a zero", nameof(ImportacaoCte));

            ImportacaoCteComposicao importacaoCteComposicaoICMS = new ImportacaoCteComposicao(0, this.Id, "ICMS", conhecimento.ValorICMS, null);
            AdicionarImportacaoComponente(importacaoCteComposicaoICMS);

            if (conhecimento.PesoTransportado <= 0)
                AddException(nameof(ImportacaoCte), nameof(conhecimento.ValorICMS), "Valor menor ou igual a zero", nameof(ImportacaoCte));

            ImportacaoCteComposicao importacaoCteComposicaoFreteValor = new ImportacaoCteComposicao(0, this.Id, "FreteValor", conhecimento.FreteValor, 2);
            AdicionarImportacaoComponente(importacaoCteComposicaoFreteValor);

            if (conhecimento.PesoTransportado <= 0)
                AddException(nameof(ImportacaoCte), nameof(conhecimento.FreteValor), "Valor menor ou igual a zero", nameof(ImportacaoCte));

            AdicionarImportacaoNFe(new ImportacaoCteNotaFiscal(0, this.Id, null, null, conhecimento.CNPJEmissorNF, conhecimento.NumeroNF, conhecimento.SerieNF, conhecimento.DataEmissaoNF));

            this.ImportacaoCteImpostos = new List<ImportacaoCteImposto>();
            var cteImposto = new ImportacaoCteImposto(0, this.Id, "ICMS00");
            cteImposto.AtualizarAliquota(Convert.ToDecimal(conhecimento.TaxaICMS));
            cteImposto.AtualizarValorBaseCalculo(Convert.ToDecimal(conhecimento.BaseCalculoICMS));
            cteImposto.AtualizarValor(Convert.ToDecimal(conhecimento.ValorICMS));
            this.ImportacaoCteImpostos.Add(cteImposto);

            AtualizarJsonComposicaoValores(configCteTransportadores);
        }

        #endregion

        #region "Propriedades"
        public int? TipoAmbiente { get; protected set; }
        public int? ImportacaoArquivoId { get; protected set; }
        public int? TipoCte { get; protected set; } //0 - CT-e Normal; 1 - CT-e de Complemento de Valores; 2 - CT-e de Anulação; 3 - CT-e de Substituição
        public int? TipoServico { get; protected set; }
        public string Chave { get; protected set; }
        public string Codigo { get; protected set; }
        public string Numero { get; protected set; }
        public int? DigitoVerificador { get; protected set; }
        public string Serie { get; protected set; }
        public DateTime? DataEmissao { get; protected set; }
        public decimal? ValorPrestacaoServico { get; protected set; }
        public string CNPJTransportador { get; protected set; }
        public string CNPJTomador { get; protected set; }
        public string CNPJEmissor { get; protected set; }
        public string ChaveComplementar { get; protected set; }
        public string JsonComposicaoValores { get; protected set; }
        public string Modal { get; protected set; }
        public int? CodigoMunicipioEnvio { get; protected set; }
        public string MunicipioEnvio { get; protected set; }
        public string UFEnvio { get; protected set; }
        public int? CodigoMunicipioInicio { get; protected set; }
        public string MunicipioInicio { get; protected set; }
        public string UFInicio { get; protected set; }
        public int? CodigoMunicipioFim { get; protected set; }
        public string MunicipioFim { get; protected set; }
        public string UFFim { get; protected set; }
        public byte? IETomadorIndicador { get; protected set; }
        public decimal? ValorTributo { get; protected set; }
        public string CFOP { get; protected set; }
        public string VersaoProcesso { get; protected set; }
        public string VersaoAplicacao { get; protected set; }
        public string ChaveCte { get; protected set; }
        public string DigestValue { get; protected set; }
        public DateTime? DataAutorizacao { get; protected set; }
        public string StatusAutorizacao { get; protected set; }
        public string ProtocoloAutorizacao { get; protected set; }
        public string MotivoAutorizacao { get; protected set; }

        #endregion

        #region "Referencias"
        public virtual ImportacaoArquivo ImportacaoArquivo { get; set; }
        public virtual Conciliacao Conciliacao { get; set; }
        public virtual List<ImportacaoCteCarga> ImportacaoCteCargas { get; set; }
        public virtual List<ImportacaoCteNotaFiscal> ImportacaoCteNotaFiscais { get; set; }
        public virtual List<ImportacaoCteComposicao> ImportacaoCteComposicoes { get; set; }
        public virtual List<ImportacaoCteImposto> ImportacaoCteImpostos { get; set; }
        #endregion

        #region "Métodos"
        public void AtualizarTipoAmbiente(int? TipoAmbiente) => this.TipoAmbiente = TipoAmbiente;
        public void AtualizarImportacaoArquivoId(int? ImportacaoArquivoId) => this.ImportacaoArquivoId = ImportacaoArquivoId;
        public void AtualizarTipoCte(int? TipoCte) => this.TipoCte = TipoCte;
        public void AtualizarTipoServico(int? TipoServico) => this.TipoServico = TipoServico;
        public void AtualizarChave(string chave)
        {
            string mensagem = string.Empty;
            bool campoInvalido = true;

            if (string.IsNullOrEmpty(chave))
                mensagem = "Campo Chave não preenchido";
            else if (chave.Length > 47)
                mensagem = "O campo Chave só poder ter até 47 caracteres";
            else
                campoInvalido = false;

            if (campoInvalido)
                AddException(nameof(ImportacaoCte), nameof(this.Chave), mensagem, XmlHelper.ObterXmlElement("Fretter.Domain.Dto.CTe.InfCte", "Id"));

            this.Chave = chave;
        }
        public void AtualizarCodigo(string Codigo) => this.Codigo = Codigo;
        public void AtualizarNumero(string numero)
        {
            string mensagem = string.Empty;
            bool campoInvalido = true;

            if (string.IsNullOrEmpty(numero))
            {
                mensagem = "Campo Número Indentificação não preenchido";
            }
            else if (!int.TryParse(numero, out int result))
            {
                mensagem = "Número Indentificação com Formato Inválido";
            } 
            else if (result <= 0)
            {
                mensagem = "Valor Número Indentificação não menor ou igual a zero";
            }
            else
            {
                campoInvalido = false;
            }


            if (campoInvalido)
                AddException(nameof(ImportacaoCte), nameof(this.Serie), mensagem, XmlHelper.ObterXmlElement("Fretter.Domain.Dto.CTe.Identificacao", "Numero"));

             this.Numero = numero;
        }
        public void AtualizarDigitoVerificador(int? DigitoVerificador) => this.DigitoVerificador = DigitoVerificador;
        public void AtualizarSerie(string serie)
        {
            
            if (!serie.All(char.IsDigit))
                AddException(nameof(ImportacaoCte), nameof(this.Serie), "Serie NF com Formato Inválido", XmlHelper.ObterXmlElement("Fretter.Domain.Dto.CTe.Identificacao", "Serie"));
            
            this.Serie = serie;
        }
        public void AtualizarDataEmissao(DateTime? DataEmissao) => this.DataEmissao = DataEmissao;
        public void AtualizarValorPrestacaoServico(decimal? valorPrestacaoServico)
        {
            if (valorPrestacaoServico <= 0)
                AddException(nameof(ImportacaoCte), nameof(this.ValorPrestacaoServico), "Valor da Prestação Serviço menor ou igual a zero", XmlHelper.ObterXmlElement("Fretter.Domain.Dto.CTe.ValorPrestacao", "ValorTotal"));
            
            this.ValorPrestacaoServico = valorPrestacaoServico;
        } 
        public void AtualizarCNPJTransportador(string cnpjTransportador) => this.CNPJTransportador = cnpjTransportador;
        public void AtualizarCNPJTomadorCONEMB(string cnpjTomador) 
        {
            if (string.IsNullOrEmpty(cnpjTomador))
                AddException(nameof(ImportacaoCte), nameof(this.CNPJTomador), "Campo não preenchido", nameof(ImportacaoCte));
            else if (!ValidateData.DocumentoValido(cnpjTomador))
                AddException(nameof(ImportacaoCte), nameof(this.CNPJTomador), "Formato Inválido", nameof(ImportacaoCte));

            this.CNPJTomador = cnpjTomador;
        }
        public void AtualizarCNPJTomadorCTe(InfCte inf)
        {
            string classe = "Fretter.Domain.Dto.CTe.";

            if (inf.Identificacao?.Tomador03 != null)
            {
                switch (inf.Identificacao?.Tomador03.Tomador)
                {
                    case 0:
                        this.CNPJTomador = (!string.IsNullOrEmpty(inf.Remetente.CNPJ.ToString())) ? String.Format("{0:00000000000000}", Convert.ToDecimal(inf.Remetente.CNPJ)) : String.Format("{0:00000000000}", Convert.ToDecimal(inf.Remetente.CPF));
                        classe += "Remetente";
                        break;
                    case 1:
                        this.CNPJTomador = (!string.IsNullOrEmpty(inf.Expedidor.CNPJ.ToString())) ? String.Format("{0:00000000000000}", Convert.ToDecimal(inf.Expedidor.CNPJ)) : String.Format("{0:00000000000}", Convert.ToDecimal(inf.Expedidor.CPF));
                        classe += "Expedidor";
                        break;
                    case 2:
                        this.CNPJTomador = (!string.IsNullOrEmpty(inf.Recebedor.CNPJ.ToString())) ? String.Format("{0:00000000000000}", Convert.ToDecimal(inf.Recebedor.CNPJ)) : String.Format("{0:00000000000}", Convert.ToDecimal(inf.Remetente.CPF));
                        classe += "Recebedor";
                        break;
                    case 3:
                        this.CNPJTomador = (!string.IsNullOrEmpty(inf.Destinatario.CNPJ.ToString())) ? String.Format("{0:00000000000000}", Convert.ToDecimal(inf.Destinatario.CNPJ)) : String.Format("{0:00000000000}", Convert.ToDecimal(inf.Destinatario.CPF));
                        classe += "Destinatario";
                        break;
                    default:
                        break;
                }
            }
            else 
            {
                this.CNPJTomador = (!string.IsNullOrEmpty(inf.Identificacao?.Tomador04.CNPJ.ToString())) ? String.Format("{0:00000000000000}", Convert.ToDecimal(inf.Identificacao?.Tomador04.CNPJ)) : String.Format("{0:00000000000}", Convert.ToDecimal(inf.Identificacao?.Tomador04.CPF));
                classe += "Tomador04";
            }

            if (string.IsNullOrEmpty(this.CNPJTomador))
                AddException(nameof(ImportacaoCte), nameof(this.CNPJTomador), "CNPJ do Tomador não preenchido", XmlHelper.ObterXmlElement(classe, "CNPJ"));
            else if (!ValidateData.DocumentoValido(this.CNPJTomador))
                AddException(nameof(ImportacaoCte), nameof(this.CNPJTomador), "CNPJ do Tomador Inválido", XmlHelper.ObterXmlElement(classe, "CNPJ"));

        }
        public void AtualizarCNPJEmissor(string cnpjEmissor)
        {
            if (string.IsNullOrEmpty(cnpjEmissor))
                AddException(nameof(ImportacaoCte), nameof(this.CNPJEmissor), "Campo CNPJ Emissor não preenchido", XmlHelper.ObterXmlElement("Fretter.Domain.Dto.CTe.Remetente", "CNPJ"));
            else if (!ValidateData.DocumentoValido(cnpjEmissor))
                AddException(nameof(ImportacaoCte), nameof(this.CNPJEmissor), "CNPJ Emissor com Formato Inválido", XmlHelper.ObterXmlElement("Fretter.Domain.Dto.CTe.Remetente", "CNPJ"));

            this.CNPJEmissor = cnpjEmissor;
        }
        public void AtualizarCNPJEmissor(Remetente remetente)
        {
            string campo = string.Empty;

            if (string.IsNullOrEmpty(remetente.CNPJ))
            {
                if (remetente.CPF != 0)
                {
                    this.CNPJEmissor = remetente.CPF.ToString();
                    campo = "CPF";
                }
            }
            else 
            {
                this.CNPJEmissor = remetente.CNPJ;
                campo = "CNPJ";
            }

            if (string.IsNullOrEmpty(this.CNPJEmissor))
            {
                AddException(nameof(ImportacaoCte), nameof(this.CNPJEmissor), "Campo CNPJ Emissor não preenchido", XmlHelper.ObterXmlElement("Fretter.Domain.Dto.CTe.Remetente", campo));
            }
            else
            {
                if (campo.Equals("CPF"))
                {
                    if (!ValidateData.CPFValido(this.CNPJEmissor))
                    {
                        AddException(nameof(ImportacaoCte), nameof(this.CNPJEmissor), "CNPJ Emissor com Formato Inválido", XmlHelper.ObterXmlElement("Fretter.Domain.Dto.CTe.Remetente", campo));
                    }
                }
                else
                {
                    if (!ValidateData.DocumentoValido(this.CNPJEmissor))
                    {
                        AddException(nameof(ImportacaoCte), nameof(this.CNPJEmissor), "CNPJ Emissor com Formato Inválido", XmlHelper.ObterXmlElement("Fretter.Domain.Dto.CTe.Remetente", campo));
                    }
                }
            }
        }
        public void AtualizarModal(string Modal) => this.Modal = Modal;
        public void AtualizarCodigoMunicipioEnvio(int? CodigoMunicipioEnvio) => this.CodigoMunicipioEnvio = CodigoMunicipioEnvio;
        public void AtualizarMunicipioEnvio(string MunicipioEnvio) => this.MunicipioEnvio = MunicipioEnvio;
        public void AtualizarUFEnvio(string UFEnvio) => this.UFEnvio = UFEnvio;
        public void AtualizarCodigoMunicipioInicio(int? CodigoMunicipioInicio) => this.CodigoMunicipioInicio = CodigoMunicipioInicio;
        public void AtualizarMunicipioInicio(string MunicipioInicio) => this.MunicipioInicio = MunicipioInicio;
        public void AtualizarUFInicio(string UFInicio) => this.UFInicio = UFInicio;
        public void AtualizarCodigoMunicipioFim(int? CodigoMunicipioFim) => this.CodigoMunicipioFim = CodigoMunicipioFim;
        public void AtualizarMunicipioFim(string MunicipioFim) => this.MunicipioFim = MunicipioFim;
        public void AtualizarUFFim(string UFFim) => this.UFFim = UFFim;
        public void AtualizarIETomadorIndicador(byte? IETomadorIndicador) => this.IETomadorIndicador = IETomadorIndicador;
        public void AtualizarValorTributo(decimal? ValorTributo) => this.ValorTributo = ValorTributo;
        public void AtualizarCFOP(string CFOP) => this.CFOP = CFOP;
        public void AtualizarVersaoProcesso(string VersaoProcesso) => this.VersaoProcesso = VersaoProcesso;
        public void AtualizarVersaoAplicacao(string VersaoAplicacao) => this.VersaoAplicacao = VersaoAplicacao;
        public void AtualizarChaveCte(string chaveCte)
        {
            string mensagem = string.Empty;
            bool campoInvalido = true;

            if (string.IsNullOrEmpty(chaveCte))
                mensagem = "Campo Chave CTe não preenchido";
            else if (chaveCte.Length != 44)
                mensagem = "O campo Chave CTE precisa ter 44 caracteres";
            else
                campoInvalido = false;

            if (campoInvalido)
                AddException(nameof(ImportacaoCte), nameof(this.ChaveCte), mensagem, XmlHelper.ObterXmlElement("Fretter.Domain.Dto.CTe.InfProt", "ChaveCTe"));

            this.ChaveCte = chaveCte;
        }

        public void AtualizarDigestValue(string DigestValue) => this.DigestValue = DigestValue;
        public void AtualizarDataAutorizacao(DateTime? DataAutorizacao) => this.DataAutorizacao = DataAutorizacao;
        public void AtualizarStatusAutorizacao(string StatusAutorizacao) => this.StatusAutorizacao = StatusAutorizacao;
        public void AtualizarProtocoloAutorizacao(string ProtocoloAutorizacao) => this.ProtocoloAutorizacao = ProtocoloAutorizacao;
        public void AtualizarMotivoAutorizacao(string MotivoAutorizacao) => this.MotivoAutorizacao = MotivoAutorizacao;
        private void AdicionarImportacaoCarga(ImportacaoCteCarga importacaoCteCarga)
        {
            if (this.ImportacaoCteCargas == null)
                this.ImportacaoCteCargas = new List<ImportacaoCteCarga>();
            this.ImportacaoCteCargas.Add(importacaoCteCarga);
        }
        private void AdicionarImportacaoNFe(ImportacaoCteNotaFiscal importacaoCteNotaFiscal)
        {
            if (this.ImportacaoCteNotaFiscais == null)
                this.ImportacaoCteNotaFiscais = new List<ImportacaoCteNotaFiscal>();
            this.ImportacaoCteNotaFiscais.Add(importacaoCteNotaFiscal);
        }
        private void AdicionarImportacaoComponente(ImportacaoCteComposicao importacaoCteComposicao)
        {
            if (this.ImportacaoCteComposicoes == null)
                this.ImportacaoCteComposicoes = new List<ImportacaoCteComposicao>();
            this.ImportacaoCteComposicoes.Add(importacaoCteComposicao);
        }
        private void AtualizarChaveComplementar(string chave)
        {
            if (!chave.Contains("CTe"))
                this.ChaveComplementar = $"CTe{chave}";
            else
                this.ChaveComplementar = chave;
        }
        private int? ObterConfigTipo(IEnumerable<ConfiguracaoCteTransportador> configCteTransportador, string alias)
        {
            int? configTipo = null;
            var parametroEncontrado = configCteTransportador.Where(x => x.Alias.ToUpper() == alias.ToUpper()).FirstOrDefault();
            if (parametroEncontrado != null)
                configTipo = parametroEncontrado.ConfiguracaoCteTipoId;
            return configTipo;
        }
        private void AtualizarJsonComposicaoValores(List<ConfiguracaoCteTransportador> configCteTransportadores)
        {
            List<ItemComposicaoDto> listComposicao = new List<ItemComposicaoDto>();

            foreach (var composicao in this.ImportacaoCteComposicoes)
            {
                string chave = null;
                var composicaoParametro = configCteTransportadores.FirstOrDefault(f => f.ConfiguracaoCteTipoId == composicao.ConfiguracaoCteTipoId);
                if (composicaoParametro != null && composicaoParametro.ConfiguracaoCteTipo.Chave != null)
                    chave = composicaoParametro.ConfiguracaoCteTipo.Chave;
                else
                    chave = composicao.Nome;

                listComposicao.Add(new ItemComposicaoDto(chave, composicao.Valor, Enum.EnumCteComposicaoTipo.Valor));
            }

            foreach (var carga in this.ImportacaoCteCargas)
            {
                string chave = null;
                var composicaoParametro = configCteTransportadores.FirstOrDefault(f => f.ConfiguracaoCteTipoId == carga.ConfiguracaoCteTipoId);
                if (composicaoParametro != null && composicaoParametro.ConfiguracaoCteTipo.Chave != null)
                    chave = composicaoParametro.ConfiguracaoCteTipo.Chave;
                else
                    chave = carga.Tipo;

                var cargaIgual = listComposicao.FirstOrDefault(x => x.chave == chave);
                if (cargaIgual != null && cargaIgual.valor <= carga.Quantidade)
                {
                    listComposicao.Remove(cargaIgual);
                }

                listComposicao.Add(new ItemComposicaoDto(chave, (decimal)carga.Quantidade, Enum.EnumCteComposicaoTipo.Peso));
            }

            this.JsonComposicaoValores = JsonConvert.SerializeObject(listComposicao);
        }
        private void AdicionarImpostos(Impostos impostos)
        {
            if (this.ImportacaoCteImpostos == null)
                this.ImportacaoCteImpostos = new List<ImportacaoCteImposto>();

            if (!string.IsNullOrEmpty(impostos.ICMS.ICMS00?.CST))
            {
                var cteImposto = new ImportacaoCteImposto(0, this.Id, "ICMS00");
                cteImposto.AtualizarAliquota(Convert.ToDecimal(impostos.ICMS.ICMS00?.AliquotaICMS));
                cteImposto.AtualizarValorBaseCalculo(Convert.ToDecimal(impostos.ICMS.ICMS00?.ValorBC));
                cteImposto.AtualizarValor(Convert.ToDecimal(impostos.ICMS.ICMS00?.ValorICMS));
                this.ImportacaoCteImpostos.Add(cteImposto);
            }
            else if (!string.IsNullOrEmpty(impostos.ICMS.ICMS20?.CST))
            {
                var cteImposto = new ImportacaoCteImposto(0, this.Id, "ICMS20");
                cteImposto.AtualizarAliquota(Convert.ToDecimal(impostos.ICMS.ICMS20?.AliquotaICMS));
                cteImposto.AtualizarValorBaseCalculo(Convert.ToDecimal(impostos.ICMS.ICMS20?.ValorBC));
                cteImposto.AtualizarValor(Convert.ToDecimal(impostos.ICMS.ICMS20?.ValorICMS));
                this.ImportacaoCteImpostos.Add(cteImposto);
            }
            else if (!string.IsNullOrEmpty(impostos.ICMS.ICMS60?.CST))
            {
                var cteImposto = new ImportacaoCteImposto(0, this.Id, "ICMS60");
                cteImposto.AtualizarAliquota(Convert.ToDecimal(impostos.ICMS.ICMS60?.AliquotaICMS));
                cteImposto.AtualizarValorBaseCalculo(Convert.ToDecimal(impostos.ICMS.ICMS60?.ValorBCST));
                cteImposto.AtualizarValor(Convert.ToDecimal(impostos.ICMS.ICMS60?.ValorICMSST));
                this.ImportacaoCteImpostos.Add(cteImposto);
            }
            else if (!string.IsNullOrEmpty(impostos.ICMS.ICMSOutraUF?.CST))
            {
                var cteImposto = new ImportacaoCteImposto(0, this.Id, "ICMSOutraUF");
                cteImposto.AtualizarAliquota(Convert.ToDecimal(impostos.ICMS.ICMSOutraUF?.AliquotaICMS));
                cteImposto.AtualizarValorBaseCalculo(Convert.ToDecimal(impostos.ICMS.ICMSOutraUF?.ValorBC));
                cteImposto.AtualizarValor(Convert.ToDecimal(impostos.ICMS.ICMSOutraUF?.ValorICMS));
                this.ImportacaoCteImpostos.Add(cteImposto);
            }
        }
        #endregion
    }
}
