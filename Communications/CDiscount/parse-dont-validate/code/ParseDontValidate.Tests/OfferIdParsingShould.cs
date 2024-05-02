using FluentAssertions;
using LanguageExt.UnsafeValueAccess;
using Xunit;

namespace ParseDontValidate.Tests;

public class OfferIdParsingShould
{
    [Fact]
    public void Succeed_when_format_is_valid()
    {
        var parsingResult = OfferId.Parse("ABCDEF0123456789_123564_1_CDISFR");

        parsingResult.IsRight.Should().BeTrue();

        parsingResult.ValueUnsafe().Should().Be(new OfferId(
            new ProductId("ABCDEF0123456789"), new SellerId(123564),
            ProductCondition.LikeNew, new SalesChannelId("CDISFR")
        ));
    }

    [Theory]
    [InlineData("ABC", "too short")]
    [InlineData("ABCDEF01234567890", "too long")]
    [InlineData("ABCDEFG123456789", "non hexa chars")]
    public void Fail_when_product_id_is_invalid(string invalidProductId, string reason)
    {
        var parsingResult = OfferId.Parse($"{invalidProductId}_123564_1_CDISFR");

        parsingResult.IsLeft.Should().BeTrue(reason);
        parsingResult.Swap().ValueUnsafe().Message.Should().Contain("Invalid ProductId");
    }

    [Theory]
    [InlineData("1234A", "non numeric chars")]
    [InlineData("12.34", "non integer numeric value")]
    public void Fail_when_seller_id_is_invalid(string invalidSellerId, string reason)
    {

        var parsingResult = OfferId.Parse($"ABCDEF0123456789_{invalidSellerId}_1_CDISFR");

        parsingResult.IsLeft.Should().BeTrue(reason);
        parsingResult.Swap().ValueUnsafe().Message.Should().Contain("Invalid SellerId");
    }

    [Theory]
    [InlineData("9", "out of range")]
    [InlineData("a", "non numeric char")]
    public void Fail_when_product_conditions_is_invalid(string invalidProductCondition, string reason)
    {
        var parsingResult = OfferId.Parse($"ABCDEF0123456789_123564_{invalidProductCondition}_CDISFR");

        parsingResult.IsLeft.Should().BeTrue(reason);
        parsingResult.Swap().ValueUnsafe().Message.Should().Contain("Invalid ProductCondition");
    }

    [Theory]
    [InlineData("CDISF", "too short")]
    [InlineData("CDISFRR", "too long")]
    [InlineData("cdisfr", "lower case chars")]
    [InlineData("CD1SFR", "non alpha chars")]
    public void Not_validate_sales_channels_in_invalid_format(string invalidSaleChannel, string reason)
    {
        var parsingResult = OfferId.Parse($"ABCDEF0123456789_123564_1_{invalidSaleChannel}");

        parsingResult.IsLeft.Should().BeTrue(reason);
        parsingResult.Swap().ValueUnsafe().Message.Should().Contain("Invalid SalesChannelId");
    }

    [Fact]
    public void Make_a_round_trip()
    {
        OfferId.Parse("ABCDEF0123456789_123564_1_CDISFR")
            .Match(offerId => offerId.ToString(), _ => "")
            .Should()
            .Be("ABCDEF0123456789_123564_1_CDISFR");
    }
}
