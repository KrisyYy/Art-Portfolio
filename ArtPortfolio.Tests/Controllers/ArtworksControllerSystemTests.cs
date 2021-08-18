using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ArtPortfolio.Tests.Controllers
{
    public class ArtworksControllerSystemTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public ArtworksControllerSystemTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        private const int artistId = 5;
        private const int artId = 10;

        [Fact]
        public async Task ArtShouldReturnSuccess()
        {
            var client = this.factory.CreateClient();

            var result = await client.GetAsync($"/Artworks/Art/{artId}");

            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task AllShouldReturnSuccess()
        {
            var client = this.factory.CreateClient();

            var result = await client.GetAsync("/Artworks/All");

            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task ArtworksShouldReturnSuccess()
        {
            var client = this.factory.CreateClient();

            var result = await client.GetAsync($"/Artworks/Artworks/{artistId}");

            Assert.True(result.IsSuccessStatusCode);
        }

        [Fact]
        public async Task CreateShouldReturnSuccess()
        {
            var client = this.factory.CreateClient();

            var result = await client.GetAsync("/Artworks/Create");

            Assert.True(result.IsSuccessStatusCode);
        }
    }
}