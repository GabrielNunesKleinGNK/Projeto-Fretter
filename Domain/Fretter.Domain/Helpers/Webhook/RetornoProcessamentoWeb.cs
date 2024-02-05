using Fretter.Domain.Dto.Webhook;
using Fretter.Domain.Interfaces.Webhook;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Transactions;

namespace Fretter.Domain.Helpers.Webhook
{
    public class RetornoProcessamentoWeb<T> : RetornoProcessamento<T> where T : IErro
    {
        public string Key { get; private set; }
        public string _empresa { get; set; }
        public int _empresaId { get; private set; }
        public RetornoProcessamentoWeb(string key, int empresaId)
        {
            Key = key;
            _empresaId = empresaId;
        }

        public override void Msg(string msg, long linha, bool erro = true, bool mysql = false, object retData = null, string json = null)
        {
            //bool cancel = false;
            //try
            //{
            //    DownloadUploadHelper.Mx?.WaitOne();
            //    var it = DownloadUploadHelper.Tb[Key] as bool?;
            //    if (it.HasValue && it.Value)
            //        cancel = true;
            //    else
            //    {
            //        if (erro)
            //        {
            //            AtualizaErro();
            //            if (_erro != null && (erros?.All(l => l.Nr_Linha != linha) ?? false))
            //            {
            //                _erro.Ds_Erro = msg;
            //                _erro.Nr_Linha = (int)linha;
            //                erros.Add(GetErros());
            //            }
            //            msg = $"Problemas ao processar a linha {linha}.";
            //        }
            //        try
            //        {
            //            var hubContext = GlobalHost.ConnectionManager?.GetHubContext<MessageHub>();
            //            hubContext?.Clients?.Client(Key)?.Mensagem(new ProcessingInfo
            //            {
            //                Ended = false,
            //                Msg = msg,
            //                Append = true,
            //                ErroLinha = erro,
            //                RetData = retData
            //            });
            //        }
            //        catch (Exception e)
            //        {
            //            Debug.WriteLine("HUB SignalR is NULL", e.Message);
            //        }

            //        if (mysql)
            //        {
            //            if (erros != null && erros.Count > 100 && _action != null)
            //            {
            //                using (var cnn = Conexao.NewSimulador(_empresaId))
            //                {
            //                    _action.Invoke(cnn, erros.ToList());
            //                    erros.Clear();
            //                }
            //            }
            //        }
            //        else
            //        {
            //            if (erros != null && erros.Count >= 100 && _action != null)
            //            {
            //                using (var cnn = Conexao.NewApp(dsAppCnn))
            //                {
            //                    _action.Invoke(cnn, erros.ToList());
            //                    erros.Clear();
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine(e.Message);
            //}
            //finally
            //{
            //    DownloadUploadHelper.Mx?.ReleaseMutex();
            //}

            //if (cancel)
            //{
            //    if (erros != null)
            //        try
            //        {
            //            if (mysql)
            //            {
            //                using (var cnn = Conexao.NewSimulador(_empresaId))
            //                {
            //                    _action.Invoke(cnn, erros.ToList());
            //                    erros.Clear();
            //                }
            //            }
            //            else
            //            {
            //                using (var cnn = Conexao.NewApp(dsAppCnn))
            //                {
            //                    _action.Invoke(cnn, erros.ToList());
            //                    erros.Clear();
            //                }
            //            }
            //        }
            //        catch (Exception e)
            //        {
            //            Debug.WriteLine(e.Message);
            //        }
            //    throw new System.ProcessException("Processamento cancelado pelo usuário.");
            //}
        }

        public void IsCancel()
        {
            //bool cancel = false;
            //try
            //{
            //    DownloadUploadHelper.Mx?.WaitOne();
            //    var it = DownloadUploadHelper.Tb[Key] as bool?;
            //    if (it.HasValue && it.Value)
            //        cancel = true;
            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine(e.Message);
            //}
            //finally
            //{
            //    DownloadUploadHelper.Mx?.ReleaseMutex();
            //}

            //if (cancel)
            //{
            //    if (erros != null)
            //        try
            //        {
            //            using (var cnn = Conexao.NewApp(dsAppCnn))
            //            {
            //                _action.Invoke(cnn, erros.ToList());
            //                erros.Clear();
            //            }
            //        }
            //        catch (Exception e)
            //        {
            //            Debug.WriteLine(e.Message);
            //        }
            //    throw new System.ProcessException("Processamento cancelado pelo usuário.");
            //}
        }

