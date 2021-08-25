using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Movies.API.Installers
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection InstallSwagger(this IServiceCollection services)
        {
            var contact = new OpenApiContact()
            {
                Name = "Ever Orellana",
                Email = "ever1509@gmail.com",
                Url = new Uri("https://github.com/ever1509")
            };

            var license = new OpenApiLicense()
            {
                Name = "Movies Catalog",
                Url = new Uri("https://www.moviescatalog.com")
            };

            var info = new OpenApiInfo()
            {
                Version = "v1",
                Title = "Movies Catalog",
                Description = "API which handle movies catalog",
                TermsOfService = new Uri("https://www.moviescatalog.com/terms/"),
                Contact = contact,
                License = license
            };

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("MoviesCatalog", info);
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                //Adding configuration of jwt for swagger UI ----------------------------------

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }},
                        new List<string>()
                    }
                });
                //----------------------------------------
            });

            return services;
        }
    }
}
