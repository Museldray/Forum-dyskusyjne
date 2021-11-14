using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum_dyskusyjne.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        public string Name { get; set; }
        [Required]
        [AllowHtml]
        public string Content { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int ThreadId { get; set; }
        public virtual Thread Thread { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
    }
}