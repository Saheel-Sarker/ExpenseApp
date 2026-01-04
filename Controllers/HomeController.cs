using ExpenseApp.Data;
using ExpenseApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ExpenseApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        decimal totalIncome;
        decimal totalExpense;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // DataSeeder.Seed(_context);

            var transactions = await _context.Transaction.Include(t => t.Category).ToListAsync();
            totalIncome = transactions
            .Where(t => !t.Category.IsExpense)
            .Sum(t => t.Amount);
            ViewBag.TotalIncome = totalIncome;

            totalExpense = transactions
                .Where(t => t.Category.IsExpense)
                .Sum(t => t.Amount);
            ViewBag.TotalExpense = totalExpense;
            setTrendsData(transactions);
            setPieData(transactions);

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
            var groupedTransactions = transactions.Where(t => t.DateTime >= thirtyDaysAgo && t.DateTime < DateTime.Now).GroupBy(t => t.DateTime.Date);
            for (DateTime date = thirtyDaysAgo; date <= DateTime.Today; date = date.AddDays(1))
            {
                incomeDic[date] = 0;
                expenseDic[date] = 0;
            }
            foreach (var group in groupedTransactions)
            {
                var date = group.Key;
                var incomeThatDay = group.Where(t => !t.Category.IsExpense).Sum(t => t.Amount);
                var ExpenseThatDay = group.Where(t => t.Category.IsExpense).Sum(t => t.Amount);
                Console.WriteLine($"date: {date} and income: {incomeThatDay}");
                Console.WriteLine($"date: {date} and income: {ExpenseThatDay}");
                incomeDic[date] = incomeThatDay;
                expenseDic[date] = ExpenseThatDay;
            }

            ViewBag.IncomeData = incomeDic;
            ViewBag.ExpenseData = expenseDic;
        }

        private void setPieData(List<Transaction> transactions)
        {
            Dictionary<string,decimal> categoryIncomes = new Dictionary<string,decimal>();
            Dictionary<string, decimal> categoryExpenses= new Dictionary<string, decimal>();
            var groupedTransactions = transactions.GroupBy(t => t.Category);
            foreach ( var group in groupedTransactions)
            {
                Category category = group.Key;
                if (category.IsExpense)
                {
                    categoryExpenses[category.Title + ' ' + category.Icon] = Math.Round(group.Sum(t => t.Amount) / totalExpense * 100);
                }
                else
                {
                    categoryIncomes[category.Title + ' ' + category.Icon] = Math.Round(group.Sum(t => t.Amount) / totalIncome * 100);
                }
            }

            ViewBag.IncomeCategoryData = categoryIncomes;
            ViewBag.ExpenseCategoryData = categoryExpenses;
        }
    }
}
