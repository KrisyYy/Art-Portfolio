using System.ComponentModel.DataAnnotations;
using static ArtPortfolio.Data.DataConstants;

namespace ArtPortfolio.Models.Artists
{
    public class BecomeArtistFormModel
    {
        [Required]
        [MaxLength(NameMaxLen, ErrorMessage = "Name field must be shorter than {1}")]
        [MinLength(NameMinLen, ErrorMessage = "Name field must be longer than {1}")]
        public string Name { get; set; }

        [MaxLength(DescriptionMaxLen, ErrorMessage = "Description field must be shorter than {1}")]
        [MinLength(DescriptionMinLen, ErrorMessage = "Description field must be longer than {1}")]
        public string Description { get; set; }
    }
}
