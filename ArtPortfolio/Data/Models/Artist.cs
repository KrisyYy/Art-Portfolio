using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static ArtPortfolio.Data.DataConstants;

namespace ArtPortfolio.Data.Models
{
    public class Artist
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLen)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLen)]
        public string Description { get; set; }

        public int Followers { get; set; } = 0;

        public bool AvailableToCommission { get; set; } = false;

        public ICollection<Commission> Commissions { get; set; } = new List<Commission>();

        [Required]
        public string UserId { get; set; }

        public ICollection<Artwork> Artworks { get; set; } = new List<Artwork>();
    }
}
