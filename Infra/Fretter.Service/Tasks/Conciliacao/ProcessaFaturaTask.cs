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
    class ProcessaFaturaTask : IJob, IJobTask
    {
        private readonly ILogHelper _logHelper;
        private readonly IConciliacaoApplication<FretterContext> _application;

        public ProcessaFaturaTask(ILogHelper logHelper, IConciliacaoApplication<FretterContext> application)
        {
            _logHelper = logHelper;
            this._application = application;
        }

        public Task Execute(IJobExecutionContext context)
        {
            string jobName = ((Quartz.Impl.JobDetailImpl)context.JobDetail)?.Name;
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                _application.ProcessaFatura();
                Console.WriteLine($"{jobName}) - {DateTime.Now:HH:mm:ss} - {watch.ElapsedMilliseconds}(ms)");
                _logHelper.LogInfo(jobName, "", null, DateTime.Now, watch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                _logHelper.LogError(jobName, "", null, DateTime.Now, watch.ElapsedMilliseconds, ex);
            }

            watch.Stop();
            return Task.FromResult(0);
        }

        public bool CanExecute()
        {
            return true;
        }
    }
}
