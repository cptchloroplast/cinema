using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using Movies.TMDB.Models;
namespace Movies.TMDB;
public sealed class TMDBService : ITMDBService
{
    private readonly IHttpClientFactory _factory;
    private readonly IOptionsMonitor<TMDBOptions> _options;
    private HttpClient _client { get => _factory.CreateClient(nameof(TMDBService)); }
    public TMDBService(IHttpClientFactory factory, IOptionsMonitor<TMDBOptions> options)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }
    private string AddQueryString(string route, IDictionary<string, string>? query = default)
    {
        if (query is null) query = new Dictionary<string, string>();
        query.Add("api_key", _options.CurrentValue.ApiKey);
        return QueryHelpers.AddQueryString(route, query);
    }
    public Task<TMDBMovie?> GetMovie(int movieId, CancellationToken cancellationToken = default)
    {
        var route = $"movie/{movieId}";
        return _client.GetFromJsonAsync<TMDBMovie>(AddQueryString(route), cancellationToken);
    }
}