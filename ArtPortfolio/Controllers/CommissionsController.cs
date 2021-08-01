using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtPortfolio.Models.Commissions;

namespace ArtPortfolio.Controllers
{
    public class CommissionsController : Controller
    {
        public IActionResult Info()
        {
            return View();
        }

        public IActionResult Request()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Request(CommissionRequestFormModel requestModel)
        {
            return RedirectToAction("Info", "Commission");
        }
    }
}
