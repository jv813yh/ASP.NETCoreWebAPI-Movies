using AutoMapper;
using Movies.Api.DTOs;
using Movies.Data.Interfaces;

public class MovieManager : IMovieManager
{
    private readonly IMovieRepository _movieRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;

    public MovieManager(IMovieRepository movieRepository, IPersonRepository personRepository, IGenreRepository genreRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _personRepository = personRepository;
        _genreRepository = genreRepository;
        _mapper = mapper;
    }

    public IList<MovieDTO> GetAllMovies()
    {
        var movies = _movieRepository.GetAll();
        return _mapper.Map<IList<MovieDTO>>(movies);
    }
}
