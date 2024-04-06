using AutoMapper;
using Movies.Api.DTOs;
using Movies.Data.Interfaces;
using Movies.Data.Models;

public class MovieManager : IMovieManager
{
    // Private fields for the repositories and the mapper
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
        // Getting all movies from the database
        IList<Movie>? movies = _movieRepository.GetAll();

        // If there are no movies in the database, return an empty list
        if(movies == null)
        {
            return new List<MovieDTO>();
        }

        // Mapping the list of movies to a list of MovieDTO objects and returning it
        return _mapper.Map<IList<MovieDTO>>(movies);
    }


    // This method adds a new movie to the database and returns it as a MovieDTO
    public async Task<MovieDTO> AddMovieAsync(MovieDTO movieDTO)
    {
        // Mapping the MovieDTO object to a Movie object
        Movie newMovie = _mapper.Map<Movie>(movieDTO);

        // Setting the DateAdded property to the current date and time
        newMovie.DateAdded = DateTime.UtcNow;

        // Getting the actors and genres from the database and adding them to the new movie
        newMovie.Actors.AddRange(_personRepository.GetPeopleByIds(movieDTO.ActorsIds));
        newMovie.Genres.AddRange(_genreRepository.GetGenresByName(movieDTO.Genres));

        // Async inserting the new movie into the database
        Movie createdMovie = await _movieRepository.InsertAsync(newMovie);

        // Mapping the created movie to a MovieDTO object and returning it
        return _mapper.Map<MovieDTO>(createdMovie);
    }
}
