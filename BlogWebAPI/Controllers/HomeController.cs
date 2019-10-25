using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlogWebAPI.Models;
using BlogWebAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BlogWebAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogRepository _blogRepository;
        public HomeController(ILogger<HomeController> logger, IBlogRepository blogRepository)
        {
            _logger = logger;
            _blogRepository = blogRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Blogs()
        {
            return View();
        }  
      
        [Authorize]
        [HttpPost]
        public IActionResult Blogs(string val)
        {
            var requestFormData = Request.Form;
            var search = requestFormData["search[value]"];
            var Skip = Convert.ToInt32(requestFormData["start"].ToString());
            var pageSize = Convert.ToInt32(requestFormData["length"].ToString());

            var total = 0;
            var listItems = _blogRepository.GetAllBlogPaging(Skip, pageSize, search, out total);
            dynamic response = new
            {
                Data = listItems,
                Draw = requestFormData["draw"],
                RecordsFiltered = total,
                RecordsTotal = total
            };
            return Ok(response);
        }

        [Authorize]
        public IActionResult BlogDetails(int Id)
        {
            if (Id == 0)
            {
                return NotFound();
            }
            var model = _blogRepository.GetOneBlog(Id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
