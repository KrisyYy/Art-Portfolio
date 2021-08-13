using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ArtPortfolio.Data.Models.Enums;
using static ArtPortfolio.Data.DataConstants;

namespace ArtPortfolio.Models.Commissions
{
    public class CommissionRequestFormModel
    {
        public int ArtistId { get; set; }

        [Required]
        [MaxLength(TitleMaxLen, ErrorMessage = "Title field must be shorter than {1}")]
        [MinLength(TitleMinLen, ErrorMessage = "Title field must be longer than {1}")]
        public string Title { get; set; }

        public int CommissionType { get; set; }

        public int SceneryType { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsForCommercialUse { get; set; }

        public List<PropFormModel> Props { get; set; }

        [MaxLength(DescriptionMaxLen, ErrorMessage = "Description field must be shorter than {1}")]
        [MinLength(DescriptionMinLen, ErrorMessage = "Description field must be longer than {1}")]
        public string NoteFromClient { get; set; }

        public decimal Price { get; set; }
    }
}
