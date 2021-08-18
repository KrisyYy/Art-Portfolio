using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ArtPortfolio.Controllers;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Models.Artworks;
using ArtPortfolio.Services.Artists;
using ArtPortfolio.Services.Artworks;
using ArtPortfolio.Services.Artworks.Models;
using ArtPortfolio.Services.Comments;
using ArtPortfolio.Tests.Mocks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace ArtPortfolio.Tests.Controllers
{
    public class ArtworksControllerTests
    {
        private const string userId = "New User ID";
        private const int artistId = 5;
        private const int artId = 10;



        [Fact]
        public void AllShouldReturnViewAndModel()
        {
            MyController<ArtworksController>
                .Instance(c => c
                    .WithData(AddArtist(1))
                    .WithData(AddArtworks()))
                .Calling(c => c.All(new AllArtworksViewModel()))
                .ShouldReturn()
                .View(v => v
                    .WithModelOfType<AllArtworksViewModel>()
                    .Passing(m => m.Artworks.Count == 5 && m.MaxPage == 1));
        }


        [Fact]
        public void ArtworksShouldReturnViewWithProperModel()
        => MyController<ArtworksController>
            .Instance(c => c
                .WithData(AddArtist(1))
                .WithData(AddArtworks()))
            .Calling(c => c.Artworks(artistId + 1))
            .ShouldReturn()
            .View(v => v
                .WithModelOfType<ArtworkListingServiceModel>()
                .Passing(m => m.Artworks.Count == 5));



        [Fact]
        public void ArtShouldReturnViewWithProperModel()
        {
            // Arrange
            var artController = Controller();

            // Act
            var result = artController.Art(artId);

            // Assert
            Assert.NotNull(result);
            var view = Assert.IsType<ViewResult>(result);
            var model = view.Model;
            Assert.IsType<ArtViewModel>(model);
        }


        [Fact]
        public void LikeShouldReturnRedirectToAction()
        {
            // Arrange
            var artController = Controller();

            // Act
            var result1 = artController.Like(artId);
            var result2 = artController.Like(0);

            // Assert
            Assert.NotNull(result1);
            var view = Assert.IsType<RedirectToActionResult>(result1);
            Assert.Equal("Art", view.ActionName);
            Assert.Equal("Artworks", view.ControllerName);

            Assert.NotNull(result2);
            Assert.IsType<BadRequestResult>(result2);
        }



        [Fact]
        public void DeleteShouldReturnProperResult()
        {
            // Arrange
            var artController = Controller();

            // Act
            var result1 = artController.Delete(artId + 1);
            var result2 = artController.Delete(artId);

            // Assert
            Assert.NotNull(result1);
            Assert.IsType<UnauthorizedResult>(result1);

            Assert.NotNull(result2); 
            var view = Assert.IsType<RedirectToActionResult>(result2);
            Assert.Equal("Index", view.ActionName);
            Assert.Equal("Home", view.ControllerName);
        }


        [Fact]
        public void CreateShouldReturnView()
        {
            // Arrange
            var artController = Controller();

            // Act
            var result = artController.Create();

            // Assert
            Assert.NotNull(result);
            var view = Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public void EditShouldReturnProperResult()
        {
            // Arrange
            var artController = Controller();

            // Act
            var result1 = artController.Edit(artId + 1);
            var result2 = artController.Edit(artId);

            // Assert
            Assert.NotNull(result1);
            Assert.IsType<UnauthorizedResult>(result1);

            Assert.NotNull(result2);
            var view = Assert.IsType<ViewResult>(result2);
            var model = view.Model;
            Assert.IsType<ArtCreateModel>(model);
        }



        private ArtworksController Controller()
        {
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var artworkService = new ArtworkService(data, mapper);
            var artistService = new ArtistService(data);
            var commentService = new CommentService(data, mapper);


            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new (ClaimTypes.Name, "Test Name"),
                new (ClaimTypes.NameIdentifier, userId)
            }, "mock"));

            data.Artists.Add(AddArtist());
            data.Artists.Add(AddArtist(1));

            data.Artworks.Add(AddArtwork());
            data.Artworks.Add(AddArtwork(1));

            data.Artworks.AddRange(AddArtworks());

            data.SaveChanges();

            var artworkController = new ArtworksController(artworkService, artistService, mapper, commentService)
            {
                ControllerContext = new ControllerContext() {HttpContext = new DefaultHttpContext() {User = user}}
            };


            return artworkController;
        }



        private Artist AddArtist(int i = 0)
        {
            return new Artist() { UserId = userId + (i == 0 ? "" : i.ToString()) , Id = artistId + i};
        }

        private Artwork AddArtwork(int i = 0)
        {
            return new Artwork() {ArtistId = artistId + i, Id = artId + i};
        }

        private IEnumerable<Artwork> AddArtworks()
        {
            return Enumerable.Range(0, 5).Select(i => new Artwork() {ArtistId = artistId + 1 });
        }
    }
}