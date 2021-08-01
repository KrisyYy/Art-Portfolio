using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Models.Artworks;

namespace ArtPortfolio.Services.Artworks
{
    public interface IArtworkService
    {
        public Artwork GetArtworkById(string id);

        public string CreateArtwork(ArtCreateModel artModel);

        public List<Artwork> GetListOfArtworks();

        public void Like(Artwork artwork);
    }
}
