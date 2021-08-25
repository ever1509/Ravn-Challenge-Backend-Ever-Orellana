using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movies.Domain.Entities;

namespace Movies.Infrastructure.Data.Mappings
{
    public class MovieMap : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(e => e.MovieId);
            builder.Property(e => e.Title).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Synopsis).IsRequired().HasMaxLength(2000);
            builder.Property(e => e.ReleaseDate).IsRequired().HasColumnType("datetime");
            builder.Property(e => e.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(e => e.Image).IsRequired();

            builder.HasOne(e => e.Category)
                .WithMany(e => e.Movies)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Category_Movies");
        }
    }
}
