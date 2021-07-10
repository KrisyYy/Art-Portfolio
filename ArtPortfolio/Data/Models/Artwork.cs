using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static ArtPortfolio.Data.DataConstants;

namespace ArtPortfolio.Data.Models
{
    public class Artwork
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(ArtTitleMaxLen)]
        public string Title { get; set; }

        [MaxLength(DescriptionMaxLen)]
        public string Description { get; set; }

        [Required] 
        public string ImageUrl { get; set; }

        public int Likes { get; set; } = 0;

        public int Views { get; set; } = 0;

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        //public int CreatorId { get; set; }

        //public User Creator { get; set; }
    }
}