using System.Collections.Generic;
using System.Linq;
using ArtPortfolio.Data;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Models.Artworks;

namespace ArtPortfolio.Services.Artworks
{
    public class ArtworkService : IArtworkService
    {
        private readonly ArtPortfolioDbContext _data;

        public ArtworkService(ArtPortfolioDbContext data)
        {
            _data = data;
        }

        public ArtViewModel GetArtworkById(int id, string userId)
        {
            return _data.Artworks.Where(a => a.Id == id)
                .Select(a => new ArtViewModel()
                {
                    Id = a.Id,
                    Title = a.Title,
                    ImageUrl = a.ImageUrl,
                    Description = a.Description,
                    Likes = a.Likes.Count,
                    Views = a.Views,
                    IsLiked = a.Likes.Any(l => l.UserId == userId && l.ArtworkId == id),
                    Comments = a.Comments.Select(c => new CommentViewModel()
                    {
                        Content = c.Content,
                        Id = c.Id
                    }).ToList()
                }).FirstOrDefault();
        }
        public int CreateArtwork(string title, string description, string imageUrl, int artistId)
        {
            var artwork = new Artwork()
            {
                Title = title,
                Description = description,
                ImageUrl = imageUrl,
                ArtistId = artistId
            };

            _data.Artworks.Add(artwork);
            _data.SaveChanges();

            return artwork.Id;
        }

        public List<ArtListingViewModel> GetListOfArtworks()
        {
            return _data.Artworks.Select(a => new ArtListingViewModel()
            {
                Id = a.Id,
                ImageUrl = a.ImageUrl,
                Likes = a.Likes.Count,
                Title = a.Title,
                ArtistId = a.ArtistId
            }).ToList();
        }

        public void Like(int id, string userId)
        {
            var like = _data.Likes.FirstOrDefault(l => l.UserId == userId && l.ArtworkId == id);
            if (like != null)
            {
                _data.Likes.Remove(like);
                _data.SaveChanges();
                return;
            }
            like = new Like()
            {
                ArtworkId = id,
                UserId = userId
            };
            _data.Likes.Add(like);
            _data.SaveChanges();
        }

        public void View(int id)
        {
            _data.Artworks.FirstOrDefault(a => a.Id == id).Views++;
            _data.SaveChanges();
        }

        public void CreateComment(string content, int artworkId, string userId)
        {
            var comment = new Comment()
            {
                Content = content,
                ArtworkId = artworkId,
                UserId = userId
            };

            _data.Comments.Add(comment);
            _data.SaveChanges();
        }
    }
}
