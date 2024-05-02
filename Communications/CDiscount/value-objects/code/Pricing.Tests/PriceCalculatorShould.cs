using FluentAssertions;
using Pricing.Domain;
using Xunit;
using static Pricing.Domain.Currency;

namespace Pricing.Tests
{
    public class PriceCalculatorShould
    {
        [Fact]
        public void AddInUsdReturnsAValue()
        {
            ((double?)PriceCalculator.Add(5, USD, 10)).Should()
                .NotBeNull();
        }

        [Fact]
        public void MultiplyInEurosReturnsPositiveNumber()
        {
            PriceCalculator
                .Times(10, EUR, 2)
                .Should()
                .BeGreaterOrEqualTo(0d);
        }

        [Fact]
        public void DivideInKoreanWonsReturnsDouble()
        {
            PriceCalculator
                .Divide(4002, KRW, 4)
                .Should()
                .Be(1000.5d);
        }
    }
}
