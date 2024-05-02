using System.Linq;
using LanguageExt;

namespace ParseDontValidate;

public record OfferId(ProductId Product, SellerId Seller, ProductCondition ProductCondition, SalesChannelId SalesChannel)
{
    public static Either<InvalidOfferId, OfferId> Parse(string potentialOfferId)
    {
        var parts = potentialOfferId.Split('_');

        return parts.Length != 4
            ? new InvalidOfferId("Invalid offerId format", nameof(potentialOfferId)) : ParseSafely(parts);
    }

    private static Either<InvalidOfferId, OfferId> ParseSafely(string[] parts)
    {
        return from productId in ProductId.Parse(parts[0])
            from sellerId in SellerId.Parse(parts[1])
            from productCondition in ProductConditionParser.Parse(parts[2])
            from salesChannel in SalesChannelId.Parse(parts[3])
            select new OfferId(productId, sellerId, productCondition, salesChannel);
    }

    public static bool IsValid(string offerId)
    {
        var parts = offerId.Split('_');

        return parts.Length == 4
           && ProductId.IsValid(parts[0])
           && SellerId.IsValid(parts[1])
           && ProductConditionParser.IsValid(parts[2])
           && SalesChannelId.IsValid(parts[3]);
    }

    public sealed override string ToString() =>
        $"{Product.ToString()}_{Seller.ToString()}_{ProductCondition.GetHashCode().ToString()}_{SalesChannel.ToString()}";
}
