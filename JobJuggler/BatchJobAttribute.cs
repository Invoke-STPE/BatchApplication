namespace JobJuggler.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class BatchJobAttribute : Attribute
{
    public string BatchId { get; }

    public BatchJobAttribute(string batchId)
    {
        BatchId = batchId;
    }
}