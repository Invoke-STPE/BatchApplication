﻿namespace Domain;

public interface IBatchJob
{
    public string BatchId { get; set; }
    public string BatchName { get; set; }
    public void Execute();
}