
using Application;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;

// var batchJobId = args[0];
var batchJobId = "010";

const int jobSuccessfull = 0;
const int jobFailed = 1;

if (batchJobId == null || string.IsNullOrWhiteSpace(batchJobId))
{
    Console.WriteLine("Invalid job id provided");
    Environment.Exit(jobFailed);
}

    var environment = "Development";
    var path = Path.Combine(Environment.CurrentDirectory, "Configuration");

    var builder = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile(Path.Combine(path, "appsettings.json"), optional: false, reloadOnChange: true)
    .AddJsonFile(Path.Combine(path, $"appsettings.{environment}.json"), optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

    var configuration = builder.Build();

    var host = ServiceContainer.ConfigureBatchJobServices(configuration);

    try
    {
        JobStarter.StartJob(batchJobId, host);
    }
    catch( ArgumentException ex) 
    {
        Console.WriteLine("ArgumentException occurred: " + ex.Message);
        throw;
    }
    catch(InvalidOperationException ex) 
    {
        Console.WriteLine("InvalidOperationException occurred: " + ex.Message);
        throw;
    }
    catch (Exception ex)
    {
        Console.WriteLine("An Unexpected error occurred:" + ex.Message);
        throw;
    }
    Console.ReadLine();