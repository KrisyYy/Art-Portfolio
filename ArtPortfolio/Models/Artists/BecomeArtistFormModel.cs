using System.ComponentModel.DataAnnotations;
using static ArtPortfolio.Data.DataConstants;

namespace ArtPortfolio.Models.Artists
{
    public class BecomeArtistFormModel
    {
        [Required]
        [StringLength(NameMaxLen, MinimumLength = NameMinLen)]
        public string Name { get; set; }

        [StringLength(DescriptionMaxLen, MinimumLength = DescriptionMinLen)]
        public string Description { get; set; }
    }
}
