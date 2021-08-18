using System.Collections.Generic;
using System.Linq;
using ArtPortfolio.Data;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Data.Models.Enums;
using ArtPortfolio.Models.Commissions;
using ArtPortfolio.Services.Commissions.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace ArtPortfolio.Services.Commissions
{
    public class CommissionService : ICommissionService
    {
        private readonly ArtPortfolioDbContext _data;
        private readonly IMapper _mapper;

        public CommissionService(ArtPortfolioDbContext data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }



        public int ArtistId(int commId)
        {
            return _data.Commissions.Find(commId).ArtistId;
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


        public CommissionServiceModel GetCommissionById(int id)
        {
            return _data.Commissions
                .Where(c => c.Id == id)
                .ProjectTo<CommissionServiceModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }


        public List<PropInfoViewModel> GetProps(int id)
        {
            return _data.Props
                .Where(p => p.CommissionId == id)
                .ProjectTo<PropInfoViewModel>(_mapper.ConfigurationProvider)
                .ToList();
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