using System.Linq;
using ArtPortfolio.Data;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Models.Artists;

namespace ArtPortfolio.Services.Artists
{
    public class ArtistService : IArtistService
    {
        private readonly ArtPortfolioDbContext _data;

        public ArtistService(ArtPortfolioDbContext data)
        {
            _data = data;
        }

        public bool IsArtist(string id)
        {
            return _data.Artists.Any(a => a.UserId == id);
        }

        public int GetIdByUser(string id)
        {
            return _data.Artists.Where(a => a.UserId == id).Select(a => a.Id).FirstOrDefault();
        }

        public string GetName(int id)
        {
            return _data.Artists.Where(a => a.Id == id).Select(a => a.Name).FirstOrDefault();
        }

        public int CreateArtist(string name, string description, string userId)
        {
            var artist = new Artist()
            {
                Name = name,
                Description = description ?? "",
                UserId = userId
            };

            _data.Artists.Add(artist);
            _data.SaveChanges();

            return artist.Id;
        }

        public ArtistViewModel GetArtistById(int id, string userId)
        {
            return _data.Artists.Where(a => a.Id == id).Select(a => new ArtistViewModel()
            {
                Id = a.Id,
                Name = a.Name,
                AvatarUrl = a.AvatarUrl,
                AvailableToCommission = a.AvailableToCommission,
                Description = a.Description,
                Followers = a.Follows.Count,
                IsFollowed = _data.Follows.Any(ar => ar.ArtistId == id && ar.UserId == userId)
            }).First();
        }

        public void Follow(int id, string userId)
        {
            var follow = _data.Follows.FirstOrDefault(f => f.UserId == userId && f.ArtistId == id);
            if (follow != null)
            {
                _data.Follows.Remove(follow);
                _data.SaveChanges();
                return;
            }
            follow = new Follow()
            {
                ArtistId = id,
                UserId = userId
            };
            _data.Follows.Add(follow);
            _data.SaveChanges();
        }

        public int ChangeAvatar(string userId, string avatarUrl)
        {
            var artistId = GetIdByUser(userId);
            _data.Artists.FirstOrDefault(a => a.Id == artistId).AvatarUrl = avatarUrl;
            _data.SaveChanges();

            return artistId;
        }

        public int ChangeName(string userId, string name)
        {
            var artistId = GetIdByUser(userId);
            _data.Artists.FirstOrDefault(a => a.Id == artistId).Name = name;
            _data.SaveChanges();

            return artistId;
        }

        public int ChangeDescription(string userId, string description)
        {
            var artistId = GetIdByUser(userId);
            _data.Artists.FirstOrDefault(a => a.Id == artistId).Description = description;
            _data.SaveChanges();

            return artistId;
        }

        public int ToggleAvailable(string userId)
        {
            var artistId = GetIdByUser(userId);
            var artist = _data.Artists.FirstOrDefault(a => a.Id == artistId);
            artist.AvailableToCommission = !artist.AvailableToCommission;
            _data.SaveChanges();

            return artistId;
        }
    }
}