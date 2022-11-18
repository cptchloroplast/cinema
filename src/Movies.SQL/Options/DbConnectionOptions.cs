using System.ComponentModel.DataAnnotations;
namespace Movies.SQL.Options;
public class DbConnectionOptions
{
    [Required]
    public string ConnectionString { get; set; } = string.Empty;
}