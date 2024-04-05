using AutoMapper;
using Movies.Api.Interfaces;
using Movies.Data.Interfaces;

namespace Movies.Api.Managers
{
    public class GenreManager : IGenreManager
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreManager(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public IList<string> GetAllGenres()
         => _mapper.Map<IList<string>>(_genreRepository.GetAll());
    }
}
