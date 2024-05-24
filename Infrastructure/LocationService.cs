
using Domain;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure;

public class LocationService : ILocationService
{
    private readonly BatchJobPaths? _batchJobsOptions;

    public LocationService(IOptions<EnviromentPaths> filePathOptions)
    {
        _batchJobsOptions = filePathOptions.Value.BatchJobs;
    }
    public string GetBatchJobFolder(params string[] paths)
    {
        var allPaths = new string[paths.Length + 1];

        if (string.IsNullOrWhiteSpace(_batchJobsOptions?.RootFolder))
        {
            throw new InvalidOperationException($"{nameof(_batchJobsOptions.RootFolder)} is not set. Please check the 'FilePaths:BatchJobs:RootFolder' configuration in appsettings.json.");
        }

        allPaths[0] = _batchJobsOptions.RootFolder;

        for (int i = 0; i < paths.Length; i++)
        {
            allPaths[i + 1] = paths[i];
        }

        return Path.Combine(allPaths);
    }
}

