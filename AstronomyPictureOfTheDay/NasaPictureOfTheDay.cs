using System;
using System.Threading.Tasks;
using AstronomyPictureOfTheDay.Entities;

namespace AstronomyPictureOfTheDay
{
   public class NasaPictureOfTheDay
    {
        IRestServiceCaller restServiceCaller = new RestServiceCaller();

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
                response.pictureOfTheDay = Newtonsoft.Json.JsonConvert.DeserializeObject<PictureOfTheDay>(json);
                response.Success = true;
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.exception = ex;
            }

            return response;
        }
    }
}
