using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum_dyskusyjne.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50), MinLength(5)]
        public string Name { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        public bool IsRead { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public virtual ICollection<MessageUser> Users { get; set; }
    }
}