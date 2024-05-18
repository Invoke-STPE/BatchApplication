namespace BatchApplication.Core;

public interface IBatchJob
{
    public string BatchId { get; set; }
    public void Execute();
}
