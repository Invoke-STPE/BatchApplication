namespace BatchApplication.Domain.BatchJobInterfaces
{
    public interface IBatchJob
    {
        string BatchId { get; }
        void Start();
    }
}