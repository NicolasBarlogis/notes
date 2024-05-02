namespace Pricing.Domain;

public sealed class Price: IEquatable<Price>
{
    public double Amount { get; }

    public Currency Currency { get; }

    private Price(double amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Price APriceOf(double amount, Currency currency)
    {
        AmountMustBePositive(amount);
        AmountMustBeFinite(amount);

        return new Price(amount, currency);
    }

    public Price Times(double value) => new(Amount * value, Currency);

    public Price DivideBy(double divisor) => new(Amount / divisor, Currency);

    public Price Plus(Price price)
    {
        CurrencyMustBeTheSame(price);

        return new Price(Amount + price.Amount, Currency);
    }

    private void CurrencyMustBeTheSame(Price price)
    {
        if (price.Currency != Currency)
            throw new ArgumentException("Can not add prices with different currencies");
    }

    private static void AmountMustBeFinite(double amount)
    {
        if (double.IsPositiveInfinity(amount))
            throw new ArgumentException("Price amount must have a finite value");
    }

    private static void AmountMustBePositive(double amount)
    {
        if (amount < 0)
            throw new ArgumentException("Price amount must not be a negative value");
    }

    public bool Equals(Price? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Amount.Equals(other.Amount) && Currency == other.Currency;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Price)obj);
    }

    public override int GetHashCode() => HashCode.Combine(Amount, (int)Currency);

    public static bool operator ==(Price? left, Price? right) =>  Equals(left, right);

    public static bool operator !=(Price? left, Price? right) =>  !Equals(left, right);
}
