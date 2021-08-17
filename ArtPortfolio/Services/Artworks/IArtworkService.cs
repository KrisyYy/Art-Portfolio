using System.Collections.Generic;
using ArtPortfolio.Models.Artworks;
using ArtPortfolio.Services.Artworks.Models;

namespace ArtPortfolio.Services.Artworks
{
    public interface IArtworkService
    {
        ArtworkServiceModel GetArtworkById(int id);

        bool IsLiked(int id, string userId);

        List<CommentViewModel> GetListOfComments(int id);

        int CreateArtwork(string title, string description, string imageUrl, int artistId);

        bool EditArtwork(int id, string title, string description, string imageUrl, int artistId);

        ArtworkListingServiceModel GetListOfArtworks(string search = null, int order = 1, int page = 1, int artPerPage = int.MaxValue);

        List<ArtworkServiceModel> ArtworksFromFollowed(string userId);

        void Like(int id, string userId);

        void View(int id);

        bool Delete(int id);

        int DeleteComment(int id);

        void CreateComment(string content, int artworkId, string userId);
    }
}
