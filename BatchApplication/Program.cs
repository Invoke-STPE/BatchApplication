
using BatchApplication;

var batchJobId = args[0];

const int jobSuccessfull = 0;
const int jobFailed = 1;

if (batchJobId == null || string.IsNullOrWhiteSpace(batchJobId))
{
    Console.WriteLine("Invalid job id provided");
    Environment.Exit(jobFailed);
}

try
{
    var host = ServiceContainer.ConfigureBatchJobServices();
    JobStarter.StartJob(batchJobId, host);
    Console.ReadLine();
}
catch (Exception)
{

	throw;
}