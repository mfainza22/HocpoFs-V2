using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using WeighingSystemCore.Models;
using WeighingSystemCore.Repositories;

namespace WeighingSystemCore.Extensions
{
    public static class ServiceExtensions
    {
        //public static void ConfigureLoggerService(this IServiceCollection services)
        //{
        //    services.AddSingleton<ILoggerManager, LoggerManager>();
        //}

        //public static void ConfigureIdentityService(this IServiceCollection services)
        //{
        //    services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
        //    services.AddTransient<IUserStore<IdentityManagement.Entities.UserAccount>, IdentityManagement.IdentityStore.UserStore>();
        //    services.AddTransient<IRoleStore<IdentityManagement.Entities.Role>, IdentityManagement.IdentityStore.RoleStore>();
        //    services.AddScoped<IUserRoleRepository, IdentityManagement.Repositories.UserRoleRepository>();
        //    services.AddScoped<IUserAccountRepository, IdentityManagement.Repositories.UserAccountRepository>();
        //    services.AddIdentity<IdentityManagement.Entities.UserAccount, IdentityManagement.Entities.Role>().AddDefaultTokenProviders();
        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //    }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        //    {
        //        options.LoginPath = new PathString("/Auth/Login");
        //        options.LogoutPath = new PathString("/Auth/LogOut");
        //        options.AccessDeniedPath = new PathString("/Auth/AccessDenied");
        //        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        //    });
        //}

        public static void ConfigureAntiForgery(this IServiceCollection services)
        {
            services.AddAntiforgery(options =>
            {
                options.FormFieldName = "__RequestVerificationToken";
                options.HeaderName = "X-CSRF-TOKEN";
                options.SuppressXFrameOptionsHeader = false;
            });
        }

        public static void ConfigureDataRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBinLocationRepository, BinLocationRepository>();
            //services.AddScoped<IPositionRepository, PositionRepository>();
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<IClientContactRepository, ClientContactRepository>();
            //services.AddScoped<ICustomerRepository, CustomerRepository>();

            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            //services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<IUnitRepository, UnitRepository>();


            //services.AddScoped<Services.StaticServices>();
        }

        public static void ConfigureSessions(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSession(s => s.IdleTimeout = TimeSpan.FromMinutes(30));
        }

        //public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
        //{
        //    app.UseExceptionHandler(appError =>
        //    {
        //        appError.Run(async context =>
        //        {
        //            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //            context.Response.ContentType = "application/json";

        //            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        //            if (contextFeature != null)
        //            {
        //                logger.LogError($"Something went wrong: {contextFeature.Error}");

        //                await context.Response.WriteAsync((new ResponseResult()
        //                {
        //                    statuscode = context.Response.StatusCode,
        //                    message = contextFeature.Error.Message
        //                }).ToJson());
        //            }
        //        });
        //    });
        //}

        //public static void ConfigureBadRequestHandler(this IApplicationBuilder app, ILoggerManager logger)
        //{

        //}

    }
}
