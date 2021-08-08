using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ArtPortfolio.Data.Models.Enums;
using static ArtPortfolio.Data.DataConstants;

namespace ArtPortfolio.Models.Commissions
{
    public class CommissionRequestFormModel
    {
        public int ArtistId { get; set; }
        public int CommissionType { get; set; }

        public int SceneryType { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsForCommercialUse { get; set; }

        public ICollection<PropFormModel> Props { get; set; }

        [StringLength(DescriptionMaxLen, MinimumLength = DescriptionMinLen)]
        public string NoteFromClient { get; set; }
    }
}
