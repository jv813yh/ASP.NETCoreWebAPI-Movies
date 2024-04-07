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

    // Method gets movie by id from the database and returns it as a ExtendedMovieDTO
    public ExtendedMovieDTO? GetMovieById(uint id)
    {
        // Getting the movie from the database by its id
        Movie? movie = _movieRepository.FindById(id);

        // Mapping the movie to a MovieDTO object and returning it
        ExtendedMovieDTO extendedMovieDTO = _mapper.Map<ExtendedMovieDTO>(movie);

        return extendedMovieDTO;
    }

    // 
    public IList<MovieDTO>? ExecuteMovieFilter(MovieFilterDTO movieFilterDTO)
    {
        // Getting the movies from the database according to the filter
        // or all movies if the filter is not used

        IList<Movie>? movies = null;

        if(HasFilterValues(movieFilterDTO))
        {
            movies = _movieRepository.GetMoviesByFilter(
                       movieFilterDTO.DirectId,
                       movieFilterDTO.ActorId,
                       movieFilterDTO.Genre,
                       movieFilterDTO.FromYear,
                       movieFilterDTO.ToYear,
                       movieFilterDTO.Limit);
        }
        else
        {
            movies = _movieRepository.GetAll();
        }

        // Mapping the movies to a list of MovieDTO objects and returning them
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

    public async Task<MovieDTO?> DeleteMovie(uint id)
    {
       if(!_movieRepository.ExistsWithId(id))
       {
              return null;
       }

       Movie? movie = _movieRepository.FindById(id);

       MovieDTO deletedMovieDTO = _mapper.Map<MovieDTO>(movie);

       movie.Actors.Clear();
       movie.Genres.Clear();

       await _movieRepository.DeleteAsync(id);

       return deletedMovieDTO;
    }

    // Method to check if the filter has values
    private bool HasFilterValues(MovieFilterDTO movieFilterDTO)
        =>  movieFilterDTO.ActorId != 0 || movieFilterDTO.DirectId != 0 || movieFilterDTO.Genre != string.Empty ||
                movieFilterDTO.FromYear != 0 || movieFilterDTO.ToYear != 0 || movieFilterDTO.Limit != 0;
}
