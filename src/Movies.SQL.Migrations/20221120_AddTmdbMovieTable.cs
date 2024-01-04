using FluentMigrator;
namespace Movies.SQL.Migrations;
[Migration(20221120)]
public class AddTmdbMovieTable : Migration
{
    public override void Up()
    {
        Create.Table("TmdbMovie")
            .WithColumn("Adult").AsBoolean().NotNullable()
            .WithColumn("BackdropPath").AsString().Nullable()
            .WithColumn("Budget").AsInt32().NotNullable()
            .WithColumn("Homepage").AsString().Nullable()
            .WithColumn("Id").AsInt32().NotNullable().Indexed()
            .WithColumn("ImdbId").AsString().NotNullable().Indexed()
            .WithColumn("OriginalLanguage").AsString().NotNullable()
            .WithColumn("OriginalTitle").AsString().NotNullable()
            .WithColumn("Overview").AsString().Nullable()
            .WithColumn("Popularity").AsFloat().NotNullable()
            .WithColumn("PosterPath").AsString().NotNullable()
            .WithColumn("ReleaseDate").AsDate().NotNullable()
            .WithColumn("Revenue").AsInt32().NotNullable()
            .WithColumn("Runtime").AsInt32().Nullable()
            .WithColumn("Status").AsString().NotNullable()
            .WithColumn("Tagline").AsString().Nullable()
            .WithColumn("Title").AsString().NotNullable()
            .WithColumn("Video").AsBoolean().NotNullable()
            .WithColumn("VoteAverage").AsFloat().NotNullable()
            .WithColumn("VoteCount").AsFloat().NotNullable()
            .WithColumn("SystemKey").AsGuid().PrimaryKey().NotNullable().Indexed()
            .WithColumn("SystemCreatedDate").AsString().NotNullable()
            .WithColumn("SystemModifiedDate").AsString().NotNullable();
    }
    public override void Down()
    {
        Delete.Table("TmdbMovie");
    }
}