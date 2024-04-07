namespace Movies.Api.DTOs
{
    // Data transfer object for filtering movies by director, actor, genre, year and limit
    public class MovieFilterDTO
    {
        public uint DirectorId { get; set; }
        public uint ActorId { get; set; }
        public string Genre { get; set; } = "";
        public int FromYear { get; set; }
        public int ToYear { get; set; }
        public int Limit { get; set; }
    }
}
