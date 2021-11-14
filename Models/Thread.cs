using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum_dyskusyjne.Models
{
    public class Thread
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int ViewCount { get; set; }
        public bool IsPinned { get; set; }
        public string OwnerId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int ForumId { get; set; }
        public virtual Forum Forum { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

    }
}