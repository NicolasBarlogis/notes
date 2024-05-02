using FluentAssertions;
using Xunit;

namespace ParseDontValidate.Tests;

public class OfferIdValidationShould
{
    [Fact]
    public void Validate_valid_formats()
    {
        var valid = OfferId.IsValid("ABCDEF0123456789_123564_1_CDISFR");

        valid.Should().BeTrue();
    }

    [Theory]
    [InlineData("ABC", "too short")]
    [InlineData("ABCDEF01234567890", "too long")]
    [InlineData("ABCDEFG123456789", "non hexa chars")]
    public void Not_validate_product_ids_in_invalid_format(string invalidProductId, string reason)
    {
        var valid = OfferId.IsValid($"{invalidProductId}_123564_1_CDISFR");

        valid.Should().BeFalse(reason);
    }

    [Theory]
    [InlineData("1234A", "non numeric chars")]
    [InlineData("12.34", "non integer numeric value")]
    public void Not_validate_seller_ids_in_invalid_format(string invalidSellerId, string reason)
    {
        var valid = OfferId.IsValid($"ABCDEF0123456789_{invalidSellerId}_1_CDISFR");

        valid.Should().BeFalse(reason);
    }

    [Theory]
    [InlineData("9", "out of range")]
    [InlineData("a", "non numeric char")]
    public void Not_validate_product_conditions_in_invalid_format(string invalidProductCondition, string reason)
    {
        var valid = OfferId.IsValid($"ABCDEF0123456789_123564_{invalidProductCondition}_CDISFR");

        valid.Should().BeFalse(reason);
    }

    [Theory]
    [InlineData("CDISF", "too short")]
    [InlineData("CDISFRR", "too long")]
    [InlineData("cdisfr", "lower case chars")]
    [InlineData("CD1SFR", "non alpha chars")]
    public void Not_validate_sales_channels_in_invalid_format(string invalidSaleChannel, string reason)
    {
        var valid = OfferId.IsValid($"ABCDEF0123456789_123564_1_{invalidSaleChannel}");

        valid.Should().BeFalse(reason);
    }
}
