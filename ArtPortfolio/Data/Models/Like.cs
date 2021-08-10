namespace ArtPortfolio.Data.Models
{
    public class Like
    {
        public int ArtworkId { get; set; }
        public Artwork Artwork { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}