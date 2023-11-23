using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AstronomyPictureOfTheDay.Sample.Avalonia.Converters
{
    /// <summary>
    /// <para>
    /// Converts a string path to a bitmap asset.
    /// </para>
    /// </summary>
    public class BitmapConverter : IValueConverter
    {
        public static BitmapConverter Instance = new BitmapConverter();

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return null;

            if (value is string rawUri && targetType.IsAssignableFrom(typeof(Bitmap)))
            {
                Bitmap? spaceImage = null;
                var getImageTask = Task.Run(async () =>
                {
                    using (HttpClient client = new HttpClient())
                    {
                        using (HttpResponseMessage response = await client.GetAsync(rawUri))
                        using (Stream stream = await response.Content.ReadAsStreamAsync())
                        {
                            spaceImage = new Bitmap(stream);
                        }
                    }
                });
                getImageTask.Wait();
                return spaceImage;
            }

            throw new NotSupportedException();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
