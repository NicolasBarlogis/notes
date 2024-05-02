using LanguageExt;

namespace ParseDontValidate;

public record SalesChannelId
{
    public string Value { get; }

    public SalesChannelId(string value)
    {
        if (!IsValid(value)) throw new InvalidOfferId("Invalid SalesChannelId", nameof(value));
        Value = value;
    }

    public static Either<InvalidOfferId, SalesChannelId> Parse(string potentialSalesChannelId) =>
        !IsValid(potentialSalesChannelId)
            ? new InvalidOfferId("Invalid SalesChannelId", nameof(potentialSalesChannelId))
            : new SalesChannelId(potentialSalesChannelId);

    public static bool IsValid(string saleChannel) =>
        saleChannel.Length == 6
        && saleChannel.All(c=> char.IsLetter(c) && char.IsUpper(c));

    public void Deconstruct(out string Value) => Value = this.Value;

    public sealed override string ToString() => Value;
}
