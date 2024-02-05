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
    class ImportacaoConfiguracaoTask : IJob, IJobTask
    {
        private readonly ILogHelper _logHelper;
        private readonly IImportacaoConfiguracaoApplication<FretterContext> _application;

        public ImportacaoConfiguracaoTask(ILogHelper logHelper, IImportacaoConfiguracaoApplication<FretterContext> application)
        {
            _logHelper = logHelper;
            this._application = application;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var watch = new Stopwatch();
            watch.Start();
            string jobName = ((Quartz.Impl.JobDetailImpl)context.JobDetail)?.Name;
            int quantidade = 0;

            try
            {
                _logHelper.LogInfo(jobName, $"Inicio", null, DateTime.Now);
                quantidade = _application.ProcessarImportacaoConfiguracao();
                _logHelper.LogInfo(jobName, $"Fim {quantidade}", null, DateTime.Now, watch.ElapsedMilliseconds);
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
