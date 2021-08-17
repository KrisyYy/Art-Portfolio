using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using ArtPortfolio.Extensions;
using ArtPortfolio.Models.Artworks;
using ArtPortfolio.Services.Artworks;
using ArtPortfolio.Services.Comments;

namespace ArtPortfolio.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IArtworkService _artworkService;

        public CommentController(ICommentService commentService, IArtworkService artworkService)
        {
            _commentService = commentService;
            _artworkService = artworkService;
        }



        [HttpPost]
        public IActionResult CreateComment(int id, CommentFormModel commentModel)
        {
            if (!ModelState.IsValid)
            {
                var artwork = _artworkService.GetArtworkById(id);
                if (artwork == null)
                {
                    return NotFound();
                }

                return View(
                    "~/Views/Artworks/Art.cshtml",
                    new ArtViewModel()
                    {
                        Artwork = artwork,
                        Comments = _commentService.GetListOfComments(id),
                        IsLiked = _artworkService.IsLiked(id, this.User.Id())
                    });
            }

            _commentService.CreateComment(commentModel.Content, id, this.User.Id());

            return RedirectToAction("Art", "Artworks", new { id = id });
        }


        public IActionResult DeleteComment(int id)
        {
            var userId = User.Id();
            if (userId != _commentService.UserId(id) && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var artId = _commentService.DeleteComment(id);

            if (artId == -1)
            {
                return BadRequest();
            }

            return RedirectToAction("Art", "Artworks", new { id = artId });
        }
    }
}
