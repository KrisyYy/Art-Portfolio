using System.Collections.Generic;
using ArtPortfolio.Services.Artworks.Models;

namespace ArtPortfolio.Models.Artists
{
    public class ArtistViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string AvatarUrl { get; set; }
        
        public string Description { get; set; }

        public int Followers { get; set; }

        public bool IsFollowed { get; set; }

        public bool AvailableToCommission { get; set; }

        public List<ArtworkServiceModel> Artworks { get; set; }
    }
}