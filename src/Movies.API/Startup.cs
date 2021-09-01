using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Movies.API.Installers;
using Movies.Application;
using Movies.Application.Common.Interfaces;
using Movies.Application.Common.Models;
using Movies.Infrastructure;
using Movies.Infrastructure.Data;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Movies.API
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
            services.AddCors(options =>
            {
                options.AddPolicy("MoviesCatalog",
                    builder => builder.WithOrigins("https://localhost:44316"));
            });                       
            services.InstallJwt(Configuration);
            services.InstallSwagger();            
            services.InstallMoviesApplication();
            services.InstallMoviesInfrastructure(Configuration);

            services.AddControllers()
                .AddNewtonsoftJson(options=> {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MoviesContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //db.Database.Migrate();

            app.UseRouting();

            app.UseCors("MoviesCatalog");

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/MoviesCatalog/swagger.json", "Movies Catalog");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