        public override void Fim(string msg, bool success, bool mysql = false, bool grav = true)
        {
            //try
            //{
            //    var hubContext = GlobalHost.ConnectionManager?.GetHubContext<MessageHub>();
            //    hubContext?.Clients?.Client(Key)?.Mensagem(new ProcessingInfo
            //    {
            //        Ended = true,
            //        Msg = msg,
            //        FileOk = success,
            //        Append = false
            //    });
            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine("HUB SignalR is NULL 2", e.Message);
            //}
            //if (!mysql)
            //{
            //    if (erros != null && erros.Count > 0 && _action != null)
            //    {

            //        using (var cnn = Conexao.NewApp(dsAppCnn))
            //        {
            //            _action.Invoke(cnn, erros.ToList());
            //            erros.Clear();
            //        }
            //    }
            //}
            //else
            //{
            //    if (erros != null && erros.Count > 0 && _action != null)
            //    {
            //        using (var cnn = Conexao.NewSimulador(_empresaId))
            //        {
            //            _action.Invoke(cnn, erros.ToList());
            //            erros.Clear();
            //        }
            //    }
            //}
            //DownloadUploadHelper.Tb?.Remove(Key);
        }

        public void Fim(string msg, bool confirm, object retData)
        {
            //try
            //{
            //    var hubContext = GlobalHost.ConnectionManager?.GetHubContext<MessageHub>();
            //    hubContext?.Clients?.Client(Key)?.Confirma(new
            //    {
            //        Msg = msg,
            //        confirm,
            //        RetData = retData
            //    });
            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine("HUB SignalR is NULL 2", e.Message);
            //}
            //DownloadUploadHelper.Tb?.Remove(Key);
        }

        public override object GetResume(long linha)
        {
            return new
            {
                //percentual decorido
                p = (TotalLinhas == 0 ? 0 : (((double)linha) / TotalLinhas) * 100).ToString("N2"),
                //tempo decorrido
                td = Sw.Elapsed.ToString(@"hh\:mm\:ss"),
                //tempo restante estimado
                te = new TimeSpan(linha * (TotalLinhas - linha) == 0 ? 0 : Sw.ElapsedTicks / linha * (TotalLinhas - linha)).ToString(@"hh\:mm\:ss"),
                t = TotalLinhas,
                l = linha
            };
        }

        public bool ExisteErro()
        {
            return erros.Count > 0;
        }

        public override RetornoProcessamento<T> Clone() => throw new NotImplementedException();
    }
    public class RetornoProcessamentoWeb<T, T2> : RetornoProcessamentoWeb<T>
       where T : IErro
       where T2 : IUpload
    {
        public T2 Model { get; private set; }
        public RetornoProcessamentoWeb(T2 model, int empresaId) : base(model.Key, empresaId)
        {
            Model = model;
        }
    }
    public class RetornoProcessamentoWebParallel<T, T2> : RetornoProcessamentoWeb<T>
   where T : IErro
   where T2 : IUpload
    {
        public T2 Model { get; private set; }
        public RetornoProcessamentoWebParallel(T2 model, int empresaId) : base(model.Key, empresaId)
        {
            Model = model;
        }
        //public void FimParallel(string msg, bool sucesso = false, int linha = 0)
        //{
        //    var c = (int)MemoryCache.Default["GU" + Key];
        //    c--;

        //    if (c > 0)
        //    {
        //        MemoryCache.Default["GU" + Key] = c;
        //        Msg(msg, linha, !sucesso);
        //    }
        //    else
        //    {
        //        MemoryCache.Default.Remove("GU" + Key);
        //        Fim(msg, sucesso);
        //    }
        //}

        //public void Add()
        //{
        //    var c = (MemoryCache.Default["GU" + Key] as int?) ?? 0;
        //    c++;
        //    MemoryCache.Default["GU" + Key] = c;
        //}
    }
    public class RetornoProcessamentoWs<T, T2> : RetornoProcessamento<T> where T2 : RetornoWsErro<T>, new() where T : IErro
    {
        public RetornoWs<T> ret;

        public RetornoWsErro<T> err;

        public RetornoProcessamentoWs(RetornoWs<T> re)
        {
            ret = re;
        }

        public override void AddRegistro(int registros, int retornoDb)
        {
            ret.qtdRegistrosImportados += registros - retornoDb;
            ret.qtdRegistrosComErro += erros.Count + retornoDb;
        }

