using System.ComponentModel.DataAnnotations;
using static ArtPortfolio.Data.DataConstants;

namespace ArtPortfolio.Models.Artists
{
    public class SettingsFormModel
    {
        [StringLength(NameMaxLen, MinimumLength = NameMinLen, ErrorMessage = "{0} must be between {2} and {1} characters long")]
        public string Name { get; set; }

        [Url]
        public string AvatarUrl { get; set; }

        [StringLength(DescriptionMaxLen, MinimumLength = DescriptionMinLen, ErrorMessage = "{0} must be between {2} and {1} characters long")]
        public string Description { get; set; }

        public bool AvailableToCommission { get; set; }
    }
}