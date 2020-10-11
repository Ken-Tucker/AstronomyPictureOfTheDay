using System;
using System.Collections.Generic;
using System.Text;

namespace AstronomyPictureOfTheDay.Entities
{
    public class MarsPictureResponse
    {
        public MarsPictureResponse()
        {
        }

        public bool Success { get; set; }

        public Exception exception { get; set; }

        public MarsPictures picturesFromMars { get; set; }
    }
}
