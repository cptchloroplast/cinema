namespace Movies.Commands;
public abstract record CommandBase 
{
  public Guid SystemKey { get; init; } = Guid.NewGuid();
  public DateTime SystemCreatedDate { get; init; } = DateTime.UtcNow;
}