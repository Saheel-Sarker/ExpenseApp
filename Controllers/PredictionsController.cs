using ExpenseApp.Data;
using ExpenseApp.Models;
using ExpenseApp.Prediction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using System.Diagnostics.Metrics;

namespace ExpenseApp.Controllers
{
    public class GraphPredictionController : Controller
    {
        private ApplicationDbContext _context;

        public GraphPredictionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int days, string sortOrder)
        {
            var transactions = await _context.Transaction.Include(t => t.Category).ToListAsync();

            PredictionModel transactionPredictor = new PredictionModel(transactions);
            transactionPredictor.Train();

            DateTime start = DateTime.Now;
            List<Transaction> futureExpenses = transactionPredictor.GetPrediction(start, days, true);
            List<Transaction> futureIncomes = transactionPredictor.GetPrediction(start, days, false);
            List<Transaction> futureTransactions = futureExpenses.Concat(futureIncomes).ToList();
            
            Dictionary<DateTime, float> expenseDates = ConvertToDictionary(futureExpenses);
            Dictionary<DateTime, float> incomeDates = ConvertToDictionary(futureIncomes);



            float totalIncome = 0;
            float totalExpense = 0;

            foreach (var t in futureExpenses)
            {
                totalExpense += (float)t.Amount;
            }

            foreach (var t in futureIncomes)
            {
                totalIncome += (float)t.Amount;
            }


            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSort = String.IsNullOrEmpty(sortOrder) ? "Date_desc" : "";
            ViewBag.CategorySort = sortOrder == "Category" ? "Category_desc" : "Category";
            ViewBag.AmountSort = sortOrder == "Amount" ? "Amount_desc" : "Amount";
            switch (sortOrder)
            {
                case "Date_desc":
                     futureTransactions = futureTransactions.OrderByDescending(t => t.DateTime).ToList();
                    break;
                case "Amount": 
                    futureTransactions = futureTransactions.OrderBy(t => t.Category.IsExpense ? 1 : -1)
                        .ThenBy(t => t.Amount * (t.Category.IsExpense ? 1 : -1)).ToList();
                    break;
                case "Amount_desc":
                    futureTransactions = futureTransactions.OrderByDescending(t => t.Category.IsExpense ? 1 : -1)
                        .ThenByDescending(t => t.Amount * (t.Category.IsExpense ? 1 : -1)).ToList();
                    break;
                case "Category":
                    futureTransactions = futureTransactions.OrderBy(t => t.Category.Title).ToList();
                    break;
                case "Category_desc":
                    futureTransactions = futureTransactions.OrderByDescending(t => t.Category.Title).ToList();
                    break;
                default: //change this
                    futureTransactions = futureTransactions.OrderBy(t => t.DateTime).ToList();
                    break;
            }

            ViewBag.TotalIncome = totalIncome;
            ViewBag.TotalExpense = totalExpense;
            ViewBag.Earnings = totalIncome - totalExpense;
            //
            //
            ViewBag.Days = days;

            ViewBag.ExpenseDates = expenseDates;
            ViewBag.IncomeDates = incomeDates;

            return View(futureTransactions);
        }

        private Dictionary<DateTime, float> ConvertToDictionary(List<Transaction> transactions)
        {
            return transactions.GroupBy(t => t.DateTime.Date)
                               .ToDictionary(g => g.Key, g => g.Sum(t => (float)t.Amount));
        }

    }

    public class PredictedTransaction
    {
        public DateTime Date { get; set; }
        public float Amount { get; set; }
        public string Category { get; set; }

        public PredictedTransaction(DateTime date, float amount, string category)
        {
            Date = date;
            Amount = amount;
            Category = category;
        }
    }

}
