using System;
using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models;

public class ExpenseDto
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public DateTime ExpenseDate { get; set; }

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public string Category { get; set; } = string.Empty;

    [Required]
    public string PaymentMethod { get; set; } = string.Empty;
}