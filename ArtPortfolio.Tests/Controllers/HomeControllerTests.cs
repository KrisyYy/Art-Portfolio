using System.Collections.Generic;
using System.Linq;
using ArtPortfolio.Controllers;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Models.Home;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace ArtPortfolio.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexShouldReturnNoArtworksAndThreeArtists()
        => MyController<HomeController>
            .Instance(c => c
                .WithUser()
                .WithData(AddArtists())
                .WithData(AddArtworks()))
            .Calling(c => c.Index())
            .ShouldReturn()
            .View(v => v
                .WithModelOfType<IndexViewModel>()
                .Passing(m => m.Artworks.Count == 0 && m.Artists.Count == 3));



        private IEnumerable<Artist> AddArtists()
        {
            var artists = new List<Artist>();
            for (int i = 1; i <= 5; i++)
            {
                artists.Add(new Artist() {Id = i, UserId = "User" + i});
            }

            return artists;
        }

        private IEnumerable<Artwork> AddArtworks()
        {
            var artworks = new List<Artwork>();
            for (int i = 1; i <= 3; i++)
            {
                artworks.Add(new Artwork() {Id = i, ArtistId = 1});
            }
            for (int i = 1; i <= 3; i++)
            {
                artworks.Add(new Artwork() { Id = 3 + i, ArtistId = 2 });
            }
            for (int i = 1; i <= 3; i++)
            {
                artworks.Add(new Artwork() { Id = 6 + i, ArtistId = 3 });
            }
            artworks.Add(new Artwork() { Id = 10, ArtistId = 4 });
            artworks.Add(new Artwork() { Id = 11, ArtistId = 5 });

            return artworks;
        }
    }
}