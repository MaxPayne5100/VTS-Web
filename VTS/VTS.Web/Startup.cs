using System;
using System.IO;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using VTS.Core.Constants;
using VTS.Core.Settings;
using VTS.DAL;
using VTS.Repos;
using VTS.Services;

namespace VTS.Web
{
    /// <summary>
    /// Class which setting up configuration and wiring up services the application will use.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Gets configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets container.
        /// </summary>
        public IContainer ApplicationContainer { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        public Startup()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            configurationBuilder.AddJsonFile("appsettings.json", false, true);

            Configuration = configurationBuilder.Build();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Services.</param>
        /// <returns>IServiceProvider.</returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var authSettingsSection = Configuration.GetSection("AuthSetting");
            services.Configure<AuthSetting>(authSettingsSection);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.AccessDeniedPath = "/Authentication/AccessDenied";
                    options.LoginPath = "/Authentication/LogIn";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.ClerkOnly, policy =>
                    policy.RequireRole(Roles.Clerk));

                options.AddPolicy(Policies.ManagerOnly, policy =>
                    policy.RequireRole(Roles.Manager));

                options.AddPolicy(Policies.EmployeeOnly, policy =>
                    policy.RequireRole(Roles.Employee));
            });

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<VTSDbContext>(options => options.UseSqlServer(
                connectionString,
                b => b.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)));

            services.AddControllersWithViews();

            services.AddOptions();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "VTS API", Version = "v1" });
            });

            services.AddAutoMapper(
                typeof(ServiceMapProfile).Assembly,
                typeof(WebApiMapProfile).Assembly);

            var builder = new ContainerBuilder();

            builder.Populate(services);
            builder.RegisterModule<ReposDependencyModule>();
            builder.RegisterModule<ServiceDependencyModule>();

            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">App.</param>
        /// <param name="env">Env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "VTS API v1");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Authentication}/{action=LogIn}/{id?}");
            });
        }
    }
}