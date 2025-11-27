using Microsoft.Extensions.DependencyInjection;
using SuppManagerDB.BL.Concrete;
using SuppManagerDB.BL.Interfaces;
using SuppManagerDB.DAL.Concrete;
using SuppManagerDB.DAL.Interfaces;
using System;

namespace SuppManagerDB.WPF.Configuration
{
    public static class AppServices
    {
        public static ServiceProvider Configure()
        {
            var services = new ServiceCollection();

            // DAL
            services.AddTransient<IUserDal, UserDal>();
            services.AddTransient<IUserPrivilegeDal, UserPrivilegeDal>();
            services.AddTransient<ISupplierDal, SupplierDal>();
            services.AddTransient<IProductDal, ProductDal>();        
            services.AddTransient<ICategoryDal, CategoryDal>();      
            services.AddTransient<IManufacturerDal, ManufacturerDal>(); 



            // BL
            services.AddTransient<IAuthManager, AuthManager>();  
            services.AddTransient<ISupplierManager, SupplierManager>();
            services.AddTransient<IProductManager, ProductManager>();       


            return services.BuildServiceProvider();
        }
    }
}
