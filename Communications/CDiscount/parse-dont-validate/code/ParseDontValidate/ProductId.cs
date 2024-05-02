using LanguageExt;

namespace ParseDontValidate;

public record ProductId
{
    public string Value { get; }

    public ProductId(string value)
    {
        if (!IsValid(value)) throw new InvalidOfferId("Invalid ProductId", nameof(value));
        Value = value;
    }

    public static bool IsValid(string productId) =>
        productId.Length == 16
        && productId.All(c => IsHexadecimal(c));

    private static bool IsHexadecimal(char c) =>
        c >= '0' && c <= '9'
        || c >= 'A' && c <= 'F';

    public static Either<InvalidOfferId, ProductId> Parse(string potentialProductId) =>
        !IsValid(potentialProductId)
            ? new InvalidOfferId("Invalid ProductId", nameof(potentialProductId))
            : new ProductId(potentialProductId);

    public void Deconstruct(out string Value)
    {
        Value = this.Value;
    }

    public sealed override string ToString() => Value;

}
