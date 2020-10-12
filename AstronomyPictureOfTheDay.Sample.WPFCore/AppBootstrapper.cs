using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using AstronomyPictureOfTheDay.Sample.WPFCore.ViewModels;

namespace AstronomyPictureOfTheDay.Sample.WPFCore
{
    public class AppBootstrapper : BootstrapperBase
    {
        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
