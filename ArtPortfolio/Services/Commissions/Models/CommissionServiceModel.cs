namespace ArtPortfolio.Services.Commissions.Models
{
    public class CommissionServiceModel
    {
        public int Id { get; init; }

        public string Title { get; set; }

        public string NoteFromClient { get; set; }

        public int CommissionType { get; set; }

        public int SceneryType { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsForCommercialUse { get; set; }

        public decimal Price { get; set; }

        public int ArtistId { get; init; }

        public int Status { get; set; }
    }
}