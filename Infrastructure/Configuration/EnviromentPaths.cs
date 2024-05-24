namespace Infrastructure.Configuration;

public class EnviromentPaths
{
  public BatchJobPaths? BatchJobs { get; set; }
}

public class BatchJobPaths
{
  public string? RootFolder { get; set; }
}