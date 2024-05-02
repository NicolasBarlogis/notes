using LanguageExt;

namespace ParseDontValidate;

public enum ProductCondition
{
    LikeNew = 1,
    VeryGoodState,
    GoodState,
    AverageState,
    Refurbished,
    New,
}

internal static class ProductConditionParser
{
    public static Either<InvalidOfferId, ProductCondition> Parse(string potentialProductCondition)
    {
        if (!IsValid(potentialProductCondition)
            || !Enum.TryParse(potentialProductCondition, out ProductCondition productCondition)
           ) {
            return new InvalidOfferId("Invalid ProductCondition", nameof(potentialProductCondition));
        }
        return productCondition;
    }

    public static bool IsValid(string productCondition) =>
        productCondition.All(char.IsDigit)
        &&  Enum.IsDefined(typeof(ProductCondition), int.Parse(productCondition));
}
