using System.Security.Claims;
using ArtPortfolio.Controllers;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Models.Artists;
using ArtPortfolio.Services.Artists;
using ArtPortfolio.Tests.Mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace ArtPortfolio.Tests.Controllers
{
    public class ArtistsControllerTests
    {
        private const string userId = "User ID";
        private const int artistId = 5;
        private const int artId = 10;



        [Fact]
        public void BecomeArtistShouldReturnBadRequest()
        {
            // Arrange
            var artistController = Controller();

            // Act
            var result = artistController.BecomeArtist();

            // Assert
            Assert.NotNull(result);
            var view = Assert.IsType<BadRequestResult>(result);
        }


        [Fact]
        public void ProfileShouldReturnView()
        {
            // Arrange
            var artistController = Controller();

            // Act
            var result1 = artistController.Profile(artistId);
            var result2 = artistController.Profile(0);

            // Assert
            Assert.NotNull(result1);
            var view = Assert.IsType<ViewResult>(result1);
            var model = view.Model;
            Assert.IsType<ArtistViewModel>(model);

            Assert.NotNull(result2);
            Assert.IsType<BadRequestResult>(result2);
        }


        [Fact]
        public void FollowShouldReturnRedirectToAction()
        {
            // Arrange
            var artistController = Controller();

            // Act
            var result1 = artistController.Follow(artistId);
            var result2 = artistController.Follow(0);

            // Assert
            Assert.NotNull(result1);
            var view = Assert.IsType<RedirectToActionResult>(result1);
            Assert.Equal("Profile", view.ActionName);
            Assert.Equal("Artists", view.ControllerName);

            Assert.NotNull(result2);
            Assert.IsType<BadRequestResult>(result2);
        }


        [Fact]
        public void SettingsShouldReturnRedirectToAction()
        {
            // Arrange
            var artistController = Controller();

            // Act
            var result1 = artistController.EditProfile("Name");
            var result2 = artistController.EditProfile();

            // Assert
            Assert.NotNull(result1);
            var view = Assert.IsType<RedirectToActionResult>(result1);
            Assert.Equal("Profile", view.ActionName);
            Assert.Equal("Artists", view.ControllerName);

            Assert.NotNull(result2);
            Assert.IsType<BadRequestResult>(result2);
        }


        [Fact]
        public void EditProfileShouldReturnView()
        {
            // Arrange
            var artistController = Controller();

            // Act
            var result = artistController.Settings();

            // Assert
            Assert.NotNull(result);
            var view = Assert.IsType<ViewResult>(result);
            var model = view.Model;
            Assert.IsType<SettingsFormModel>(model);
        }



        private ArtistsController Controller()
        {
            var data = DatabaseMock.Instance;
            
            var artistService = new ArtistService(data);


            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new (ClaimTypes.Name, "Test Name"),
                new (ClaimTypes.NameIdentifier, userId)
            }, "mock"));

            data.Artists.Add(AddArtist());
            data.Artworks.Add(AddArtwork());

            data.SaveChanges();

            var artistController = new ArtistsController(artistService)
            {
                ControllerContext = new ControllerContext() { HttpContext = new DefaultHttpContext() { User = user } }
            };


            return artistController;
        }



        private Artist AddArtist()
        {
            return new Artist() { UserId = userId, Id = artistId };
        }

        private Artwork AddArtwork()
        {
            return new Artwork() { ArtistId = artistId, Id = artId };
        }
    }
}