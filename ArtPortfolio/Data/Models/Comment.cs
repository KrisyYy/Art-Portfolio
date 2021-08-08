using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static ArtPortfolio.Data.DataConstants;

namespace ArtPortfolio.Data.Models
{
    public class Comment
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(CommentMaxLen)]
        public string Content { get; set; }

        public int ArtworkId { get; init; }

        public Artwork Artwork { get; init; }

        [Required]
        public string UserId { get; init; }

        public User User { get; init; }
    }
}