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
    class ImportacaoSkuAnymarketTask : IJob, IJobTask
    {
        private readonly ILogHelper _logHelper;
        private readonly IAnymarketApplication<FretterContext> _application;

        public ImportacaoSkuAnymarketTask(ILogHelper logHelper, IAnymarketApplication<FretterContext> application)
        {
            _logHelper = logHelper;
            this._application = application;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var watch = new Stopwatch();
            watch.Start();
            string jobName = ((Quartz.Impl.JobDetailImpl)context.JobDetail)?.Name;

            try
            {
                _logHelper.LogInfo(jobName, $"Inicio", null, DateTime.Now);
                await _application.ImportarSku();
                _logHelper.LogInfo(jobName, $"", null, DateTime.Now, watch.ElapsedMilliseconds);
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
