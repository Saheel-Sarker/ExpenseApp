using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExpenseApp.Data;
using ExpenseApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;


namespace ExpenseApp.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;


        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;

        }

        // GET: Transactions
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSort = sortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.AmountSort = sortOrder == "Amount" ? "Amount_desc" : "Amount";
            ViewBag.CategorySort = sortOrder == "Category" ? "Category_desc" : "Category";
            ViewBag.DateSort = String.IsNullOrEmpty(sortOrder) ? "Date_desc" : "";

            var transactions = from t in _context.Transaction.Include(t => t.Category)
                               select t;
            List<Transaction> tempTransactionList = new List<Transaction>();

            if (!String.IsNullOrEmpty(searchString))
            {
                transactions = transactions.Where(t => t.Title.Contains(searchString) 
                || t.Title.Contains(searchString)
                || t.Category.Title.Contains(searchString)
                || t.DateTime.ToString().Contains(searchString)
                || t.Amount.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    transactions = transactions.OrderByDescending(t => t.Title);
                    break;
                case "Title":
                    transactions = transactions.OrderBy(t => t.Title);
                    break;
                case "Date_desc":
                    transactions = transactions.OrderByDescending(t => t.DateTime);
                    break;
                case "Amount":
                    transactions = transactions
                        .OrderBy(t => t.Category.IsExpense ? 1 : -1)
                        .ThenBy(t => t.Amount * (t.Category.IsExpense ? 1 : -1));  
                    break;
                case "Amount_desc":
                    transactions = transactions.OrderByDescending(t => t.Category.IsExpense ? 1 : -1)
                        .ThenByDescending(t => t.Amount * (t.Category.IsExpense ? 1 : -1));
                    break;
                case "Category":
                    transactions = transactions.OrderBy(t => t.Category.Title);
                    break;
                case "Category_desc":
                    transactions = transactions.OrderByDescending(t => t.Category.Title);
                    break;
                default:
                    transactions = transactions.OrderBy(t => t.DateTime);
                    break;
            }

            return View(transactions);
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            var categories = _context.Category.Select(c => new
            {
                c.Id,
                Title = (c.IsExpense ? "🟥 " : "🟩 ") + c.Title
            }).ToList();

            ViewData["CategoryId"] = new SelectList(categories, "Id", "Title");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Amount,DateTime,CategoryId")] Transaction transaction)
        {

            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var categories = _context.Category.Select(c => new
            {
                c.Id,
                Title = (c.IsExpense ? "🟥 " : "🟩 ") + c.Title
            }).ToList();

            ViewData["CategoryId"] = new SelectList(categories, "Id", "Title", transaction.CategoryId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Title", transaction.CategoryId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Amount,DateTime,CategoryId")] Transaction transaction)
        {

            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Title", transaction.CategoryId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transaction
                .Include(t => t.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transaction.FindAsync(id);
            if (transaction != null)
            {
                _context.Transaction.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transaction.Any(e => e.Id == id);
        }
    }
}
