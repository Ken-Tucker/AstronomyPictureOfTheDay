﻿using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AstronomyPictureOfTheDay
{
    public class RestServiceCaller : IRestServiceCaller
    {
        private readonly HttpClient client;

        public RestServiceCaller()
        {
            client = new HttpClient();
        }

        public RestServiceCaller(HttpClient httpClient)
        {
            client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<string> GetAPODJsonAsync(string apiKey)
        {
            string url = $"https://api.nasa.gov/planetary/apod?api_key={apiKey}";
            string json = await client.GetStringAsync(url);
            return json;
        }

        public async Task<string> GetMarsPictureJsonAsync(string rover, DateTime earthDate, string apiKey)
        {
            string url = $"https://api.nasa.gov/mars-photos/api/v1/rovers/{rover}/photos?earth_date={earthDate:yyyy-MM-dd}&api_key={apiKey}";
            string json = await client.GetStringAsync(url);
            return json;
        }
    }
}
