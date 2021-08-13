using System.ComponentModel.DataAnnotations;
using static ArtPortfolio.Data.DataConstants;

namespace ArtPortfolio.Models.Artworks
{
    public class CommentFormModel
    {
        [Required]
        [MaxLength(CommentMaxLen, ErrorMessage = "Comment must be shorter than {1}")]
        [MinLength(1, ErrorMessage = "Comment cannot be empty")]
        public string Content { get; set; }
    }
}