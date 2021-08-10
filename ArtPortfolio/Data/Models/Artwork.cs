using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static ArtPortfolio.Data.DataConstants;

namespace ArtPortfolio.Data.Models
{
    public class Artwork
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(TitleMaxLen)]
        public string Title { get; set; }

        [MaxLength(DescriptionMaxLen)]
        public string Description { get; set; }

        [Required] 
        public string ImageUrl { get; set; }
        
        public ICollection<Like> Likes { get; set; } = new List<Like>();

        public int Views { get; set; } = 0;

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public int ArtistId { get; init; }

        public Artist Artist { get; init; }
    }
}