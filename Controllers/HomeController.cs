using ExpenseApp.Data;
using ExpenseApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ExpenseApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            DataSeeder dataSeeder = new DataSeeder(_context);
            dataSeeder.SeedData();
            var transactions = await _context.Transaction.Include(t => t.Category).ToListAsync();
            setTrendsData(transactions);
            setPieData(transactions);

            decimal totalIncome = transactions
                .Where(t => !t.Category.IsExpense)
                .Sum(t => t.Amount);
            ViewBag.TotalIncome = totalIncome;

            decimal totalExpense= transactions
                .Where(t => t.Category.IsExpense)
                .Sum(t => t.Amount);
            ViewBag.TotalExpense = totalExpense;
            decimal balance = totalIncome - totalExpense;
            ViewBag.Balance = balance; 

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void setTrendsData(List<Transaction> transactions)
        {
            Dictionary<DateTime, decimal> incomeDic = new Dictionary<DateTime, decimal>();
            Dictionary<DateTime, decimal> expenseDic = new Dictionary<DateTime, decimal>();

            DateTime thirtyDaysAgo = DateTime.Today.AddDays(-30);
            var groupedTransactions = transactions.Where(t => t.DateTime >= thirtyDaysAgo).GroupBy(t => t.DateTime.Date);
            foreach (var group in groupedTransactions)
            {
                var date = group.Key;
                var totalIncome = group.Where(t => !t.Category.IsExpense).Sum(t => t.Amount);
                var totalExpense = group.Where(t => t.Category.IsExpense).Sum(t => t.Amount);
                incomeDic[date] = totalIncome;
                expenseDic[date] = totalExpense;
            }

            var incomeData = incomeDic.Select(kvp => new { XValue = kvp.Key, YValue = kvp.Value }).ToArray();
            var expenseData = expenseDic.Select(kvp => new { XValue = kvp.Key, YValue = kvp.Value }).ToArray();

            ViewBag.IncomeData = incomeData;
            ViewBag.ExpenseData = expenseData;
        }

        private void setPieData(List<Transaction> transactions)
        {
            Dictionary<Category,decimal> categoryIncomes = new Dictionary<Category,decimal>();
            Dictionary<Category, decimal> categoryExpenses= new Dictionary<Category, decimal>();
            var groupedTransactions = transactions.GroupBy(t => t.Category);
            foreach ( var group in groupedTransactions)
            {
                Category category = group.Key;
                if (category.IsExpense)
                {
                    categoryExpenses[category] = group.Sum(t => t.Amount);
                }
                else
                {
                    categoryIncomes[category] = group.Sum(t => t.Amount);
                }
            }

            var incomeData = categoryIncomes.Select(kvp => new { XValue = kvp.Key.Title, YValue = kvp.Value }).ToArray();
            var expenseData = categoryExpenses.Select(kvp => new { XValue = kvp.Key.Title, YValue = kvp.Value }).ToArray();

            ViewBag.IncomeCategoryData = incomeData;
            ViewBag.ExpenseCategoryData = expenseData;
        }

    }
}
