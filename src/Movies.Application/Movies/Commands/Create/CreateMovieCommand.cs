using MediatR;
using System;

namespace Movies.Application.Movies.Commands.Create
{
    public class CreateMovieCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
    }
}
