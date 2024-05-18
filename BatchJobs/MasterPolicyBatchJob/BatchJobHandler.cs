using BatchApplication.Core;

namespace MasterPolicyBatchJob;

public class BatchJobHandler : IBatchJob
{
    private readonly ICsvReader _csvReader;

    public string BatchId { get; set; } = "010";

    public BatchJobHandler(ICsvReader csvReader)
    {
        _csvReader = csvReader;
    }
    public void Execute()
    {
        
    }
}
