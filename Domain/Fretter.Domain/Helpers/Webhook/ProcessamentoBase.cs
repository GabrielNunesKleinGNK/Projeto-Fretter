using Fretter.Domain.Dto.Webhook;
using Fretter.Domain.Interfaces.Webhook;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fretter.Domain.Helpers.Webhook
{
    public abstract class ProcessamentoBase<T> : IDisposable where T : IErro
    {
        public static readonly Mutex Mx = new Mutex();
        public int ArquivoId { get; protected set; }
        public string UsuarioId { get; protected set; }
        public EEmpresa Emp { get; set; }
        protected EEmpresa EmpMtk { get; set; }
        public RetornoProcessamento<T> Retorno { get; protected set; }
        public TimeSpan FusoCliente { get; set; }

        public readonly List<Task> LstTaskSave = new List<Task>();

        protected ProcessamentoBase(
            EEmpresa emp,
            EEmpresa empMkt,
            RetornoProcessamento<T> retorno)
        {
            Emp = emp;
            EmpMtk = empMkt;
            Retorno = retorno;
            Retorno.Processador = this;
        }

        public abstract void SalvarArquivo(object arquivo);

        public abstract void Gravar();

        public virtual void Dispose() { }
    }
    public abstract class ProcessamentoBase<T, T2> : ProcessamentoBase<T>
        where T : IErro
        // where T2 : IParam
    {
        public readonly List<T2> _list = new List<T2>();
        public bool Saving { get; protected set; }
        public bool ErrorSaving { get; protected set; }
        protected ProcessamentoBase(
            EEmpresa emp,
            EEmpresa empMkt,
            RetornoProcessamento<T> retorno) : base(emp, empMkt, retorno) { }
        public abstract bool Processa(T2 p);
    }

    public abstract class ProcessamentoBase<T, T2, T3> : ProcessamentoBase<T, T2>
        where T : IErro
        // where T2 : IParam
        // where T3 : IParam
    {
        public readonly List<T3> _listComplemento = new List<T3>();

        protected ProcessamentoBase(
            EEmpresa emp,
            EEmpresa empMkt,
            RetornoProcessamento<T> retorno) : base(emp, empMkt, retorno) { }
        public abstract bool ProcessaComplemento(T3 p);
        public abstract void GravarComplemento();
    }

    public abstract class ProcessamentoBaseSimulador<T, T2> : ProcessamentoBase<T, T2, T2>
        where T : IErro
        where T2 : IParam
    {
        protected ProcessamentoBaseSimulador(
            EEmpresa emp,
            EEmpresa empMkt,
            RetornoProcessamento<T> retorno) : base(emp, empMkt, retorno) { }
    }

    public interface IParam
    {
        int id { get; set; }
        int linha { get; }
    }
}
