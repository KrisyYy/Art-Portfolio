using System.ComponentModel.DataAnnotations;
using static ArtPortfolio.Data.DataConstants;

namespace ArtPortfolio.Data.Models
{
    public class Prop
    {
        public int Id { get; init; }
        [Required]
        [MaxLength(NameMaxLen)]
        public string Name { get; set; }
        public int Quantity { get; set; } = 1;
        [MaxLength(DescriptionMaxLen)]
        public string Description { get; set; }

        public int CommissionId { get; init; }
        public Commission Commission { get; init; }
    }
}