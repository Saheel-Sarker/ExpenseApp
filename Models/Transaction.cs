using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace ExpenseApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title Required")]
        public string Title { get; set; } = "";
        [Required(ErrorMessage = "Amount Required")]
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
