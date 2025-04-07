using Microsoft.ML.Data;

namespace ExpenseApp.Prediction
{
    public class TransactionData
    {
        [LoadColumn(0)]
        public float Date { get; set; }
        [LoadColumn(1)]
        public float Amount { get; set; }
        [LoadColumn(2)]
        public float MonthOfYearSin { get; set; }
        [LoadColumn(3)]
        public float MonthOfYearCos { get; set; }

        public float DayOfMonthSin { get; set; }
        [LoadColumn(3)]
        public float DayOfMonthCos { get; set; }

        public int CategoryId { get; set; }

        public TransactionData(DateTime dateTime, decimal amount, int categoryId)
        {
            Amount = (float)amount;
            Date = dateTime.Ticks;
            DayOfMonthSin = (float)Math.Sin(2 * Math.PI * dateTime.Day / 7.0);
            DayOfMonthCos = (float)Math.Cos(2 * Math.PI * dateTime.Day / 7.0);
            MonthOfYearSin = (float)Math.Sin(2 * Math.PI * dateTime.Month / 12.0);
            MonthOfYearCos = (float)Math.Cos(2 * Math.PI * dateTime.Month / 12.0);
            CategoryId = categoryId;

        }
    }

}
