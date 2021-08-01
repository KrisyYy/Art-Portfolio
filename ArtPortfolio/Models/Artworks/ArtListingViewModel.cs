using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtPortfolio.Models.Artworks
{
    public class ArtListingViewModel
    {
        public string Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public int Likes { get; set; }

    }
}
