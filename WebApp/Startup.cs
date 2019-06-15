using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ICommands.IGenreCommands;
using Application.ICommands.IMovieCommands;
using Application.ICommands.IUserCommands;
using EfCommands.EfGenreCommands;
using EfCommands.EfMovieCommands;
using EfCommands.EfUserCommands;
using EfDataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp
{
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<MovieContext>();
            services.AddTransient<IGetMovieCommand, EfGetMovieCommand>();
            services.AddTransient<IGetOneMovieCommand, EfGetOneMovieCommand>();
            services.AddTransient<ICreateMovieCommand, EfCreateMovieCommand>();
            services.AddTransient<IGetGenreCommand, EfGetGenreCommand>();
            services.AddTransient<IGetUserCommand, EfGetUserCommand>();
            services.AddTransient<ICreateUserCommand, EfCreateUserCommand>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
            services.AddTransient<IGetOneUserCommand, EfGetOneUserCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<IUpdateMovieCommand, EfUpdateMovieCommand>();
            services.AddTransient<IDeleteMovieCommand, EfDeleteMovieCommand>();
            services.AddTransient<ICreateMovieAndGenreCommand, EfCreateMovieAndGenreCommand>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Movie}/{action=Index}/{id?}");
            });
        }
    }
}
