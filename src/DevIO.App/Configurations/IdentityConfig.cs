//using AutoMapper.Configuration;
//using DevIO.App.Data;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;

//namespace DevIO.App.Configurations
//{
//    public static class IdentityConfig
//    {
//        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration Configuration)
//        {
//            services.Configure<CookiePolicyOptions>(options =>
//            {
//                options.CheckConsentNeeded = context => true;
//                options.MinimumSameSitePolicy = SameSiteMode.None;
//            });

//            services.AddDbContext<ApplicationDbContext>(options =>
//                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

//            return services;
//        }

//    }
//}
