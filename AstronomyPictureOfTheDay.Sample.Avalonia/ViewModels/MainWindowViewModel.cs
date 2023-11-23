using AstronomyPictureOfTheDay.Entities;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace AstronomyPictureOfTheDay.Sample.Avalonia.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            LoadMarsPictureCommand = new AsyncRelayCommand(LoadMarsPicture);
            LoadPictureCommand = new AsyncRelayCommand(LoadPicture);
            _picOfDay = "";
            var getPictureTask = Task.Run(async () => await GetPictureOfTheDay());
            getPictureTask.Wait();
        }


        private const string LoadImage = "Loading image";
        public IAsyncRelayCommand LoadMarsPictureCommand { get; }

        private async Task<bool> LoadMarsPicture()
        {
            Title = LoadImage;
            await GetMarsPictureOfTheDay();
            return true;
        }

        public IAsyncRelayCommand LoadPictureCommand { get; }

        private async Task<bool> LoadPicture()
        {
            Title = LoadImage;
            await GetPictureOfTheDay();
            return true;
        }

        private string _title = LoadImage;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged();
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
                OnPropertyChanged();
            }
        }

        public async Task<bool> GetPictureOfTheDay()
        {
            var apod = new NasaPictureOfTheDay();
            PictureOfTheDayResponse response = null;
            response = await apod.GetTodaysPictureAsync("DEMO_KEY");
            if (response != null && response.Success)
            {

                Title = response.pictureOfTheDay.title;
                PictureOfDay = response.pictureOfTheDay.url;
            }

            return true;
        }

        public async Task<bool> GetMarsPictureOfTheDay()
        {
            var marsPictureOfTheDay = new NasaPictureOfTheDay();
            MarsPictureResponse response = null;

            response = await marsPictureOfTheDay.GetMarsPictureAsync(RoverEnum.Curiosity, DateTime.Now.AddDays(-30), "DEMO_KEY");
            if (response != null && response.Success)
            {
                Title = response.picturesFromMars.photos[0].camera.full_name;
                PictureOfDay = response.picturesFromMars.photos[0].img_src;
            }

            return true;
        }


    }
}
