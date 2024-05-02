namespace Pricing.Domain;

public static class PriceCalculator
{
    public static double Add(double amount, Currency currency, double amount2) => amount + amount2;
    public static double Times(double amount, Currency currency, int value) => amount * value;
    public static double Divide(double amount, Currency currency, int value) => amount / value;
}
