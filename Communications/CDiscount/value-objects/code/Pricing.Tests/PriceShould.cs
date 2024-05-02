using System;
using FluentAssertions;
using Pricing.Domain;
using Xunit;
using static Pricing.Domain.Price;
using static Pricing.Domain.Currency;

namespace Pricing.Tests;

public class PriceShould
{
    [Fact]
    public void be_multiplied()
    {
        var price = APriceOf(10, EUR);
        var twice = price.Times(2);
        twice.Should().Be(APriceOf(20, EUR));
    }

    [Fact]
    public void be_divided()
    {
        APriceOf(10, EUR)
            .DivideBy(2)
            .Should()
                .Be(APriceOf(5, EUR));
    }

    [Fact]
    public void be_added_to_price_with_same_currency()
    {
        APriceOf(10, EUR)
            .Plus(APriceOf(10, EUR))
            .Should()
                .Be(APriceOf(20, EUR));
    }

    [Fact]
    public void be_not_added_to_price_with_different_currency()
    {
        var addDifferentCurrencies = () => APriceOf(10, EUR).Plus(APriceOf(10, USD));
        addDifferentCurrencies.Should().Throw<ArgumentException>().WithMessage("Can not add prices with different currencies");
    }

    [Fact]
    public void have_positive_amount()
    {
        var buildingNegativePrice = () => APriceOf(-1, EUR);
        buildingNegativePrice.Should().Throw<ArgumentException>().WithMessage("Price amount must not be a negative value");
    }

    [Fact]
    public void have_finite_amount()
    {
        var buildingInfinitePrice = () => APriceOf(double.PositiveInfinity, EUR);
        buildingInfinitePrice.Should().Throw<ArgumentException>().WithMessage("Price amount must have a finite value");
    }
}
