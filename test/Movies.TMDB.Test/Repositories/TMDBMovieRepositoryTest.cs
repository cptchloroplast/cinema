using Movies.TMDB.Entities;
using Movies.TMDB.Repositories;
using Okkema.SQL.Test.Repositories;
using Okkema.SQL.Repositories;
using Movies.Migrations;
using Microsoft.Extensions.Logging;
using Moq;
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