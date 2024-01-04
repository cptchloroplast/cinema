using Microsoft.Extensions.Options;
using System.Data;
using Movies.SQL.Repositories;
using Movies.SQL.Factories;
using Movies.SQL.Options;
using Movies.TMDB.Entities;
using Microsoft.Extensions.Logging;
using Movies.SQL.Extensions;
namespace Movies.TMDB.Repositories;
public class TMDBMovieRepository : RepositoryBase<TMDBMovieEntity>
{
    public TMDBMovieRepository(
        ILogger<TMDBMovieRepository> logger,
        IDbConnectionFactory factory,
        IOptionsMonitor<DbConnectionOptions> options) : base(logger, factory, options) { }
    private const string CREATE = 
        @"INSERT INTO TmdbMovie (
            Adult,
            BackdropPath,
            Budget,
            Homepage,
            Id,
            ImdbId, 
            OriginalLanguage,
            OriginalTitle,
            Overview,
            Popularity,
            PosterPath,
            ReleaseDate,
            Revenue,
            Runtime,
            Status,
            Tagline,
            Title,
            Video,
            VoteAverage,
            VoteCount,
            SystemKey,
            SystemCreatedDate,
            SystemModifiedDate
        )
        VALUES (
            @Adult,
            @BackdropPath,
            @Budget,
            @Homepage,
            @Id,
            @ImdbId, 
            @OriginalLanguage,
            @OriginalTitle,
            @Overview,
            @Popularity,
            @PosterPath,
            @ReleaseDate,
            @Revenue,
            @Runtime,
            @Status,
            @Tagline,
            @Title,
            @Video,
            @VoteAverage,
            @VoteCount,
            @SystemKey,
            @SystemCreatedDate,
            @SystemModifiedDate
        )";
    public override int Create(TMDBMovieEntity entity) => 
        UseConnection((IDbConnection connection) => 
            connection.ExecuteCommand(CREATE, entity));
    private const string READ = 
        @"SELECT
            Adult,
            BackdropPath,
            Budget,
            Homepage,
            Id,
            ImdbId, 
            OriginalLanguage,
            OriginalTitle,
            Overview,
            Popularity,
            PosterPath,
            ReleaseDate,
            Revenue,
            Runtime,
            Status,
            Tagline,
            Title,
            Video,
            VoteAverage,
            VoteCount,
            SystemKey,
            SystemCreatedDate,
            SystemModifiedDate
        FROM TmdbMovie 
        WHERE SystemKey = @SystemKey";
    public override TMDBMovieEntity? Read(Guid SystemKey) => 
        UseConnection((IDbConnection connection) => 
            connection.ExecuteQuery<TMDBMovieEntity>(READ, new { SystemKey }));
    private const string UPDATE = 
        @"UPDATE TmdbMovie
        SET 
            Adult = @Adult,
            BackdropPath = @BackdropPath,
            Budget = @Budget,
            Homepage = @Homepage,
            Id = @Id,
            ImdbId = @ImdbId, 
            OriginalLanguage = @OriginalLanguage,
            OriginalTitle = @OriginalTitle,
            Overview = @Overview,
            Popularity = @Popularity,
            PosterPath = @PosterPath,
            ReleaseDate = @ReleaseDate,
            Revenue = @Revenue,
            Runtime = @Runtime,
            Status = @Status,
            Tagline = @Tagline,
            Title = @Title,
            Video = @Video,
            VoteAverage = @VoteAverage,
            VoteCount = @VoteCount,
            SystemModifiedDate = @SystemModifiedDate
        WHERE SystemKey = @SystemKey";
    public override int Update(TMDBMovieEntity entity) => 
        UseConnection((IDbConnection connection) => 
            connection.ExecuteCommand(UPDATE, entity));
    private const string DELETE = 
        @"DELETE FROM TmdbMovie
        WHERE SystemKey = @SystemKey";
    public override int Delete(Guid SystemKey) =>
        UseConnection((IDbConnection connection) => 
            connection.ExecuteCommand(DELETE, new { SystemKey }));
}