using Fretter.Domain.Dto.Fatura;
using Fretter.Domain.Entities.Fretter;
using Fretter.Domain.Helpers.Proceda.Entidades;
using Fretter.Domain.Helpers.Proceda.Enums;
using Fretter.Domain.Helpers.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fretter.Domain.Helpers.Proceda
{
    public static class PROCEDAHelper
    {
        public static EnumDOCCOBTipo IdentificaDOCCOB(IFormFile file)
        {
            var doccobTipo = EnumDOCCOBTipo.DOCCOB50;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string linha;
                int idLinha = 1;
                while ((linha = reader.ReadLine()) != null)
                {
                    string codigo = linha.Substring(0, 3);
                    if (idLinha == 2 && codigo == "350")
                    {
                        doccobTipo = EnumDOCCOBTipo.DOCCOB30;
                        break;
                    }

                    idLinha += 1;
                }
            }

            return doccobTipo;
        }
        public static RetornoLeituraDoccob ObterDOCCOB30(IFormFile file)
        {
            DOCCOB docCob = null;
            DOCCOB_Cabecalho cabecalho = null;
            DOCCOB_Cobranca cobranca = null;
            DOCCOB_Conhecimento conhecimento = null;
            DOCCOB_NotaFiscal notaFiscal = null;
            RetornoLeituraDoccob retornoLeituraDoccob = new RetornoLeituraDoccob();

            retornoLeituraDoccob.EnumDOCCOBTipo = EnumDOCCOBTipo.DOCCOB30;

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string linha;
                int idLinha = 1;
                while ((linha = reader.ReadLine()) != null)
                {
                    string codigo = linha.Substring(0, 3);
                    if (codigo == "000")
                    {
                        docCob = new DOCCOB();
                        docCob.Id_Remetente = ObterValorPosicional<string>(linha, 4, 35);
                        docCob.Id_Destinatario = ObterValorPosicional<string>(linha, 39, 35);
                        docCob.Data = ObterValorPosicional<DateTime>(linha, 74, 6);
                    }
                    if (codigo == "350")
                    {
                        if (cabecalho != null)
                            docCob.Cabecalhos.Add(cabecalho);

                        cabecalho = new DOCCOB_Cabecalho();
                        cabecalho.Id_Documento = ObterValorPosicional<string>(linha, 4, 14);
                    }
                    if (codigo == "351")
                    {
                        cabecalho.Transportadora.CNPJ = ObterValorPosicional<string>(linha, 4, 14);
                        cabecalho.Transportadora.RazaoSocial = ObterValorPosicional<string>(linha, 18, 40);

                        if (!ValidateData.DocumentoValido(cabecalho.Transportadora.CNPJ))
                            retornoLeituraDoccob.Criticas.Add(new FaturaArquivoCriticaDTO { Descricao = "CNPJ inválido", Linha = idLinha, Posicao = 4 });

                    }
                    if (codigo == "352")
                    {
                        if (cobranca != null)
                            cabecalho.Transportadora.Cobrancas.Add(cobranca);

                        cobranca = new DOCCOB_Cobranca();
                        cobranca.FilialEmissora = ObterValorPosicional<string>(linha, 4, 10);

                        string tipo = ObterValorPosicional<string>(linha, 14, 1);

                        if (!string.IsNullOrEmpty(tipo) && tipo.All(char.IsDigit))
                        {
                            cobranca.Tipo = Convert.ToInt32(tipo);
                        }
                        else
                        {
                            retornoLeituraDoccob.Criticas.Add(new FaturaArquivoCriticaDTO { Descricao = "Tipo Cobrança inválido", Linha = idLinha, Posicao = 14 });
                        }

                        cobranca.Serie = ObterValorPosicional<string>(linha, 15, 3);
                        cobranca.Numero = ObterValorPosicional<string>(linha, 18, 10);
                        cobranca.DataEmissao = ObterValorPosicional<DateTime>(linha, 28, 8);
                        cobranca.DataVencimento = ObterValorPosicional<DateTime>(linha, 36, 8);
                        cobranca.ValorTotal = ObterValorPosicional<decimal>(linha, 44, 15);
                        cobranca.TipoCobranca = ObterValorPosicional<string>(linha, 59, 3);
                    }

                    if (codigo == "353")
                    {
                        if (conhecimento != null)
                            cobranca.Conhecimentos.Add(conhecimento);

                        conhecimento = new DOCCOB_Conhecimento();
                        conhecimento.Id = idLinha;
                        conhecimento.Serie = ObterValorPosicional<string>(linha, 14, 5);
                        conhecimento.Numero = ObterValorPosicional<string>(linha, 19, 12);

                        var validaLayoutRestoque = ObterValorPosicional<string>(linha, 31, 3);

                        if (string.IsNullOrEmpty(validaLayoutRestoque)) //Restoque possui um que pula 3 casas (em branco) nessa posição 
                        {
                            conhecimento.ValorFrete = ObterValorPosicional<decimal>(linha, 34, 15);
                            conhecimento.DataEmissao = ObterValorPosicional<DateTime>(linha, 49, 8);
                            conhecimento.DocumentoRemetente = ObterValorPosicional<string>(linha, 57, 14);
                            conhecimento.DocumentoDestinatario = ObterValorPosicional<string>(linha, 71, 14);
                            conhecimento.DocumentoEmissor = ObterValorPosicional<string>(linha, 85, 14);

                            if (ValidateData.DataMinima(conhecimento.DataEmissao))
                                retornoLeituraDoccob.Criticas.Add(new FaturaArquivoCriticaDTO { Descricao = "Data Emissão inválida", Linha = idLinha, Posicao = 49 });
                        }
                        else //layout correto do manual é o abaixo
                        {
                            conhecimento.ValorFrete = ObterValorPosicional<decimal>(linha, 31, 15);
                            conhecimento.DataEmissao = ObterValorPosicional<DateTime>(linha, 46, 8);
                            conhecimento.DocumentoRemetente = ObterValorPosicional<string>(linha, 54, 14);
                            conhecimento.DocumentoDestinatario = ObterValorPosicional<string>(linha, 68, 14);
                            conhecimento.DocumentoEmissor = ObterValorPosicional<string>(linha, 82, 14);

                            if (ValidateData.DataMinima(conhecimento.DataEmissao))
                                retornoLeituraDoccob.Criticas.Add(new FaturaArquivoCriticaDTO { Descricao = "Data Emissão inválida", Linha = idLinha, Posicao = 46 });
                        }
                    }

                    if (codigo == "354")
                    {
                        if (notaFiscal != null && cobranca != null)
                            cobranca.NotasFiscais.Add(notaFiscal);

                        notaFiscal = new DOCCOB_NotaFiscal();
                        notaFiscal.Id = idLinha;
                        notaFiscal.NotaSerie = ObterValorPosicional<string>(linha, 4, 3).TrimStart('0');
                        notaFiscal.NotaNumero = ObterValorPosicional<string>(linha, 7, 8).TrimStart('0');

                        if (string.IsNullOrEmpty(notaFiscal.NotaNumero) || !notaFiscal.NotaNumero.All(char.IsDigit))
                            retornoLeituraDoccob.Criticas.Add(new FaturaArquivoCriticaDTO { Descricao = "Número da Nota inválido", Linha = idLinha, Posicao = 7 });

                        notaFiscal.DataEmissao = ObterValorPosicional<DateTime>(linha, 15, 8);

                        if (ValidateData.DataMinima(notaFiscal.DataEmissao))
                            notaFiscal.DataEmissao = conhecimento.DataEmissao;

                        if ((!ValidateData.DataMinima(notaFiscal.DataEmissao)) && notaFiscal.DataEmissao <= DateTime.Now.AddYears(-2))
                            retornoLeituraDoccob.Criticas.Add(new FaturaArquivoCriticaDTO { Descricao = $"Data Emissão Periodo inválido {notaFiscal.DataEmissao.Date.ToShortDateString()}", Linha = idLinha, Posicao = 15 });

                        notaFiscal.Peso = ObterValorPosicional<decimal>(linha, 23, 7);
                        notaFiscal.Valor = ObterValorPosicional<decimal>(linha, 30, 15);
                        notaFiscal.DocumentoEmissor = ObterValorPosicional<string>(linha, 45, 14);
                    }
                    idLinha += 1;
                }
            }
            if (docCob == null)
                throw new ApplicationException("Erro ao ler arquivo DOCCOB, código 000 não encontrado");

            if (cobranca != null)
                cabecalho.Transportadora.Cobrancas.Add(cobranca);
            if (cabecalho != null)
                docCob.Cabecalhos.Add(cabecalho);
            if (conhecimento != null)
                cobranca.Conhecimentos.Add(conhecimento);
            if (notaFiscal != null)
                cobranca.NotasFiscais.Add(notaFiscal);

            retornoLeituraDoccob.Doccob = docCob;

            return retornoLeituraDoccob;
        }
        public static RetornoLeituraDoccob ObterDOCCOB50(IFormFile file)
        {
            DOCCOB docCob = null;
            DOCCOB_Cabecalho cabecalho = null;
            DOCCOB_Cobranca cobranca = null;
            DOCCOB_Conhecimento conhecimento = null;
            DOCCOB_NotaFiscal notaFiscal = null;
            RetornoLeituraDoccob retornoLeituraDoccob = new RetornoLeituraDoccob();

            retornoLeituraDoccob.EnumDOCCOBTipo = EnumDOCCOBTipo.DOCCOB50;

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                string linha;
                int idLinha = 1;
                while ((linha = reader.ReadLine()) != null)
                {
                    string codigo = linha.Substring(0, 3);
                    if (codigo == "000")
                    {
                        docCob = new DOCCOB();
                        docCob.Id_Remetente = ObterValorPosicional<string>(linha, 4, 35);
                        docCob.Id_Destinatario = ObterValorPosicional<string>(linha, 39, 35);
                        docCob.Data = ObterValorPosicional<DateTime>(linha, 74, 6);
                    }
                    if (codigo == "550")
                    {
                        if (cabecalho != null)
                            docCob.Cabecalhos.Add(cabecalho);

                        cabecalho = new DOCCOB_Cabecalho();
                        cabecalho.Id_Documento = ObterValorPosicional<string>(linha, 4, 14);
                    }
                    if (codigo == "551")
                    {
                        cabecalho.Transportadora.CNPJ = ObterValorPosicional<string>(linha, 4, 14);
                        cabecalho.Transportadora.RazaoSocial = ObterValorPosicional<string>(linha, 18, 50);

                        if (!ValidateData.DocumentoValido(cabecalho.Transportadora.CNPJ))
                            retornoLeituraDoccob.Criticas.Add(new FaturaArquivoCriticaDTO { Descricao = "CNPJ inválido", Linha = idLinha, Posicao = 4 });
                    }
                    if (codigo == "552")
                    {
                        if (cobranca != null)
                            cabecalho.Transportadora.Cobrancas.Add(cobranca);

                        cobranca = new DOCCOB_Cobranca();
                        cobranca.FilialEmissora = ObterValorPosicional<string>(linha, 4, 10);

                        string tipo = ObterValorPosicional<string>(linha, 14, 1);

                        if (!string.IsNullOrEmpty(tipo) && tipo.All(char.IsDigit))
                        {
                            cobranca.Tipo = Convert.ToInt32(tipo);
                        }
                        else
                        {
                            retornoLeituraDoccob.Criticas.Add(new FaturaArquivoCriticaDTO { Descricao = "Tipo Cobrança inválido", Linha = idLinha, Posicao = 14 });
                        }

                        cobranca.Serie = ObterValorPosicional<string>(linha, 15, 3);
                        cobranca.Numero = ObterValorPosicional<string>(linha, 18, 10);
                        cobranca.DataEmissao = ObterValorPosicional<DateTime>(linha, 28, 8);
                        cobranca.DataVencimento = ObterValorPosicional<DateTime>(linha, 36, 8);
                        cobranca.ValorTotal = ObterValorPosicional<decimal>(linha, 44, 15);
                        cobranca.TipoCobranca = ObterValorPosicional<string>(linha, 59, 3);
                        cobranca.CFOP = ObterValorPosicional<string>(linha, 187, 5);
                        cobranca.CodigoAcessoNFe = ObterValorPosicional<string>(linha, 192, 9);
                        cobranca.ChaveAcessoNFe = ObterValorPosicional<string>(linha, 201, 45);
                        cobranca.ProtocoloNFe = ObterValorPosicional<string>(linha, 246, 15);
                    }
                    if (codigo == "555")
                    {
                        if (conhecimento != null)
                            cobranca.Conhecimentos.Add(conhecimento);

                        conhecimento = new DOCCOB_Conhecimento();
                        conhecimento.Id = idLinha;
                        conhecimento.Filial = ObterValorPosicional<string>(linha, 4, 10);
                        conhecimento.Serie = ObterValorPosicional<string>(linha, 14, 5);
                        conhecimento.Numero = ObterValorPosicional<string>(linha, 19, 12);
                        conhecimento.ValorFrete = ObterValorPosicional<decimal>(linha, 31, 15);
                        conhecimento.DataEmissao = ObterValorPosicional<DateTime>(linha, 46, 8);
                        conhecimento.DocumentoRemetente = ObterValorPosicional<string>(linha, 54, 14);
                        conhecimento.DocumentoDestinatario = ObterValorPosicional<string>(linha, 68, 14);
                        conhecimento.DocumentoEmissor = ObterValorPosicional<string>(linha, 82, 14);
                        conhecimento.UfEmbarcadora = ObterValorPosicional<string>(linha, 96, 2);
                        conhecimento.UfDestinataria = ObterValorPosicional<string>(linha, 98, 2);
                        conhecimento.UfEmissora = ObterValorPosicional<string>(linha, 100, 2);
                        conhecimento.CodigoIVA = ObterValorPosicional<string>(linha, 112, 2);
                        conhecimento.Devolucao = ObterValorPosicional<string>(linha, 194, 1);
                    }
                    if (codigo == "556")
                    {
                        if (notaFiscal != null)
                            cobranca.NotasFiscais.Add(notaFiscal);

                        notaFiscal = new DOCCOB_NotaFiscal();
                        notaFiscal.Id = idLinha;
                        notaFiscal.NotaSerie = ObterValorPosicional<string>(linha, 4, 3).TrimStart('0');
                        notaFiscal.NotaNumero = ObterValorPosicional<string>(linha, 7, 9).TrimStart('0');
                        notaFiscal.DataEmissao = ObterValorPosicional<DateTime>(linha, 16, 8);
                        notaFiscal.Peso = ObterValorPosicional<decimal>(linha, 24, 15);
                        notaFiscal.Valor = ObterValorPosicional<decimal>(linha, 31, 15);
                        notaFiscal.DocumentoEmissor = ObterValorPosicional<string>(linha, 46, 14);
                        notaFiscal.NumeroRomaneio = ObterValorPosicional<string>(linha, 60, 20);
                        notaFiscal.NumeroSAP1 = ObterValorPosicional<string>(linha, 80, 20);
                        notaFiscal.NumeroSAP2 = ObterValorPosicional<string>(linha, 100, 20);
                        notaFiscal.NumeroSAP3 = ObterValorPosicional<string>(linha, 120, 20);
                        notaFiscal.Devolucao = ObterValorPosicional<string>(linha, 141, 1) == "S" ? true : false;

                        if (string.IsNullOrEmpty(notaFiscal.NotaNumero) || !notaFiscal.NotaNumero.All(char.IsDigit))
                            retornoLeituraDoccob.Criticas.Add(new FaturaArquivoCriticaDTO { Descricao = "Número da Nota inválido", Linha = idLinha, Posicao = 7 });

                        if (ValidateData.DataMinima(notaFiscal.DataEmissao))
                            retornoLeituraDoccob.Criticas.Add(new FaturaArquivoCriticaDTO { Descricao = "Data Emissão inválida", Linha = idLinha, Posicao = 16 });
                        
                        if ((!ValidateData.DataMinima(notaFiscal.DataEmissao)) && notaFiscal.DataEmissao <= DateTime.Now.AddYears(-2))
                            retornoLeituraDoccob.Criticas.Add(new FaturaArquivoCriticaDTO { Descricao = "Data Emissão Periodo inválido", Linha = idLinha, Posicao = 16 });
                    }

                    idLinha += 1;
                }
                if (docCob == null)
                    throw new ApplicationException("Erro ao ler arquivo DOCCOB, código 000 não encontrado");

                if (cobranca != null)
                    cabecalho.Transportadora.Cobrancas.Add(cobranca);
                if (cabecalho != null)
                    docCob.Cabecalhos.Add(cabecalho);
                if (conhecimento != null)
                    cobranca.Conhecimentos.Add(conhecimento);
                if (notaFiscal != null)
                    cobranca.NotasFiscais.Add(notaFiscal);
            }

            retornoLeituraDoccob.Doccob = docCob;

            return retornoLeituraDoccob;
        }
        public static CONEMB ObterCONEMB30(byte[] file)
        {
            CONEMB conemb = null;
            CONEMB_Cabecalho cabecalho = null;
            CONEMB_Transportadora transportadora = null;
            CONEMB_Conhecimento conhecimento = null;

            using (var stream = new MemoryStream(file))
            using (var reader = new StreamReader(stream))
            {
                string linha;
                int count = 0;
                try
                {
                    while ((linha = reader.ReadLine()) != null)
                    {
                        count++;
                        if (string.IsNullOrEmpty(linha))
                            continue;

                        string codigo = linha.Substring(0, 3);
                        if (codigo == "000")
                        {
                            conemb = new CONEMB();
                            conemb.Id_Remetente = linha.ObterValorPosicional<string>(4, 35);
                            conemb.Id_Destinatario = linha.ObterValorPosicional<string>(39, 35);
                            conemb.Data = linha.ObterValorPosicional<DateTime>(74, 6);
                        }
                        if (codigo == "320")
                        {
                            if (cabecalho != null)
                                conemb.Cabecalhos.Add(cabecalho);

                            cabecalho = new CONEMB_Cabecalho();
                            cabecalho.Id_Documento = linha.ObterValorPosicional<string>(4, 14);
                        }
                        if (codigo == "321")
                        {
                            cabecalho.Transportadora.CNPJ = linha.ObterValorPosicional<string>(4, 14);
                            cabecalho.Transportadora.RazaoSocial = linha.ObterValorPosicional<string>(18, 50);
                        }
                        if (codigo == "322")
                        {
                            if (conhecimento != null)
                                cabecalho.Transportadora.Conhecimentos.Add(conhecimento);

                            conhecimento = new CONEMB_Conhecimento();
                            conhecimento.Filial = linha.ObterValorPosicional<string>(4, 10);
                            conhecimento.Serie = linha.ObterValorPosicional<string>(14, 5).TrimStart('0');
                            conhecimento.Numero = linha.ObterValorPosicional<string>(19, 12).TrimStart('0');
                            conhecimento.DataEmissao = linha.ObterValorPosicional<DateTime>(31, 8);
                            conhecimento.CondicaoFrete = linha.ObterValorPosicional<string>(39, 1);
                            conhecimento.PesoTransportado = linha.ObterValorPosicional<decimal>(40, 7);
                            conhecimento.ValorTotalFrete = linha.ObterValorPosicional<decimal>(47, 15);
                            conhecimento.BaseCalculoICMS = linha.ObterValorPosicional<decimal>(62, 15);
                            conhecimento.TaxaICMS = linha.ObterValorPosicional<decimal>(77, 4);
                            conhecimento.ValorICMS = linha.ObterValorPosicional<decimal>(81, 15);
                            conhecimento.ValorFretePorVolume = linha.ObterValorPosicional<decimal>(96, 15);
                            conhecimento.FreteValor = linha.ObterValorPosicional<decimal>(111, 15);
                            conhecimento.ValorSECCAT = linha.ObterValorPosicional<decimal>(126, 15);
                            conhecimento.ValorITR = linha.ObterValorPosicional<decimal>(141, 15);
                            conhecimento.ValorDespacho = linha.ObterValorPosicional<decimal>(156, 15);
                            conhecimento.ValorPedagio = linha.ObterValorPosicional<decimal>(171, 15);
                            conhecimento.ValorADEME = linha.ObterValorPosicional<decimal>(186, 15);
                            conhecimento.SubstituicaoTributaria = linha.ObterValorPosicional<int>(201, 1);
                            conhecimento.NaturezaOperacao = linha.ObterValorPosicional<string>(202, 3);
                            conhecimento.CNPJFilialEmissora = linha.ObterValorPosicional<string>(205, 14);
                            conhecimento.CNPJEmissorNota = linha.ObterValorPosicional<string>(219, 14);
                            conhecimento.CNPJEmissorNF = linha.ObterValorPosicional<string>(219, 14);
                            conhecimento.SerieNF = linha.ObterValorPosicional<string>(233, 3).TrimStart('0');
                            conhecimento.NumeroNF = linha.ObterValorPosicional<string>(236, 8).TrimStart('0');
                            conhecimento.TipoDocumento = linha.ObterValorPosicional<string>(674, 1);
                            conhecimento.DataEmissaoNF = null;

                            conhecimento.Linha = count;

                        }
                    }
                }
                catch (Exception e)
                {
                    throw new ApplicationException($"Erro ao ler linha {count} do arquivo 3.0. {e.Message}");
                }

                if (conemb == null)
                    throw new ApplicationException("Erro ao ler arquivo CONEMB 3.0, código 000 não encontrado");
                if (conhecimento != null)
                    cabecalho.Transportadora.Conhecimentos.Add(conhecimento);
                if (cabecalho != null)
                    conemb.Cabecalhos.Add(cabecalho);
            }
            return conemb;
        }
        public static CONEMB ObterCONEMB50(byte[] file)
        {
            CONEMB conemb = null;
            CONEMB_Cabecalho cabecalho = null;
            CONEMB_Transportadora transportadora = null;
            CONEMB_Conhecimento conhecimento = null;

            using (var stream = new MemoryStream(file))
            using (var reader = new StreamReader(stream))
            {
                string linha;
                int count = 0;
                try
                {
                    while ((linha = reader.ReadLine()) != null)
                    {
                        count++;
                        if (string.IsNullOrEmpty(linha))
                            continue;
                        string codigo = linha.Substring(0, 3);
                        if (codigo == "000")
                        {
                            conemb = new CONEMB();
                            conemb.Id_Remetente = linha.ObterValorPosicional<string>(4, 35);
                            conemb.Id_Destinatario = linha.ObterValorPosicional<string>(39, 35);
                            conemb.Data = linha.ObterValorPosicional<DateTime>(74, 6);
                        }
                        if (codigo == "520")
                        {
                            if (cabecalho != null)
                                conemb.Cabecalhos.Add(cabecalho);

                            cabecalho = new CONEMB_Cabecalho();
                            cabecalho.Id_Documento = linha.ObterValorPosicional<string>(4, 14);
                        }
                        if (codigo == "521")
                        {
                            cabecalho.Transportadora.CNPJ = linha.ObterValorPosicional<string>(4, 14);
                            cabecalho.Transportadora.RazaoSocial = linha.ObterValorPosicional<string>(18, 50);
                        }
                        if (codigo == "522")
                        {
                            if (conhecimento != null)
                                cabecalho.Transportadora.Conhecimentos.Add(conhecimento);

                            conhecimento = new CONEMB_Conhecimento();
                            conhecimento.Filial = linha.ObterValorPosicional<string>(4, 10);
                            conhecimento.Serie = linha.ObterValorPosicional<string>(14, 5).TrimStart('0');
                            conhecimento.Numero = linha.ObterValorPosicional<string>(19, 12).TrimStart('0');
                            conhecimento.DataEmissao = (linha.ObterValorPosicional<DateTime>(31, 8).Year == 1) ? null : conhecimento.DataEmissao = linha.ObterValorPosicional<DateTime>(31, 8);
                            conhecimento.CondicaoFrete = linha.ObterValorPosicional<string>(39, 1);
                            conhecimento.CNPJFilialEmissora = linha.ObterValorPosicional<string>(40, 14);
                            conhecimento.CNPJEmissorNota = linha.ObterValorPosicional<string>(54, 14);
                            conhecimento.CFOP = linha.ObterValorPosicional<string>(110, 5);
                            conhecimento.DANFE = linha.ObterValorPosicional<string>(219, 44);
                            conhecimento.ProtocoloAutorizacao = linha.ObterValorPosicional<string>(264, 15);
                            conhecimento.DigestValue = linha.ObterValorPosicional<string>(279, 9);
                            conhecimento.UFEnvio = linha.ObterValorPosicional<string>(335, 2);
                            conhecimento.UFInicio = linha.ObterValorPosicional<string>(337, 2);
                            conhecimento.UFFim = linha.ObterValorPosicional<string>(339, 2);

                            conhecimento.Linha = count;
                        }
                        if (codigo == "523")
                        {
                            conhecimento.PesoTransportado = linha.ObterValorPosicional<decimal>(12, 9);
                            conhecimento.ValorTotalFrete = linha.ObterValorPosicional<decimal>(41, 15);
                            conhecimento.TipoDocumento = linha.ObterValorPosicional<string>(206, 1);
                            conhecimento.BaseCalculoICMS = linha.ObterValorPosicional<decimal>(207, 15);
                            conhecimento.TaxaICMS = linha.ObterValorPosicional<decimal>(222, 5);
                            conhecimento.ValorICMS = linha.ObterValorPosicional<decimal>(227, 15);
                            conhecimento.ValorFretePorVolume = linha.ObterValorPosicional<decimal>(56, 15);
                            conhecimento.FreteValor = linha.ObterValorPosicional<decimal>(71, 15);
                            conhecimento.ValorSECCAT = linha.ObterValorPosicional<decimal>(101, 15);
                            conhecimento.ValorITR = linha.ObterValorPosicional<decimal>(116, 15);
                            conhecimento.ValorDespacho = linha.ObterValorPosicional<decimal>(131, 15);
                            conhecimento.ValorPedagio = linha.ObterValorPosicional<decimal>(146, 15);
                            conhecimento.ValorADEME = linha.ObterValorPosicional<decimal>(161, 15);
                            //conhecimento.NaturezaOperacao = null;

                            conhecimento.Linha = count;
                        }
                        if (codigo == "524")
                        {
                            conhecimento.CNPJEmissorNF = linha.ObterValorPosicional<string>(4, 14);
                            conhecimento.NumeroNF = linha.ObterValorPosicional<string>(18, 9).TrimStart('0');
                            conhecimento.SerieNF = linha.ObterValorPosicional<string>(27, 3).TrimStart('0');
                            conhecimento.DataEmissaoNF = (linha.ObterValorPosicional<DateTime>(30, 8).Year == 1) ? null : conhecimento.DataEmissaoNF = linha.ObterValorPosicional<DateTime>(30, 8);

                            conhecimento.Linha = count;
                        }
                    }
                }
                catch (Exception e)
                {
                    throw new ApplicationException($"Erro ao ler linha {count} do arquivo 5.0. {e.Message}");
                }

                if (conemb == null)
                    throw new ApplicationException("Erro ao ler arquivo CONEMB 5.0, código 000 não encontrado");
                if (conhecimento != null)
                    cabecalho.Transportadora.Conhecimentos.Add(conhecimento);
                if (cabecalho != null)
                    conemb.Cabecalhos.Add(cabecalho);
            }
            return conemb;
        }
        public static CONEMB ObterCONEMB(byte[] file)
        {
            var tipo = ObterVersaoCONEMB(file);
            switch (tipo)
            {
                case EnumVersaoCONEMB.TresZero: return ObterCONEMB30(file);
                case EnumVersaoCONEMB.CincoZero: return ObterCONEMB50(file);
                default:
                    throw new ApplicationException("Erro ao obter dados arquivo CONEMB versão não suportada.");
            }
        }
        public static EnumVersaoCONEMB ObterVersaoCONEMB(byte[] file)
        {
            using (var stream = new MemoryStream(file))
            using (var reader = new StreamReader(stream))
            {
                string linha;
                string versao = "";
                int i = 1;
                while (i <= 2)
                {
                    linha = reader.ReadLine();
                    if (!string.IsNullOrEmpty(linha))
                    {
                        versao += linha.Left(1);
                        i++;
                    }
                }

                switch (versao)
                {
                    case "03": return EnumVersaoCONEMB.TresZero;
                    case "05": return EnumVersaoCONEMB.CincoZero;
                    default:
                        throw new ApplicationException("Erro ao identificar a versão do arquivo CONEMB");
                }
            }
        }
        public static T ObterValorPosicional<T>(this string linha, int posicao, int tamanho)
        {
            posicao--;
            string result = linha.Substring(posicao, tamanho).Trim();
            try
            {
                if (typeof(T) == typeof(DateTime))
                {
                    int dia = result.Substring(0, 2).ToInt();
                    int mes = result.Substring(2, 2).ToInt();
                    int ano;
                    if (result.Length == 6)
                        ano = $"20{result.Substring(4, 2)}".ToInt();
                    else
                        ano = result.Substring(4, 4).ToInt();
                    DateTime data = new DateTime(ano, mes, dia);
                    return (T)Convert.ChangeType(data, typeof(T));
                }
                if (typeof(T) == typeof(decimal))
                {
                    decimal valor = result.ToDecimal() / 100;
                    return (T)Convert.ChangeType(valor, typeof(T));
                }
            }
            catch (Exception)
            {
                if (typeof(T).IsValueType)
                {
                    if (typeof(T) == typeof(DateTime))
                        return (T)Convert.ChangeType(default(DateTime), typeof(T));
                    else
                        return (T)Convert.ChangeType(string.Empty, typeof(T));
                }
            }

            return (T)Convert.ChangeType(result, typeof(T));
        }
    }

}
