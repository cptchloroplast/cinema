namespace Movies.Commands.Movies;
public record CreateMovieV1 : CommandBase
{
    public int TmdbId { get; set; }
}
