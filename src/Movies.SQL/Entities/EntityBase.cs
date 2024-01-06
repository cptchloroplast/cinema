namespace Movies.SQL.Entities;
public abstract record EntityBase 
{
    public Guid SystemKey { get; init; } = Guid.NewGuid();
    public DateTime SystemCreatedDate { get; init; } = DateTime.UtcNow;
    public DateTime SystemModifiedDate { get; set; } = DateTime.UtcNow;
}