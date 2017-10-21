using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FCGagarin.BLL.Services.Interfaces;
using FCGagarin.DAL.DTO;
using FCGagarin.DAL.Entities;
using Newtonsoft.Json;

namespace FCGagarin.BLL.Services
{
    public class ImportService : IImportService
    {
        readonly HttpClient client = new HttpClient();
        private readonly IRoundService _roundService;

        public ImportService(IRoundService roundService)
        {
            _roundService = roundService;
        }

        public void ImportRounds(int clubId, string season)
        {
            try
            {
                Run(clubId, season);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void Run(int clubId, string season)
        {
            var path = $"http://api.fcgagarin.ru/rounds/{clubId}/{season}";
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

            var rounds = Task.Run(() => GetRoundsAsync(path)).Result;
            _roundService.SaveOrUpdate(rounds);

        }

        async Task<IEnumerable<RoundDTO>> GetRoundsAsync(string path)
        {
            IEnumerable<RoundDTO> rounds = null;
            HttpResponseMessage response = await client.GetAsync(new Uri(path));
            if (response.IsSuccessStatusCode)
            {
                rounds = await response.Content.ReasAsJsonAsync<IEnumerable<RoundDTO>>();
            }
            return rounds;
        }
    }

    public static class HttpContentExtensions
    {
        public static async Task<T> ReasAsJsonAsync<T>(this HttpContent content)
        {
            string json = await content.ReadAsStringAsync();
            T value = JsonConvert.DeserializeObject<T>(json);
            return value;
        }
    }
}