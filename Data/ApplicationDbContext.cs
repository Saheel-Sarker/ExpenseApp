using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ExpenseApp.Models;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;

namespace ExpenseApp.Data
{
    public class ApplicationDbContext : IdentityDbContext <IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>()
                .HasMany(c => c.Transactions)
                .WithOne(t => t.Category)
                .HasForeignKey(t => t.CategoryId)
                .IsRequired();

            builder.Entity<Transaction>()
                .Property(t => t.Amount)
                .HasColumnType("decimal(18, 2)");

        }

           
        public DbSet<ExpenseApp.Models.Transaction> Transaction { get; set; } = default!;
        public DbSet<ExpenseApp.Models.Category> Category { get; set; } = default!;
    }
}
