using Movies.SQL.Entities;
namespace Movies.SQL.Repositories;
public interface IMovieRepository
{
    public Task Create(MovieEntity movie);
    public Task<MovieEntity> Read(int tmdbId);
    public Task Update(MovieEntity movie);
    public Task Delete(int tmdbId);
}