        public override void Msg(string msg, long linha, bool erro = true, bool mysql = false, object retData = null, string json = null)
        {
            if (erro && _erro != null)
                AtualizaErro();

            if (err == null)
                SetErro();

            if (erro)
            {
                ret.erros.Add(err.Ret<T2>(_i, msg));
                var e = err.RetTb();
                _ac?.Invoke(e);
                erros.Add(e);
            }
            else
                ret.msg = msg;
        }

        public override void Fim(string msg, bool success, bool mysql = false, bool grav = true)
        {
            //if (!mysql)
            //{
            //if (grav && erros.Count > 0 && _action != null)
            //{
            //    using (var t = new TransactionScope())

            //    using (var cnn = Conexao.NewApp(dsAppCnn))
            //    {
            //        _action.Invoke(cnn, erros.ToList());
            //        t.Complete();
            //        erros.Clear();
            //    }
            //}
            //}
            //else
            //{
            //    if (grav && erros.Count > 0 && _actionMysql != null)
            //    {
            //        using (var cnn = Conexao.NewMysql)
            //        {
            //            _actionMysql.Invoke(cnn, erros.ToList());
            //            erros.Clear();
            //        }
            //    }
            //}
        }

        public void SetErro()
        {
            err = new T2();
        }

        public override RetornoProcessamento<T> Clone() => throw new NotImplementedException();
    }
    public class RetornoProcessamentoXml<T> : RetornoProcessamento<T> where T : IErro
    {
        public RetornoWs ret;

        public RetornoWsErro err;

        public RetornoProcessamentoXml(RetornoWs re)
        {
            ret = re;
        }

        public override void AddRegistro(int registros, int retornoDb)
        {

            ret.qtdRegistrosImportados = registros - retornoDb;
            ret.qtdRegistrosComErro = erros.Count + retornoDb;
        }

        public override void Msg(string msg, long linha, bool erro = true, bool mysql = false, object retData = null, string json = null)
        {
            if (erro)
                AtualizaErro();
            if (err == null)
                SetErro();
            ret.erros.Add(err.Ret(_i, msg));
        }

        public override void Fim(string msg, bool success, bool mysql = false, bool grav = true)
        {
            //if (!mysql)
            //{
            //if (erros.Count > 0 && _action != null)
            //{
            //    using (var t = new TransactionScope())

            //    using (var cnn = Conexao.NewApp(dsAppCnn))
            //    {
            //        _action.Invoke(cnn, erros.ToList());
            //        t.Complete();
            //        erros.Clear();
            //    }
            //}
            //}
            //else
            //{
            //    if (erros.Count > 0 && _actionMysql != null)
            //    {
            //        using (var cnn = Conexao.NewMysql)
            //        {
            //            _actionMysql.Invoke(cnn, erros.ToList());
            //            erros.Clear();
            //        }
            //    }
            //}
        }

        public void SetErro()
        {
            err = new RetornoWsErro();
        }

        public override RetornoProcessamento<T> Clone() => throw new NotImplementedException();
    }
    public class RetornoWsErro
    {
        /// <summary>
        /// Endereço de refenrencia do erro.
        /// ex.: 1.2.4.3
        /// tipo = linha 1
        /// filial  = linha 2
        /// transportador = linha 4
        /// entrega = linha 3
        /// </summary>
        public string referenciaErro { get; set; }

        /// <summary>
        /// Erro da entrega
        /// </summary>
        public string erro { get; set; }

        public RetornoWsErro Ret(int[] i, string err)
        {

            referenciaErro = string.Join(",", i);
            erro = err;
            return this;
        }
    }
    public abstract class RetornoWsErro<T> : RetornoWsErro
    {
        public T2 Ret<T2>(int[] i, string err) where T2 : RetornoWsErro<T>
        {
            referenciaErro = string.Join(",", i);
            erro = err;
            return JsonConvert.DeserializeObject<T2>(JsonConvert.SerializeObject(this));
            //return JSON.Deserialize<T2>(JSON.Serialize(this, new Options(excludeNulls: true, includeInherited: true)));
        }

        public abstract T RetTb();
    }
    /// <summary>
    /// Objeto de retorno da importação de entrega
    /// </summary>
    public class RetornoWs
    {
        public RetornoWs()
        {
            erros = new List<RetornoWsErro>();
        }

        public RetornoWs RetMsg(string erroMsg = null)
        {
            if (erroMsg != null)
                erro = erroMsg;
            status = $"{qtdRegistrosImportados} registro(s) recebido(s) com sucesso e {qtdRegistrosComErro} regitro(s) com erro.";

            return this;
        }

