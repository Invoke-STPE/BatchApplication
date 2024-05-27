using Domain;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application
{
    public static class JobStarter
    {
        private static readonly Dictionary<string, Type> _jobs = [];

        static JobStarter()
        {
            Type[] batchJobsTypes = BatchJobAssemblyManager.GetLoadedBatchJobTypes();

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

            if (jobExists == false) { throw new ArgumentException($"Invalid job ID: {jobId}. Please ensure that the provided job ID corresponds to an existing batch job ID."); }
            
            var service = host.Services.GetRequiredService(jobType!) as IBatchJob;
            if (service is IBatchJob job)
            {
                job.Execute();
            }
            else
            {
                throw new InvalidOperationException($"The service of type {jobType} does not implement the IBatchJob interface and therefore does not provide the required 'Execute' method.");
            }
        }
    }
}