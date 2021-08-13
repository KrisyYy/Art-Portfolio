using System.Collections.Generic;

namespace ArtPortfolio.Models.Artworks
{
    public class AllArtworksViewModel
    {
        public List<ArtListingViewModel> Artworks { get; set; }

        public string Search { get; set; }

        public string Order { get; set; }
    }
}