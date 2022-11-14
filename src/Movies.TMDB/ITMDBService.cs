using Movies.TMDB.Models;
namespace Movies.TMDB;
public interface ITMDBService
{
    public Task<TMDBMovie?> GetMovie(int movieId, CancellationToken cancellationToken = default);
}