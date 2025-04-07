using ExpenseApp.Models;

namespace ExpenseApp.Data
{
    public static class DataSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Ensure database is created
            context.Database.EnsureCreated();

            // Check if data is already seeded
            if (context.Category.Any() || context.Transaction.Any()) return;

            // Seed Categories
            var categories = new List<Category>
            {
                new Category { Title = "Salary", Icon = "💼", IsExpense = false },
                new Category { Title = "Freelance", Icon = "💻", IsExpense = false },
                new Category { Title = "Groceries", Icon = "🛒", IsExpense = true },
                new Category { Title = "Rent", Icon = "🏠", IsExpense = true },
                new Category { Title = "Utilities", Icon = "🔌", IsExpense = true },
                new Category { Title = "Entertainment", Icon = "🎬", IsExpense = true },
                new Category { Title = "Transportation", Icon = "🚗", IsExpense = true },
                new Category { Title = "Dining Out", Icon = "🍽", IsExpense = true },
            };

            context.Category.AddRange(categories);

            // Seed Transactions
            var transactions = new List<Transaction>
            {
                // May transactions
                new Transaction { Title = "Monthly Salary", Amount = 3000.00m, DateTime = new DateTime(2025, 5, 1).ToUniversalTime().ToUniversalTime().ToUniversalTime(), Category = categories[0] },
                new Transaction { Title = "Freelance Project", Amount = 500.00m, DateTime = new DateTime(2025, 5, 15).ToUniversalTime(), Category = categories[1] },
                new Transaction { Title = "Grocery Shopping", Amount = 150.00m, DateTime = new DateTime(2025, 5, 3).ToUniversalTime(), Category = categories[2] },
                new Transaction { Title = "Rent Payment", Amount = 1200.00m, DateTime = new DateTime(2025, 5, 5).ToUniversalTime(), Category = categories[3] },
                new Transaction { Title = "Electricity Bill", Amount = 100.00m, DateTime = new DateTime(2025, 5, 10).ToUniversalTime(), Category = categories[4] },
                new Transaction { Title = "Movie Night", Amount = 50.00m, DateTime = new DateTime(2025, 5, 7).ToUniversalTime(), Category = categories[5] },
                new Transaction { Title = "Bus Pass", Amount = 30.00m, DateTime = new DateTime(2025, 5, 12).ToUniversalTime(), Category = categories[6] },
                new Transaction { Title = "Dinner at Restaurant", Amount = 60.00m, DateTime = new DateTime(2025, 5, 20).ToUniversalTime(), Category = categories[7] },

                new Transaction { Title = "Monthly Salary", Amount = 3000.00m, DateTime = new DateTime(2025, 4, 1).ToUniversalTime(), Category = categories[0] },
                new Transaction { Title = "Freelance Project", Amount = 450.00m, DateTime = new DateTime(2025, 4, 10).ToUniversalTime(), Category = categories[1] },
                new Transaction { Title = "Grocery Shopping", Amount = 130.00m, DateTime = new DateTime(2025, 4, 5).ToUniversalTime(), Category = categories[2] },
                new Transaction { Title = "Rent Payment", Amount = 1200.00m, DateTime = new DateTime(2025, 4, 5).ToUniversalTime(), Category = categories[3] },
                new Transaction { Title = "Water Bill", Amount = 90.00m, DateTime = new DateTime(2025, 4, 12).ToUniversalTime(), Category = categories[4] },
                new Transaction { Title = "Concert Tickets", Amount = 75.00m, DateTime = new DateTime(2025, 4, 15).ToUniversalTime(), Category = categories[5] },
                new Transaction { Title = "Gasoline", Amount = 40.00m, DateTime = new DateTime(2025, 4, 18).ToUniversalTime(), Category = categories[6] },
                new Transaction { Title = "Dinner Out", Amount = 50.00m, DateTime = new DateTime(2025, 4, 22).ToUniversalTime(), Category = categories[7] },

                // May transactions
                new Transaction { Title = "Monthly Salary", Amount = 3000.00m, DateTime = new DateTime(2025, 5, 1).ToUniversalTime(), Category = categories[0] },
                new Transaction { Title = "Freelance Project", Amount = 500.00m, DateTime = new DateTime(2025, 5, 15).ToUniversalTime(), Category = categories[1] },
                new Transaction { Title = "Grocery Shopping", Amount = 150.00m, DateTime = new DateTime(2025, 5, 3).ToUniversalTime(), Category = categories[2] },
                new Transaction { Title = "Rent Payment", Amount = 1200.00m, DateTime = new DateTime(2025, 5, 5).ToUniversalTime(), Category = categories[3] },
                new Transaction { Title = "Electricity Bill", Amount = 100.00m, DateTime = new DateTime(2025, 5, 10).ToUniversalTime(), Category = categories[4] },
                new Transaction { Title = "Movie Night", Amount = 50.00m, DateTime = new DateTime(2025, 5, 7).ToUniversalTime(), Category = categories[5] },
                new Transaction { Title = "Bus Pass", Amount = 30.00m, DateTime = new DateTime(2025, 5, 12).ToUniversalTime(), Category = categories[6] },
                new Transaction { Title = "Dinner at Restaurant", Amount = 60.00m, DateTime = new DateTime(2025, 5, 20).ToUniversalTime(), Category = categories[7] },

                // June transactions
                new Transaction { Title = "Monthly Salary", Amount = 3000.00m, DateTime = new DateTime(2025, 6, 1).ToUniversalTime(), Category = categories[0] },
                new Transaction { Title = "Freelance Project", Amount = 600.00m, DateTime = new DateTime(2025, 6, 20).ToUniversalTime(), Category = categories[1] },
                new Transaction { Title = "Grocery Shopping", Amount = 140.00m, DateTime = new DateTime(2025, 6, 2).ToUniversalTime(), Category = categories[2] },
                new Transaction { Title = "Rent Payment", Amount = 1200.00m, DateTime = new DateTime(2025, 6, 5).ToUniversalTime(), Category = categories[3] },
                new Transaction { Title = "Utility Bill", Amount = 90.00m, DateTime = new DateTime(2025, 6, 10).ToUniversalTime(), Category = categories[4] },
                new Transaction { Title = "Concert Tickets", Amount = 75.00m, DateTime = new DateTime(2025, 6, 15).ToUniversalTime(), Category = categories[5] },
                new Transaction { Title = "Taxi Ride", Amount = 45.00m, DateTime = new DateTime(2025, 6, 18).ToUniversalTime(), Category = categories[6] },
                new Transaction { Title = "Lunch Out", Amount = 25.00m, DateTime = new DateTime(2025, 6, 20).ToUniversalTime(), Category = categories[7] },

                // July transactions
                new Transaction { Title = "Monthly Salary", Amount = 3100.00m, DateTime = new DateTime(2025, 7, 1).ToUniversalTime(), Category = categories[0] },
                new Transaction { Title = "Freelance Project", Amount = 700.00m, DateTime = new DateTime(2025, 7, 10).ToUniversalTime(), Category = categories[1] },
                new Transaction { Title = "Grocery Shopping", Amount = 140.00m, DateTime = new DateTime(2025, 7, 4).ToUniversalTime(), Category = categories[2] },
                new Transaction { Title = "Rent Payment", Amount = 1250.00m, DateTime = new DateTime(2025, 7, 5).ToUniversalTime(), Category = categories[3] },
                new Transaction { Title = "Water Bill", Amount = 80.00m, DateTime = new DateTime(2025, 7, 10).ToUniversalTime(), Category = categories[4] },
                new Transaction { Title = "Netflix Subscription", Amount = 15.00m, DateTime = new DateTime(2025, 7, 7).ToUniversalTime(), Category = categories[5] },
                new Transaction { Title = "Uber Ride", Amount = 20.00m, DateTime = new DateTime(2025, 7, 12).ToUniversalTime(), Category = categories[6] },
                new Transaction { Title = "Brunch Out", Amount = 35.00m, DateTime = new DateTime(2025, 7, 14).ToUniversalTime(), Category = categories[7] },
                new Transaction { Title = "Grocery Shopping", Amount = 170.00m, DateTime = new DateTime(2025, 7, 18).ToUniversalTime(), Category = categories[2] },

                // August transactions
                new Transaction { Title = "Monthly Salary", Amount = 3200.00m, DateTime = new DateTime(2025, 8, 1).ToUniversalTime(), Category = categories[0] },
                new Transaction { Title = "Freelance Project", Amount = 550.00m, DateTime = new DateTime(2025, 8, 20).ToUniversalTime(), Category = categories[1] },
                new Transaction { Title = "Grocery Shopping", Amount = 150.00m, DateTime = new DateTime(2025, 8, 2).ToUniversalTime(), Category = categories[2] },
                new Transaction { Title = "Rent Payment", Amount = 1300.00m, DateTime = new DateTime(2025, 8, 5).ToUniversalTime(), Category = categories[3] },
                new Transaction { Title = "Gas Bill", Amount = 70.00m, DateTime = new DateTime(2025, 8, 10).ToUniversalTime(), Category = categories[4] },
                new Transaction { Title = "Concert Tickets", Amount = 80.00m, DateTime = new DateTime(2025, 8, 15).ToUniversalTime(), Category = categories[5] },
                new Transaction { Title = "Bus Pass", Amount = 35.00m, DateTime = new DateTime(2025, 8, 18).ToUniversalTime(), Category = categories[6] },
                new Transaction { Title = "Dinner Out", Amount = 40.00m, DateTime = new DateTime(2025, 8, 20).ToUniversalTime(), Category = categories[7] },
                new Transaction { Title = "Grocery Shopping", Amount = 150.00m, DateTime = new DateTime(2025, 8, 22).ToUniversalTime(), Category = categories[2] },

                // September transactions
                new Transaction { Title = "Monthly Salary", Amount = 3300.00m, DateTime = new DateTime(2025, 9, 1).ToUniversalTime(), Category = categories[0] },
                new Transaction { Title = "Freelance Project", Amount = 600.00m, DateTime = new DateTime(2025, 9, 20).ToUniversalTime(), Category = categories[1] },
                new Transaction { Title = "Grocery Shopping", Amount = 160.00m, DateTime = new DateTime(2025, 9, 2).ToUniversalTime(), Category = categories[2] },
                new Transaction { Title = "Rent Payment", Amount = 1350.00m, DateTime = new DateTime(2025, 9, 5).ToUniversalTime(), Category = categories[3] },
                new Transaction { Title = "Electricity Bill", Amount = 85.00m, DateTime = new DateTime(2025, 9, 10).ToUniversalTime(), Category = categories[4] },
                new Transaction { Title = "Theater Tickets", Amount = 50.00m, DateTime = new DateTime(2025, 9, 15).ToUniversalTime(), Category = categories[5] },
                new Transaction { Title = "Taxi Ride", Amount = 25.00m, DateTime = new DateTime(2025, 9, 18).ToUniversalTime(), Category = categories[6] },
                new Transaction { Title = "Lunch Out", Amount = 30.00m, DateTime = new DateTime(2025, 9, 20).ToUniversalTime(), Category = categories[7] },
                new Transaction { Title = "Grocery Shopping", Amount = 150.00m, DateTime = new DateTime(2025, 9, 22).ToUniversalTime(), Category = categories[2] },

                // October transactions
                new Transaction { Title = "Monthly Salary", Amount = 3400.00m, DateTime = new DateTime(2025, 10, 1).ToUniversalTime(), Category = categories[0] },
                new Transaction { Title = "Freelance Project", Amount = 750.00m, DateTime = new DateTime(2025, 10, 20).ToUniversalTime(), Category = categories[1] },
                new Transaction { Title = "Grocery Shopping", Amount = 165.00m, DateTime = new DateTime(2025, 10, 2).ToUniversalTime(), Category = categories[2] },
                new Transaction { Title = "Rent Payment", Amount = 1400.00m, DateTime = new DateTime(2025, 10, 5).ToUniversalTime(), Category = categories[3] },
                new Transaction { Title = "Gas Bill", Amount = 90.00m, DateTime = new DateTime(2025, 10, 10).ToUniversalTime(), Category = categories[4] },
                new Transaction { Title = "Concert Tickets", Amount = 85.00m, DateTime = new DateTime(2025, 10, 15).ToUniversalTime(), Category = categories[5] },
                new Transaction { Title = "Bus Pass", Amount = 40.00m, DateTime = new DateTime(2025, 10, 18).ToUniversalTime(), Category = categories[6] },
                new Transaction { Title = "Dinner Out", Amount = 45.00m, DateTime = new DateTime(2025, 10, 20).ToUniversalTime(), Category = categories[7] },
                new Transaction { Title = "Grocery Shopping", Amount = 170.00m, DateTime = new DateTime(2025, 10, 22).ToUniversalTime(), Category = categories[2] },

                // November transactions
                new Transaction { Title = "Monthly Salary", Amount = 3500.00m, DateTime = new DateTime(2025, 11, 1).ToUniversalTime(), Category = categories[0] },
                new Transaction { Title = "Freelance Project", Amount = 800.00m, DateTime = new DateTime(2025, 11, 20).ToUniversalTime(), Category = categories[1] },
                new Transaction { Title = "Grocery Shopping", Amount = 175.00m, DateTime = new DateTime(2025, 11, 2).ToUniversalTime(), Category = categories[2] },
                new Transaction { Title = "Rent Payment", Amount = 1450.00m, DateTime = new DateTime(2025, 11, 5).ToUniversalTime(), Category = categories[3] },
                new Transaction { Title = "Electricity Bill", Amount = 100.00m, DateTime = new DateTime(2025, 11, 10).ToUniversalTime(), Category = categories[4] },
                new Transaction { Title = "Theater Tickets", Amount = 55.00m, DateTime = new DateTime(2025, 11, 15).ToUniversalTime(), Category = categories[5] },
                new Transaction { Title = "Taxi Ride", Amount = 30.00m, DateTime = new DateTime(2025, 11, 18).ToUniversalTime(), Category = categories[6] },
                new Transaction { Title = "Lunch Out", Amount = 35.00m, DateTime = new DateTime(2025, 11, 20).ToUniversalTime(), Category = categories[7] },
                new Transaction { Title = "Grocery Shopping", Amount = 180.00m, DateTime = new DateTime(2025, 11, 22).ToUniversalTime(), Category = categories[2] },

                // December transactions
                new Transaction { Title = "Monthly Salary", Amount = 3600.00m, DateTime = new DateTime(2025, 12, 1).ToUniversalTime(), Category = categories[0] },
                new Transaction { Title = "Freelance Project", Amount = 850.00m, DateTime = new DateTime(2025, 12, 20).ToUniversalTime(), Category = categories[1] },
                new Transaction { Title = "Grocery Shopping", Amount = 185.00m, DateTime = new DateTime(2025, 12, 2).ToUniversalTime(), Category = categories[2] },
                new Transaction { Title = "Rent Payment", Amount = 1500.00m, DateTime = new DateTime(2025, 12, 5).ToUniversalTime(), Category = categories[3] },
                new Transaction { Title = "Gas Bill", Amount = 95.00m, DateTime = new DateTime(2025, 12, 10).ToUniversalTime(), Category = categories[4] },
                new Transaction { Title = "Concert Tickets", Amount = 90.00m, DateTime = new DateTime(2025, 12, 15).ToUniversalTime(), Category = categories[5] },
                new Transaction { Title = "Bus Pass", Amount = 45.00m, DateTime = new DateTime(2025, 12, 18).ToUniversalTime(), Category = categories[6] },
                new Transaction { Title = "Dinner Out", Amount = 50.00m, DateTime = new DateTime(2025, 12, 20).ToUniversalTime(), Category = categories[7] },
                new Transaction { Title = "Grocery Shopping", Amount = 190.00m, DateTime = new DateTime(2025, 12, 22).ToUniversalTime(), Category = categories[2] },
            };

            // Add transactions before the cutoff date
   
          

            context.Transaction.AddRange(transactions);
            context.SaveChanges();
        }
    }
}
