using BatchApplication.Core;

namespace MasterPolicyBatchJob;

public class MasterPolicyBatchJob : IBatchJob
{
    private readonly ICsvReaderService _csvReader;

    public string BatchId { get; set; } = "010";

    public MasterPolicyBatchJob()
    {
        
    }

    public MasterPolicyBatchJob(ICsvReaderService csvReader)
    {
        _csvReader = csvReader;
    }
    public void Execute()
    {
        
    }
}
