namespace ArtPortfolio.Services.Artworks.Models
{
    public class ArtworkServiceModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }
    }
}