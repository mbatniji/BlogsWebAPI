using BlogWebAPI.Repositories.Core;
using BlogWebAPI.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BlogWebAPI.Repositories
{
    public static class Registerer
    {
        public static void RegisterComponents(this IServiceCollection services)
        {
            services.AddScoped<IBlogRepository, BlogRepository>();
        }
    }
}
