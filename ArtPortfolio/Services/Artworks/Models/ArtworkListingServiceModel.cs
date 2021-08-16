using System.Collections.Generic;

namespace ArtPortfolio.Services.Artworks.Models
{
    public class ArtworkListingServiceModel
    {
        public int MaxPage { get; set; }
        public List<ArtworkServiceModel> Artworks { get; set; }
    }
}