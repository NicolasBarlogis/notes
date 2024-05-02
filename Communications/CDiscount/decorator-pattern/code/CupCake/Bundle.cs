namespace CupCake;

public class Bundle : ICake
{
    private readonly IEnumerable<ICake> _products;

    public Bundle(ICake cake, params ICake[] products)
    {
        _products = products.ToList().Prepend(cake);
    }

    public string Name =>
        $"📦 composed of {string.Join(" and ", ProductsWithNumbers().Select(FormatProductWithNumber()))}";

    private IEnumerable<IGrouping<string, string>> ProductsWithNumbers()
    {
        return _products.Select(product => product.Name).GroupBy(name => name);
    }

    private static Func<IGrouping<string, string>, string> FormatProductWithNumber()
    {
        return groupedProduct => $"{groupedProduct.Count()} {groupedProduct.Key}";
    }

    public Price Price => (_products.Aggregate( new Price(0), (price, product) => price + product.Price) * 0.9f);
}
