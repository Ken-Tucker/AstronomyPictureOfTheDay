﻿using AstronomyPictureOfTheDay.Entities;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace AstronomyPictureOfTheDay.Sample.Avalonia.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly INasaPictureOfTheDay _apod;
        private MarsPictureResponse _marsPictureResponse;
        private readonly DispatcherTimer _dispatcherTimer = new DispatcherTimer();
        public MainWindowViewModel(INasaPictureOfTheDay nasaPictureOfTheDay)
        {
            _apod = nasaPictureOfTheDay;
            LoadMarsPictureCommand = new AsyncRelayCommand(LoadMarsPicture);
            LoadPictureCommand = new AsyncRelayCommand(LoadPicture);
            _picOfDay = "";
            _marsPictureResponse = new MarsPictureResponse();
            var getPictureTask = Task.Run(async () => await GetPictureOfTheDay());
            getPictureTask.Wait();
            _dispatcherTimer.Interval = TimeSpan.FromSeconds(3);
            _dispatcherTimer.Tick += (sender, e) => CameraIndex++;
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
            _dispatcherTimer.Stop();
            PictureOfTheDayResponse response = await _apod.GetPictureByDateAsync(DateTime.Today, "DEMO_KEY");
            bool result = response?.Success == true;
            if (response != null && response.Success)
            {

                Title = response.pictureOfTheDay.title;
                PictureOfDay = response.pictureOfTheDay.url;
            }
            else
            {
                Title = "Failed to get picture of the day";
            }

            return result;
        }

        private int _cameraIndex = 0;
        public int CameraIndex
        {
            get
            {
                return _cameraIndex;
            }
            set
            {
                if (_marsPictureResponse.picturesFromMars.photos.Length > 0)
                {
                    _cameraIndex = value >= _marsPictureResponse.picturesFromMars.photos.Length ? 0 : value;

                    Title = $"{_marsPictureResponse.picturesFromMars.photos[_cameraIndex].camera.full_name} pic {value + 1} of {_marsPictureResponse.picturesFromMars.photos.Length}";
                    PictureOfDay = _marsPictureResponse.picturesFromMars.photos[_cameraIndex].img_src;

                    OnPropertyChanged();
                }
            }
        }
        public async Task<bool> GetMarsPictureOfTheDay()
        {
            bool result = false;
            MarsPictureResponse response = await _apod.GetMarsPictureAsync(RoverEnum.Perseverance, DateTime.Now.AddDays(-30), "DEMO_KEY");
            if (response != null && response.Success && response.picturesFromMars.photos != null && response.picturesFromMars.photos.Length > 0)
            {
                _marsPictureResponse = response;
                CameraIndex = 0;
                result = true;
                _dispatcherTimer.Start();
            }
            else
            {
                Title = "Failed to get mars picture of the day";
            }

            return result;
        }


    }
}
