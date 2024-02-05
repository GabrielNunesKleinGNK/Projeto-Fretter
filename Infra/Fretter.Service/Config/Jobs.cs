using Quartz;

namespace Fretter.Service.Config
{
    public class JobsExecucao
    {
        public string Nome { get; set; }
        public int Intervalo { get; set; }
        public IJobDetail Job { get; set; }
        public ITrigger Trigger { get; set; }
    }
    public class TempoIntervalo
    {
        public int Intervalo { get; set; }
        public Unidade Unidade { get; set; }

        public TempoIntervalo() => Unidade = Unidade.Minutos;
    }
    public enum Unidade
    {
        Milisegundos = 1,
        Segundos = 2,
        Minutos = 3,
        Horas = 4,
    }
}
