using AutoMapper;
using Movies.Api.DTOs;
using Movies.Data.Models;

namespace Movies.Api
{
    public class AutoMapperConfigurationProfile : Profile
    {
        public AutoMapperConfigurationProfile()
        {
            CreateMap<Person, PersonDTO>();
            CreateMap<PersonDTO, Person>();
        }
    }
}
