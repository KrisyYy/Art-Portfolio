using ArtPortfolio.Extensions;
using AutoMapper;
using Moq;

namespace ArtPortfolio.Tests.Mocks
{
    public static class MapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperConfig = new MapperConfiguration(cfg =>
                    cfg.AddProfile<MappingProfile>());

                return new Mapper(mapperConfig);
            }
        }
    }
}