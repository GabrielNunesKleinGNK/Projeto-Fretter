using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Quartz.Impl;
using System.Threading.Tasks;
using Fretter.Service.Infra.Host;

namespace Fretter.Service
{
    public static class Program
    {
        static IServiceCollection services = new ServiceCollection();

        public static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();
            var scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
            var service = new ServiceConfiguration(config, scheduler);
            service.Start();
            Console.WriteLine("Fretter.Background Iniciado. Pressione Ctrl+C para finalizar.");
            Console.ReadLine();
        }
    }
}
