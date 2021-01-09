namespace FastFoodWorkshop
{
    using AutoMapper;
    using Common.WebConstants;
    using Data;
    using Service;
    using Service.Contracts;
    using ServiceModels.Applicant;
    using ServiceModels.User;
    using ServiceModels.Manager;
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Middleware;
    using Models;
    using System.Linq;
    using System.Collections.Generic;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<FastFoodWorkshopDbContext>(options =>
                options.UseLazyLoadingProxies().
                UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<FastFoodUser, IdentityRole<int>>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<FastFoodWorkshopDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication()
                .AddFacebook(facebook =>
                {
                    facebook.AppId = Configuration[Security.FacebookAppId];
                    facebook.AppSecret = Configuration[Security.FacebookSecret];
                });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            services.AddMvc(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAutoMapper(config =>
                {
                    config.CreateMap<ApplicantCvViewModel, ApplicantCV>()
                    .ForMember(dest => dest.Picture, opt => opt.Ignore());

                    config.CreateMap<ApplicantCV, ApplicantCvViewModel>()
                    .ForMember(dest => dest.Picture, opt => opt.Ignore());

                    config.CreateMap<JobInputModel, Job>();

                    config.CreateMap<EducationViewModel, Education>();

                    config.CreateMap<FastFoodUser, UserDetailsViewModel>()
                    .ReverseMap();

                    config.CreateMap<UserRegisterViewModel, FastFoodUser>()
                    .ForMember(dest => dest.Picture, opt => opt.Ignore());

                    config.CreateMap<RestaurantViewModel, Restaurant>()
                    .ReverseMap();

                    config.CreateMap<ComplaintViewModel, Complaint>()
                    .ReverseMap();

                    config.CreateMap<OrderViewModel, Order>()
                    .ReverseMap();

                    config.CreateMap<UserComplaintViewModel, Complaint>();

                    config.CreateMap<DeliveryCarViewModel, DeliveryCar>()
                    .ReverseMap();

                    config.CreateMap<ProductViewModel, Product>()
                    .ForMember(dest => dest.Picture, opt => opt.Ignore());

                    config.CreateMap<Product, ProductViewModel>()
                    .ForMember(dest => dest.Picture, opt => opt.Ignore());

                    config.CreateMap<Category, CategoryViewModel>()
                    .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
                    .ReverseMap();

                    config.CreateMap<MenuViewModel, Menu>()
                    .ReverseMap();
                });

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = Security.SessionCookieName;
                options.IdleTimeout = TimeSpan.FromDays(1);
                options.Cookie.HttpOnly = true;
            });

            //App services
            services.AddScoped<RoleManager<IdentityRole<int>>>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IApplicantService, ApplicantService>();
            services.AddScoped<IFacebookService, FacebookService>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddTransient<SeedAdminAndRolesMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            logger.AddFile("Logs/Log.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSeedAdminAndRoles();
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "areas",
                   template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
