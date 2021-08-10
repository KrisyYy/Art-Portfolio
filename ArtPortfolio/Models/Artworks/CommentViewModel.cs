namespace ArtPortfolio.Models.Artworks
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int ArtworkId { get; set; }
        public string UserId { get; set; }
    }
}