using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Simpl;
using Quartz.Spi;

namespace Fretter.Service.Infra.Host
{
    public class JobFactory : IJobFactory
    {
        protected readonly IServiceProvider _serviceProvider;
        private IServiceScope _scope;
        protected readonly ConcurrentDictionary<IJob, IServiceScope> _scopes = new ConcurrentDictionary<IJob, IServiceScope>();

        public JobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            //return Container.GetService(bundle.JobDetail.JobType) as IJob;
            //_scope = Container.CreateScope();
            //return _scope.ServiceProvider.GetService(bundle.JobDetail.JobType) as IJob;

            var scope = _serviceProvider.CreateScope();
            IJob job;

            try
            {
                job = scope.ServiceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message} Trace: {ex.StackTrace}");
                scope.Dispose();
                throw ex;
            }

            if (!_scopes.TryAdd(job, scope))
            {
                scope.Dispose();
                Console.WriteLine($"Failed to track DI scope");
                throw new Exception("Failed to track DI scope");
            }

            return job;
        }

        public void ReturnJob(IJob job)
        {
            (job as IDisposable)?.Dispose();

            if (_scopes.TryRemove(job, out var scope))
            {
                scope.Dispose();
            }
        }

    }
}
