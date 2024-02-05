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
    class ProcessaEntregaDevolucaoTask : IJob, IJobTask
    {
        private readonly ILogHelper _logHelper;
        private readonly IEntregaDevolucaoApplication<FretterContext> _application;

        public ProcessaEntregaDevolucaoTask(ILogHelper logHelper, IEntregaDevolucaoApplication<FretterContext> application)
        {
            _logHelper = logHelper;
            this._application = application;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            string jobName = ((Quartz.Impl.JobDetailImpl)context.JobDetail)?.Name;
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                int quantidade = await _application.ProcessaEntregaDevolucaoIntegracao();
                Console.WriteLine($"{jobName}) - {DateTime.Now:HH:mm:ss} - {watch.ElapsedMilliseconds}(ms) - {quantidade} messages");
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
