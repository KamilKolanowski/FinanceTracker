using FinanceTrackerAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerAPI.Services;

public class FinanceTrackerDbService
{
    private readonly FinanceTrackerDbContext _context;

    public FinanceTrackerDbService(FinanceTrackerDbContext context)
    {
        _context = context;
    }

    internal async Task<IEnumerable<Expense>> GetExpenses()
    {
        return await _context.Expenses.ToListAsync();
    }
    internal async Task<Expense?> GetExpense(int id)
    {
        return await _context.Expenses.FindAsync(id);
    }
    
    internal async Task AddExpense(Expense expense)
    {
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
    }

    internal async Task DeleteExpense(int id)
    {
        _context.Expenses.Remove(await _context.Expenses.FindAsync(id));
        await _context.SaveChangesAsync();
    }

    internal async Task UpdateExpense(Expense expense)
    {
        _context.Expenses.Update(expense);
        await _context.SaveChangesAsync();
    }
}