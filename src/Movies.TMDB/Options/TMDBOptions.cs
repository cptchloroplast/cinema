using System.ComponentModel.DataAnnotations; 
namespace Movies.TMDB.Options;
public class TMDBOptions
{
    [Url]
    public const string BaseAddress = "https://api.themoviedb.org/3/";
    [Required]
    public string ApiKey { get; set; } = string.Empty;
}