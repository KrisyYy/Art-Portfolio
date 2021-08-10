namespace ArtPortfolio.Data.Models
{
    public class Follow
    {
        public int ArtistId { get; set; }
        public Artist Artist { get; set; }
        public string UserId  { get; set; }
        public User User { get; set; }
    }
}