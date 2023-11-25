using AstronomyPictureOfTheDay.Entities;
using FakeItEasy;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace AstronomyPictureOfTheDay.Xunit.Tests
{
    public class MarsPictureTests
    {
        readonly string key = "testing";


        [Fact]
        public async Task TestHttpExceptionReturnFalse()
        {
            var rest = A.Fake<IRestServiceCaller>();
            A.CallTo(() => rest.GetAPODJsonAsync(key))
                               .ThrowsAsync(new HttpRequestException(key));

            NasaPictureOfTheDay nasa = new NasaPictureOfTheDay(rest);

            var results = await nasa.GetTodaysPictureAsync(key);


            Assert.False(results.Success);
            Assert.NotNull(results.exception);
        }

        [Fact]
        public async Task TestTaskExceptionReturnFalse()
        {
            var rest = A.Fake<IRestServiceCaller>();
            A.CallTo(() => rest.GetAPODJsonAsync(key))
                               .ThrowsAsync(new TaskCanceledException(key));

            NasaPictureOfTheDay nasa = new NasaPictureOfTheDay(rest);

            var results = await nasa.GetTodaysPictureAsync(key);


            Assert.False(results.Success);
            Assert.NotNull(results.exception);
        }

        [Fact]
        public async Task TestInvalidOperationExceptionReturnFalse()
        {
            var rest = A.Fake<IRestServiceCaller>();
            A.CallTo(() => rest.GetAPODJsonAsync(key))
                               .ThrowsAsync(new InvalidOperationException(key));

            NasaPictureOfTheDay nasa = new NasaPictureOfTheDay(rest);

            var results = await nasa.GetTodaysPictureAsync(key);


            Assert.False(results.Success);
            Assert.NotNull(results.exception);
        }

        [Fact]
        public async Task TestGetResults()
        {
            DateTime pictureDate = DateTime.Today;
            var rest = A.Fake<IRestServiceCaller>();
            A.CallTo(() => rest.GetMarsPictureJsonAsync("Curiosity", pictureDate, "DEMO_KEY"))
                               .Returns<Task<string>>
                               (Task.FromResult<string>(GetMarsPictureJson()));

            NasaPictureOfTheDay nasa = new NasaPictureOfTheDay(rest);

            MarsPictureResponse results = null;

            results = await nasa.GetMarsPictureAsync(RoverEnum.Curiosity, pictureDate, "DEMO_KEY"); ;

            Assert.True(results.Success);
            Assert.NotNull(results.picturesFromMars.photos);
        }

        private string GetMarsPictureJson()
        {
            string json = string.Empty;
            using (StreamReader srMars = new StreamReader("MarsPic.json"))
            {
                json = srMars.ReadToEnd();

            }
            return json;
        }
    }
}
