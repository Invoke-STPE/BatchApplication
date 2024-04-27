using System.Reflection;
using _003_AccountOpportunityDailyDataHandlingBatchJob.BatchJobs;
using _027_BookingDateBatchJob.Domain.BatchJobInterfaces;
using BatchApplication.Domain;
using BatchApplication.Domain.BatchJobInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BatchApplication.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        // public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        // {
        //     services.AddTransient<IBookingDateBatchJob, BookingDateBatchJob>();
        //     services.AddTransient<IAccountOpportunityDailyDataHandlingBatchJob, AccountOpportunityDailyDataHandlingBatchJob>();

        //     return services;
        // }
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            var batchJobAttribute = typeof(BatchJobAttribute);
            var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(p =>  p.IsDefined(batchJobAttribute, false) && !p.IsInterface)
            .Select(s => new { Service = s.GetInterfaces().First(), Implementation = s })
            .Where(w => w.Service != null);

            System.Console.WriteLine("test");
            return services;
        }
        
        
    }
}