        //public T2 RetMsg<T2>(string erroMsg = null) where T2 : class
        //{
        //    if (erroMsg != null)
        //        erro = erroMsg;
        //    status = $"{qtdRegistrosImportados} registro(s) recebido(s) com sucesso e {qtdRegistrosComErro} regitro(s) com erro.";

        //    return JSON.Deserialize<T2>(JSON.Serialize(this));
        //}

        /// <summary>
        /// Protocolo
        /// </summary>
        public int protocolo { get; set; }

        /// <summary>
        /// Quantidade de Registros
        /// </summary>
        public int qtdRegistros { get; set; }

        /// <summary>
        /// Quantidade de registros importados com sucesso.
        /// </summary>
        public int qtdRegistrosImportados { get; set; }

        /// <summary>
        /// Quantidade de registros não importados (com erro).
        /// </summary>
        public int qtdRegistrosComErro { get; set; }

        /// <summary>
        /// Msg de Retorno
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// Status de Retorno
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// Erro geral de processamento
        /// </summary>
        public string erro { get; set; }

        /// <summary>
        /// Registros válidos
        /// </summary>
        public List<RetornoWsErro> erros { get; set; }
    }
    public class RetornoWs<T>
    {
        public RetornoWs()
        {
            erros = new List<RetornoWsErro<T>>();
        }

        public RetornoWs<T> RetMsg(string erroMsg = null)
        {
            if (erroMsg != null)
                erro = erroMsg;
            status = $"{qtdRegistrosImportados} registro(s) recebido(s) com sucesso e {qtdRegistrosComErro} regitro(s) com erro.";

            return this;
        }

        //public T2 RetMsg<T2>(string erroMsg = null) where T2 : class
        //{
        //    if (erroMsg != null)
        //        erro = erroMsg;
        //    status = $"{qtdRegistrosImportados} registro(s) recebido(s) com sucesso e {qtdRegistrosComErro} regitro(s) com erro.";

        //    return JSON.Deserialize<T2>(JSON.Serialize(this));
        //}

        /// <summary>
        /// Protocolo
        /// </summary>
        public int protocolo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string protocoloHash { get; set; }

        /// <summary>
        /// Quantidade de Registros
        /// </summary>
        public int qtdRegistros { get; set; }

        /// <summary>
        /// Quantidade de registros importados com sucesso.
        /// </summary>
        public int qtdRegistrosImportados { get; set; }

        /// <summary>
        /// Quantidade de registros não importados (com erro).
        /// </summary>
        public int qtdRegistrosComErro { get; set; }

        /// <summary>
        /// Mensagem de Retorno
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// Status de Retorno
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// Erro geral de processamento
        /// </summary>
        public string erro { get; set; }

        /// <summary>
        /// Registros válidos
        /// </summary>
        public List<RetornoWsErro<T>> erros { get; set; }
    }
    public class retornoEntregaErro<T>
    {
        /// <summary>
        /// Protocolo
        /// </summary>
        public int protocolo { get; set; }

        /// <summary>
        /// Quantidade de Registros
        /// </summary>
        public int qtd_registros { get; set; }

        /// <summary>
        /// Status de Retrono
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// Registros válidos
        /// </summary>
        public List<T> registrosValidos { get; set; }

        /// <summary>
        /// Registros Inválidos
        /// </summary>
        public List<T> registrosInvalidos { get; set; }
    }
    public class retornoEntregaErro : RetornoWsErro<EEntregaErro>
    {
        public int empresaId { get; set; }
        public int? arquivoId { get; set; }
        public int? transportadorId { get; set; }

        public void ResolveErro(int __empresaId, int __arquivoId)
        {
            empresaId = __empresaId;
            arquivoId = __arquivoId;
        }

        public override EEntregaErro RetTb()
        {
            return new EEntregaErro
            {
                Ds_Erro = string.Concat("(", referenciaErro, ") ", erro),
                Id_Empresa = empresaId,
                Id_Transportador = transportadorId,
                Id_Arquivo = arquivoId
            };
        }

        /// <summary>
        /// CNPJ filial da entrega
        /// </summary>
        public string filial { get; set; }

        /// <summary>
        /// CNPJ filial da entrega
        /// </summary>
        public string transportador { get; set; }

        /// <summary>
        /// Nota Fiscal da entrega
        /// </summary>
        public string nf { get; set; }

        /// <summary>
        /// Série da entrega
        /// </summary>
        public string serie { get; set; }
    }
}
