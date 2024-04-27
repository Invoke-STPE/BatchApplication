using System.Reflection;
using BatchApplication.Domain;
using BatchApplication.Domain.BatchJobInterfaces;
using BatchApplication.Infrastructure;
using Microsoft.Extensions.Hosting;

namespace BatchApplication
{
    public static class ServiceContainer
    {
        public static IHost ConfigureBatchJobServices()
        {
            

            var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddInfrastructureServices();
            }).Build();

            return host;
        }
    }
}
