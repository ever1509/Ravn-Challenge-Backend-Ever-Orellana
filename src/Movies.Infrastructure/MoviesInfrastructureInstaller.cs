﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Common.Interfaces;
using Movies.Application.Common.Models;
using Movies.Infrastructure.Data;
using Movies.Infrastructure.File;
using Movies.Infrastructure.Identity;

namespace Movies.Infrastructure
{
    public static class MoviesInfrastructureInstaller
    {
        public static IServiceCollection InstallMoviesInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<MoviesContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("MoviesConnection"),
                    b => b.MigrationsAssembly(typeof(MoviesContext).Assembly.FullName)));

            services.AddScoped<IMoviesContext>(provider => provider.GetService<MoviesContext>());

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MoviesContext>();

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.Configure<AzureStorageSettings>(configuration.GetSection("AzureStorageSettings"));
            services.AddScoped<IFileService, AzureFileService>();

            return services;
        }
    }
}
