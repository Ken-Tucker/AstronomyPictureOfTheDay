using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AstronomyPictureOfTheDay.Tests
{
    public class RestServiceCallerTests
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly RestServiceCaller _restServiceCaller;

        public RestServiceCallerTests()
        {
            _httpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandler.Object);
            _restServiceCaller = new RestServiceCaller(_httpClient);
        }

        [Fact]
        public async Task GetAPODJsonAsync_ReturnsJsonString()
        {
            // Arrange
            var apiKey = "DEMO_KEY";
            var expectedJson = "{\"date\":\"2023-10-01\",\"explanation\":\"Test explanation\"}";
            var requestUri = new Uri($"https://api.nasa.gov/planetary/apod?api_key={apiKey}");

            _httpMessageHandler
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                     )
                    .ReturnsAsync(new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(expectedJson)
                    })
                    .Verifiable();

            // Act
            var result = await _restServiceCaller.GetAPODJsonAsync(apiKey);

            // Assert
            Assert.Equal(expectedJson, result);
        }

        [Fact]
        public async Task GetMarsPictureJsonAsync_ReturnsJsonString()
        {
            // Arrange
            var rover = "curiosity";
            var earthDate = new DateTime(2023, 10, 01);
            var apiKey = "DEMO_KEY";
            var expectedJson = "{\"photos\":[]}";
            var requestUri = new Uri($"https://api.nasa.gov/mars-photos/api/v1/rovers/{rover}/photos?earth_date={earthDate:yyyy-MM-dd}&api_key={apiKey}");

            _httpMessageHandler
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                     )
                    .ReturnsAsync(new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(expectedJson)
                    })
                    .Verifiable();

            // Act
            var result = await _restServiceCaller.GetMarsPictureJsonAsync(rover, earthDate, apiKey);

            // Assert
            Assert.Equal(expectedJson, result);
        }
    }
}
