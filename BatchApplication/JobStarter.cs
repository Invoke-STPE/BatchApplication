using BatchApplication.Domain.BatchJobInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BatchApplication
{
    public static class JobStarter
    {
        private static readonly Dictionary<string, Type> _jobs = [];

        /// <summary>
        /// Populates the _jobs dictionary with batch job IDs and their corresponding interface types.
        /// </summary>
        static JobStarter()
        {
            var batchJobType = typeof(IBatchJob);
            var batchJobTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => batchJobType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToList();

            foreach (var type in batchJobTypes)
            {
                var interfaces = type.GetInterfaces().Where(i => i != typeof(IBatchJob));
                foreach (var @interface in interfaces)
                {
                    if (Activator.CreateInstance(type) is IBatchJob job)
                    {
                        _jobs.Add(job.BatchId, @interface);
                    }
                }
            }
        }

        public static void StartJob(string jobId, IHost host)
        {
            var jobExists = _jobs.TryGetValue(jobId, out Type? jobType);

            if (jobExists == false) { throw new ArgumentException($"Invalid job id: {jobId}"); }
            
            var service = host.Services.GetRequiredService(jobType!) as IBatchJob;
            if (service is IBatchJob job)
            {
                job.Start();
            }
            else
            {
                throw new InvalidOperationException($"Service of type {jobType} does not implement IBatchJob");
            }
        }
    }
}
