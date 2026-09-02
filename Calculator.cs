using System.Globalization;

namespace InvoicingApp
{
    public partial class Program
    {
        interface ICaclulator
        {
            decimal Calculate(decimal amount);
        }
        public class StandardVatCalculator : ICaclulator
        {
            public decimal Calculate(decimal taxableAmount)
            {
                return taxableAmount * 18 / 100;
            }
        }
        public class ZeroRatedVatCalculator : ICaclulator
        {
            public decimal Calculate(decimal taxableAmount)
            {
                return 0;
            }
        }
        public class DiscountCalculator : ICaclulator
        {
            private decimal discountPercent;

            public decimal Calculate(decimal taxableAmount)
            {
                return taxableAmount * this.discountPercent/100;
            }
            public DiscountCalculator(decimal discountPercent)
            {
                this.discountPercent = discountPercent;
            }
        }
    }
}