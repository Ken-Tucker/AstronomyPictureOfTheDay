using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace AstronomyPictureOfTheDay.Sample.WPFCore.ViewModels
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive
    {
        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        public ShellViewModel()
        {
            Message = "Hello World";

            Task.Run(async () => { await ActivateItemAsync(new MainViewModel()); });
            Task.WaitAll();
        }

        public async void ShowAstronomyPicture()
        {
           await ChangeActiveItemAsync(new MainViewModel(), true);
        }

        public async void ShowMarsPicture()
        {
            await ChangeActiveItemAsync(new MarsViewModel(), true);
        }
    }
}
