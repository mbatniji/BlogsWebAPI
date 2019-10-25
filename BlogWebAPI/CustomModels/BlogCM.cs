using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogWebAPI.CustomModels
{
    public class BlogCM
    {
        public int Id { get; set; }

        public string Author { get; set; }

        [Display(Name = "Author Mame")]
        public string AuthorMame { get; set; }

        public string Title { get; set; }

        [Display(Name = "Sub Title")]
        public string Subtitle { get; set; }

        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        public string Body { get; set; }

        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }
    }
}
