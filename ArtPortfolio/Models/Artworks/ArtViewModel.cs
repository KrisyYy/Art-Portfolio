using System.Collections.Generic;
using ArtPortfolio.Services.Artworks.Models;

namespace ArtPortfolio.Models.Artworks
{
    public class ArtViewModel
    {
        public ArtworkServiceModel Artwork { get; set; }

        public bool IsLiked { get; set; }

        public List<CommentViewModel> Comments { get; set; }
    }
}