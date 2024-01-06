using Okkema.Messages;
namespace Movies.Commands;
public record CreateTMDBMovieV1 : MessageBase
{
    public int TmdbId { get; set; }
}
