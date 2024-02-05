using Quartz;
using System;
using System.Threading.Tasks;
using Fretter.Domain.Interfaces.Application;
using Fretter.Domain.Interfaces.Service;
using Fretter.Repository.Contexts;
using System.Diagnostics;
using Fretter.Domain.Interfaces.Helper;

namespace Fretter.Service.Tasks
{
    [DisallowConcurrentExecution]
    class ProcessaCallbackEntregaTask : IJob, IJobTask
    {
        private readonly ILogHelper _logHelper;
        private readonly IEntregaStageCallBackApplication<FretterContext> _application;

        public ProcessaCallbackEntregaTask(ILogHelper logHelper, IEntregaStageCallBackApplication<FretterContext> application)
        {
            _logHelper = logHelper;
            _application = application;
        }

        public Task Execute(IJobExecutionContext context)
        {
            int quantidade = 0;
            string jobName = ((Quartz.Impl.JobDetailImpl)context.JobDetail)?.Name;
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                 quantidade = _application.ProcessaEntregaStageCallback();
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
