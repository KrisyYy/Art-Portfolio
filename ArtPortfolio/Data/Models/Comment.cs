using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static ArtPortfolio.Data.DataConstants;

namespace ArtPortfolio.Data.Models
{
    public class Comment
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(CommentMaxLen)]
        public string Content { get; set; }

        public int Likes { get; set; } = 0;

        //public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        //public int AuthorId { get; set; }

        //public User Author { get; set; }
    }
}