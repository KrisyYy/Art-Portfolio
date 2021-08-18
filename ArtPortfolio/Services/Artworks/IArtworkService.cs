using System.Collections.Generic;
using ArtPortfolio.Services.Artworks.Models;

namespace ArtPortfolio.Services.Artworks
{
    public interface IArtworkService
    {
        ArtworkServiceModel GetArtworkById(int id);

        int ArtistId(int artId);

        bool IsLiked(int id, string userId);

        int CreateArtwork(
            string title, 
            string description, 
            string imageUrl, 
            int artistId
            );

        bool EditArtwork(
            int id, 
            string title, 
            string description, 
            string imageUrl, 
            int artistId
            );

        ArtworkListingServiceModel GetListOfArtworks(
            string search = null, 
            int order = 1, 
            int page = 1, 
            int artPerPage = int.MaxValue
            );

        List<ArtworkServiceModel> ArtworksFromFollowed(string userId);

        void Like(int id, string userId);

        void View(int id);

        bool Delete(int id);
    }
}
