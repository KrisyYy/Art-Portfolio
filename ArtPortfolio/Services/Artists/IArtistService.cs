using System.Collections.Generic;
using ArtPortfolio.Models.Artists;

namespace ArtPortfolio.Services.Artists
{
    public interface IArtistService
    {
        bool IsArtist(string id);

        bool IsArtist(int id);

        int GetIdByUser(string id);

        string GetName(int id);

        int CreateArtist(
            string name, 
            string description, 
            string userId
            );

        ArtistViewModel GetArtistById(int id, string userId);

        List<ArtistListingViewModel> RecommendedArtists(string userId);

        void Follow(int id, string userId);

        bool EditProfile(
            int id, 
            string change, 
            string name, 
            string avatarUrl, 
            string description
            );

    }
}