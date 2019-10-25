using BlogWebAPI.CustomModels;
using BlogWebAPI.Models;
using System.Collections.Generic;

namespace BlogWebAPI.Repositories.Interfaces
{
    public interface IBlogRepository : IRepository<Blog>
    {
        /// <summary>
        /// Get the list of Blogs splitted as a pages
        /// </summary>
        /// <param name="skip">Skip value</param>
        /// <param name="pageSize">Row number for each page</param>
        /// <param name="search">Search value</param>
        /// <param name="total">The total number for blogs</param>
        /// <returns></returns>
        IEnumerable<BlogCM> GetAllBlogPaging(int skip, int pageSize, string search, out int total);
        /// <summary>
        /// Get one blog by Id
        /// </summary>
        /// <param name="Id">Blog Id</param>
        /// <returns></returns>
        BlogCM GetOneBlog(int Id);
    }
}
