
using Fretter.Domain.Interfaces.Webhook;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;

namespace Fretter.Domain.Helpers.Webhook
{
    public abstract class RetornoProcessamento<T> where T : IErro
    {
        protected readonly Stopwatch Sw;
        protected List<T> erros = new List<T>();
        protected T _erro;
        protected int[] _i { get; set; }
        public T Erro => _erro;
        protected static readonly Mutex Mx = new Mutex();
        public string dsAppCnn;

        public abstract RetornoProcessamento<T> Clone();

        public T GetErros()
        {
            string o;
            try
            {
                o = JsonConvert.SerializeObject(_erro);
                //o = JSON.Serialize(_erro, new Options(excludeNulls: true, includeInherited: true));
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao Serializa q1", e);
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(o);
                //return JSON.Deserialize<T>(o);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao Deserializar q1", e);
            }
        }

        public long TotalLinhas { get; set; }
        public ProcessamentoBase<T> Processador { get; internal set; }

        protected Action<SqlConnection, List<T>> _action { get; set; }
        //  protected Action<MySqlConnection, List<T>> _actionMysql { get; set; }
        protected RetornoProcessamento()
        {
            Sw = Stopwatch.StartNew();
        }

        internal void SetAction(Action<SqlConnection, List<T>> actionLogErros)
        {
            if (_action == null)
                _action = actionLogErros;
        }

        //internal void SetActionMysql(Action<MySqlConnection, List<T>> actionLogErros)
        //{
        //    if (_actionMysql == null)
        //        _actionMysql = actionLogErros;
        //}

        public abstract void Msg(string msg, long linha, bool erro = true, bool mysql = false, object retData = null, string json = null);
        public virtual void Sucesso(string msg) { }
        public abstract void Fim(string msg, bool success, bool mysql = false, bool grav = true);
        public virtual object GetResume(long linha)
        {
            return null;
        }

        public virtual void AddRegistro(int registros, int retornoDb) { }

        public void Set(params int[] i)
        {
            _i = i;
        }

        public void PreparaErro(T erro)
        {
            _erro = erro;
        }

        protected Action<T> _ac;

        public void AlteraErro(Action<T> ac)
        {
            ac.Invoke(_erro);
        }

        public void PreparaErro(Action<T> ac)
        {
            _ac = ac;
        }

        public void AtualizaErro()
        {
            _ac?.Invoke(_erro);
        }

        public virtual void LimpaListaErros()
        {
            erros.Clear();
        }
    }
    public class RetornoProcessamentoFtp<T> : RetornoProcessamento<T> where T : IErro
    {
        public RetornoProcessamentoFtp() { }

        public override RetornoProcessamento<T> Clone()
        {
            string o;
            try
            {
                o = JsonConvert.SerializeObject(_erro);
                //o = JSON.Serialize(_erro, new Options(excludeNulls: true, includeInherited: true));
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao Serializa q1", e);
            }

            return new RetornoProcessamentoFtp<T>
            {
                //_erro = JSON.Deserialize<T>(o),
                _erro = JsonConvert.DeserializeObject<T>(o),
                Processador = Processador,
                _action = _action,
                _ac = _ac,
            };
        }

        public override void Msg(string msg, long linha, bool erro = true, bool mysql = false, object retData = null, string json = null)
        {
            try
            {
                Mx.WaitOne();
                if (erro)
                {
                    AtualizaErro();
                    if (_erro != null && erros.All(l => l.Nr_Linha != linha))
                    {
                        _erro.Ds_Erro = msg;
                        _erro.Nr_Linha = (int)linha;
                        _erro.Ds_Json = json ?? _erro.Ds_Json;
                        erros.Add(GetErros());
                    }
                    msg = $"Problemas ao processar {linha}.";
                }

                //if (erros.Count >= 100 && _action != null)
                //    using (var cnn = Conexao.NewApp(dsAppCnn))
                //    {
                //        _action.Invoke(cnn, erros.ToList());
                //        erros.Clear();
                //    }
            }
            finally
            {
                Mx.ReleaseMutex();
            }
        }

        public override void Fim(string msg, bool success, bool mysql = false, bool grav = true)
        {
            //if (erros.Count <= 0 || _action == null) return;
            //using (var cnn = Conexao.NewApp(dsAppCnn))
            //{
            //    _action.Invoke(cnn, erros.ToList());
            //    erros.Clear();
            //}
        }

        public bool ExisteErro()
        {
            return erros.Count > 0;
        }
    }
    [Serializable]
    public class ProcessException : Exception
    {
        public ProcessException()
        {
        }

        public ProcessException(string message) : base(message)
        {
        }

        public ProcessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProcessException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
