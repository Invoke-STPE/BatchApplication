using JobJuggler;
using JobJuggler.Attributes;
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
            var batchJobAttribute = typeof(BatchJobAttribute);
            var batchJobTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsDefined(batchJobAttribute, false) && !t.IsInterface)
                .ToList();

            foreach (var type in batchJobTypes)
            {
                if (Activator.CreateInstance(type) is BatchJobAttribute batchJob)
                {
                    _jobs.Add(batchJob.BatchId, type);
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
                job.Execute();
            }
            else
            {
                throw new InvalidOperationException($"Service of type {jobType} does not implement IBatchJob");
            }
        }
    }
}
