using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtPortfolio.Data;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Models.Artworks;

namespace ArtPortfolio.Services.Artworks
{
    public class ArtworkService : IArtworkService
    {
        private ArtPortfolioDbContext data;

        public ArtworkService(ArtPortfolioDbContext data)
        {
            this.data = data;
        }

        public Artwork GetArtworkById(string id)
        {
            var artwork = this.data.Artworks.FirstOrDefault(a => a.Id == id);
            return artwork;
        }
        public string CreateArtwork(ArtCreateModel artModel)
        {
            var artwork = new Artwork()
            {
                Title = artModel.Title,
                Description = artModel.Description,
                ImageUrl = artModel.ImageUrl
            };

            this.data.Artworks.Add(artwork);
            this.data.SaveChanges();

            return artwork.Id;
        }

        public List<Artwork> GetListOfArtworks()
        {
            return this.data.Artworks.ToList();
        }

        public void Like(Artwork artwork)
        {
            artwork.Likes++;
            this.data.SaveChanges();
        }
    }
}
