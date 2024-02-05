using Quartz;
using System;
using System.Threading.Tasks;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Service;
using Fretter.Repository.Contexts;
using Fretter.Domain.Interfaces.Helper;
using System.Diagnostics;

namespace Fretter.Service.Tasks
{
    [DisallowConcurrentExecution]
    class ProcessaIndicadorTask : IJob, IJobTask
    {
        private readonly ILogHelper _logHelper;
        private readonly IIndicadorConciliacaoApplication<FretterContext> _application;

        public ProcessaIndicadorTask(ILogHelper logHelper, IIndicadorConciliacaoApplication<FretterContext> application)
        {
            _logHelper = logHelper;
            this._application = application;
        }

        public Task Execute(IJobExecutionContext context)
        {
            int quantidade = 0;
            string jobName = ((Quartz.Impl.JobDetailImpl)context.JobDetail)?.Name;
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                quantidade = _application.ProcessaIndicadorConciliacao();
                _logHelper.LogInfo(jobName, "", null, DateTime.Now, watch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(jobName, "", null, DateTime.Now, watch.ElapsedMilliseconds, ex);
            }

            watch.Stop();
            return Task.FromResult(quantidade);
        }

        public bool CanExecute()
        {
            return true;
        }
    }
}
