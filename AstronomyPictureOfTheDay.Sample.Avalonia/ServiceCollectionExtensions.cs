using AstronomyPictureOfTheDay.Sample.Avalonia.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AstronomyPictureOfTheDay.Sample.Avalonia
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCommonServices(this IServiceCollection collection)
        {
            collection.AddTransient<MainWindowViewModel>();
            collection.AddTransient<IRestServiceCaller, RestServiceCaller>();
            collection.AddTransient<INasaPictureOfTheDay, NasaPictureOfTheDay>();
            collection.AddHttpClient(
                NasaPictureOfTheDay.HttpClientFactoryName,
                client =>
                {
                    // Set the base address of the named client.
                    client.BaseAddress = new Uri(NasaPictureOfTheDay.HttpClientFactoryUrl);
                });
        }
    }
}
