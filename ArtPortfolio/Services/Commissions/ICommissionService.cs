using System.Collections.Generic;
using ArtPortfolio.Models.Commissions;
using ArtPortfolio.Services.Commissions.Models;

namespace ArtPortfolio.Services.Commissions
{
    public interface ICommissionService
    {
        int ArtistId(int commId);

        int Create(
            string title,
            string noteFromClient,
            int commissionType,
            int sceneryType,
            bool isPrivate,
            bool isForCommercialUse,
            int artistId,
            decimal price
            );

        int AddProp(
            string name,
            int quantity,
            string description,
            int commissionId
            );

        CommissionServiceModel GetCommissionById(int id);

        List<PropInfoViewModel> GetProps(int id);

        void UpdateCommission(int id, int status);

        List<CommissionListingViewModel> GetListOfCommissions(int id);
    }
}