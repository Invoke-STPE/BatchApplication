using BatchApplication.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BatchApplication
{
    public static class JobStarter
    {
        private static readonly Dictionary<string, Type> _jobs = [];

        static JobStarter()
        {
            Type[] batchJobsTypes = BatchJobAssemblyManager.GetBatchJobTypes();

            foreach (var type in batchJobsTypes)
            {
                if (Activator.CreateInstance(type) is IBatchJob batchJob)
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
