using BussinessLogicLayer.Services.Abstract;
using BussinessLogicLayer.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Configrations.Extensions.StartupExtensions
{
    public static class DependencyInjectionServiceCollections
    {
        public static void AddDependencyInjectionServiceCollections(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IApplicationUserService, ApplicationUserService>();
        }
    }
}
