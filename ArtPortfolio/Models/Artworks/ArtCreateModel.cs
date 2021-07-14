using System.ComponentModel.DataAnnotations;

namespace ArtPortfolio.Models.Artworks
{
    public class ArtCreateModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

    }
}