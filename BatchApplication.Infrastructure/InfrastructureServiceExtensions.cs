using _003_AccountOpportunityDailyDataHandlingBatchJob.BatchJobs;
using _027_BookingDateBatchJob.Domain.BatchJobInterfaces;
using BatchApplication.Domain.BatchJobInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BatchApplication.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<IBookingDateBatchJob, BookingDateBatchJob>();
            services.AddTransient<IAccountOpportunityDailyDataHandlingBatchJob, AccountOpportunityDailyDataHandlingBatchJob>();

            return services;
        }
    }
}
