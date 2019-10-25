using BlogWebAPI.CustomModels;
using BlogWebAPI.Data;
using BlogWebAPI.Models;
using BlogWebAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlogWebAPI.Repositories.Core
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        public BlogRepository(ApplicationDbContext context) : base(context)
        {
        }
        public ApplicationDbContext BlogDBContext
        {
            get { return Context as ApplicationDbContext; }
        }
        public IEnumerable<BlogCM> GetAllBlogPaging(int skip, int pageSize, string search,out int total)
        {
            var blogList = BlogDBContext.Blog
                .Where(x => (string.IsNullOrEmpty(search) || 
                x.Title.Contains(search) || 
                x.Body.Contains(search) ||
                x.Subtitle.Contains(search)))
                .OrderByDescending(x=>x.Id).ToList();

            total = blogList.Count();

            var RetList = new List<BlogCM>();
            foreach(var raw in blogList.Skip(skip).Take(pageSize).ToList())
            {
                RetList.Add(new BlogCM
                {
                    Id = raw.Id,
                    Author = raw.Author,
                    AuthorMame = BlogDBContext.Users.Find(raw.Author)?.UserName,
                    Title = raw.Title,
                    Subtitle = raw.Subtitle,
                    CreateDate = raw.CreateDate
                });
            }
            return RetList;
        }

        public BlogCM GetOneBlog(int Id)
        {
            var blogData = BlogDBContext.Blog.Find(Id);
            if (blogData == null)
            {
                return null;
            }

            var RetList =new BlogCM
                {
                    Id = blogData.Id,
                    Author = blogData.Author,
                    AuthorMame = BlogDBContext.Users.Find(blogData.Author)?.UserName,
                    Title = blogData.Title,
                    Subtitle = blogData.Subtitle,
                    CreateDate = blogData.CreateDate,
                    ImageUrl= blogData.ImageUrl,
                    Body= blogData.Body
                    

            };
           
            return RetList;
        }
    }
}