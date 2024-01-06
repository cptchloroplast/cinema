using Microsoft.Extensions.Logging;
using Okkema.Queue.Consumers;
using Movies.TMDB.Services;
using Movies.TMDB.Entities;
using AutoMapper;
using Movies.TMDB.Repositories;
using Okkema.Messages.Handlers;
namespace Movies.Commands.Handlers;
public class CreateTMDBMovieV1Handler : MessageHandlerBase<CreateTMDBMovieV1>
{
    private readonly IMapper _mapper;
    private readonly ITMDBService _tmdbService;
    private readonly ITMDBMovieRepository _movieRepository;
    public CreateTMDBMovieV1Handler(
        ILogger<CreateTMDBMovieV1Handler> logger,
        IConsumer<CreateTMDBMovieV1> consumer,
        IMapper mapper,
        ITMDBService tmdbService,
        ITMDBMovieRepository movieRepository) : base (logger, consumer)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _tmdbService = tmdbService ?? throw new ArgumentNullException(nameof(tmdbService));
        _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
    }
    public override async Task HandleAsync(CreateTMDBMovieV1 command, CancellationToken cancellationToken = default)
    {
        var entity = _movieRepository.Read(command.TmdbId);
        if (entity is null)
        {
            _logger.LogDebug("Get TMDB movie with {TmdbId}", command.TmdbId);
            var movie = await _tmdbService.GetMovieAsync(command.TmdbId, cancellationToken);
            if (movie is null)
            {
                _logger.LogWarning("No movie found for {TmdbId}", command.TmdbId);
                return;
            }
            _logger.LogInformation("Creating TMDB movie {Title}", movie.Title);
            entity = _mapper.Map<TMDBMovieEntity>(movie);
            _movieRepository.Create(entity);
        }
    }
}
