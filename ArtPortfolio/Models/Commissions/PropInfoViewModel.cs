namespace ArtPortfolio.Models.Commissions
{
    public class PropInfoViewModel
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public string Description { get; set; }

        public int CommissionId { get; init; }
    }
}