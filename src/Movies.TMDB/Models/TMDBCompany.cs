using System.Text.Json.Serialization;
namespace Movies.TMDB.Models;
public class TMDBCompany
{
    public string Name { get; set; } = string.Empty;
    public int Id { get; set; }
    [JsonPropertyName("logo_path")]
    public string? LogoPath { get; set; }
    [JsonPropertyName("origin_country")]
    public string OriginCountry { get; set; } = string.Empty;
}