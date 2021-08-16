using System.Collections.Generic;
using ArtPortfolio.Services.Artworks.Models;

namespace ArtPortfolio.Models.Artworks
{
    public class AllArtworksViewModel
    {
        public const int ArtPerPage = 12;

        public int Page { get; set; } = 1;

        public int MaxPage { get; set; }

        public List<ArtworkServiceModel> Artworks { get; set; }

        public string Search { get; set; }

        public int Order { get; set; }
    }
}