using Microsoft.ML.Data;

namespace ExpenseApp.Prediction
{
    public class PredictionOutput
    {
        [ColumnName("Score")]
        public float Amount { get; set; }
    }
}
