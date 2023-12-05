using AstronomyPictureOfTheDay.Entities;
using System;
using System.Threading.Tasks;

namespace AstronomyPictureOfTheDay
{
    public interface INasaPictureOfTheDay
    {
        Task<MarsPictureResponse> GetMarsPictureAsync(RoverEnum rover, DateTime earthDate, string apiKey);
        Task<PictureOfTheDayResponse> GetTodaysPictureAsync(string apiKey);
    }
}