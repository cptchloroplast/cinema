using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using Movies.TMDB.Models;
using Movies.TMDB.Options;
using Microsoft.Extensions.Logging;
using System.Net;
namespace Movies.TMDB.Services;
public sealed class TMDBService : ITMDBService
{
    private readonly ILogger<TMDBService> _logger;
    private readonly IHttpClientFactory _factory;
    private readonly IOptionsMonitor<TMDBOptions> _options;
    private HttpClient _client { get => _factory.CreateClient(nameof(TMDBService)); }
    public TMDBService(ILogger<TMDBService> logger, IHttpClientFactory factory, IOptionsMonitor<TMDBOptions> options)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }
    private string AddQueryString(string route, IDictionary<string, string?>? query = default)
    {
        if (query is null) query = new Dictionary<string, string?>();
        query.Add("api_key", _options.CurrentValue.ApiKey);
        return QueryHelpers.AddQueryString(route, query);
    }
    public async Task<TMDBMovie?> GetMovieAsync(int movieId, CancellationToken cancellationToken = default)
    {
        var route = $"movie/{movieId}";
        try 
        {
            return await _client.GetFromJsonAsync<TMDBMovie>(AddQueryString(route), cancellationToken);
        }
        catch (HttpRequestException exception)
        {
            if (exception.StatusCode == HttpStatusCode.NotFound) return null;
            _logger.LogWarning("The client threw an {Exception} while getting a movie", exception);
            throw;
        }
    }
}