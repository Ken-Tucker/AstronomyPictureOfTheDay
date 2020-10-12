using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AstronomyPictureOfTheDay
{
    public class RestServiceCaller : IRestServiceCaller
    {
        public RestServiceCaller()
        {
        }

        private static HttpClient client = new HttpClient();

        public async Task<string> GetAPODJsonAsync(string apiKey)
        {
            string url = $"https://api.nasa.gov/planetary/apod?api_key={apiKey}";
            string json = string.Empty;

            json = await client.GetStringAsync(url);
            return json;
        }

        public async Task<string> GetMarsPictureJsonAsync(string rover, DateTime earthDate, string apiKey)
        {
            string url = $"https://api.nasa.gov/mars-photos/api/v1/rovers/{rover}/photos?earth_date={earthDate.ToString("yyyy-MM-dd")}&api_key={apiKey}";
            string json = string.Empty;

            json = await client.GetStringAsync(url);
            return json;
        }
    }
}
