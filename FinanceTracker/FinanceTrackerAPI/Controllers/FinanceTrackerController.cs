using FinanceTrackerAPI.Models;
using FinanceTrackerAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceTrackerController : ControllerBase
    {
        private readonly FinanceTrackerDbContext _context;
        private readonly FinanceTrackerDbService _service;
        public FinanceTrackerController(FinanceTrackerDbContext context, FinanceTrackerDbService service)
        {
            _context = context;
            _service = service;
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
            catch (HttpIOException) {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database error occurred while retrieving the expense.");
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
            catch (HttpIOException) {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database error occurred while adding the expense.");
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while adding the expense: " + ex.Message);
            }
        }




    }
    // catch

    // return new ObjectResult("Update failed: " + ex.Message)
    // {
    //     StatusCode = StatusCodes.Status500InternalServerError
    // };
    // (Exception ex)
    // {
    // }
        
    }
}
