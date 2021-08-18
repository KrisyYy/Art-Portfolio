using System;
using System.Collections.Generic;
using System.Linq;
using ArtPortfolio.Data;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Services.Artworks.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace ArtPortfolio.Services.Artworks
{
    public class ArtworkService : IArtworkService
    {
        private readonly ArtPortfolioDbContext _data;
        private readonly IMapper _mapper;

        public ArtworkService(ArtPortfolioDbContext data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }



        public ArtworkServiceModel GetArtworkById(int id)
        {
            return _data.Artworks
                .Where(a => a.Id == id)
                .ProjectTo<ArtworkServiceModel>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
        }


        public int ArtistId(int artId)
        {
            return _data.Artworks.Find(artId).ArtistId;
        }


        public bool IsLiked(int id, string userId)
        {
            return _data.Likes.Any(l => l.UserId == userId && l.ArtworkId == id);
        }


        public int CreateArtwork(string title, string description, string imageUrl, int artistId)
        {
            var artwork = new Artwork()
            {
                Title = title,
                Description = description,
                ImageUrl = imageUrl,
                ArtistId = artistId
            };

            _data.Artworks.Add(artwork);
            _data.SaveChanges();

            return artwork.Id;
        }


        public bool EditArtwork(int id, string title, string description, string imageUrl, int artistId)
        {
            var artwork = _data.Artworks.Find(id);

            if (artwork == null)
            {
                return false;
            }

            artwork.Title = title;
            artwork.Description = description;
            artwork.ImageUrl = imageUrl;
            
            _data.SaveChanges();

            return true;
        }


        public ArtworkListingServiceModel GetListOfArtworks(string search = null, int order = 1, int page = 1, int artPerPage = int.MaxValue)
        {
            var artworksQuery = _data.Artworks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                artworksQuery = artworksQuery
                    .Where(a => 
                        a.Title.ToLower().Contains(search) ||
                        a.Artist.Name.ToLower().Contains(search) ||
                        a.Description != null &&
                        a.Description.ToLower().Contains(search));
            }

            artworksQuery = order switch
            {
                0 => artworksQuery.OrderByDescending(a => a.Likes.Count),
                1 => artworksQuery.OrderByDescending(a => a.Id),
                2 => artworksQuery.OrderBy(a => a.Title),
                _ => artworksQuery
            };
            var maxPage = 1;
            if (artworksQuery.Count() > artPerPage)
            {
                maxPage = (int)Math.Ceiling((double)artworksQuery.Count() / artPerPage);
            }

            artworksQuery = artworksQuery
                .Skip((page - 1) * artPerPage)
                .Take(artPerPage);

            var artworks = GetArtworks(artworksQuery);

            return new ArtworkListingServiceModel()
            {
                Artworks = artworks,
                MaxPage = maxPage
            };
        }


        public List<ArtworkServiceModel> ArtworksFromFollowed(string userId)
        {
            return GetArtworks(_data.Artworks
                .Where(a => a.Artist.Follows.Any(f => f.UserId == userId))
                .OrderByDescending(a => a.Id)
                .Take(3));
        }


        public void Like(int id, string userId)
        {
            var like = _data.Likes.FirstOrDefault(l => l.UserId == userId && l.ArtworkId == id);
            if (like != null)
            {
                _data.Likes.Remove(like);
                _data.SaveChanges();
                return;
            }
            like = new Like()
            {
                ArtworkId = id,
                UserId = userId
            };
            _data.Likes.Add(like);
            _data.SaveChanges();
        }


        public void View(int id)
        {
            var artwork = _data.Artworks.FirstOrDefault(a => a.Id == id);
            if (artwork == null)
            {
                return;
            }  
            artwork.Views++;
            _data.SaveChanges();
        }


        public bool Delete(int id)
        {
            var artwork = _data.Artworks.FirstOrDefault(a => a.Id == id);
            if (artwork == null)
            {
                return false;
            }

            _data.Artworks.Remove(artwork);
            _data.SaveChanges();

            return true;
        }



        private List<ArtworkServiceModel> GetArtworks(IQueryable<Artwork> artworks)
        {
            return artworks
                .ProjectTo<ArtworkServiceModel>(_mapper.ConfigurationProvider)
                .ToList();
        }
    }
}
