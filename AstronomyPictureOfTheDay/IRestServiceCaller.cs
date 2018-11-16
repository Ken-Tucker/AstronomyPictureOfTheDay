using System.Threading.Tasks;

namespace AstronomyPictureOfTheDay
{
    public interface IRestServiceCaller
    {
        Task<string> GetAPODJson(string apiKey);
    }
}