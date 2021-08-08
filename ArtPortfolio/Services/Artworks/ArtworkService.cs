using System.Collections.Generic;
using System.Linq;
using ArtPortfolio.Data;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Services.Artworks.Models;

namespace ArtPortfolio.Services.Artworks
{
    public class ArtworkService : IArtworkService
    {
        private ArtPortfolioDbContext data;

        public ArtworkService(ArtPortfolioDbContext data)
        {
            this.data = data;
        }

        public ArtworkServiceModel GetArtworkById(int id)
        {
            return this.data.Artworks.Where(a => a.Id == id)
                .Select(a => new ArtworkServiceModel()
                {
                    Id = a.Id,
                    Title = a.Title,
                    ImageUrl = a.ImageUrl,
                    Description = a.Description,
                    Likes = a.Likes,
                    Views = a.Views
                }).FirstOrDefault();
        }
        public int CreateArtwork(string title, string description, string imageUrl, int artistId)
        {
            var artwork = new Artwork()
            {
                Title = title,
                Description = description,
                ImageUrl = imageUrl,
                ArtistId = artistId
            };

            this.data.Artworks.Add(artwork);
            this.data.SaveChanges();

            return artwork.Id;
        }

        public List<ListOfArtworksServiceModel> GetListOfArtworks()
        {
            return this.data.Artworks.Select(a => new ListOfArtworksServiceModel()
            {
                Id = a.Id,
                ImageUrl = a.ImageUrl,
                Likes = a.Likes,
                Title = a.Title,
                ArtistId = a.ArtistId
            }).ToList();
        }

        public void Like(int id)
        {
            this.data.Artworks.FirstOrDefault(a => a.Id == id).Likes++;
            this.data.SaveChanges();
        }

        public void View(int id)
        {
            this.data.Artworks.FirstOrDefault(a => a.Id == id).Views++;
            this.data.SaveChanges();
        }
    }
}
