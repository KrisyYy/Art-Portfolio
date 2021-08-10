using System.Collections.Generic;
using ArtPortfolio.Models.Commissions;

namespace ArtPortfolio.Services.Commissions
{
    public interface ICommissionService
    {
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

        CommissionInfoViewModel GetCommission(int id);

        void UpdateCommission(int id);

        List<CommissionListingViewModel> GetListOfCommissions(int id);
    }
}