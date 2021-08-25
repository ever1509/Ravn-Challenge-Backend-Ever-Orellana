using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies.Application.Common.Interfaces;
using Movies.Domain.Entities;
using Movies.Infrastructure.Identity;
using System.Reflection;

namespace Movies.Infrastructure.Data
{
    public class MoviesContext : IdentityDbContext<ApplicationUser>, IMoviesContext
    {
        public MoviesContext(DbContextOptions<MoviesContext> options)
            :base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieRate> MovieRates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
