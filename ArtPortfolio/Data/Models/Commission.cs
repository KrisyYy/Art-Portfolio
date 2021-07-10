using System;
using System.ComponentModel.DataAnnotations;
using ArtPortfolio.Data.Models.Enums;
using static ArtPortfolio.Data.DataConstants;

namespace ArtPortfolio.Data.Models
{
    public class Commission
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [MaxLength(DescriptionMaxLen)]
        public string NoteFromClient { get; set; }

        [Required]
        public CommissionType CommissionType { get; set; }

        public bool HasScenery { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsForCommercialUse { get; set; }

        public DateTime? Deadline { get; set; }

        //public decimal Price { get; set; }

        //public string ClientId { get; set; }

        //public User Client { get; set; }

        //public string ArtistId { get; set; }

        //public User Artist { get; set; }
    }
}