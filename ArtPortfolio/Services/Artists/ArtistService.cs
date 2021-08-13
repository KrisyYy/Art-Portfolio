using System.Collections.Generic;
using System.Linq;
using ArtPortfolio.Data;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Models.Artists;
using ArtPortfolio.Models.Artworks;

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
            return _data.Artists
                .Where(a => a.Id == id)
                .Select(a => new ArtistViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    AvatarUrl = a.AvatarUrl,
                    AvailableToCommission = a.AvailableToCommission,
                    Description = a.Description,
                    Followers = a.Follows.Count,
                    IsFollowed = _data.Follows.Any(ar => ar.ArtistId == id && ar.UserId == userId),
                    Artworks = a.Artworks.Select(ar => new ArtListingViewModel()
                    {
                        Id = ar.Id,
                        ArtistId = a.Id,
                        Title = ar.Title,
                        ImageUrl = ar.ImageUrl,
                        Likes = ar.Likes.Count
                    }).ToList()
                }).First();
        }

        public List<ArtistListingViewModel> RecommendedArtists(string userId)
        {
            return _data.Artists
                .Where(a => a.Artworks.Count >= 3 && a.UserId != userId)
                .OrderByDescending(a => a.Follows.Count)
                .Take(3)
                .Select(a => new ArtistListingViewModel()
                {
                    Id = a.Id,
                    AvatarUrl = a.AvatarUrl,
                    Name = a.Name,
                    IsFollowed = _data.Follows.Any(f => f.ArtistId == a.Id && f.UserId == userId),
                    Artworks = a.Artworks
                        .OrderByDescending(ar => ar.Likes.Count)
                        .Select(ar => new ArtistPartialArtViewModel()
                        {
                            Id = ar.Id,
                            ImageUrl = ar.ImageUrl
                        }).Take(3).ToList()
                }).ToList();
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