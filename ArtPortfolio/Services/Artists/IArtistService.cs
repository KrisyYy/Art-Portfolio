using ArtPortfolio.Data.Models;

namespace ArtPortfolio.Services.Artists
{
    public interface IArtistService
    {
        bool IsArtist(string id);
        int GetIdByUser(string id);
        string GetName(int id);
        int CreateArtist(string name, string description, string userId);
        Artist GetArtist(int id);
        void Follow(int id);
    }
}