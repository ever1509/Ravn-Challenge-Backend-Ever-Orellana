namespace Movies.Application.Common.Models.Requests
{
    public class PageRequest
    {       
        public string Q { get; set; }
        public int Page { get; set; } = 1;
        public int per_page { get; set; } = 4;
        public string Sorts { get; set; }        
    }
}
