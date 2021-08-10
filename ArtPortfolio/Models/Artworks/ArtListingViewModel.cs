namespace ArtPortfolio.Models.Artworks
{
    public class ArtListingViewModel
    {
        public int Id { get; set; }

        public int ArtistId { get; set; }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public int Likes { get; set; }

    }
}
