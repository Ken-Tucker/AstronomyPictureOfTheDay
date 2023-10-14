using AstronomyPictureOfTheDay.Entities;
using Caliburn.Micro;
using System.Threading.Tasks;

namespace AstronomyPictureOfTheDay.Sample.WPFCore.ViewModels
{
    public class MainViewModel : PropertyChangedBase
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


        public MainViewModel()
        {
            var apod = new NasaPictureOfTheDay();
            PictureOfTheDayResponse response = null;
            Task.Run(async () =>
            {
                response = await apod.GetTodaysPictureAsync("DEMO_KEY");
                if (response != null && response.Success)
                {

                    Title = response.pictureOfTheDay.title;
                    PictureOfDay = response.pictureOfTheDay.url;
                }

            });
            Task.WaitAll();

        }
    }
}
