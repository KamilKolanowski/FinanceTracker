using FinanceTrackerAPI.Models;
using FinanceTrackerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly FinanceTrackerDbContext _context;
        private readonly FinanceTrackerDbService _service;

        public ExpensesController(FinanceTrackerDbContext context, FinanceTrackerDbService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            try
            {
                var expenses = await _service.GetExpenses();
                return Ok(expenses);
            }
            catch (HttpIOException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database error occurred while retrieving expenses.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while retrieving expenses: " + ex.Message);
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpense(int id)
        {
            try
            {
                var expense = await _service.GetExpense(id);
                if (expense == null)
                {
                    return NotFound("Expense not found.");
                }

                return Ok(expense);
            }
            catch (HttpIOException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database error occurred while retrieving the expense.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while retrieving the expense: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostExpense([FromBody] Expense expense)
        {
            try
            {
                _context.Expenses.Add(expense);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
            }
            catch (HttpIOException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database error occurred while adding the expense.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while adding the expense: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutExpense(int id, [FromBody] Expense expense)
        {
            if (id != expense.Id)
            {
                return BadRequest("Expense Id mismatch.");
            }

            try
            {
                await _service.UpdateExpense(expense);
                return NoContent();
            }
            catch (HttpIOException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database error occurred while updating the expense.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while updating the expense: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteExpense(int id)
        {
            try
            {
                await _service.DeleteExpense(id);
                return NoContent();
            }
            catch (HttpIOException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Database error occurred while deleting the expense.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while deleting the expense: " + ex.Message);
            }
        }
    }
}

