﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using HtmlAgilityPack;

namespace Parser.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public List<Round> Get(int club, int season)
        {
            var url = $"http://lfl.ru/moscow8x8/calendar?club_id={club}&matches=all&sort=timeasc&season_id={season}";
            var web = new WebClient();
            var str = web.DownloadString(url);
            //doc.DocumentNode.SelectNodes("//table[@class='round_table calendar calendar_result  league_tournament_calendar_table']").Select(d=>d.).ToArray(); //new string[] { "value1", "value2" };

            //var query = from table in doc.DocumentNode.SelectNodes("//table[@class='round_table calendar calendar_result  league_tournament_calendar_table']")
            //    from row in table.SelectNodes("tr")
            //    from cell in row.SelectNodes("th|td")
            //    select new TestClass { Row = row?.InnerText ?? "", CellText = cell?.InnerText ?? ""};

            var doc = new HtmlDocument();
            doc.LoadHtml(str);

            var query =
                from table in doc.DocumentNode.SelectNodes(
                    "//table[@class='round_table calendar calendar_result  league_tournament_calendar_table']/tbody")
                from row in table.SelectNodes("tr")
                from cell in row.SelectNodes("th|td")
                select new TestClass { Row = row?.InnerText ?? "", CellText = cell?.InnerText.Trim() ?? "" };

            var i = 0;
            var result = new List<Round>();
            var round = new Round();
            foreach (var testClass in query)
            {
                switch (i)
                {
                    case 0:
                        round = new Round();
                        round.Number = testClass.CellText;
                        break;
                    case 1:
                        round.Date = testClass.CellText;
                        break;
                    case 2:
                        round.Time = testClass.CellText;
                        break;
                    case 3:
                        round.Home = testClass.CellText;
                        break;
                    case 4:
                        round.Score = testClass.CellText;
                        break;
                    case 5:
                        round.Guest = testClass.CellText;
                        break;
                    case 6:
                        round.Arena = testClass.CellText;
                        break;
                    case 7:
                        round.Tournament = testClass.CellText;
                        result.Add(round);
                        i = -1;
                        break;
                }
                i++;
            }
            return result;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

    
}
