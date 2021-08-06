namespace ArtPortfolio.Models.Artists
{
    public class ArtistProfileViewModel
    {
        public string Name { get; set; }

        public string AvatarUrl { get; set; }
        
        public string Description { get; set; }

        public int Followers { get; set; }

        public bool AvailableToCommission { get; set; } = false;
    }
}