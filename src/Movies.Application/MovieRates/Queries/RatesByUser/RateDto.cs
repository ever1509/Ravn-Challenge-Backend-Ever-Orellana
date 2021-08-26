using AutoMapper;
using Movies.Application.Common.Mappings;
using Movies.Domain.Entities;
using System;

namespace Movies.Application.MovieRates.Queries.RatesByUser
{
    public class RateDto : IMapFrom<MovieRate>
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public DateTime ReleaseDate { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<MovieRate, RateDto>()
                .ForMember(d => d.MovieId, opt => opt.MapFrom(src => src.MovieId))                
                .ForMember(d => d.Title, opt => opt.MapFrom(src => src.Movie.Title))
                .ForMember(d => d.Synopsis, opt => opt.MapFrom(src => src.Movie.Synopsis))
                .ForMember(d => d.ReleaseDate, opt => opt.MapFrom(src => src.Movie.ReleaseDate));            
        }
    }
}
