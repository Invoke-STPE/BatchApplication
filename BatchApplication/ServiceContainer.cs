using JobJuggler;
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
                services.RegisterBatchJobs();
            }).Build();

            return host;
        }
    }
}
