using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Movies.Application.Common.Interfaces
{
    public interface IMoviesContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Movie> Movies { get; set; }
        DbSet<MovieRate> MovieRates { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    }
}
