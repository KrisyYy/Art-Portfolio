using System.Linq;
using ArtPortfolio.Data;
using ArtPortfolio.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace ArtPortfolio.Services.Artists
{
    public class ArtistService : IArtistService
    {
        private readonly ArtPortfolioDbContext data;
        private readonly UserManager<User> userManager;

        public ArtistService(ArtPortfolioDbContext data, UserManager<User> userManager)
        {
            this.data = data;
            this.userManager = userManager;
        }

        public bool IsArtist(string id)
        {
            return this.data.Artists.Any(a => a.UserId == id);
        }

        public int GetIdByUser(string id)
        {
            return this.data.Artists.Where(a => a.UserId == id).Select(a => a.Id).FirstOrDefault();
        }

        public string GetName(int id)
        {
            return this.data.Artists.Where(a => a.Id == id).Select(a => a.Name).FirstOrDefault();
        }

        public int CreateArtist(string name, string description, string userId)
        {
            var artist = new Artist()
            {
                Name = name,
                Description = description ?? "",
                UserId = userId
            };

            this.data.Artists.Add(artist);
            this.data.SaveChanges();

            return artist.Id;
        }

        public Artist GetArtist(int id)
        {
            return this.data.Artists.FirstOrDefault(a => a.Id == id);
        }

        public void Follow(int id)
        {
            this.GetArtist(id).Followers++;
            this.data.SaveChanges();
        }
    }
}