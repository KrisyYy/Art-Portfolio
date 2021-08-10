using ArtPortfolio.Models.Artists;

namespace ArtPortfolio.Services.Artists
{
    public interface IArtistService
    {
        bool IsArtist(string id);

        int GetIdByUser(string id);

        string GetName(int id);

        int CreateArtist(string name, string description, string userId);

        ArtistViewModel GetArtistById(int id, string userId);

        void Follow(int id, string userId);

        int ChangeAvatar(string userId, string avatarUrl);

        int ChangeName(string userId, string name);

        int ChangeDescription(string userId, string description);

        int ToggleAvailable(string userId);

    }
}