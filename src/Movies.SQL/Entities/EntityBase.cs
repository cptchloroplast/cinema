namespace Movies.SQL.Entities;
public abstract class EntityBase 
{
    public Guid SystemKey { get; set; }
    public DateTime SystemCreatedDate { get; set; }
    public DateTime SystemUpdatedDate { get; set; }
}