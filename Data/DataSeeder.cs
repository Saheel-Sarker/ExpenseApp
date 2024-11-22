using ExpenseApp.Data;
using ExpenseApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class DataSeeder
{
    private readonly ApplicationDbContext _context;

    public DataSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public void SeedData()
    {
        // Check if there is existing data in the database
        if (_context.Category.Any() || _context.Transaction.Any())
        {
            return; // Data already seeded
        }

        // Sample categories
        var categories = new List<Category>
        {
            new Category { Title = "Food", Icon = "icon-food", IsExpense = true },
            new Category { Title = "Transportation", Icon = "icon-transportation", IsExpense = true },
            new Category { Title = "Salary", Icon = "icon-salary", IsExpense = false },
            new Category { Title = "Groceries", Icon = "fas fa-shopping-cart", IsExpense = true },
            new Category { Title = "Utilities", Icon = "fas fa-lightbulb", IsExpense = true },
            new Category { Title = "Transportation", Icon = "fas fa-car", IsExpense = true },
            new Category { Title = "Salary", Icon = "fas fa-money-bill-wave", IsExpense = false },
            new Category { Title = "Investments", Icon = "fas fa-chart-line", IsExpense = false },
            // Add more sample categories as needed
        };

        // Sample transactions
        var random = new Random();
        var transactions = new List<Transaction>();

        for (int i = 0; i < 25; i++)
        {
            var category = categories[random.Next(categories.Count)];
            var isExpense = category.IsExpense;
            var amount = (decimal)(random.NextDouble() * 1000); // Random amount between 0 and 1000

            transactions.Add(new Transaction
            {
                Title = $"Transaction {i + 1}",
                Amount = amount,
                DateTime = DateTime.Now.AddDays(-i), // Random date in the past 100 days
                Category = category,
            });
        }

        // Add categories and transactions to the context
        _context.Category.AddRange(categories);
        _context.Transaction.AddRange(transactions);

        // Save changes to the database
        _context.SaveChanges();
    }
}