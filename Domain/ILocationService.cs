﻿namespace Domain;

public interface ILocationService
{
  public string GetBatchJobFolder(params string[] paths);
}