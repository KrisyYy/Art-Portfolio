using System;
using System.Collections.Generic;
using ArtPortfolio.Data.Models;
using ArtPortfolio.Data.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using ArtPortfolio.Models.Commissions;
using ArtPortfolio.Services.Artists;
using ArtPortfolio.Services.Commissions;
using Microsoft.AspNetCore.Authorization;

namespace ArtPortfolio.Controllers
{
    [Authorize]
    public class CommissionsController : Controller
    {
        private readonly IArtistService _artistService;
        private readonly ICommissionService _commissionService;

        public CommissionsController(IArtistService artistService, ICommissionService commissionService)
        {
            _artistService = artistService;
            _commissionService = commissionService;
        }
        
        public IActionResult Info(int id)
        {
            var data = _commissionService.GetCommission(id);
            if (data == null)
            {
                return BadRequest();
            }

            var artistName = _artistService.GetName(data.ArtistId);
            ViewBag.ArtistName = artistName;

            return View(data);
        }
        

        public IActionResult Commission(int id)
        {
            var artistName = _artistService.GetName(id);
            ViewBag.ArtistName = artistName;
            ViewBag.ArtistId = id;
            return View();
        }

        [HttpPost]
        public IActionResult Commission(CommissionRequestFormModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                return View(requestModel);
            }

            var commId = _commissionService.Create(
                requestModel.Title,
                requestModel.NoteFromClient, 
                requestModel.CommissionType, 
                requestModel.SceneryType,
                requestModel.IsPrivate, 
                requestModel.IsForCommercialUse,
                requestModel.ArtistId,
                requestModel.Price
            );

            if (requestModel.Props != null)
            {
                foreach (var prop in requestModel.Props)
                {
                    _commissionService.AddProp(
                        prop.Name,
                        prop.Quantity,
                        prop.Description,
                        commId
                    );
                }
            }

            return RedirectToAction("Info", "Commissions", new {id = commId});
        }

        public IActionResult MyCommissions(int id)
        {
            var data = _commissionService.GetListOfCommissions(id);

            return View(data);
        }

        public IActionResult UpdateCommission(int id)
        {
            _commissionService.UpdateCommission(id);

            return RedirectToAction("Info", "Commissions", new { id = id });
        }
    }
}
