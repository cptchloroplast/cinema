using Okkema.SQL.Repositories;
using Movies.TMDB.Entities;

namespace Movies.TMDB.Repositories;
public interface ITMDBMovieRepository : IRepository<TMDBMovieEntity>
{
  /// <summary>
  /// Read TMDB movie by TMDB id
  /// </summary>
  /// <param name="tmdbId">TMDB id</param>
  /// <returns>TMDB movie entity</returns>
  public TMDBMovieEntity? Read(int tmdbId);
}