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
                       movieFilterDTO.DirectorId,
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

    // This method updates a movie by id in the database and returns it as a MovieDTO
    public async Task<MovieDTO?> UpdateMovie(uint id, MovieDTO updateMovieDTO)
    {
        Movie? movieUpdated = _movieRepository.FindById(id);

        // Check if the movie with the given id exists in the database
        if(null == movieUpdated)
        {
            return null;
        }

        // Mapping the MovieDTO object to a Movie object
        _mapper.Map<MovieDTO, Movie>(updateMovieDTO, movieUpdated);

        // Setting the Id and DateAdded properties of the movie
        movieUpdated.Id = id;
        movieUpdated.DateAdded = DateTime.UtcNow;

        // Getting the actors and genres from the database 
        IEnumerable<Person> Actors = _personRepository.GetPeopleByIds(updateMovieDTO.ActorsIds);
        IEnumerable<Genre> Genres = _genreRepository.GetGenresByName(updateMovieDTO.Genres);

        // Removing the actors that are not in the updateMovieDTO
        foreach(Person person in movieUpdated.Actors.Except(Actors))
        {
            movieUpdated.Actors.Remove(person);
        }
        
        // Removing the genres that are not in the updateMovieDTO
        foreach(Genre genre in movieUpdated.Genres.Except(Genres))
        {
            movieUpdated.Genres.Remove(genre);
        }

        // Adding the actors and genres that are in the updateMovieDTO
        // except the ones that are already in the movie !!!
        movieUpdated.Actors.AddRange(Actors.Except(movieUpdated.Actors));
        movieUpdated.Genres.AddRange(Genres.Except(movieUpdated.Genres));

        // Async updating the movie in the database
        Movie newUpdatedMovie = await _movieRepository.UpdateAsync(movieUpdated);

        return _mapper.Map<MovieDTO>(newUpdatedMovie);
    }

    // This method deletes a movie by id from the database and returns it as a MovieDTO
    public async Task<MovieDTO?> DeleteMovie(uint id)
    {
        // Mehod ExistsWithId checks if the movie with the given id exists in the database
        // and also changes the state of the entity to Detached to avoid problems with
        // tracking multiple entities with the same ID
        if(!_movieRepository.ExistsWithId(id))
        {
            return null;
        }

        Movie? movie = _movieRepository.FindById(id);

        MovieDTO deletedMovieDTO = _mapper.Map<MovieDTO>(movie);

        // Cancels all links between these entities. Entity Framework would
        // otherwise because of these bindings
        // was unable to remove the Person
        movie.Actors.Clear();
        movie.Genres.Clear();

        // Async deleting the movie from the database
        await _movieRepository.DeleteAsync(id);

        return deletedMovieDTO;
    }

    // Method to check if the filter has values
    private bool HasFilterValues(MovieFilterDTO movieFilterDTO)
        =>  movieFilterDTO.ActorId != 0 || movieFilterDTO.DirectorId != 0 || movieFilterDTO.Genre != string.Empty ||
                movieFilterDTO.FromYear != 0 || movieFilterDTO.ToYear != 0 || movieFilterDTO.Limit != 0;
}
