using BatchApplication.Core;

namespace MasterPolicyBatchJob;

public class BatchJobHandler : IBatchJob
{
    public string BatchId { get; set; } = "010";

    public void Execute()
    {
        throw new NotImplementedException();
    }
}
