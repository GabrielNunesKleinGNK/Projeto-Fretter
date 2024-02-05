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
    class ProcessaCTeTask : IJob, IJobTask
    {
        private readonly ILogHelper _logHelper;
        private readonly IImportacaoArquivoService<FretterContext> _importacaoArquivo;

        public ProcessaCTeTask(ILogHelper logHelper, IImportacaoArquivoService<FretterContext> importacaoArquivo)
        {
            _logHelper = logHelper;
            this._importacaoArquivo = importacaoArquivo;
        }

        public Task Execute(IJobExecutionContext context)
        {
            string jobName = ((Quartz.Impl.JobDetailImpl)context.JobDetail)?.Name;
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                _importacaoArquivo.ProcessarArquivos();
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
