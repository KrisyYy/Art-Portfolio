using System.ComponentModel.DataAnnotations;
using static ArtPortfolio.Data.DataConstants;

namespace ArtPortfolio.Models.Commissions
{
    public class PropFormModel
    {
        [Required]
        [StringLength(NameMaxLen, MinimumLength = NameMinLen)]
        public string Name { get; set; }
        public int Quantity { get; set; }
        [StringLength(DescriptionMaxLen, MinimumLength = DescriptionMinLen)]
        public string Description { get; set; }
    }
}