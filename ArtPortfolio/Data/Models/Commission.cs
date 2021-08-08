using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ArtPortfolio.Data.Models.Enums;
using static ArtPortfolio.Data.DataConstants;

namespace ArtPortfolio.Data.Models
{
    public class Commission
    {
        public int Id { get; init; }

        [MaxLength(DescriptionMaxLen)]
        public string NoteFromClient { get; set; }

        [Required]
        public CommissionType CommissionType { get; set; }

        [Required]
        public SceneryType SceneryType { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsForCommercialUse { get; set; }

        public ICollection<Prop> Props { get; set; } = new List<Prop>();

        public decimal Price { get; set; }

        public int ArtistId { get; init; }

        public Artist Artist { get; init; }

        public bool IsComplete { get; set; } = false;
    }
}