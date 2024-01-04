using System.Text.Json.Serialization;
namespace Movies.TMDB.Models;
public class TMDBCountry
{
    [JsonPropertyName("iso_3166_1")]
    public string Iso3166_1 { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

}