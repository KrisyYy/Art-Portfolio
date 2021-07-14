using System.Collections;
using System.Collections.Generic;
using ArtPortfolio.Models.Comments;

namespace ArtPortfolio.Models.Artworks
{
    public class ArtViewModel
    {
        public string Id { get; set; }

        public string PictureUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Views { get; set; }

        public int Likes { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}