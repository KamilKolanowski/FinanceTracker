using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FinanceTracker.Models;

namespace FinanceTracker.Services;

internal class ExpensesApi
{
    private readonly HttpClient _sharedHttpClient = new ()
    {
        BaseAddress = new Uri("http://localhost:5271"),
    };
    
    internal async Task<List<ExpenseDto>> GetExpensesAsync()
    {
        try
        {
            return await _sharedHttpClient.GetFromJsonAsync<List<ExpenseDto>>("/api/expenses");
        }
        catch (HttpRequestException e)
        {
            throw new Exception("Error fetching expenses: " + e.Message);
        }
    }

    internal async Task<ExpenseDto> GetExpenseAsync(int id)
    {
        try
        {
            return await _sharedHttpClient.GetFromJsonAsync<ExpenseDto>("/api/expenses/" + id);
        }
        catch (HttpRequestException e)
        {
            throw new Exception("Error fetching expense with ID: " + id + ": " + e.Message);
        }
    }
    
    internal async Task<string> AddExpenseAsync(string expenseJson)
    {
        try
        {
            var content = new StringContent(expenseJson, System.Text.Encoding.UTF8, "application/json");
            var response = await _sharedHttpClient.PostAsync("/api/expenses", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException e)
        {
            throw new Exception($"Error adding expense: {e.Message}");
        }
    }

    internal async Task<string> UpdateExpenseAsync(ExpenseDto expenseDto)
    {
        try
        {
            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(expenseDto),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await _sharedHttpClient.PutAsync("/api/expenses/" + expenseDto.Id, content);
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException e)
        {
            throw new Exception($"Error updating expense: {e.Message}");
        }
    }

    internal async Task<string> DeleteExpenseAsync(int id)
    {
        try
        {
            var response = await _sharedHttpClient.DeleteAsync("/api/expenses/" + id);
            return "Expense deleted successfully.";
        }
        catch (HttpRequestException e)
        {
            throw new Exception($"Error deleting expense: {e.Message}");
        }
    }
}