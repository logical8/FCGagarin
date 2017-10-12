using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using HtmlAgilityPack;

namespace Parser.Controllers
{
    public class RoundsController : ApiController
    {
        [Route("Rounds/{club}/{season}", Name = "ClubAndSeason")]
        public List<Round> Get(int club, int season)
        {
            var url = $"http://lfl.ru/moscow8x8/calendar?club_id={club}&matches=all&sort=timeasc&season_id={season}";
            var web = new WebClient();
            var str = web.DownloadString(url);

            var doc = new HtmlDocument();
            doc.LoadHtml(str);

            var query =
                from table in doc.DocumentNode.SelectNodes(
                    "//table[@class='round_table calendar calendar_result  league_tournament_calendar_table']/tbody")
                from row in table.SelectNodes("tr")
                from cell in row.SelectNodes("th|td")
                select new { CellText = cell?.InnerText.Trim() ?? "" };

            var i = 0;
            var result = new List<Round>();
            var round = new Round();
            foreach (var cell in query)
            {
                switch (i)
                {
                    case 0:
                        round = new Round();
                        round.Number = cell.CellText;
                        break;
                    case 1:
                        round.Date = cell.CellText;
                        break;
                    case 2:
                        round.Time = cell.CellText;
                        break;
                    case 3:
                        round.Home = cell.CellText;
                        break;
                    case 4:
                        round.Score = cell.CellText;
                        break;
                    case 5:
                        round.Guest = cell.CellText;
                        break;
                    case 6:
                        round.Arena = cell.CellText;
                        break;
                    case 7:
                        round.Tournament = cell.CellText;
                        result.Add(round);
                        i = -1;
                        break;
                }
                i++;
            }
            return result;
        }

        // GET: api/Rounds/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Rounds
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Rounds/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Rounds/5
        public void Delete(int id)
        {
        }
    }

    internal class TestClass
    {
        public string Row { get; set; }
        public string CellText { get; set; }
    }


    public class Round
    {
        public string Number { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Home { get; set; }
        public string Score { get; set; }
        public string Guest { get; set; }
        public string Arena { get; set; }
        public string Tournament { get; set; }
    }
}
