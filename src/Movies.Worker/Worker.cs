using Movies.TMDB;
namespace Movies.Worker;
public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ITMDBService _tmdb;
    public Worker(ILogger<Worker> logger, ITMDBService tmdb)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _tmdb = tmdb ?? throw new ArgumentNullException(nameof(tmdb));
    }
    protected override async Task ExecuteAsync(CancellationToken cancellation)
    {
        try
        {
            var movie = await _tmdb.GetMovie(348); // Alien
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
        }
    }
}
