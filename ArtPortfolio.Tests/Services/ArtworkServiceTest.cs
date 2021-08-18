using System.Linq;
using ArtPortfolio.Data;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Services.Artworks;
using ArtPortfolio.Services.Artworks.Models;
using ArtPortfolio.Tests.Mocks;
using Xunit;

namespace ArtPortfolio.Tests.Services
{
    public class ArtworkServiceTest
    {
        private const int artId = 5;
        private const int artistId = 10;
        private const string userId = "User Id";

        [Fact]
        public void ArtistIdShouldReturnCurrentArtworksArtistId()
        {
            // Arrange
            using var data = this.Data();
            var artworkService = new ArtworkService(data, MapperMock.Instance);

            // Act
            var result = artworkService.ArtistId(artId);

            // Assert
            Assert.Equal(10, result);
        }


        [Fact]
        public void LikeAndIsLikedShouldBeSuccessfull()
        {
            // Arrange
            using var data = this.Data();
            var artworkService = new ArtworkService(data, MapperMock.Instance);

            // Act
            artworkService.Like(artId, userId);
            var result1 = artworkService.IsLiked(artId, userId);

            artworkService.Like(artId, userId);
            var result2 = artworkService.IsLiked(artId, userId);

            // Assert
            Assert.True(result1);
            Assert.False(result2);
        }


        [Fact]
        public void GetArtworkByIdShouldReturnProperArtwork()
        {
            // Arrange
            using var data = this.Data();
            var artworkService = new ArtworkService(data, MapperMock.Instance);

            // Act
            var result = artworkService.GetArtworkById(artId);
            

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsType<ArtworkServiceModel>(result);
            Assert.Equal(artId, model.Id);
            Assert.Equal(artistId, model.ArtistId);
        }


        [Fact]
        public void CreateArtworkShouldBeSuccessful()
        {
            // Arrange
            using var data = this.Data();
            var artworkService = new ArtworkService(data, MapperMock.Instance);

            // Act
            var title = "Title";
            var description = "Description";
            var imageUrl = "ImageUrl";
            var currentArtId = artworkService.CreateArtwork(title, description, imageUrl, artistId);
            var artwork = artworkService.GetArtworkById(currentArtId);

            // Assert
            Assert.NotNull(artwork);
            Assert.Equal(artistId, artwork.ArtistId);
            Assert.Equal(title, artwork.Title);
            Assert.Equal(description, artwork.Description);
            Assert.Equal(imageUrl, artwork.ImageUrl);
        }


        [Fact]
        public void EditShouldBeSuccessful()
        {
            // Arrange
            using var data = this.Data();
            var artworkService = new ArtworkService(data, MapperMock.Instance);

            // Act
            var title = "Title";
            var description = "Description";
            var imageUrl = "ImageUrl";
            var editedTitle = "Title2";
            var editedDescription = "Description2";
            var editedImageUrl = "ImageUrl2";
            var currentArtId = artworkService.CreateArtwork(title, description, imageUrl, artistId);
            var result = artworkService.EditArtwork(currentArtId, editedTitle, editedDescription, editedImageUrl, artistId);
            var artwork = artworkService.GetArtworkById(currentArtId);

            // Assert
            Assert.True(result);
            Assert.NotNull(artwork);
            Assert.Equal(artistId, artwork.ArtistId);
            Assert.Equal(editedTitle, artwork.Title);
            Assert.Equal(editedDescription, artwork.Description);
            Assert.Equal(editedImageUrl, artwork.ImageUrl);
        }


        [Fact]
        public void DeleteShouldSuccessfullyRemoveArtwork()
        {
            // Arrange
            using var data = this.Data();
            var artworkService = new ArtworkService(data, MapperMock.Instance);

            // Act
            var result = artworkService.Delete(artId);

            // Assert
            Assert.Null(artworkService.GetArtworkById(artId));
        }


        [Fact]
        public void GetListOfArtworksShouldWorkProperly()
        {
            // Arrange
            using var data = this.Data();
            var artworkService = new ArtworkService(data, MapperMock.Instance);

            var artworks = Enumerable.Range(0, 5).Select(i => new Artwork() {ArtistId = artistId});
            data.Artworks.AddRange(artworks);
            data.SaveChanges();

            // Act
            var result1 = artworkService.GetListOfArtworks();
            var result2 = artworkService.GetListOfArtworks(page:2, artPerPage: 2);

            // Assert
            Assert.Equal(6, result1.Artworks.Count);
            Assert.Equal(2, result2.Artworks.Count);
            Assert.Equal(1, result1.MaxPage);
            Assert.Equal(3, result2.MaxPage);
        }


        [Fact]
        public void ArtworksFromFollowedShouldWorkProperly()
        {
            // Arrange
            using var data = this.Data();
            var artworkService = new ArtworkService(data, MapperMock.Instance);

            var follow = new Follow() {ArtistId = artistId, UserId = userId};

            var newUser = new User() { Id = "User 2" };
            var newArtist = new Artist() {Id = artistId + 1, UserId = "User 2"};
            var newArtwork = new Artwork() {Id = artId + 1, ArtistId = artistId + 1 };

            data.Users.Add(newUser);
            data.Artists.Add(newArtist);
            data.Artworks.Add(newArtwork);
            data.Follows.Add(follow);
            data.SaveChanges();

            // Act
            var result = artworkService.ArtworksFromFollowed(userId);

            // Assert
            Assert.Contains(result, a => a.Id == artId);
            Assert.DoesNotContain(result, a => a.Id == artId + 1);
        }



        private ArtPortfolioDbContext Data()
        {
            var data = DatabaseMock.Instance;

            var user = new User() { Id = userId };
            var artist = new Artist() { Id = artistId, UserId = userId };
            var artwork = new Artwork() { Id = artId, ArtistId = artistId };

            data.Users.Add(user);
            data.Artists.Add(artist);
            data.Artworks.Add(artwork);

            data.SaveChanges();

            return data;
        }
    }
}