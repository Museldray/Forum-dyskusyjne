using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum_dyskusyjne.Models
{
    public class Moderator
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int ForumId { get; set; }
        public virtual Forum Forum { get; set; }
    }
}