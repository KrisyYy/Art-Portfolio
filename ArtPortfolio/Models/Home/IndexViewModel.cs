using System.Collections.Generic;
using ArtPortfolio.Models.Artists;
using ArtPortfolio.Models.Artworks;

namespace ArtPortfolio.Models.Home
{
    public class IndexViewModel
    {
        public List<ArtListingViewModel> Artworks { get; set; }
        public List<ArtistListingViewModel> Artists { get; set; }
    }
}