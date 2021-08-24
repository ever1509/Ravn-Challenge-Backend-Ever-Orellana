using System.Collections.Generic;

namespace Movies.Domain.Entities
{
    public class Category
    {
        public Category()
        {
            Movies = new HashSet<Movie>();
        }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
    }
}
