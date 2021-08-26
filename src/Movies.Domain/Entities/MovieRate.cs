using System;

namespace Movies.Domain.Entities
{
    public class MovieRate
    {
        public int MovieId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
