using System;
using System.Threading.Tasks;

namespace AstronomyPictureOfTheDay
{
    public interface IRestServiceCaller
    {
        Task<string> GetAPODJsonAsync(string apiKey);

        Task<string> GetMarsPictureJsonAsync(string rover, DateTime earthDate, string apiKey);

        Task<string> GetAPODByDateJsonAsync(DateTime pictureDate, string apiKey);
    }
}