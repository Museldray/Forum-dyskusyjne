using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Forum_dyskusyjne.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        public string Source { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}