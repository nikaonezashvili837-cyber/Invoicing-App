namespace InvoicingApp
{
    public partial class Program
    {
        public interface IVatCaclulator
        {
            decimal CalculateVat(decimal subtotal);
        }
        public class StandardVatCalculator:IVatCaclulator
        {
            public decimal CalculateVat(decimal subtotal)
            {
                return subtotal*18/100;
            }
        }
        public class ZeroRatedVatCalculator : IVatCaclulator
        {
            public decimal CalculateVat(decimal subtotal)
            {
                return 0;
            }
        }
    }
}