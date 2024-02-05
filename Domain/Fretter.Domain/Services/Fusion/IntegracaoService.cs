using Fretter.Domain.Dto.EmpresaIntegracao;
using Fretter.Domain.Dto.Fusion;
using Fretter.Domain.Entities;
using Fretter.Domain.Helpers;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fretter.Domain.Services
{
    public class IntegracaoService<TContext> : ServiceBase<TContext, Integracao>, IIntegracaoService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        private readonly IIntegracaoRepository<TContext> _repository;
        public IntegracaoService(
                IIntegracaoRepository<TContext> repository,
                IUnitOfWork<TContext> unitOfWork,
                IUsuarioHelper user) : base(repository, unitOfWork, user)
        {
            _user = user;
            _repository = repository;
        }

        public List<DeParaEmpresaIntegracao> BuscaCamposDePara()
        {
            return _repository.BuscaCamposDePara("[Fretter].[GetCamposDeParaTelaIntegracoes]");
        }
        public async Task<TesteIntegracaoRetorno> TesteIntegracao(EmpresaIntegracao dadosParaConsulta)
        {
            TesteIntegracaoRetorno retorno = new TesteIntegracaoRetorno();
            try
            {
                if (!string.IsNullOrEmpty(dadosParaConsulta?.URLBase) && dadosParaConsulta.ListaIntegracoes.Any())
                {
                    EmpresaIntegracaoItem integracao = dadosParaConsulta.ListaIntegracoes.FirstOrDefault();
                    Stopwatch watch = new Stopwatch();

                    var client = new WebApiClient(dadosParaConsulta.URLBase);

                    if (!string.IsNullOrEmpty(dadosParaConsulta.ApiKey))
                        client = new WebApiClient(dadosParaConsulta.URLBase, dadosParaConsulta.ApiKey);
                    else if (!string.IsNullOrEmpty(dadosParaConsulta.URLToken))
                        await client.AuthenticateGrantTypeOAuth2(dadosParaConsulta.URLToken, dadosParaConsulta.ClientId, dadosParaConsulta.ClientSecret,
                            dadosParaConsulta.ClientScope, dadosParaConsulta.Usuario, dadosParaConsulta.Senha);
                    else client.IsAnonymous = true;

                    if (!string.IsNullOrEmpty(integracao.LayoutHeader) && integracao.EnvioBody == true)
                    {
                        var listHeader = integracao.LayoutHeader.Split("|").ToList();
                        var listHeaderItem = new List<KeyValuePair<string, string>>();

                        foreach (var itemHeader in listHeader)
                        {
                            if (itemHeader.Contains("="))
                            {
                                var listHeaderValue = itemHeader.Split("=").ToList();
                                listHeaderItem.Add(new KeyValuePair<string, string>(listHeaderValue[0], listHeaderValue[1]));
                            }
                        }

                        var itemEstruturaLayout = Regex.Match(integracao.Layout, @"\[{(.*?)\}]").Groups[1].Value;

                        if (integracao.Verbo.ToUpper() == "POST")
                        {
                            watch.Start();
                            var res = await client.PostWithHeader<object>(integracao.URL, itemEstruturaLayout, listHeaderItem);
                            res.Dispose();
                            watch.Stop();

                            TesteIntegracaoRetorno response = new TesteIntegracaoRetorno()
                            {
                                Body = res.RequestMessage.Content.ReadAsStringAsync().Result,
                                StatusCode = res.StatusCode.GetHashCode(),
                                Tempo = watch.Elapsed.TotalMilliseconds
                            };

                            return response;
                        }
                    }
                    else
                    {
                        if (dadosParaConsulta?.ListaIntegracoes?.FirstOrDefault()?.Verbo.ToUpper() == "GET")
                        {
                            var urlQueryParameter = $"{integracao.URL}{integracao.Layout}";
                            watch.Start();
                            var res = await client.GetWithResponse<object>(urlQueryParameter);
                            res.Dispose();
                            watch.Stop();

                            TesteIntegracaoRetorno response = new TesteIntegracaoRetorno()
                            {
                                Body = res.RequestMessage.Content.ReadAsStringAsync().Result,
                                StatusCode = res.StatusCode.GetHashCode(),
                                Tempo = watch.Elapsed.TotalMilliseconds
                            };

                            return response;
                        }
                        else if (integracao.Verbo.ToUpper() == "PUT")
                        {
                            var urlParametro = integracao.URL;
                            dynamic objRequest = null;
                            if (integracao.Layout != null)
                            {
                                var request = integracao.Layout.Trim();
                                objRequest = JObject.Parse(request);
                            }

                            watch.Start();
                            var res = await client.PutWithResponse<object>(urlParametro, objRequest);
                            res.Dispose();
                            watch.Stop();

                            TesteIntegracaoRetorno response = new TesteIntegracaoRetorno()
                            {
                                Body = res.RequestMessage.Content.ReadAsStringAsync().Result,
                                StatusCode = res.StatusCode.GetHashCode(),
                                Tempo = watch.Elapsed.TotalMilliseconds
                            };

                            return response;
                        }
                    }
                }

            }
            catch (Exception)
            {
                return retorno;
            }

            return retorno;
        }
    }
}
