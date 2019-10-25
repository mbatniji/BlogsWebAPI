using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebAPI.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ImageUrl { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
