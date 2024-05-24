using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Configuration
{
    public static class ServiceContainer
    {
        public static IHost ConfigureBatchJobServices(IConfigurationRoot configuration)
        {
            var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.Configure<EnviromentPaths>(configuration.GetSection("FilePaths"));
                services.AddTransient<ICsvReaderService, CsvReaderService>();
                services.AddTransient<IFileSystem, FileSystemReader>();
                services.AddTransient<ILocationService, LocationService>();
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