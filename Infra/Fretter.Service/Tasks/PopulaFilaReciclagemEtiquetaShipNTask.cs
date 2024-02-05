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
    class PopulaFilaReciclagemEtiquetaShipNTask : IJob, IJobTask
    {
        private readonly IEntregaStageApplication<FretterContext> _application;
        private readonly ILogHelper _logHelper;

        public PopulaFilaReciclagemEtiquetaShipNTask(ILogHelper logHelper, IEntregaStageApplication<FretterContext> application, IEntregaStageService<FretterContext> importacaoArquivo)
        {
            _logHelper = logHelper;
            _application = application;
        }

        public Task Execute(IJobExecutionContext context)
        {
            string jobName = ((Quartz.Impl.JobDetailImpl)context.JobDetail)?.Name;
            var watch = new Stopwatch();
            watch.Start();

            try
            {
                _application.PopulaFilaReclicagemEtiquetas();
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
