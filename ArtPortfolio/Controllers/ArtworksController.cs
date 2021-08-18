using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ArtPortfolio.Extensions;
using ArtPortfolio.Models.Artworks;
using ArtPortfolio.Services.Artists;
using ArtPortfolio.Services.Artworks;
using ArtPortfolio.Services.Comments;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace ArtPortfolio.Controllers
{
    [Authorize]
    public class ArtworksController : Controller
    {
        private readonly IArtworkService _artworkService;
        private readonly IArtistService _artistService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public ArtworksController(IArtworkService artworkService, IArtistService artistService, IMapper mapper, ICommentService commentService)
        {
            _artworkService = artworkService;
            _artistService = artistService;
            _mapper = mapper;
            _commentService = commentService;
        }



        public IActionResult Art(int id)
        {
            var artwork = _artworkService.GetArtworkById(id);
            if (artwork == null)
            {
                return NotFound();
            }

            // TODO: not call if user was redirected to this page
            _artworkService.View(id);

            return View(new ArtViewModel()
            {
                Artwork = artwork,
                Comments = _commentService.GetListOfComments(id),
                IsLiked = _artworkService.IsLiked(id, this.User.Id())
            });
        }


        public IActionResult Delete(int id)
        {
            var userId = User.Id();
            var artistId = _artistService.GetIdByUser(userId);

            if (artistId != _artworkService.ArtistId(id) && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var delete = _artworkService.Delete(id);
            if (!delete)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Home");
        }


        public IActionResult All([FromQuery]AllArtworksViewModel artModel)
        {
            var artworks = _artworkService.GetListOfArtworks(artModel.Search, artModel.Order, artModel.Page, AllArtworksViewModel.ArtPerPage);

            artModel.Artworks = artworks.Artworks;
            artModel.MaxPage = artworks.MaxPage;

            return View(artModel);
        }
        

        public IActionResult Artworks(int id)
        {
            if (!_artistService.IsArtist(id))
            {
                return NotFound();
            }
            var artData = _artworkService.GetListOfArtworks();
            artData.Artworks = artData.Artworks
                .Where(a => a.ArtistId == id)
                .ToList();

            ViewBag.ArtistId = id;

            return View(artData);
        }

        
        public IActionResult Create()
        {
            var userId = this.User.Id();

            if (!_artistService.IsArtist(userId))
            {
                return RedirectToAction("BecomeArtist", "Artists");
            }

            return View();
        }


        [HttpPost]
        public IActionResult Create(ArtCreateModel artModel)
        {
            var userId = this.User.Id();
            var artistId = _artistService.GetIdByUser(userId);

            if (artistId == 0)
            {
                return RedirectToAction("BecomeArtist", "Artists");
            }

            if (!ModelState.IsValid)
            {
                return View(artModel);
            }

            var artId = _artworkService.CreateArtwork
            (
                artModel.Title,
                artModel.Description,
                artModel.ImageUrl,
                artistId
            );

            return RedirectToAction("Art", "Artworks", new {id = artId});
        }


        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!_artistService.IsArtist(userId) && !User.IsAdmin())
            {
                return RedirectToAction("BecomeArtist", "Artists");
            }

            var artwork = _artworkService.GetArtworkById(id);
            var artistId = _artistService.GetIdByUser(userId);

            if (artwork.ArtistId != artistId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var artForm = _mapper.Map<ArtCreateModel>(artwork);

            return View(artForm);
        }


        [HttpPost]
        public IActionResult Edit(int id, ArtCreateModel artModel)
        {
            var userId = this.User.Id();
            var artistId = _artistService.GetIdByUser(userId);

            if (artistId == 0 && !User.IsAdmin())
            {
                return RedirectToAction("BecomeArtist", "Artists");
            }

            if (!ModelState.IsValid)
            {
                return View(artModel);
            }

            if (artistId != _artworkService.ArtistId(id) || !User.IsAdmin())
            {
                return Unauthorized();
            }

            var editArtwork = _artworkService.EditArtwork
            (
                id,
                artModel.Title,
                artModel.Description,
                artModel.ImageUrl,
                artistId
            );

            if (!editArtwork)
            {
                return BadRequest();
            }

            return RedirectToAction("Art", "Artworks", new { id = id });
        }


        public IActionResult Like(int id)
        {
            if (_artworkService.GetArtworkById(id) == null)
            {
                return BadRequest();
            }
            _artworkService.Like(id, this.User.Id());

            return RedirectToAction("Art", "Artworks", new { id = id});
        }
    }
}
