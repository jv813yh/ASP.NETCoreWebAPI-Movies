namespace Movies.Api.Interfaces
{
    // Interface for the GenreManager class
    public interface IGenreManager
    {
        // This method returns a list of all genres
        IList<string> GetAllGenres();
    }
}
