namespace Pricing.Domain;

public interface ICurrencyConverter
{
    Price Convert(Price price, Currency to);
}
