namespace ArtPortfolio.Services.Commissions
{
    public interface ICommissionService
    {
        int Create(
            string noteFromClient,
            int commissionType,
            int sceneryType,
            bool isPrivate,
            bool isForCommercialUse,
            int artistId
            );

        int AddProp(
            string name,
            int quantity,
            string description,
            int commissionId
            );
    }
}