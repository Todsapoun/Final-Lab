using Final_Lab.Database;
using Final_Lab.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Lab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        //Variable
        private readonly DataDbContext _dbContext;

        //Cotructure Method
        public PositionsController(DataDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        // Get Push Put and Delete
        // Show All Data
        [HttpGet]
        public async Task<ActionResult<List<Positions>>> getPositions()
        {
            var Positions = await _dbContext.Positions.ToListAsync();

            if (Positions.Count == 0)
            {
                return NotFound();
            }

            return Ok(Positions);
        }

        // Get By Id
        [HttpGet("ID")]
        public async Task<ActionResult<Positions>> getPositionsID(string id)
        {
            var Positions = await _dbContext.Positions.FindAsync(id);
            if (Positions == null)
            {
                return NotFound();
            }
            return Ok(Positions);
        }

        // Get By getPositionsID
        [HttpGet("Positions ID")]
        public async Task<ActionResult<List<Employees>>> getEmpPositionsID(string positionId)
        {
            var employees = _dbContext.Employees.Where(e => e.position_Id == positionId).ToList();

            if (employees.Count == 0)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        // Post
        [HttpPost]
        public async Task<ActionResult<Positions>> postPosition(Positions positions)
        {
            try
            {
                _dbContext.Positions.Add(positions);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
            return Ok(positions);
        }

        // Put
        [HttpPut]
        public async Task<ActionResult<Positions>> putPosition(string id, Positions newPositions)
        {
            var Positions = await _dbContext.Positions.FindAsync(id);
            if (Positions == null)
            {
                return NotFound();
            }
            Positions.positionId = newPositions.positionId;
            Positions.positionName = newPositions.positionName;
            Positions.baseSalary = newPositions.baseSalary;
            Positions.salaryIncreaseRate = newPositions.salaryIncreaseRate;

            await _dbContext.SaveChangesAsync();
            return Ok(Positions);
        }

        // Delete
        [HttpDelete]
        public async Task<ActionResult<Positions>> deletePositions(string id)
        {
            var Employees = _dbContext.Employees.Where(e => e.position_Id == id).ToList();
            if (Employees != null && Employees.Count > 0)
            {
                return BadRequest("Cannot delete position with employees assigned to it.");
            }
            var position = _dbContext.Positions.SingleOrDefault(p => p.positionId == id);
            if (position == null)
            {
                return NotFound();
            }
            _dbContext.Positions.Remove(position);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
