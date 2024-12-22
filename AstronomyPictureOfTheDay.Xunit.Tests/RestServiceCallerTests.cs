using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AstronomyPictureOfTheDay.Xunit.Tests
{
    public class RestServiceCallerTests
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly RestServiceCaller _restServiceCaller;
        private readonly Mock<IHttpClientFactory> _httpClientFactory;
        public RestServiceCallerTests()
        {
            _httpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandler.Object)
            {
                BaseAddress = new Uri(NasaPictureOfTheDay.HttpClientFactoryUrl)
            };
            _httpClientFactory = new Mock<IHttpClientFactory>();
            _httpClientFactory
                .Setup(x => x.CreateClient(NasaPictureOfTheDay.HttpClientFactoryName))
                .Returns(_httpClient);

            _restServiceCaller = new RestServiceCaller(_httpClientFactory.Object);
        }

        [Fact]
        public async Task GetAPODJsonAsync_ReturnsJsonString()
        {
            // Arrange
            var apiKey = "DEMO_KEY";
            var expectedJson = "{\"date\":\"2023-10-01\",\"explanation\":\"Test explanation\"}";

            using var returnMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedJson)
            };
            _httpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                 )
                .ReturnsAsync(returnMessage)
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
            using var returnMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedJson)
            };
            _httpMessageHandler
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                     )
                    .ReturnsAsync(returnMessage)
                    .Verifiable();

            // Act
            var result = await _restServiceCaller.GetMarsPictureJsonAsync(rover, earthDate, apiKey);

            // Assert
            Assert.Equal(expectedJson, result);
        }
    }
}
