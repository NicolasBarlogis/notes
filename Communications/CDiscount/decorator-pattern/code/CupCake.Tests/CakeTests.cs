using FluentAssertions;
using Xunit;

namespace CupCake.Tests;

public class CakeTests
{
    [Fact]
    public void cupcakes_should_be_named_with_symbol()
    {
        var cupCake = new CupCake();
        cupCake.Name.Should().Be("🧁");
    }

    [Fact]
    public void cupcakes_should_cost_3_dollars()
    {
        var cupCake = new CupCake();
        cupCake.Price.ToString().Should().Be("3.00$");
    }

    [Fact]
    public void cookies_should_be_named_with_symbol()
    {
        var cookie = new Cookie();
        cookie.Name.Should().Be("🍪");
    }

    [Fact]
    public void cookies_should_cost_2_dollars()
    {
        var cookie = new Cookie();
        cookie.Price.ToString().Should().Be("2.00$");
    }
}
