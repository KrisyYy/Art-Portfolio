using System.Collections.Generic;
using ArtPortfolio.Services.Artworks.Models;

namespace ArtPortfolio.Services.Artworks
{
    public interface IArtworkService
    {
        ArtworkServiceModel GetArtworkById(int id);

        int CreateArtwork(string title, string description, string imageUrl, int artistId);

        List<ListOfArtworksServiceModel> GetListOfArtworks();

        void Like(int id);

        void View(int id);
    }
}
