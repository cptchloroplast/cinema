using AutoFixture;
using AutoFixture.Xunit2;
namespace Movies.Test;
public class AutoMockDataAttribute : AutoDataAttribute
{
    public AutoMockDataAttribute() 
        :base(() => new Fixture()) {}
}