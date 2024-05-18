using BatchApplication.Core;
namespace OpportunityDailyDataHandler;

public class BatchJobHandler : IBatchJob
{
    public string BatchId { get; set; } = "027";

    public void Execute()
    {
        throw new NotImplementedException();
    }
}
