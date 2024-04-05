using AutoMapper;
using Movies.Api.DTOs;
using Movies.Data.Interfaces;
using Movies.Data.Models;

public class MovieManager : IMovieManager
{
    private readonly IMovieRepository _movieRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IMapper _mapper;

    public MovieManager(IMovieRepository movieRepository, IPersonRepository personRepository, 
        IGenreRepository genreRepository, IMapper mapper)
    {
        _movieRepository = movieRepository;
        _personRepository = personRepository;
        _genreRepository = genreRepository;
        _mapper = mapper;
    }

    // This method returns a list of all movies
    public IList<MovieDTO> GetAllMovies()
    {
        IList<Movie>? movies = _movieRepository.GetAll();

        if(movies == null)
        {
            return new List<MovieDTO>();
        }

        return _mapper.Map<IList<MovieDTO>>(movies);
    }

    public async Task<MovieDTO> AddMovieAsync(MovieDTO movieDTO)
    {
        Movie newMovie = _mapper.Map<Movie>(movieDTO);

        newMovie.Actors.AddRange(_personRepository.GetPersonsByIds(movieDTO.ActorsIds));
        newMovie.Genres.AddRange(_genreRepository.GetGenresByName(movieDTO.Genres));

        Movie createdMovie = await _movieRepository.InsertAsync(newMovie);

        return _mapper.Map<MovieDTO>(createdMovie);
    }
}
