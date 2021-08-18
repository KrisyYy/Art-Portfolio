using ArtPortfolio.Data.Models;
using ArtPortfolio.Models.Artworks;
using ArtPortfolio.Models.Commissions;
using ArtPortfolio.Services.Artworks.Models;
using ArtPortfolio.Services.Commissions.Models;
using AutoMapper;

namespace ArtPortfolio.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Artwork, ArtworkServiceModel>()
                .ForMember(a => a.Likes, cfg => cfg.MapFrom(a => a.Likes.Count))
                .ForMember(a => a.ArtistName, cfg => cfg.MapFrom(a => a.Artist.Name));

            this.CreateMap<Comment, CommentViewModel>()
                .ForMember(c => c.Username, cfg => cfg.MapFrom(c => c.User.UserName));

            this.CreateMap<ArtworkServiceModel, ArtCreateModel>();

            this.CreateMap<Commission, CommissionServiceModel>();

            this.CreateMap<Prop, PropInfoViewModel>();
        }
    }
}