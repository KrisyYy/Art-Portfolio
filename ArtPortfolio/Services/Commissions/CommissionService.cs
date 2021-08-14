using System.Collections.Generic;
using System.Linq;
using ArtPortfolio.Data;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Data.Models.Enums;
using ArtPortfolio.Models.Commissions;

namespace ArtPortfolio.Services.Commissions
{
    public class CommissionService : ICommissionService
    {
        private readonly ArtPortfolioDbContext _data;

        public CommissionService(ArtPortfolioDbContext data)
        {
            _data = data;
        }

        public int Create(string title, string noteFromClient, int commissionType, int sceneryType, bool isPrivate, bool isForCommercialUse, int artistId, decimal price)
        {
            var commission = new Commission()
            {
                Title = title,
                CommissionType = (CommissionType)commissionType,
                SceneryType = (SceneryType)sceneryType,
                IsForCommercialUse = isForCommercialUse,
                IsPrivate = isPrivate,
                ArtistId = artistId,
                NoteFromClient = noteFromClient,
                Price = price
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

        public CommissionInfoViewModel GetCommission(int id)
        {
            var commission = _data.Commissions.FirstOrDefault(c => c.Id == id);
            if (commission == null)
            {
                return null;
            }

            var props = _data.Props.Where(p => p.CommissionId == commission.Id);

            var commissionData = new CommissionInfoViewModel()
            {
                Id = commission.Id,
                Title = commission.Title,
                CommissionType = (int) commission.CommissionType,
                SceneryType = (int) commission.SceneryType,
                IsForCommercialUse = commission.IsForCommercialUse,
                IsPrivate = commission.IsPrivate,
                NoteFromClient = commission.NoteFromClient,
                Price = commission.Price,
                Status = (int) commission.Status,
                ArtistId = commission.ArtistId,
                Props = props.Select(p => new PropInfoViewModel()
                {
                    Name = p.Name,
                    Quantity = p.Quantity,
                    Description = p.Description,
                    CommissionId = commission.Id
                }).ToList()
            };

            return commissionData;
        }

        public void UpdateCommission(int id, int status)
        {
            var commission = _data.Commissions.FirstOrDefault(c => c.Id == id);
            if (commission == null)
                return;

            commission.Status = (Status)status;
            commission.IsComplete = commission.Status == Status.Finished;

            _data.SaveChanges();
        }

        public List<CommissionListingViewModel> GetListOfCommissions(int id)
        {
            return _data.Commissions
                .Where(c => c.ArtistId == id)
                .Select(c => new CommissionListingViewModel()
                {
                    Id = c.Id,
                    IsComplete = c.IsComplete,
                    Price = c.Price,
                    Status = (int)c.Status,
                    Title = c.Title
                }).ToList();
        }
    }
}