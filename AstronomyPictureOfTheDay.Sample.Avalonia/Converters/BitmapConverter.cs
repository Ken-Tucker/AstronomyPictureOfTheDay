using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;

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

                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = client.GetAsync(rawUri).GetAwaiter().GetResult())
                    using (Stream stream = response.Content.ReadAsStreamAsync().GetAwaiter().GetResult())
                    {
                        Bitmap? spaceImage = new Bitmap(stream);

                        return spaceImage;
                    }
                }
            }

            throw new NotSupportedException();
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
