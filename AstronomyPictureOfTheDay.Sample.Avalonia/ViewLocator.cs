using AstronomyPictureOfTheDay.Sample.Avalonia.ViewModels;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using System;

namespace AstronomyPictureOfTheDay.Sample.Avalonia
{
    public class ViewLocator : IDataTemplate
    {
        public Control Build(object? data)
        {
            if (data != null)
            {
                var name = data.GetType().FullName!.Replace("ViewModel", "View");
                var type = Type.GetType(name);

                if (type != null)
                {
                    return (Control)Activator.CreateInstance(type)!;
                }
            }
            return new TextBlock { Text = $"Not Found: {data}" };
        }

        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }
    }
}