using Movies.TMDB.Entities;
using Movies.TMDB.Repositories;
using Movies.SQL.Test.Repositories;
using Movies.SQL.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Movies.SQL.Migrations;
using System.Reflection;
namespace Movies.TMDB.Test.Repositories;
public class TMDBMovieRepositoryTest : RepositoryBaseTest<TMDBMovieEntity>
{
    private readonly IRepository<TMDBMovieEntity> _repo;
    private readonly ILogger<TMDBMovieRepository> _logger;
    protected override IRepository<TMDBMovieEntity> Repository { get => _repo; }
    public TMDBMovieRepositoryTest() : base(new Assembly[] { typeof(AddTmdbMovieTable).Assembly })
    {
        _logger = Mock.Of<ILogger<TMDBMovieRepository>>();
        _repo = new TMDBMovieRepository(_logger, _factory, _options);
    }
}