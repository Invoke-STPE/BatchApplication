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
            }).Build();

            return host;
        }
    }
}
