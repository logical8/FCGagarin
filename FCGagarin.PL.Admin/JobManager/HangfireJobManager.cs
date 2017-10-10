using FCGagarin.PL.Admin.Jobs;
using Hangfire;

namespace FCGagarin.PL.Admin.JobManager
{
    public class HangfireJobManager : IJobManager
    {
        public void CheckSiteReccuring(string url)
        {
            RecurringJob.AddOrUpdate<CheckSiteWorksJob>(x => x.Process(url), Cron.Minutely);
        }
    }
}