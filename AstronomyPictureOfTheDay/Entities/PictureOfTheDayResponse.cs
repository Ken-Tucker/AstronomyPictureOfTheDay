using System;
namespace AstronomyPictureOfTheDay.Entities
{
    public class PictureOfTheDayResponse
    {
        public PictureOfTheDayResponse()
        {
        }

        public bool Success { get; set; }

        public Exception exception { get; set; }

        public PictureOfTheDay pictureOfTheDay { get; set; }
        public bool CanRetry { get; internal set; }
    }
}
