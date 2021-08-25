using System;
using System.Collections.Generic;

namespace Movies.Domain.Entities
{
    public class Movie
    {
        public Movie()
        {
            MovieRates = new HashSet<MovieRate>();
        }
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public ICollection<MovieRate> MovieRates { get; set; }
    }
}
