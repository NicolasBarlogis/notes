using Xunit;

namespace CupCake.Tests;

public class BundleTests
{
    [Fact]
    public void check_bundle_name()
    {
        var bundle = new Bundle(new CupCake());

        Assert.Equal("📦 composed of 1 🧁", bundle.Name);
    }

    [Fact]
    public void check_bundle_name_with_cookie()
    {
        var bundle = new Bundle(new Cookie());

        Assert.Equal("📦 composed of 1 🍪", bundle.Name);
    }

    [Fact]
    public void check_bundle_price_who_contained_cookie()
    {
        var bundle = new Bundle(new Cookie());

        Assert.Equal("1.80$", bundle.Price.ToString());
    }

    [Fact]
    public void check_bundle_name_who_contained_chocolate()
    {
        var bundle = new Bundle(new Chocolate(new CupCake()));

        Assert.Equal("📦 composed of 1 🧁 with 🍫", bundle.Name);
    }

    [Fact]
    public void check_bundle_name_with_cookies()
    {
        var bundle = new Bundle(new CupCake(), new Cookie());

        Assert.Equal("📦 composed of 1 🧁 and 1 🍪", bundle.Name);
    }

    [Fact]
    public void check_bundle_price_who_contained_cake_and_cookie()
    {
        var bundle = new Bundle(new CupCake(), new Cookie());

        Assert.Equal("4.50$", bundle.Price.ToString());
    }

    [Fact]
    public void check_bundle_name_who_contained_two_cup_cake_and_three_cookie()
    {
        var bundle = new Bundle(new CupCake(), new CupCake(), new Cookie(), new Cookie(), new Cookie());

        Assert.Equal("📦 composed of 2 🧁 and 3 🍪", bundle.Name);
    }

    [Fact]
    public void check_bundle_price_who_contained_two_cup_cake_and_three_cookie()
    {
        var bundle = new Bundle(new CupCake(), new CupCake(), new Cookie(), new Cookie(), new Cookie());

        Assert.Equal("10.80$", bundle.Price.ToString());
    }

    [Fact]
    public void check_bundle_name_who_contained_one_bundle_and_two_cup_cake_and_one_cookie()
    {
        var bundle = new Bundle(new Bundle(new CupCake(), new CupCake()), new Cookie());

        Assert.Equal("📦 composed of 1 📦 composed of 2 🧁 and 1 🍪", bundle.Name);
    }

    [Fact]
    public void check_bundle_price_who_contained_one_bundle_and_two_cup_cake_and_one_cookie()
    {
        var bundle = new Bundle(new Bundle(new CupCake(), new CupCake()), new Cookie());

        Assert.Equal("6.66$", bundle.Price.ToString());
    }

    [Fact]
    public void check_bundle_name_who_contained_same_bundles()
    {
        var bundle = new Bundle(new Bundle(new CupCake(), new CupCake()),
            new Bundle(new CupCake(), new CupCake()));

        Assert.Equal("📦 composed of 2 📦 composed of 2 🧁", bundle.Name);
    }

    [Fact]
    public void check_bundle_price_who_contained_same_bundles()
    {
        var bundle = new Bundle(new Bundle(new CupCake(), new CupCake()),
            new Bundle(new CupCake(), new CupCake()));

        Assert.Equal("9.72$", bundle.Price.ToString());
    }
}
