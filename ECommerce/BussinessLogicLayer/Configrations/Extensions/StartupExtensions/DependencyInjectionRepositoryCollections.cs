using DataAccessLayer.Repositories.EntityFrameworkRepositories.Abstract;
using DataAccessLayer.Repositories.EntityFrameworkRepositories.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Configrations.Extensions.StartupExtensions
{
    public static class DependencyInjectionRepositoryCollections
    {
        public static void AddDependencyInjectionRepositoryCollections(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IPaymentRepository, PaymentRepository>();


            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        }
    }
}
