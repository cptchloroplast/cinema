using System.Text.Json.Serialization;
namespace Movies.TMDB.Models;
public class TMDBMovie
{
    public bool Adult { get; set; }
    [JsonPropertyName("backdrop_path")]
    public string? BackdropPath { get; set; }
    [JsonPropertyName("belongs_to_collection")]
    public TMDBCollection? BelongsToCollection { get; set; }
    public int Budget { get; set; }
    public List<TMDBGenre> Genres { get; set; } = new List<TMDBGenre>();
    public string? Homepage { get; set; }
    public int Id { get; set; }
    [JsonPropertyName("imdb_id")]
    public string? ImdbId { get; set; }
    [JsonPropertyName("original_language")]
    public string OrginalLanguage { get; set; } = string.Empty;
    [JsonPropertyName("original_title")]
    public string OriginalTitle { get; set; } = string.Empty;
    public string? Overview { get; set; }
    public float Popularity { get; set; }
    [JsonPropertyName("poster_path")]
    public string? PosterPath { get; set; }
    [JsonPropertyName("production_companies")]
    public List<TMDBCompany> ProductionCompanies { get; set; } = new List<TMDBCompany>();
    [JsonPropertyName("production_countries")]
    public List<TMDBCountry> ProductionCountries { get; set; } = new List<TMDBCountry>();
    [JsonPropertyName("release_date")]
    public string ReleaseDate { get; set; } = string.Empty;
    public int Revenue { get; set; }
    public int? Runtime { get; set; }
    [JsonPropertyName("spoken_languages")]
    public List<TMDBLanguage> SpokenLanguages { get; set; } = new List<TMDBLanguage>();
    public string Status { get; set; } = string.Empty;
    public string? Tagline { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool Video { get; set; }
    [JsonPropertyName("vote_average")]
    public float VoteAverage { get; set; }
    [JsonPropertyName("vote_count")]
    public float VoteCount { get; set; }
}