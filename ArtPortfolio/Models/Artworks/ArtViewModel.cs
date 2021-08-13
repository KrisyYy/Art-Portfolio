using System.Collections.Generic;

namespace ArtPortfolio.Models.Artworks
{
    public class ArtViewModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public int ArtistId { get; set; }

        public string ArtistName { get; set; }

        public string Description { get; set; }

        public int Views { get; set; }

        public int Likes { get; set; }

        public bool IsLiked { get; set; }

        public List<CommentViewModel> Comments { get; set; }
    }
}