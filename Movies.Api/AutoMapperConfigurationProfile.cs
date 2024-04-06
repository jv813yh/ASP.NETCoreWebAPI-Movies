using AutoMapper;
using Movies.Api.DTOs;
using Movies.Data.Models;

namespace Movies.Api
{
    // Configuration profile for the AutoMapper library
    public class AutoMapperConfigurationProfile : Profile
    {
        public AutoMapperConfigurationProfile()
        {
            CreateMap<Person, PersonDTO>();
            CreateMap<PersonDTO, Person>();

            CreateMap<Genre, string>()
                .ConstructUsing(genre => genre.Name);


            CreateMap<Movie, MovieDTO>()
                .ForMember(dto => dto.ActorsIds, opt => opt.MapFrom(movie => movie.Actors.Select(a => a.PersonId).ToList()));


            //
            CreateMap<Movie, ExtendedMovieDTO>()
                   .ForMember(dto => dto.ActorsIds, opt => opt.MapFrom(movie => movie.Actors.Select(a => a.PersonId).ToList()));


            // Actors and Genres are ignored here,
            // but I use functions in PersonRepository and GenreRepository to get them
            CreateMap<MovieDTO, Movie>()
                .ForMember(movie => movie.Actors, opt => opt.Ignore())
                .ForMember(movie => movie.Genres, opt => opt.Ignore());

        }
    }
}
