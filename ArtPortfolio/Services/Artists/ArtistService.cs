using System.Collections.Generic;
using System.Linq;
using ArtPortfolio.Data;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Models.Artists;
using ArtPortfolio.Models.Artworks;
using ArtPortfolio.Services.Artworks.Models;

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


        public bool IsArtist(int id)
        {
            return _data.Artists.Any(a => a.Id == id);
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
                    IsFollowed = _data.Follows.Any(f => f.ArtistId == id && f.UserId == userId),
                    Artworks = a.Artworks.Select(ar => new ArtworkServiceModel()
                    {
                        Id = ar.Id,
                        ArtistId = a.Id,
                        ArtistName = a.Name,
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
                .ToArray()
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
                        })
                        .Take(3)
                        .ToList()
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


        public bool EditProfile(int id, string change, string name, string avatarUrl, string description)
        {
            var artist = _data.Artists.FirstOrDefault(a => a.Id == id);
            if (artist == null)
            {
                return false;
            }

            switch (change)
            {
                case "Name":
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        name = "My name";
                    }
                    artist.Name = name;
                    break;
                case "AvatarUrl":
                    if (string.IsNullOrWhiteSpace(avatarUrl))
                    {
                        avatarUrl = "https://i.imgur.com/rtrF2Ih.jpg";
                    }
                    artist.AvatarUrl = avatarUrl;
                    break;
                case "Description":
                    if (string.IsNullOrWhiteSpace(description))
                    {
                        description = "Hello world!";
                    }
                    artist.Description = description;
                    break;
                case "AvailableToCommission":
                    artist.AvailableToCommission = !artist.AvailableToCommission;
                    break;
                default: return false;
            }

            _data.SaveChanges();

            return true;
        }
    }
}