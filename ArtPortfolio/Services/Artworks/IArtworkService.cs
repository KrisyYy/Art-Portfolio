using System.Collections.Generic;
using ArtPortfolio.Models.Artworks;

namespace ArtPortfolio.Services.Artworks
{
    public interface IArtworkService
    {
        ArtViewModel GetArtworkById(int id, string userId);

        int CreateArtwork(string title, string description, string imageUrl, int artistId);

        List<ArtListingViewModel> GetListOfArtworks();
        List<ArtListingViewModel> ArtworksFromFollowed(string userId);

        void Like(int id, string userId);

        void View(int id);

        bool Delete(int id);

        int DeleteComment(int id);

        void CreateComment(string content, int artworkId, string userId);
    }
}
