using Microsoft.AspNetCore.Mvc;
using ArtPortfolio.Extensions;
using ArtPortfolio.Models.Artists;
using ArtPortfolio.Services.Artists;
using Microsoft.AspNetCore.Authorization;

namespace ArtPortfolio.Controllers
{
    [Authorize]
    public class ArtistsController : Controller
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }
        
        public IActionResult BecomeArtist()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BecomeArtist(BecomeArtistFormModel artistModel)
        {
            var currentUserId = this.User.GetId();
            if (_artistService.IsArtist(currentUserId))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(artistModel);
            }

            var artistId = _artistService.CreateArtist
            (
                artistModel.Name, 
                artistModel.Description, 
                currentUserId
            );

            return RedirectToAction("Profile", "Artists", new {id = artistId});
        }
        
        public IActionResult Profile(int id)
        {
            var artist = _artistService.GetArtistById(id, this.User.GetId());
            if (artist == null)
            {
                return BadRequest();
            }

            return View(artist);
        }
        
        public IActionResult Follow(int id)
        {
            _artistService.Follow(id, this.User.GetId());

            return RedirectToAction("Profile", "Artists", new {id = id});
        }

        public IActionResult Settings()
        {
            var userId = this.User.GetId();
            var artistId = _artistService.GetIdByUser(userId);
            var artist = _artistService.GetArtistById(artistId, userId);

            var data = new SettingsFormModel()
            {
                Name = artist.Name,
                AvatarUrl = artist.AvatarUrl,
                Description = artist.Description,
                AvailableToCommission = artist.AvailableToCommission
            };

            return View(data);
        }

        [HttpPost]
        public IActionResult ChangeAvatar(string avatarUrl)
        {
            var userId = this.User.GetId();
            var artistId = _artistService.ChangeAvatar(userId, avatarUrl);

            return RedirectToAction("Profile", "Artists", new { id = artistId });
        }

        [HttpPost]
        public IActionResult ChangeName(string name)
        {
            var userId = this.User.GetId();
            var artistId = _artistService.ChangeName(userId, name);

            return RedirectToAction("Profile", "Artists", new { id = artistId });
        }

        [HttpPost]
        public IActionResult ChangeDescription(string description)
        {
            var userId = this.User.GetId();
            var artistId = _artistService.ChangeDescription(userId, description);

            return RedirectToAction("Profile", "Artists", new { id = artistId });
        }

        [HttpPost]
        public IActionResult ToggleAvailable()
        {
            var userId = this.User.GetId();
            var artistId = _artistService.ToggleAvailable(userId);

            return RedirectToAction("Profile", "Artists", new { id = artistId });
        }
    }
}
