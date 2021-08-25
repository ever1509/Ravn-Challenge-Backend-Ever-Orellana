using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain.Entities;

namespace Movies.Infrastructure.Data.Mappings
{
    public class MovieRatesMap : IEntityTypeConfiguration<MovieRate>
    {
        public void Configure(EntityTypeBuilder<MovieRate> builder)
        {
            builder.HasKey(e => new { e.MovieId, e.UserId });
            builder.Property(e => e.CreatedDate).HasColumnType("datetime");

            builder.HasOne(e => e.Movie)
                .WithMany(d => d.MovieRates)
                .HasForeignKey(e => e.MovieId)
                .HasConstraintName("FK_Movie_MovieRates");
        }
    }
}
