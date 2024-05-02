namespace CupCake;

public abstract class Topping : ICake
{
    private readonly string _name;

    private readonly Price _price;

    private readonly ICake _cake;

    protected Topping(string name, ICake cake, float price)
    {
        _name = name;
        _cake = cake;
        _price = new Price(price);
    }

    public string Name => $"{_cake.Name} {(_cake is Topping ? "and" : "with")} {_name}";

    public Price Price => _price + _cake.Price;
}
