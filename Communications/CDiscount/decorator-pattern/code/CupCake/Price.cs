namespace CupCake;

public record Price(float Value)
{
    public override string ToString()
        => $"{this.Value:N2}$".Replace(",", ".");

    public static Price operator +(Price price) => price;

    public static Price operator +(Price price, Price otherPrice)
        => new Price(price.Value + otherPrice.Value);

    public static Price operator *(Price price, float multiplier)
        => new Price(price.Value * multiplier);
}
