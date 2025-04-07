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
                new Transaction { Title = "Monthly Salary", Amount = 3000.00m, DateTime = new DateTime(2024, 5, 1), Category = categories[0] },
                new Transaction { Title = "Freelance Project", Amount = 500.00m, DateTime = new DateTime(2024, 5, 15), Category = categories[1] },
                new Transaction { Title = "Grocery Shopping", Amount = 150.00m, DateTime = new DateTime(2024, 5, 3), Category = categories[2] },
                new Transaction { Title = "Rent Payment", Amount = 1200.00m, DateTime = new DateTime(2024, 5, 5), Category = categories[3] },
                new Transaction { Title = "Electricity Bill", Amount = 100.00m, DateTime = new DateTime(2024, 5, 10), Category = categories[4] },
                new Transaction { Title = "Movie Night", Amount = 50.00m, DateTime = new DateTime(2024, 5, 7), Category = categories[5] },
                new Transaction { Title = "Bus Pass", Amount = 30.00m, DateTime = new DateTime(2024, 5, 12), Category = categories[6] },
                new Transaction { Title = "Dinner at Restaurant", Amount = 60.00m, DateTime = new DateTime(2024, 5, 20), Category = categories[7] },

                new Transaction { Title = "Monthly Salary", Amount = 3000.00m, DateTime = new DateTime(2024, 4, 1), Category = categories[0] },
                new Transaction { Title = "Freelance Project", Amount = 450.00m, DateTime = new DateTime(2024, 4, 10), Category = categories[1] },
                new Transaction { Title = "Grocery Shopping", Amount = 130.00m, DateTime = new DateTime(2024, 4, 5), Category = categories[2] },
                new Transaction { Title = "Rent Payment", Amount = 1200.00m, DateTime = new DateTime(2024, 4, 5), Category = categories[3] },
                new Transaction { Title = "Water Bill", Amount = 90.00m, DateTime = new DateTime(2024, 4, 12), Category = categories[4] },
                new Transaction { Title = "Concert Tickets", Amount = 75.00m, DateTime = new DateTime(2024, 4, 15), Category = categories[5] },
                new Transaction { Title = "Gasoline", Amount = 40.00m, DateTime = new DateTime(2024, 4, 18), Category = categories[6] },
                new Transaction { Title = "Dinner Out", Amount = 50.00m, DateTime = new DateTime(2024, 4, 22), Category = categories[7] },

                // May transactions
                new Transaction { Title = "Monthly Salary", Amount = 3000.00m, DateTime = new DateTime(2024, 5, 1), Category = categories[0] },
                new Transaction { Title = "Freelance Project", Amount = 500.00m, DateTime = new DateTime(2024, 5, 15), Category = categories[1] },
                new Transaction { Title = "Grocery Shopping", Amount = 150.00m, DateTime = new DateTime(2024, 5, 3), Category = categories[2] },
                new Transaction { Title = "Rent Payment", Amount = 1200.00m, DateTime = new DateTime(2024, 5, 5), Category = categories[3] },
                new Transaction { Title = "Electricity Bill", Amount = 100.00m, DateTime = new DateTime(2024, 5, 10), Category = categories[4] },
                new Transaction { Title = "Movie Night", Amount = 50.00m, DateTime = new DateTime(2024, 5, 7), Category = categories[5] },
                new Transaction { Title = "Bus Pass", Amount = 30.00m, DateTime = new DateTime(2024, 5, 12), Category = categories[6] },
                new Transaction { Title = "Dinner at Restaurant", Amount = 60.00m, DateTime = new DateTime(2024, 5, 20), Category = categories[7] },

                // June transactions
                new Transaction { Title = "Monthly Salary", Amount = 3000.00m, DateTime = new DateTime(2024, 6, 1), Category = categories[0] },
                new Transaction { Title = "Freelance Project", Amount = 600.00m, DateTime = new DateTime(2024, 6, 20), Category = categories[1] },
                new Transaction { Title = "Grocery Shopping", Amount = 140.00m, DateTime = new DateTime(2024, 6, 2), Category = categories[2] },
                new Transaction { Title = "Rent Payment", Amount = 1200.00m, DateTime = new DateTime(2024, 6, 5), Category = categories[3] },
                new Transaction { Title = "Utility Bill", Amount = 90.00m, DateTime = new DateTime(2024, 6, 10), Category = categories[4] },
                new Transaction { Title = "Concert Tickets", Amount = 75.00m, DateTime = new DateTime(2024, 6, 15), Category = categories[5] },
                new Transaction { Title = "Taxi Ride", Amount = 45.00m, DateTime = new DateTime(2024, 6, 18), Category = categories[6] },
                new Transaction { Title = "Lunch Out", Amount = 25.00m, DateTime = new DateTime(2024, 6, 20), Category = categories[7] },

                // July transactions
                new Transaction { Title = "Monthly Salary", Amount = 3100.00m, DateTime = new DateTime(2024, 7, 1), Category = categories[0] },
                new Transaction { Title = "Freelance Project", Amount = 700.00m, DateTime = new DateTime(2024, 7, 10), Category = categories[1] },
                new Transaction { Title = "Grocery Shopping", Amount = 140.00m, DateTime = new DateTime(2024, 7, 4), Category = categories[2] },
                new Transaction { Title = "Rent Payment", Amount = 1250.00m, DateTime = new DateTime(2024, 7, 5), Category = categories[3] },
                new Transaction { Title = "Water Bill", Amount = 80.00m, DateTime = new DateTime(2024, 7, 10), Category = categories[4] },
                new Transaction { Title = "Netflix Subscription", Amount = 15.00m, DateTime = new DateTime(2024, 7, 7), Category = categories[5] },
                new Transaction { Title = "Uber Ride", Amount = 20.00m, DateTime = new DateTime(2024, 7, 12), Category = categories[6] },
                new Transaction { Title = "Brunch Out", Amount = 35.00m, DateTime = new DateTime(2024, 7, 14), Category = categories[7] },
                new Transaction { Title = "Grocery Shopping", Amount = 170.00m, DateTime = new DateTime(2024, 7, 18), Category = categories[2] },

                // August transactions
                new Transaction { Title = "Monthly Salary", Amount = 3200.00m, DateTime = new DateTime(2024, 8, 1), Category = categories[0] },
                new Transaction { Title = "Freelance Project", Amount = 550.00m, DateTime = new DateTime(2024, 8, 20), Category = categories[1] },
                new Transaction { Title = "Grocery Shopping", Amount = 150.00m, DateTime = new DateTime(2024, 8, 2), Category = categories[2] },
                new Transaction { Title = "Rent Payment", Amount = 1300.00m, DateTime = new DateTime(2024, 8, 5), Category = categories[3] },
                new Transaction { Title = "Gas Bill", Amount = 70.00m, DateTime = new DateTime(2024, 8, 10), Category = categories[4] },
                new Transaction { Title = "Concert Tickets", Amount = 80.00m, DateTime = new DateTime(2024, 8, 15), Category = categories[5] },
                new Transaction { Title = "Bus Pass", Amount = 35.00m, DateTime = new DateTime(2024, 8, 18), Category = categories[6] },
                new Transaction { Title = "Dinner Out", Amount = 40.00m, DateTime = new DateTime(2024, 8, 20), Category = categories[7] },
                new Transaction { Title = "Grocery Shopping", Amount = 150.00m, DateTime = new DateTime(2024, 8, 22), Category = categories[2] },

                // September transactions
                new Transaction { Title = "Monthly Salary", Amount = 3300.00m, DateTime = new DateTime(2024, 9, 1), Category = categories[0] },
                new Transaction { Title = "Freelance Project", Amount = 600.00m, DateTime = new DateTime(2024, 9, 20), Category = categories[1] },
                new Transaction { Title = "Grocery Shopping", Amount = 160.00m, DateTime = new DateTime(2024, 9, 2), Category = categories[2] },
                new Transaction { Title = "Rent Payment", Amount = 1350.00m, DateTime = new DateTime(2024, 9, 5), Category = categories[3] },
                new Transaction { Title = "Electricity Bill", Amount = 85.00m, DateTime = new DateTime(2024, 9, 10), Category = categories[4] },
                new Transaction { Title = "Theater Tickets", Amount = 50.00m, DateTime = new DateTime(2024, 9, 15), Category = categories[5] },
                new Transaction { Title = "Taxi Ride", Amount = 25.00m, DateTime = new DateTime(2024, 9, 18), Category = categories[6] },
                new Transaction { Title = "Lunch Out", Amount = 30.00m, DateTime = new DateTime(2024, 9, 20), Category = categories[7] },
                new Transaction { Title = "Grocery Shopping", Amount = 150.00m, DateTime = new DateTime(2024, 9, 22), Category = categories[2] },

                // October transactions
                new Transaction { Title = "Monthly Salary", Amount = 3400.00m, DateTime = new DateTime(2024, 10, 1), Category = categories[0] },
                new Transaction { Title = "Freelance Project", Amount = 750.00m, DateTime = new DateTime(2024, 10, 20), Category = categories[1] },
                new Transaction { Title = "Grocery Shopping", Amount = 165.00m, DateTime = new DateTime(2024, 10, 2), Category = categories[2] },
                new Transaction { Title = "Rent Payment", Amount = 1400.00m, DateTime = new DateTime(2024, 10, 5), Category = categories[3] },
                new Transaction { Title = "Gas Bill", Amount = 90.00m, DateTime = new DateTime(2024, 10, 10), Category = categories[4] },
                new Transaction { Title = "Concert Tickets", Amount = 85.00m, DateTime = new DateTime(2024, 10, 15), Category = categories[5] },
                new Transaction { Title = "Bus Pass", Amount = 40.00m, DateTime = new DateTime(2024, 10, 18), Category = categories[6] },
                new Transaction { Title = "Dinner Out", Amount = 45.00m, DateTime = new DateTime(2024, 10, 20), Category = categories[7] },
                new Transaction { Title = "Grocery Shopping", Amount = 170.00m, DateTime = new DateTime(2024, 10, 22), Category = categories[2] },

                // November transactions
                new Transaction { Title = "Monthly Salary", Amount = 3500.00m, DateTime = new DateTime(2024, 11, 1), Category = categories[0] },
                new Transaction { Title = "Freelance Project", Amount = 800.00m, DateTime = new DateTime(2024, 11, 20), Category = categories[1] },
                new Transaction { Title = "Grocery Shopping", Amount = 175.00m, DateTime = new DateTime(2024, 11, 2), Category = categories[2] },
                new Transaction { Title = "Rent Payment", Amount = 1450.00m, DateTime = new DateTime(2024, 11, 5), Category = categories[3] },
                new Transaction { Title = "Electricity Bill", Amount = 100.00m, DateTime = new DateTime(2024, 11, 10), Category = categories[4] },
                new Transaction { Title = "Theater Tickets", Amount = 55.00m, DateTime = new DateTime(2024, 11, 15), Category = categories[5] },
                new Transaction { Title = "Taxi Ride", Amount = 30.00m, DateTime = new DateTime(2024, 11, 18), Category = categories[6] },
                new Transaction { Title = "Lunch Out", Amount = 35.00m, DateTime = new DateTime(2024, 11, 20), Category = categories[7] },
                new Transaction { Title = "Grocery Shopping", Amount = 180.00m, DateTime = new DateTime(2024, 11, 22), Category = categories[2] },

                // December transactions
                new Transaction { Title = "Monthly Salary", Amount = 3600.00m, DateTime = new DateTime(2024, 12, 1), Category = categories[0] },
                new Transaction { Title = "Freelance Project", Amount = 850.00m, DateTime = new DateTime(2024, 12, 20), Category = categories[1] },
                new Transaction { Title = "Grocery Shopping", Amount = 185.00m, DateTime = new DateTime(2024, 12, 2), Category = categories[2] },
                new Transaction { Title = "Rent Payment", Amount = 1500.00m, DateTime = new DateTime(2024, 12, 5), Category = categories[3] },
                new Transaction { Title = "Gas Bill", Amount = 95.00m, DateTime = new DateTime(2024, 12, 10), Category = categories[4] },
                new Transaction { Title = "Concert Tickets", Amount = 90.00m, DateTime = new DateTime(2024, 12, 15), Category = categories[5] },
                new Transaction { Title = "Bus Pass", Amount = 45.00m, DateTime = new DateTime(2024, 12, 18), Category = categories[6] },
                new Transaction { Title = "Dinner Out", Amount = 50.00m, DateTime = new DateTime(2024, 12, 20), Category = categories[7] },
                new Transaction { Title = "Grocery Shopping", Amount = 190.00m, DateTime = new DateTime(2024, 12, 22), Category = categories[2] },
            };

            // Add transactions before the cutoff date
   
          

            context.Transaction.AddRange(transactions);
            context.SaveChanges();
        }
    }
}
