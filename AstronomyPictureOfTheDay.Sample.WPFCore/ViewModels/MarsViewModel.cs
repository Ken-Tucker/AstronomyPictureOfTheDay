using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AstronomyPictureOfTheDay.Entities;
using Caliburn.Micro;

namespace AstronomyPictureOfTheDay.Sample.WPFCore.ViewModels
{
    public class MarsViewModel:PropertyChangedBase
    {
        private string _title = "Loading image";

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                NotifyOfPropertyChange();
            }
        }

        private string _picOfDay;

        public string PictureOfDay
        {
            get
            {
                return _picOfDay;
            }
            set
            {
                _picOfDay = value;
                NotifyOfPropertyChange();
            }
        }

        public MarsViewModel()
        {
            var marsPictureOfTheDay = new NasaPictureOfTheDay();
            MarsPictureResponse response = null;
            Task.Run(async () =>
            {
                response = await marsPictureOfTheDay.GetMarsPictureAsync(RoverEnum.Curiosity, DateTime.Now.AddDays(-7),"DEMO_KEY");
                if (response != null)
                {
                    if (response.Success)
                    {
                        Title = response.picturesFromMars.photos[0].camera.full_name;
                        PictureOfDay = response.picturesFromMars.photos[0].img_src;
                    }
                }

            });
            Task.WaitAll();

        }

    }
}
