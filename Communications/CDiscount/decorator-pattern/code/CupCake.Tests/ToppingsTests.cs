using System.Runtime.InteropServices;
using FluentAssertions;
using Xunit;

namespace CupCake.Tests;

public class ToppingsTests
{
    [Fact]
    public void Should_have_compound_name_when_contains_one_topping()
    {
        var cupCake = new Chocolate(new CupCake());
        cupCake.Name.Should().Be("🧁 with 🍫");
    }

    [Fact]
    public void Should_cost_sum_of_topic_and_cake_prices_when_it_have_one_topping()
    {
        var cookie = new Nut(new Cookie());
        cookie.Price.ToString().Should().Be("2.20$");
    }

    [Fact]
    public void Should_have_compound_name_when_contains_many_toppings()
    {
        var chocolateCookie = new Chocolate(new Cookie());
        var nutChocolateCookie = new Nut(chocolateCookie);
        var sugarNutChocolateCookie = new Sugar(nutChocolateCookie);

        chocolateCookie.Name.Should().Be("🍪 with 🍫");
        nutChocolateCookie.Name.Should().Be("🍪 with 🍫 and 🥜");
        sugarNutChocolateCookie.Name.Should().Be("🍪 with 🍫 and 🥜 and 🍚");
    }

    [Fact]
    public void Should_cost_sum_of_topics_and_cake_prices_when_it_have_many_toppings()
    {
        var chocolateCookie = new Chocolate(new Cookie());
        var nutChocolateCookie = new Nut(chocolateCookie);
        var sugarNutChocolateCookie = new Sugar(nutChocolateCookie);

        chocolateCookie.Price.ToString().Should().Be("2.10$");
        nutChocolateCookie.Price.ToString().Should().Be("2.30$");
        sugarNutChocolateCookie.Price.ToString().Should().Be("2.80$");
    }
}

interface IDomainService {
    void MakeSomeStuff();
}
class SomeDomainService: IDomainService {
    public void MakeSomeStuff()
    { /* ... */ }
}
class FeaturedDomainService: IDomainService {
    private readonly IDomainService _decoratedService;
    private readonly bool _featureEnabled = false;

    public FeaturedDomainService(IDomainService decoratedService, bool featureEnabled)
    {
        _decoratedService = decoratedService;
        _featureEnabled = featureEnabled;
    }

    public void MakeSomeStuff()
    {
        if (_featureEnabled)
            _decoratedService.MakeSomeStuff();

    }
}
class LoggedDomainService: IDomainService {
    private readonly IDomainService _decoratedService;
    public LoggedDomainService(IDomainService decoratedService)
    {
        _decoratedService = decoratedService;
    }

    public void MakeSomeStuff()
    {
        Log("before");
        _decoratedService.MakeSomeStuff();
        Log("after");
    }

    private void Log(string log)
    { /* ... */ }
}
