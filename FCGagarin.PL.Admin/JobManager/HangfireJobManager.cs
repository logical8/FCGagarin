using System;
using FCGagarin.PL.Admin.Jobs;
using Hangfire;

namespace FCGagarin.PL.Admin.JobManager
{
    public class HangfireJobManager : IJobManager
    {
        public void CheckSiteReccuring()
        {
            RecurringJob.AddOrUpdate<CheckSiteWorksJob>(x => x.Process(), Cron.MinuteInterval(new Random().Next(10)));
        }
    }
}