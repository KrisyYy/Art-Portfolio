using System.Collections.Generic;
using System.Linq;
using ArtPortfolio.Data;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Models.Artworks;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace ArtPortfolio.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly ArtPortfolioDbContext _data;
        private readonly IMapper _mapper;

        public CommentService(ArtPortfolioDbContext data, IMapper mapper)
        {
            _data = data;
            _mapper = mapper;
        }

        public string Username(string id)
        {
            return _data.Users.Find(id).UserName;
        }

        public string UserId(int commentId)
        {
            return _data.Comments.Find(commentId).UserId;
        }


        public List<CommentViewModel> GetListOfComments(int id)
            => GetComments(_data.Comments.Where(c => c.ArtworkId == id));


        public int DeleteComment(int id)
        {
            var comment = _data.Comments.FirstOrDefault(a => a.Id == id);
            if (comment == null)
            {
                return -1;
            }

            var artId = comment.ArtworkId;

            _data.Comments.Remove(comment);
            _data.SaveChanges();

            return artId;
        }


        public void CreateComment(string content, int artworkId, string userId)
        {
            var comment = new Comment()
            {
                Content = content,
                ArtworkId = artworkId,
                UserId = userId
            };

            _data.Comments.Add(comment);
            _data.SaveChanges();
        }



        private List<CommentViewModel> GetComments(IQueryable<Comment> comments)
            => comments
                .ProjectTo<CommentViewModel>(_mapper.ConfigurationProvider)
                .ToList();
    }
}