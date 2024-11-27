using AstronomyPictureOfTheDay.Entities;
using global::Xunit;
using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AstronomyPictureOfTheDay.Xunit.Tests
{


    public class NasaPictureOfTheDayTests
    {
        private readonly Mock<IRestServiceCaller> mockRestServiceCaller;
        private readonly NasaPictureOfTheDay nasaPictureOfTheDay;

        public NasaPictureOfTheDayTests()
        {
            mockRestServiceCaller = new Mock<IRestServiceCaller>();
            nasaPictureOfTheDay = new NasaPictureOfTheDay(mockRestServiceCaller.Object);
        }

        [Fact]
        public async Task GetTodaysPictureAsync_ReturnsSuccessResponse()
        {
            // Arrange
            string apiKey = "test_api_key";
            string jsonResponse = "{\"title\":\"Test Title\"}";
            mockRestServiceCaller.Setup(x => x.GetAPODJsonAsync(apiKey)).ReturnsAsync(jsonResponse);

            // Act
            var response = await nasaPictureOfTheDay.GetTodaysPictureAsync(apiKey);

            // Assert
            Assert.True(response.Success);
            Assert.NotNull(response.pictureOfTheDay);
            Assert.Equal("Test Title", response.pictureOfTheDay.title);
        }

        [Fact]
        public async Task GetTodaysPictureAsync_ReturnsHttpRequestException()
        {
            // Arrange
            string apiKey = "test_api_key";
            mockRestServiceCaller.Setup(x => x.GetAPODJsonAsync(apiKey)).ThrowsAsync(new HttpRequestException());

            // Act
            var response = await nasaPictureOfTheDay.GetTodaysPictureAsync(apiKey);

            // Assert
            Assert.False(response.Success);
            Assert.IsType<HttpRequestException>(response.exception);
            Assert.True(response.CanRetry);
        }

        [Fact]
        public async Task GetTodaysPictureAsync_ReturnsTaskCanceledException()
        {
            // Arrange
            string apiKey = "test_api_key";
            mockRestServiceCaller.Setup(x => x.GetAPODJsonAsync(apiKey)).ThrowsAsync(new TaskCanceledException());

            // Act
            var response = await nasaPictureOfTheDay.GetTodaysPictureAsync(apiKey);

            // Assert
            Assert.False(response.Success);
            Assert.IsType<TaskCanceledException>(response.exception);
            Assert.True(response.CanRetry);
        }

        [Fact]
        public async Task GetTodaysPictureAsync_ReturnsInvalidOperationException()
        {
            // Arrange
            string apiKey = "test_api_key";
            mockRestServiceCaller.Setup(x => x.GetAPODJsonAsync(apiKey)).ThrowsAsync(new InvalidOperationException());

            // Act
            var response = await nasaPictureOfTheDay.GetTodaysPictureAsync(apiKey);

            // Assert
            Assert.False(response.Success);
            Assert.IsType<InvalidOperationException>(response.exception);
            Assert.False(response.CanRetry);
        }

        [Fact]
        public async Task GetMarsPictureAsync_ReturnsSuccessResponse()
        {
            // Arrange
            RoverEnum rover = RoverEnum.Curiosity;
            DateTime earthDate = DateTime.Now;
            string apiKey = "test_api_key";
            string jsonResponse = "{\"photos\":[]}";
            mockRestServiceCaller.Setup(x => x.GetMarsPictureJsonAsync(rover.ToString(), earthDate, apiKey)).ReturnsAsync(jsonResponse);

            // Act
            var response = await nasaPictureOfTheDay.GetMarsPictureAsync(rover, earthDate, apiKey);

            // Assert
            Assert.True(response.Success);
            Assert.NotNull(response.picturesFromMars);
            Assert.Empty(response.picturesFromMars.photos);
        }

        [Fact]
        public async Task GetMarsPictureAsync_ReturnsHttpRequestException()
        {
            // Arrange
            RoverEnum rover = RoverEnum.Curiosity;
            DateTime earthDate = DateTime.Now;
            string apiKey = "test_api_key";
            mockRestServiceCaller.Setup(x => x.GetMarsPictureJsonAsync(rover.ToString(), earthDate, apiKey)).ThrowsAsync(new HttpRequestException());

            // Act
            var response = await nasaPictureOfTheDay.GetMarsPictureAsync(rover, earthDate, apiKey);

            // Assert
            Assert.False(response.Success);
            Assert.IsType<HttpRequestException>(response.exception);
            Assert.True(response.CanRetry);
        }

        [Fact]
        public async Task GetMarsPictureAsync_ReturnsTaskCanceledException()
        {
            // Arrange
            RoverEnum rover = RoverEnum.Curiosity;
            DateTime earthDate = DateTime.Now;
            string apiKey = "test_api_key";
            mockRestServiceCaller.Setup(x => x.GetMarsPictureJsonAsync(rover.ToString(), earthDate, apiKey)).ThrowsAsync(new TaskCanceledException());

            // Act
            var response = await nasaPictureOfTheDay.GetMarsPictureAsync(rover, earthDate, apiKey);

            // Assert
            Assert.False(response.Success);
            Assert.IsType<TaskCanceledException>(response.exception);
            Assert.True(response.CanRetry);
        }

        [Fact]
        public async Task GetMarsPictureAsync_ReturnsInvalidOperationException()
        {
            // Arrange
            RoverEnum rover = RoverEnum.Curiosity;
            DateTime earthDate = DateTime.Now;
            string apiKey = "test_api_key";
            mockRestServiceCaller.Setup(x => x.GetMarsPictureJsonAsync(rover.ToString(), earthDate, apiKey)).ThrowsAsync(new InvalidOperationException());

            // Act
            var response = await nasaPictureOfTheDay.GetMarsPictureAsync(rover, earthDate, apiKey);

            // Assert
            Assert.False(response.Success);
            Assert.IsType<InvalidOperationException>(response.exception);
            Assert.False(response.CanRetry);
        }
    }

}
