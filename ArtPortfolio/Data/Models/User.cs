using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ArtPortfolio.Data.Models
{
    public class User : IdentityUser
    {
        public DateTime DateOfRegistration { get; init; } = DateTime.UtcNow;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Like> Liked { get; set; } = new List<Like>();
        public ICollection<Follow> Followed { get; set; } = new List<Follow>();
    }
}
