using System;

namespace Movies.Domain.Entities
{
    public class MovieRate
    {
        public int MovieRateId { get; set; }
        public int MovieId { get; set; }
        public string UserID { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
