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
        int ChangeAvatar(string userId, string avatarUrl);
        int ChangeName(string userId, string name);
        int ChangeDescription(string userId, string description);
        int ToggleAvailable(string userId);

    }
}