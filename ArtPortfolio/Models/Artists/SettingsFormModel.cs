using System.ComponentModel.DataAnnotations;
using static ArtPortfolio.Data.DataConstants;

namespace ArtPortfolio.Models.Artists
{
    public class SettingsFormModel
    {
        [StringLength(NameMaxLen, MinimumLength = NameMinLen)]
        public string Name { get; set; }

        [Url]
        public string AvatarUrl { get; set; }

        [StringLength(DescriptionMaxLen, MinimumLength = DescriptionMinLen)]
        public string Description { get; set; }

        public bool AvailableToCommission { get; set; }
    }
}