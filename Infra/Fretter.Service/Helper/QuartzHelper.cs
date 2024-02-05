using Fretter.Service.Config;
using Quartz;
using System;

namespace Fretter.Service.Helper
{
    public static class QuartzHelper
    {
        public static JobsExecucao CriarJobExecucao(TempoIntervalo tempoIntervalo, string nome = "", dynamic type = null)
        {
            return new JobsExecucao
            {
                Nome = nome,
                Intervalo = tempoIntervalo.Intervalo,
                Job = QuartzHelper.CriarJob(nome, type),
                Trigger = QuartzHelper.CriarTrigger(nome, tempoIntervalo)
            };
        }

        public static IJobDetail CriarJob(string jobNome, dynamic type = null)
        {
            return JobBuilder.Create(type)
                             .WithIdentity(new JobKey(jobNome, "Principal"))
                             .Build();
        }

        public static ITrigger CriarTrigger(string jobNome, TempoIntervalo tempoIntervalo)
        {
            return TriggerBuilder.Create()
                                 .WithIdentity(new TriggerKey(jobNome, "Principal"))
                                 .WithSimpleSchedule(x => x.TempoIntervalo(tempoIntervalo))
                                 .StartNow()
                                 .Build();
        }

        public static void TempoIntervalo(this SimpleScheduleBuilder x, TempoIntervalo tempoIntervalo)
        {
            switch (tempoIntervalo.Unidade)
            {
                case Unidade.Milisegundos:
                    x.WithInterval(TimeSpan.FromMilliseconds(tempoIntervalo.Intervalo)).RepeatForever();
                    break;
                case Unidade.Segundos:
                    x.WithIntervalInSeconds(tempoIntervalo.Intervalo).RepeatForever();
                    break;

                case Unidade.Horas:
                    x.WithIntervalInHours(tempoIntervalo.Intervalo).RepeatForever();
                    break;

                case Unidade.Minutos:
                default:
                    x.WithIntervalInMinutes(tempoIntervalo.Intervalo).RepeatForever();
                    break;
            }
        }
    }
}
