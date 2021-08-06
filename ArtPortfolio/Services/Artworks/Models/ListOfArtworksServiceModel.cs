namespace ArtPortfolio.Services.Artworks.Models
{
    public class ListOfArtworksServiceModel
    {
        public int Id { get; set; }
        public int ArtistId { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public int Likes { get; set; }
    }
}