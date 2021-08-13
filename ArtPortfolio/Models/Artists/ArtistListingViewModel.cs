using System.Collections.Generic;
using ArtPortfolio.Models.Artworks;

namespace ArtPortfolio.Models.Artists
{
    public class ArtistListingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public List<ArtistPartialArtViewModel> Artworks { get; set; }
        public bool IsFollowed { get; set; }
    }
}