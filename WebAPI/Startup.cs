using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ICommands.ICommentCommands;
using Application.ICommands.IGenreCommands;
using Application.ICommands.IMovieCommands;
using Application.ICommands.IUserCommands;
using EfCommands.EfCommentCommands;
using EfCommands.EfGenreCommands;
using EfCommands.EfMovieCommands;
using EfCommands.EfUserCommands;
using EfDataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace WebAPI
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "dataset-movie API", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                    new ApiKeyScheme
                    {
                        In = "header",
                        Description = "Please enter into field the word 'Bearer' following by space and JWT",
                        Name = "Authorization",
                        Type = "apiKey"
                    });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                { "Bearer", Enumerable.Empty<string>() },
            });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<MovieContext>();
            services.AddTransient<IGetGenreCommand, EfGetGenreCommand>();
            services.AddTransient<IGetOneGenreCommand, EfGetOneGenreCommand>();
            services.AddTransient<ICreateGenreCommand, EfCreateGenreCommand>();
            services.AddTransient<IDeleteGenreCommand, EfDeleteGenreCommand>();
            services.AddTransient<IUpdateGenreCommand, EfUpdateGenreCommand>();
            services.AddTransient<IGetOneUserCommand, EfGetOneUserCommand>();
            services.AddTransient<ICreateUserCommand, EfCreateUserCommand>();
            services.AddTransient<IGetUserCommand, EfGetUserCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
            services.AddTransient<ICreateMovieCommand, EfCreateMovieCommand>();
            services.AddTransient<IGetMovieCommand, EfGetMovieCommand>();
            services.AddTransient<IGetOneMovieCommand, EfGetOneMovieCommand>();
            services.AddTransient<IDeleteMovieCommand, EfDeleteMovieCommand>();
            services.AddTransient<IUpdateMovieCommand, EfUpdateMovieCommand>();
            services.AddTransient<ICreateCommentComand, EfCreateCommentCommand>();
            services.AddTransient<IGetOneCommentCommand, EfGetOneCommentCommand>();
            services.AddTransient<IGetCommentComand, EfGetCommentCommand>();
            services.AddTransient<IUpdateCommentComand, EfUpdateCommentCommand>();
            services.AddTransient<IDeleteCommentComand, EfDeleteCommentCommand>();
            services.AddTransient<IGetUserLoginCommand, EfGetLoginUserCommand>();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "dataset-movie API V1");
            });
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
