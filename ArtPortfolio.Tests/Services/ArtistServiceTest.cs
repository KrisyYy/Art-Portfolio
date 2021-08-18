using System.Collections.Generic;
using System.Linq;
using ArtPortfolio.Data;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Models.Artists;
using ArtPortfolio.Services.Artists;
using ArtPortfolio.Tests.Mocks;
using Xunit;

namespace ArtPortfolio.Tests.Services
{
    public class ArtistServiceTest
    {
        private const int artId = 5;
        private const int artistId = 10;
        private const string artistName = "Artist Name";
        private const string userId = "User Id";
        private const string fakeId = "Fake Id";



        [Fact]
        public void IsArtistShouldWorkBothWithIntAndString()
        {
            // Arrange
            using var data = this.Data();
            var artistService = new ArtistService(data);

            // Act
            var result1 = artistService.IsArtist(artistId);
            var result2 = artistService.IsArtist(userId);
            var result3 = artistService.IsArtist(fakeId);

            // Assert
            Assert.True(result1);
            Assert.True(result2);
            Assert.False(result3);
        }


        [Fact]
        public void GetIdByUserShouldReturnCorrectId()
        {
            // Arrange
            using var data = this.Data();
            var artistService = new ArtistService(data);

            // Act
            var result1 = artistService.GetIdByUser(userId);
            var result2 = artistService.GetIdByUser(fakeId);

            // Assert
            Assert.Equal(artistId, result1);
            Assert.Equal(0, result2);
        }


        [Fact]
        public void GetNameShouldReturnCorrectName()
        {
            // Arrange
            using var data = this.Data();
            var artistService = new ArtistService(data);

            // Act
            var result1 = artistService.GetName(artistId);
            var result2 = artistService.GetName(0);


            // Assert
            Assert.Equal(artistName, result1);
            Assert.Null(result2);
        }


        [Fact]
        public void CreateArtistShouldWorkProperly()
        {
            // Arrange
            using var data = this.Data();
            var artistService = new ArtistService(data);
            var name = "Example Name";
            var description = "Example Description";

            // Act
            var result = artistService.CreateArtist(name, description, userId);
            var artist = artistService.GetArtistById(result, userId);

            // Assert
            Assert.True(result != 0);
            Assert.Equal(name, artist.Name);
            Assert.Equal(description, artist.Description);
        }


        [Fact]
        public void RecommendedArtistsShouldWorkProperly()
        {
            // Arrange
            using var data = this.Data();
            var artistService = new ArtistService(data);

            // Act
            var result1 = artistService.RecommendedArtists(userId);

            ExtraData(data);
            var result2 = artistService.RecommendedArtists(userId);

            // Assert
            Assert.Empty(result1);
            Assert.IsType<List<ArtistListingViewModel>>(result1);

            Assert.Equal(1, result2.Count);
            Assert.IsType<List<ArtistListingViewModel>>(result2);
        }


        [Fact]
        public void FollowShouldToggleSuccessfully()
        {
            // Arrange
            using var data = this.Data();
            var artistService = new ArtistService(data);

            // Act
            artistService.Follow(artistId, userId);
            var result1 = data.Follows.Any(a => a.ArtistId == artistId && a.UserId == userId);

            artistService.Follow(artistId, userId);
            var result2 = data.Follows.Any(a => a.ArtistId == artistId && a.UserId == userId);

            // Assert
            Assert.True(result1);
            Assert.False(result2);
        }


        [Fact]
        public void EditProfileShouldWorkProperly()
        {
            // Arrange
            using var data = this.Data();
            var artistService = new ArtistService(data);

            // Act
            var result1 = artistService.EditProfile(id:artistId, change:"Name", null, null, null);
            var result2 = artistService.EditProfile(id:artistId, change:"AvatarUrl", null, null, null);
            var result3 = artistService.EditProfile(id:artistId, change:"Description", null, null, null);
            var result4 = artistService.EditProfile(id:artistId, change: "AvailableToCommission", null, null, null);

            var artist = artistService.GetArtistById(artistId, userId);

            // Assert
            Assert.True(result1);
            Assert.True(result2);
            Assert.True(result3);
            Assert.True(result4);

            Assert.Equal("My name", artist.Name);
            Assert.Equal("https://i.imgur.com/rtrF2Ih.jpg", artist.AvatarUrl);
            Assert.Equal("Hello world!", artist.Description);
            Assert.True(artist.AvailableToCommission);
        }



        private ArtPortfolioDbContext Data()
        {
            var data = DatabaseMock.Instance;

            var user = new User() { Id = userId };
            var artist = new Artist() { Id = artistId, UserId = userId, Name = artistName };
            var artwork = new Artwork() { Id = artId, ArtistId = artistId };

            data.Users.Add(user);
            data.Artists.Add(artist);
            data.Artworks.Add(artwork);

            data.SaveChanges();

            return data;
        }

        private void ExtraData(ArtPortfolioDbContext data)
        {
            var user2 = new User() { Id = fakeId };
            var artist2 = new Artist() { Id = artistId + 1, UserId = fakeId };
            var artworks2 = Enumerable.Range(0, 4).Select(i => new Artwork() { ArtistId = artistId + 1 });

            data.Users.Add(user2);
            data.Artists.Add(artist2);
            data.Artworks.AddRange(artworks2);

            data.SaveChanges();
        }
    }
}