using System.Text.Json.Serialization;
namespace Movies.TMDB.Models;
public class TMDBLanguage
{
    [JsonPropertyName("iso_639_1")]
    public string Iso639_1 { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}