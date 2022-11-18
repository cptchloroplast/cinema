using FluentMigrator;
namespace Movies.SQL.Migrations;
[Migration(20221116)]
public class AddMovieTable : Migration
{
    public override void Up()
    {
        Create.Table("Movies")
            .WithColumn("TmdbId").AsInt64().PrimaryKey().Identity()
            .WithColumn("Title").AsString().NotNullable()
            .WithColumn("CreatedDate").AsString().NotNullable()
            .WithColumn("ModifiedDate").AsString().NotNullable();
    }
    public override void Down()
    {
        Delete.Table("Movies");
    }
}