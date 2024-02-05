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
    class ProcessaEntregaStageReprocessamentoTask : IJob, IJobTask
    {
        private readonly ILogHelper _logHelper;
        private readonly IEntregaConfiguracaoApplication<FretterContext> _application;

        public ProcessaEntregaStageReprocessamentoTask(ILogHelper logHelper, IEntregaConfiguracaoApplication<FretterContext> application)
        {
            this._application = application;
            _logHelper = logHelper;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var watch = new Stopwatch();
            watch.Start();
            string jobName = ((Quartz.Impl.JobDetailImpl)context.JobDetail)?.Name;

            try
            {
                await _application.ReprocessaEntregaMirakl();
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
