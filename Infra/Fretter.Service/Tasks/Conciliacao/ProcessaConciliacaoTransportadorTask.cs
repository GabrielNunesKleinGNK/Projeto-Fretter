using Quartz;
using System;
using System.Threading.Tasks;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Service;
using Fretter.Repository.Contexts;
using Fretter.Domain.Interfaces.Helper;
using System.Diagnostics;

namespace Fretter.Service.Tasks.Conciliacao
{    
    [DisallowConcurrentExecution]
    class ProcessaConciliacaoTransportadorTask : IJob, IJobTask
    {
        private readonly ILogHelper _logHelper;
        private readonly IConciliacaoTransportadorApplication<FretterContext> _application;

        public ProcessaConciliacaoTransportadorTask(ILogHelper logHelper, IConciliacaoTransportadorApplication<FretterContext> application)
        {
            _logHelper = logHelper;
            this._application = application;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            int quantidade = 0;
            string jobName = ((Quartz.Impl.JobDetailImpl)context.JobDetail)?.Name;
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                quantidade = await _application.ProcessaConciliacaoTransportador();
                Console.WriteLine($"{jobName}) - {DateTime.Now:HH:mm:ss} - {watch.ElapsedMilliseconds}(ms) - {quantidade} registros");
                _logHelper.LogInfo(jobName, "", null, DateTime.Now, watch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(jobName, "", null, DateTime.Now, watch.ElapsedMilliseconds, ex);
            }

            watch.Stop();
        }

        public bool CanExecute()
        {
            return true;
        }
    }
}

