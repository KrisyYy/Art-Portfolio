using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtPortfolio.Services.Commissions.Models;

namespace ArtPortfolio.Models.Commissions
{
    public class CommissionInfoViewModel
    {
        public CommissionServiceModel Commission { get; set; }

        public List<PropInfoViewModel> Props { get; set; }
    }
}
