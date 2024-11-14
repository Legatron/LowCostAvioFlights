using LowCostAvioFlights.Infrastructure;
using LowCostAvioFlights.Services;
using Microsoft.Extensions.Options;
using Moq;
using System.Net.Http;
using Xunit;

namespace LowCostAvioFlights.UnitTests
{
    public class AmadeusOAuthClientTests
    {

        [Fact]
        public async Task GetAccessTokenAsyncReturnsValidAccessTokenWhenCacheIsEmptyAndApiCallIsSuccessful()
        {
            // Arrange
            var apiSettings = new AmadeusApiSettings { 
                ApiKey = "Si7MJioXKIBltgrIsa7YUpucsnUASdRz", 
                ApiSecret = "JkQ4zCb6fRwmV05R", 
                BaseUrl = "https://test.api.amadeus.com", 
                OauthTokenHttps = "https://test.api.amadeus.com/v1/security/oauth2/token"
            };
            var options = Options.Create(apiSettings);
            var httpClient = new HttpClient();
            var httpClientFactory = new Mock<IHttpClientFactory>();
            httpClientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);
            var tokenCacheService = new Mock<ITokenCacheService>();
            tokenCacheService.Setup(s => s.GetCachedTokenAsync()).ReturnsAsync((string)null); // Specify the type of the result explicitly
            var amadeusOAuthClient = new AmadeusOAuthClient(options, httpClientFactory.Object, tokenCacheService.Object);

            // Act
            var accessToken = await amadeusOAuthClient.GetAccessTokenAsync();

            // Assert
            Assert.NotNull(accessToken);
            httpClientFactory.Verify(f => f.CreateClient(It.IsAny<string>()), Times.Once);
            tokenCacheService.Verify(s => s.GetCachedTokenAsync(), Times.Once);
        }
    }
}
