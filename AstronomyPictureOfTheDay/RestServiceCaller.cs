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

        public async Task<string> GetAPODJson(string apiKey)
        {
            string url = $"https://api.nasa.gov/planetary/apod?api_key={apiKey}";
            string json = string.Empty;

            json = await client.GetStringAsync(url);
            return json;
        }
    }
}
