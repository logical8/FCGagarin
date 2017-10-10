using System.Net;

namespace FCGagarin.PL.Admin.Jobs
{
    public interface ICheckSiteWorksJob
    {
        void Process(string url);
    }

    public class CheckSiteWorksJob : ICheckSiteWorksJob
    {
        public void Process(string url)
        {
            CheckSiteWorks(url);
        }

        public void CheckSiteWorks(string url)
        {
            WebClient client = new WebClient();
            client.DownloadString(url);
        }
    }
}