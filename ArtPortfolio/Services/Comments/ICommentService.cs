using System.Collections.Generic;
using ArtPortfolio.Models.Artworks;

namespace ArtPortfolio.Services.Comments
{
    public interface ICommentService
    {
        string UserId(int commentId);

        List<CommentViewModel> GetListOfComments(int id);

        int DeleteComment(int id);

        void CreateComment(
            string content,
            int artworkId,
            string userId
            );
    }
}