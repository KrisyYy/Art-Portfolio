using System.ComponentModel.DataAnnotations;
using static ArtPortfolio.Data.DataConstants;

namespace ArtPortfolio.Models.Artworks
{
    public class CommentFormModel
    {
        [Required]
        [StringLength(CommentMaxLen, MinimumLength = 1)]
        public string Content { get; set; }
    }
}