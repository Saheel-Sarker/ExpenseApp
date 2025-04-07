using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.ComponentModel.DataAnnotations;

namespace ExpenseApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title Required")]
        public string Title { get; set; } = "";
        public string Icon { get; set; } = "";
        public bool IsExpense { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    }
}
