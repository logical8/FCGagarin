using System.Net;

namespace FCGagarin.PL.Admin.Jobs
{
    public interface ICheckSiteWorksJob
    {
        void Process();
    }

    public class CheckSiteWorksJob : ICheckSiteWorksJob
    {
        public void Process()
        {
            CheckSiteWorks("http://fcgagarin.ru");
        }

        public void CheckSiteWorks(string url)
        {
            WebClient client = new WebClient();
            client.DownloadString(url);
        }
    }
}