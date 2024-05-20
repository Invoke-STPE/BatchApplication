using BatchApplication.Core;
using Microsoft.Extensions.DependencyInjection;
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
                services.AddTransient<ICsvReaderService, CsvReaderService>();
                services.AddTransient<IFileSystem, FileSystemReader>();
                services.AddBatchJobs();
            }).Build();

            return host;
        }

        public static IServiceCollection AddBatchJobs(this IServiceCollection services)
        {
            Type[] batchJobsTypes = BatchJobAssemblyManager.GetBatchJobTypes();

            foreach (var batchJob in batchJobsTypes)
            {
                services.AddTransient(batchJob);
            }

            return services;
        }
    }
}
