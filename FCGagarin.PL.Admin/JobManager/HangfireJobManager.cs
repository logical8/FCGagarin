using System;
using FCGagarin.PL.Admin.Jobs;
using Hangfire;

namespace FCGagarin.PL.Admin.JobManager
{
    public class HangfireJobManager : IJobManager
    {
        public void ImportRoundsRecurring()
        {
            RecurringJob.AddOrUpdate<ImportRoundsJob>(x=>x.Process(), Cron.Hourly);
        }

        public void CheckSiteRecurring()
        {
            RecurringJob.AddOrUpdate<CheckSiteWorksJob>(x => x.Process(), Cron.Minutely);
        }
    }
}