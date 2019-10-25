using System;
using System.Collections.Generic;
using System.Text;
using BlogWebAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogWebAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Blog> Blog { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region "Seed Data"



            builder.Entity<Blog>().HasData(
                    new {Id=1, Author = "e803093a-0c3e-44dc-bdd0-1cf4190d4c1a", Title = "Text", Subtitle = "Sub Text", ImageUrl= "https://media.licdn.com/dms/image/C4D0BAQHXLZiGuQOGhA/company-logo_200_200/0?e=2159024400&v=beta&t=IR3o7r2Lq_ZfG15XdvBDwmmFWnKpP9cU0yFP1z1zlXw", Body="body", CreateDate=DateTime.Now },
                     new {Id=2, Author = "e803093a-0c3e-44dc-bdd0-1cf4190d4c1a", Title = "Text2", Subtitle = "Sub Text2", ImageUrl = "https://media.licdn.com/dms/image/C4D0BAQHXLZiGuQOGhA/company-logo_200_200/0?e=2159024400&v=beta&t=IR3o7r2Lq_ZfG15XdvBDwmmFWnKpP9cU0yFP1z1zlXw", Body = "body2", CreateDate = DateTime.Now }
                );

            #endregion
        }
    }
}
