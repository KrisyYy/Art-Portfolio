using System;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Data.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using ArtPortfolio.Models.Commissions;
using ArtPortfolio.Services.Artists;
using Microsoft.AspNetCore.Authorization;

namespace ArtPortfolio.Controllers
{
    public class CommissionsController : Controller
    {
        private readonly IArtistService _artistService;

        public CommissionsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [Authorize]
        public IActionResult Info(int id)
        {
            return View();
        }

        [Authorize]
        public IActionResult Commission(int id)
        {
            var artistName = _artistService.GetName(id);
            ViewBag.ArtistName = artistName;
            ViewBag.ArtistId = id;
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Commission(CommissionRequestFormModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var commission = new Commission()
            {
                CommissionType = (CommissionType) requestModel.CommissionType,
                SceneryType = (SceneryType) requestModel.SceneryType,
                IsForCommercialUse = requestModel.IsForCommercialUse,
                IsPrivate = requestModel.IsPrivate,
                ArtistId = requestModel.ArtistId,
                NoteFromClient = requestModel.NoteFromClient
            };

            return RedirectToAction("Info", "Commissions", new {id = commission.Id});
        }

        [Authorize]
        public IActionResult MyCommissions(int id)
        {
            return View();
        }
    }
}
