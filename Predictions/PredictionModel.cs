using ExpenseApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.ML;
using Microsoft.ML.Data;
using Syncfusion.EJ2.Spreadsheet;
using System;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpenseApp.Prediction
{
    public class PredictionModel
    {
        private readonly MLContext _mlContext;
        private ITransformer _incomeModel;
        private ITransformer _expenseModel;
        private List<Transaction> _transactions;
        private List<TransactionData> _incomeData;
        private List<TransactionData> _expenseData;
        private const double _modelThreshold = 0.5;


        public PredictionModel(List<Transaction> transactions)
        {
            _mlContext = new MLContext(seed: 0);
            _transactions = transactions;
            _incomeData = new List<TransactionData>();
            _expenseData = new List<TransactionData>();
            foreach (Transaction transaction in transactions)
            {
                if (!transaction.Category.IsExpense)
                {

                    _incomeData.Add(new TransactionData(transaction.DateTime, transaction.Amount, transaction.CategoryId));
                }
                else if (transaction.Category.IsExpense)
                {
                    
                    _expenseData.Add(new TransactionData(transaction.DateTime, transaction.Amount, transaction.CategoryId));
                }
            }
        }

        public void Train()
        {
            Console.WriteLine("here");
            if (_incomeData.Count > 0)
            {
                Console.WriteLine("Hi");
                TrainModel(ref _incomeModel, _incomeData, "Income");
            }
            if (_expenseData.Count > 0)
            {
                Console.WriteLine("Bye");
                TrainModel(ref _expenseModel, _expenseData, "Expense");

            }
        }

        public void TrainModel(ref ITransformer model, List<TransactionData> data, string modelName)
        {
            IDataView trainingData = _mlContext.Data.LoadFromEnumerable<TransactionData>(data);
            var pipeline = _mlContext.Transforms.Conversion.MapValueToKey(nameof(TransactionData.CategoryId), nameof(TransactionData.CategoryId))
                .Append(_mlContext.Transforms.Categorical.OneHotEncoding(nameof(TransactionData.CategoryId), nameof(TransactionData.CategoryId)))
                .Append(_mlContext.Transforms.Concatenate("Features",
                    nameof(TransactionData.Date),
                    nameof(TransactionData.DayOfMonthSin),
                    nameof(TransactionData.DayOfMonthCos),
                    nameof(TransactionData.MonthOfYearSin),
                    nameof(TransactionData.MonthOfYearCos),
                    nameof(TransactionData.CategoryId))) // Include the one-hot encoded CategoryId
                .Append(_mlContext.Transforms.NormalizeMinMax("Features")) // Normalize the features
                .Append(_mlContext.Regression.Trainers.FastTree(labelColumnName: nameof(TransactionData.Amount), featureColumnName: "Features"));
            model = pipeline.Fit(trainingData);
            Evaluate(model, trainingData, modelName);
        }

        public void Evaluate(ITransformer model, IDataView testData, string modelName)
        {
            var predictions = model.Transform(testData);
            var metrics = _mlContext.Regression.Evaluate(predictions, nameof(TransactionData.Amount), "Score");

            Console.WriteLine();
            Console.WriteLine($"*************************************************");
            Console.WriteLine($"*       {modelName} Model quality metrics evaluation");
            Console.WriteLine($"*------------------------------------------------");
            Console.WriteLine($"*       RSquared Score:      {metrics.RSquared:0.##}");
            Console.WriteLine($"*       Root Mean Squared Error:      {metrics.RootMeanSquaredError:#.##}");
            Console.WriteLine($"*       Mean Absolute Error:      {metrics.MeanAbsoluteError:#.##}");
        }
        public List<Transaction> GetPrediction(DateTime startDate, int numberOfDays, bool isExpense)
        {
            RegressionMetrics metric;
            List<Transaction> predictedTransactions = new List<Transaction>();
            List<TransactionDataWithCategory> futureTransactions = GetFutureTransactionsDates(startDate, numberOfDays, isExpense);

            if (!isExpense)
            {
                if (_incomeData.Count == 0)
                {
                    return predictedTransactions;
                }
                IDataView incomeTrainingData = _mlContext.Data.LoadFromEnumerable<TransactionData>(_incomeData);
                var incomePredictions = _incomeModel.Transform(incomeTrainingData);
                metric = _mlContext.Regression.Evaluate(incomePredictions, nameof(TransactionData.Amount), "Score");
                if (metric.RSquared < _modelThreshold)
                {
                    var groupedTransactions = _transactions
                                     .Where(t => !t.Category.IsExpense)
                                     .GroupBy(t => t.Category).ToList();
                    Dictionary<Category, float> categoryAmount = new Dictionary<Category, float>();
                    foreach (var group in groupedTransactions)
                    {
                        float count = group.Count();
                        float amount = 0;
                        foreach (var transaction in group)
                        {
                            amount = amount + (float)transaction.Amount;
                        }
                        float averageCategory = amount / count;
                        categoryAmount[group.Key] = averageCategory;
                    }

                    foreach (var transaction in futureTransactions)
                    {
                        foreach (var group in categoryAmount)
                        {
                            if (group.Key.Id == transaction.TransactionData.CategoryId)
                            {
                                predictedTransactions.Add(new Transaction()
                                {
                                    Title = "",
                                    Amount = (decimal)group.Value,
                                    DateTime = new DateTime((long)transaction.TransactionData.Date),
                                    Category = group.Key,

                                });
                            }
                        }
                    }

                    return predictedTransactions;
                }
            } 
            else if (isExpense)
            {
                if (_expenseData.Count == 0)
                {
                    return predictedTransactions;
                }
                IDataView expenseTrainingData = _mlContext.Data.LoadFromEnumerable<TransactionData>(_expenseData);
                var expensePredictions = _expenseModel.Transform(expenseTrainingData);
                metric = _mlContext.Regression.Evaluate(expensePredictions, nameof(TransactionData.Amount), "Score");
                if (metric.RSquared < _modelThreshold)
                {
                    var groupedTransactions = _transactions
                                    .Where(t => t.Category.IsExpense)
                                    .GroupBy(t => t.Category).ToList();
                    Dictionary<Category, float> categoryAmount = new Dictionary<Category,float>();
                    foreach ( var group in groupedTransactions)
                    {
                        float count = group.Count();
                        float amount = 0;
                        foreach (var transaction in group)
                        {
                            amount = amount + (float)transaction.Amount;
                        }    
                        float averageCategory = amount / count;
             
                        categoryAmount[group.Key] = averageCategory;
                    }
                   
                    foreach (var transaction in futureTransactions)
                    {
                        foreach (var group in categoryAmount)
                        {
                            if (group.Key.Id == transaction.TransactionData.CategoryId)
                            {
                                predictedTransactions.Add(new Transaction()
                                {
                                    Title = "",
                                    Amount = (decimal)group.Value,
                                    DateTime = new DateTime((long)transaction.TransactionData.Date),
                                    Category = group.Key,

                                });
                            }
                        }
                    }


                    return predictedTransactions;
                }
            }
            
            var predictionFunction = _mlContext.Model.CreatePredictionEngine<TransactionData, PredictionOutput>(isExpense ? _expenseModel : _incomeModel);
            foreach (TransactionDataWithCategory t in futureTransactions)
            {
                predictedTransactions.Add(new Transaction()
                {
                    Title = "",
                    Amount = (decimal)predictionFunction.Predict(t.TransactionData).Amount,
                    DateTime = new DateTime((long)t.TransactionData.Date),
                    Category = t.Category
                });

            }

            return predictedTransactions;
        }
 

        public List<TransactionDataWithCategory> GetFutureTransactionsDates(DateTime startDate, int days, bool isExpense)
        {
            List<TransactionDataWithCategory> futureTransactions = new List<TransactionDataWithCategory>();
            DateTime BeforeDate = startDate.AddMonths(-1);
            foreach (Transaction t in _transactions)
            {
                if (BeforeDate < t.DateTime.Date && t.DateTime.Date < BeforeDate.AddDays(days) && t.Category.IsExpense == isExpense)
                {
                    /*Console.WriteLine($"Transaction is: {t.Title} and amount is: {t.Amount}");*/
                    futureTransactions.Add(new TransactionDataWithCategory(new TransactionData(t.DateTime.AddMonths(1), t.Amount, t.CategoryId), t.Category));
                }
            }
            return futureTransactions;
        }
    }

    public class TransactionDataWithCategory
    {
        public TransactionData TransactionData { get; set; }
        public Category Category { get; set; }

        public TransactionDataWithCategory(TransactionData transactionData, Category category)
        {
            TransactionData = transactionData;
            Category = category;
        }
    }
}
