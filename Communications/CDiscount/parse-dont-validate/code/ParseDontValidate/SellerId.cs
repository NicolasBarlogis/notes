using LanguageExt;

namespace ParseDontValidate;

public record SellerId(long Value)
{
    public static bool IsValid(string sellerId) =>
        sellerId.All(char.IsDigit)
        && Convert.ToInt64(sellerId).ToString() == sellerId;

    public static Either<InvalidOfferId, SellerId> Parse(string potentialSellerId)
    {
        return !IsValid(potentialSellerId)
            ? new InvalidOfferId("Invalid SellerId", nameof(potentialSellerId))
            : new SellerId((long) Convert.ToInt64(potentialSellerId));
    }

    public sealed override string ToString() => Value.ToString();

}
