using System.Collections.Generic;
using ArtPortfolio.Models.Artists;
using ArtPortfolio.Models.Artworks;
using ArtPortfolio.Services.Artworks.Models;

namespace ArtPortfolio.Models.Home
{
    public class IndexViewModel
    {
        public List<ArtworkServiceModel> Artworks { get; set; }
        public List<ArtistListingViewModel> Artists { get; set; }
    }
}