using Xunit;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Contrib.HttpClient;
using System.Net;
using FluentAssertions;
using System.Text.Json;
using Movies.Test;
using Movies.TMDB.Models;
namespace Movies.TMDB.Test;
public class TMDBServiceTest
{
    private readonly HttpMessageHandler _handler;
    private readonly IHttpClientFactory _factory;
    private readonly IOptionsMonitor<TMDBOptions> _options;
    private readonly ITMDBService _service;
    private readonly string _apiKey;
    private readonly int _movieId;
    public TMDBServiceTest()
    {
        _handler = Mock.Of<HttpMessageHandler>();
        _factory = Mock.Get(_handler).CreateClientFactory();
        Mock.Get(_factory).Setup(x => x.CreateClient(nameof(TMDBService)))
            .Returns(() => {
                var client = Mock.Get(_handler).CreateClient();
                client.BaseAddress = new Uri("http://localhost");
                return client;
            });
        _options = Mock.Of<IOptionsMonitor<TMDBOptions>>();
        _apiKey = Guid.NewGuid().ToString("N");
        Mock.Get(_options).Setup(x => x.CurrentValue)
            .Returns(new TMDBOptions{ ApiKey = _apiKey });
        _service = new TMDBService(_factory, _options);
        _movieId = new Random().Next();
    }
    [Fact]
    public async Task ThrowsAnExceptionWhenUnableToDeserializeResponse()
    {
        Mock.Get(_handler).SetupAnyRequest()
            .ReturnsResponse(HttpStatusCode.OK);
        var act = () => _service.GetMovie(_movieId);
        await act.Should().ThrowAsync<JsonException>();
    }
    [Theory]
    [AutoMockData]
    public async Task AddsTheApiKeyToTheRequest(TMDBMovie movie)
    {
        Mock.Get(_handler).SetupAnyRequest()
            .ReturnsJsonResponse(HttpStatusCode.OK, movie);
        var result = await _service.GetMovie(_movieId);
        Mock.Get(_handler).VerifyRequest(x => x.RequestUri?.Query.Contains(_apiKey) ?? false);
    }
    [Theory]
    [AutoMockData]
    public async Task GetsAMovieById(TMDBMovie movie)
    {
        Mock.Get(_handler).SetupAnyRequest()
            .ReturnsJsonResponse(HttpStatusCode.OK, movie);
        var result = await _service.GetMovie(_movieId);
        result.Should().BeEquivalentTo(movie);
        Mock.Get(_handler).VerifyRequest(x => x.RequestUri?.AbsolutePath.Contains(_movieId.ToString()) ?? false);
    }
    [Fact]
    public async Task ThrowsAnExceptionWhenReceivesErrorStatusCode()
    {
        Mock.Get(_handler).SetupAnyRequest()
            .ReturnsResponse(HttpStatusCode.InternalServerError);
        var act = () => _service.GetMovie(_movieId);
        await act.Should().ThrowAsync<HttpRequestException>();
    }
}