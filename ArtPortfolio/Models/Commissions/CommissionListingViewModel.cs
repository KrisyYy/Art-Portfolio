namespace ArtPortfolio.Models.Commissions
{
    public class CommissionListingViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public decimal Price { get; set; }
        public bool IsComplete { get; set; }
    }
}