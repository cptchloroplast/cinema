using Movies.TMDB.Models;
namespace Movies.TMDB.Services;
public interface ITMDBService
{
    public Task<TMDBMovie?> GetMovieAsync(int movieId, CancellationToken cancellationToken = default);
}