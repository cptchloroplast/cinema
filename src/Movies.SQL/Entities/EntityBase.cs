namespace Movies.SQL.Entities;
public abstract record EntityBase 
{
    public Guid SystemKey { get; set; }
    public DateTime SystemCreatedDate { get; set; }
    public DateTime SystemModifiedDate { get; set; }
}