using Xunit;
using SailorTheCat;
using Microsoft.AspNetCore.Mvc.Testing;
using Assert = Xunit.Assert;

namespace TestSailor

{

    public class SailorTestHttp : IClassFixture<WebApplicationFactory<SailorTheCat.Startup>>
    {
        private readonly WebApplicationFactory<SailorTheCat.Startup> _factory;
        private readonly HttpClient httpClient;

        public SailorTestHttp(WebApplicationFactory<SailorTheCat.Startup> factory)
        {
            _factory = factory;
            httpClient = _factory.CreateClient();
        }

        [Fact]
        public async Task TestWebAppStarts()
        {

            //Arrange - build the test

            var client = _factory.CreateClient();
            // Act - do the testing
            var response = await client.GetAsync("/");
            int code = (int)response.StatusCode;


            //Assert - check to see if it is correct
            Assert.Equal(200, code);

        }

        [Theory]
        [InlineData("/Index")]
        [InlineData("/Privacy")]
        [InlineData("/About")]
        [InlineData("/catalog")]

        public async Task SailorPagesTest(string URL)
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync(URL);
            int code = (int)response.StatusCode;
            //Assert
            Assert.Equal(200, code);
        }


        [Theory]
        [InlineData("Version 1.45")]
        [InlineData("NetCore 9.0")]
        [InlineData("Release 2024 - 12 - 28")]
        public async Task SailorTestHTTPContent(string txtstr)
        {
            //Arrange
            //Act
            var respose = await httpClient.GetAsync("/About");
            var httpContent = await respose.Content.ReadAsStringAsync();
            var contentString = httpContent.ToString();
            //Assert
            Assert.Contains(txtstr, contentString);
        }
    }
}
