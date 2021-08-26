using AutoMapper;
using Movies.Application.Common.Mappings;
using Movies.Domain.Entities;
using System;

namespace Movies.Application.Movies.Queries.Movieslist
{
    public class MovieDto : IMapFrom<Movie>
    {
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string MovieImage { get; set; }
        public int MovieRates { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Movie, MovieDto>()
                .ForMember(d=>d.Title, opt=>opt.MapFrom(src=>src.Title))
                .ForMember(d=>d.Synopsis, opt=>opt.MapFrom(src=>src.Synopsis))
                .ForMember(d=>d.ReleaseDate, opt=>opt.MapFrom(src=>src.ReleaseDate))
                .ForMember(d=>d.MovieImage, opt=>opt.MapFrom(src=>src.Image))
                .ForMember(d=>d.MovieRates, opt=>opt.MapFrom(src=>src.MovieRates.Count));
        }

    }
}
