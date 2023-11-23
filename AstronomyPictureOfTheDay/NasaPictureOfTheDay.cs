using AstronomyPictureOfTheDay.Entities;
using System;
using System.Threading.Tasks;

namespace AstronomyPictureOfTheDay
{
    public class NasaPictureOfTheDay
    {
        readonly IRestServiceCaller restServiceCaller = new RestServiceCaller();

        public NasaPictureOfTheDay()
        {
        }

        public NasaPictureOfTheDay(IRestServiceCaller restService)
        {
            restServiceCaller = restService;
        }

        public async Task<PictureOfTheDayResponse> GetTodaysPictureAsync(string apiKey)
        {
            PictureOfTheDayResponse response = new PictureOfTheDayResponse();
            try
            {
                string json = await restServiceCaller.GetAPODJsonAsync(apiKey);
#if NETSTANDARD2_0
                response.pictureOfTheDay = Newtonsoft.Json.JsonConvert.DeserializeObject<PictureOfTheDay>(json);
#else
                response.pictureOfTheDay = System.Text.Json.JsonSerializer.Deserialize<PictureOfTheDay>(json);
#endif
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.exception = ex;
            }

            return response;
        }

        public async Task<MarsPictureResponse> GetMarsPictureAsync(RoverEnum rover, DateTime earthDate, string apiKey)
        {
            MarsPictureResponse response = new MarsPictureResponse();
            try
            {
                string json = await restServiceCaller.GetMarsPictureJsonAsync(rover.ToString(), earthDate, apiKey);
#if NETSTANDARD2_0
                response.picturesFromMars = Newtonsoft.Json.JsonConvert.DeserializeObject<MarsPictures>(json);
#else
                response.picturesFromMars = System.Text.Json.JsonSerializer.Deserialize<MarsPictures>(json);
#endif
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.exception = ex;
            }

            return response;
        }
    }
}
