using ArtPortfolio.Data;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Data.Models.Enums;

namespace ArtPortfolio.Services.Commissions
{
    public class CommissionService : ICommissionService
    {
        private ArtPortfolioDbContext _data;

        public CommissionService(ArtPortfolioDbContext data)
        {
            _data = data;
        }

        public int Create(string noteFromClient, int commissionType, int sceneryType, bool isPrivate, bool isForCommercialUse, int artistId)
        {
            var commission = new Commission()
            {
                CommissionType = (CommissionType)commissionType,
                SceneryType = (SceneryType)sceneryType,
                IsForCommercialUse = isForCommercialUse,
                IsPrivate = isPrivate,
                ArtistId = artistId,
                NoteFromClient = noteFromClient
            };

            _data.Commissions.Add(commission);
            _data.SaveChanges();

            return commission.Id;
        }

        public int AddProp(string name, int quantity, string description, int commissionId)
        {
            var prop = new Prop()
            {
                Name = name,
                Quantity = quantity,
                Description = description,
                CommissionId = commissionId
            };

            _data.Props.Add(prop);
            _data.SaveChanges();

            return prop.Id;
        }
    }
}