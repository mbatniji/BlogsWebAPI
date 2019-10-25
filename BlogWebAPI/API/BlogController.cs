using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BlogWebAPI.CustomModels;
using BlogWebAPI.Enum;
using BlogWebAPI.Hubs;
using BlogWebAPI.Models;
using BlogWebAPI.Repositories.Interfaces;
using BlogWebAPI.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace BlogWebAPI.API
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/v1/[controller]")]
    public class BlogController : Controller
    {
        private readonly IHttpContextAccessor _httpContext;
        protected readonly string _userId;
        private readonly IMapper _mapper;
        private readonly IBlogRepository _blogRepository;
        private readonly ILogger<AuthController> _logger;
        private readonly IHubContext<ChatHub> _hub;
        public BlogController(
        IHubContext<ChatHub> hub,
          ILogger<AuthController> logger,
          IBlogRepository blogRepository,
          IHttpContextAccessor httpContext,
          IMapper mapper
            )
        {
            _mapper = mapper;
            _hub = hub;
            _logger = logger;
            _httpContext = httpContext;
            _blogRepository = blogRepository;

            if (_httpContext.HttpContext.User != null)
            {
                var claimsIdentity = _httpContext.HttpContext.User.Identity as ClaimsIdentity;
                var claims = claimsIdentity.Claims.Select(x => new { type = x.Type, value = x.Value });
                _userId = claims.FirstOrDefault(x => x.type == "UserId")?.value;

            }
        }

        /// <summary>
        /// Get the list of Blogs splitted as a pages
        /// </summary>
        /// <param name="Page">Page number</param>
        /// <param name="SearchValue">search value parameter</param>
        /// <returns></returns>
        public ActionResult GetBlogItem(int Page = 1, string SearchValue = "")
        {
            var _takeDefault = 20;
            var skip = (Page - 1) * _takeDefault;
            var take = _takeDefault;
            var blogList = _blogRepository.GetAllBlogPaging(skip, take, SearchValue, out int outputvalue);

            return Ok(
                          new APIResponse
                          {
                              status = true,
                              statuscode = (int)API_StatusCode.Success,
                              items = new { TotalCount = outputvalue, Blogs = blogList }
                          });
        }

        /// <summary>
        /// Get one Blog by Id
        /// GET: api/BlogItem/5
        /// </summary>
        /// <param name="id">Blog Id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlogItem(int id)
        {
            var blogItem = await _blogRepository.Find(id);

            if (blogItem == null)
            {
                return NotFound(
                    new APIResponse
                    {
                        status = false,
                        message = "Sorry!! couldn't found the Data",
                        statuscode = (int)API_StatusCode.Error,
                    });
            }

            return Ok(
                          new APIResponse
                          {
                              status = true,
                              statuscode = (int)API_StatusCode.Success,
                              items = blogItem
                          });
        }

        /// <summary>
        /// Add new Blog
        /// </summary>
        /// <param name="blogItem">BLog model</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Blog>> PostBlogItem(BlogVM blogItem)
        {
            if (ModelState.IsValid)
            {
                await _hub.Clients.All.SendAsync("notifyUser_broadcast", "New Blog", blogItem.Title, "Add");

                blogItem.CreateDate = DateTime.Now;
                blogItem.Author= _userId;
                var model = _mapper.Map<Blog>(blogItem);
             
                return await _blogRepository.Add(model) > 0
                    ? Ok(
                          new APIResponse
                          {
                              status = true,
                              statuscode = (int)API_StatusCode.Success,
                              items = model
                          })
                    : (ActionResult<Blog>)UnprocessableEntity(

                             new APIResponse
                             {
                                 status = false,
                                 message = "Invalid Data",
                                 statuscode = (int)API_StatusCode.Error,
                             });

            }
            else
            {
                return UnprocessableEntity(

                     new APIResponse
                     {
                         status = false,
                         statuscode = (int)API_StatusCode.Error,
                         message = "Model State Error",
                         errorlist = ModelState.Where(e => e.Value.Errors.Count > 0).Select(e => new { Name = e.Key, Message = e.Value.Errors.First().ErrorMessage }).ToList()


                     });

            }
            
        }

        /// <summary>
        /// Update Blog
        /// </summary>
        /// <param name="id">Blog Id</param>
        /// <param name="blogItem">Blog model</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<Blog>> PutBlogItem(int id, BlogVM blogItem)
        {
            if (id != blogItem.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {

                await _hub.Clients.All.SendAsync("notifyUser_broadcast", "Update Blog", blogItem.Title, "Update");

                blogItem.CreateDate = DateTime.Now;
                blogItem.Author = _userId;
                var model = _mapper.Map<Blog>(blogItem);
                return await _blogRepository.Update(model) > 0
                    ? Ok(
                          new APIResponse
                          {
                              status = true,
                              statuscode = (int)API_StatusCode.Success,
                              items = model
                          })
                    : (ActionResult<Blog>)UnprocessableEntity(

                             new APIResponse
                             {
                                 status = false,
                                 message = "Invalid Data",
                                 statuscode = (int)API_StatusCode.Error
                             });
            }
            else
            {
                return UnprocessableEntity(

                     new APIResponse
                     {
                         status = false,
                         statuscode = (int)API_StatusCode.Error,
                         message = "Model State Error",
                         errorlist = ModelState.Where(e => e.Value.Errors.Count > 0).Select(e => new { Name = e.Key, Message = e.Value.Errors.First().ErrorMessage }).ToList()


                     });

            }
        }

        /// <summary>
        /// Delete Blog
        /// </summary>
        /// <param name="id">BLog Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Blog>> DeleteBlogItem(int id)
        {
            var blogItem = await _blogRepository.Find(id);
            if (blogItem == null)
            {
                return NotFound(
                    new APIResponse
                    {
                        status = false,
                        message = "Sorry!! couldn't found the Data",
                        statuscode = (int)API_StatusCode.Error,
                    });
            }
            await _hub.Clients.All.SendAsync("notifyUser_broadcast", "Delete Blog", blogItem.Title,"Delete");
            return await _blogRepository.Remove(blogItem) > 0
               ? Ok(
                            new APIResponse
                            {
                                status = true,
                                statuscode = (int)API_StatusCode.Success,
                                message = "Data deleted successfully"
                            })
                      : (ActionResult<Blog>)UnprocessableEntity(

                               new APIResponse
                               {
                                   status = false,
                                   message = "Invalid Data",
                                   statuscode = (int)API_StatusCode.Error,
                               });
        }
    }
}