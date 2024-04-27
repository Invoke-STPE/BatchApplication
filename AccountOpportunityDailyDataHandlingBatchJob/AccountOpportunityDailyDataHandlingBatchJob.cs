using BatchApplication.Domain;
using BatchApplication.Domain.BatchJobInterfaces;

namespace _003_AccountOpportunityDailyDataHandlingBatchJob.BatchJobs;
public class AccountOpportunityDailyDataHandlingBatchJob : IAccountOpportunityDailyDataHandlingBatchJob
{
    public string BatchId => BatchJobIds.AccountOpportunityDailyDataHandling;
    public void Start()
    {

    }
}
