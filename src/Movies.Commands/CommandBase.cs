namespace Movies.Commands;
public abstract record CommandBase 
{
  public Guid SystemKey { get; set; }
}