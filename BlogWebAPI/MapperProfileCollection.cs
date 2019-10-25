using AutoMapper;
using BlogWebAPI.Models;
using BlogWebAPI.ViewModels;


namespace BlogWebAPI
{
    public class MapperProfileCollection : Profile
    {
        public MapperProfileCollection()
        {
            CreateMap<Blog, BlogVM>().ReverseMap(); 
        }
    }
}
