using Okkema.SQL.Entities;
namespace Movies.TMDB.Entities;
public record TMDBMovieEntity : EntityBase
{
    public bool Adult { get; set; }
    public string? BackdropPath { get; set; }
    // public TMDBCollection? BelongsToCollection { get; set; }
    public int Budget { get; set; }
    // public List<TMDBGenre> Genres { get; set; } = new List<TMDBGenre>();
    public string? Homepage { get; set; }
    public int Id { get; set; }
    public string? ImdbId { get; set; }
    public string OriginalLanguage { get; set; } = string.Empty;
    public string OriginalTitle { get; set; } = string.Empty;
    public string? Overview { get; set; }
    public float Popularity { get; set; }
    public string? PosterPath { get; set; }
    // public List<TMDBCompany> ProductionCompanies { get; set; } = new List<TMDBCompany>();
    // public List<TMDBCountry> ProductionCountries { get; set; } = new List<TMDBCountry>();
    public DateTime ReleaseDate { get; set; }
    public int Revenue { get; set; }
    public int? Runtime { get; set; }
    // public List<TMDBLanguage> SpokenLanguages { get; set; } = new List<TMDBLanguage>();
    public string Status { get; set; } = string.Empty;
    public string? Tagline { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool Video { get; set; }
    public float VoteAverage { get; set; }
    public float VoteCount { get; set; }
}