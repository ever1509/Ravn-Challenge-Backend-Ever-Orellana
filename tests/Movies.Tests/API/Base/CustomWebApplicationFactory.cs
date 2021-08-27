using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Movies.API;
using Movies.Application.Common.Interfaces;
using Movies.Infrastructure.Data;
using Movies.Tests.API.Base.Seeds;
using System;
using System.Linq;

namespace Movies.Tests.API.Base
{
    public class CustomWebApplicationFactory: WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices((services) =>
            {              
               
                services.AddMvc(options =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .AddAuthenticationSchemes("Test")
                        .Build();

                    options.Filters.Add(new AuthorizeFilter(policy));
                });

               
                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = "Test";
                    options.DefaultChallengeScheme = "Test";
                    options.DefaultScheme = "Test";
                })
                
                .AddScheme<AuthenticationSchemeOptions, TestAuthorizationHandler>("Test", options => { });

            });
            builder.ConfigureServices(services =>
            {

                var descriptorContextVb = services.SingleOrDefault(d =>
                    d.ServiceType == typeof(DbContextOptions<MoviesContext>));

                if (descriptorContextVb != null)
                {
                    services.Remove(descriptorContextVb);
                }

                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<MoviesContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                services.AddScoped<IMoviesContext>(provider =>
                    provider.GetService<MoviesContext>());

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var dbContext = scopedServices.GetRequiredService<MoviesContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory>>();

                    dbContext.Database.EnsureCreated();

                    try
                    {
                        DbSeederTests.InitSeedDataFromTestDb(dbContext);
                    }
                    catch (Exception e)
                    {
                        logger.LogError(e, $"An Error occurred seeding the database with test message. Error: {e.Message}");
                    }
                }

            });
        }
    }
}
