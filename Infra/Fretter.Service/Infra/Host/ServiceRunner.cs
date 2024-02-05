using Fretter.Domain.Interfaces.Service;
using Fretter.Service.Config;
using Fretter.Service.Helper;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Fretter.Domain.Config.TaskDelayConfig;

namespace Fretter.Service.Infra.Host
{
    public class ServiceRunner
    {
        private readonly IScheduler _scheduler;
        private readonly string _task;
        private readonly List<TaskDelayItemConfig> _tasksDelay;

        public ServiceRunner(IScheduler scheduler, string task, List<TaskDelayItemConfig> tasksDelay)
        {
            this._scheduler = scheduler;
            this._task = task;
            this._tasksDelay = tasksDelay;
        }

        public bool Start()
        {
            var myThread = new Thread(new ThreadStart(StartScheduler));
            myThread.Start();
            return true;
        }

        private void StartScheduler() => StartAndScheduleTasks().Wait();

        public void Stop()
        {
            _scheduler.Shutdown().Wait();
        }

        private async Task StartAndScheduleTasks()
        {
            await _scheduler.Start();

            var jobtype = typeof(IJobTask);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => jobtype.IsAssignableFrom(p) && p.Name != nameof(IJobTask) && _task.Split(",").Contains(p.Name))
                .ToList();

            Console.WriteLine($"ServiceRunner - Inicialização agendamento das Tasks: {types.Count()}");

            foreach (var type in types)
            {
                try
                {
                    var taskNome = type.Name;
                    var intervalo = new TempoIntervalo() { Unidade = Unidade.Minutos, Intervalo = 1 };
                    var taskDelay = _tasksDelay.Where(t => t.TaskName == taskNome)?.FirstOrDefault();

                    if (taskDelay != null)
                    {
                        intervalo.Unidade = Unidade.Segundos;
                        intervalo.Intervalo = taskDelay.TaskDelay;
                    }

                    var genericJob = QuartzHelper.CriarJobExecucao(intervalo, taskNome, type);
                    await _scheduler.ScheduleJob(genericJob.Job, genericJob.Trigger);
                    Console.WriteLine($"ServiceRunner - Task {type.Name} agendada com Sucesso.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ServiceRunner - Erro ao Instanciar a Task {type.Name}. Erro: {ex.Message}, Inner: {ex.InnerException}");
                }

            }
        }
    }
}

