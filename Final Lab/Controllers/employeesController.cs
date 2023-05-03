using Final_Lab.Database;
using Final_Lab.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Final_Lab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly DataDbContext _dbContext;
        public EmployeesController(DataDbContext DbContext)
        {
            _dbContext = DbContext;
        }

        // Show All Data
        [HttpGet]
        public async Task<ActionResult<List<Employees>>> getEmployees()
        {
            var Employees = await _dbContext.Employees.ToListAsync();
            if (Employees.Count == 0)
            {
                return NotFound();
            }

            return Ok(Employees);
        }

        // Get ID
        [HttpGet("Id")]
        public async Task<ActionResult<Employees>> getEmployeeById(string id)
        {
            var employees = await _dbContext.Employees.FindAsync(id);
            if (employees == null)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        // Salary Current Year
        [HttpGet("Salary Current Year")]
        public async Task<ActionResult<Employees>> getSalaryCurrentYear(string id)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
            };
            var employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            
            var position = _dbContext.Positions.Find(employee.position_Id);
            var currentDate = DateTime.Now;
            var currentYearSalary = position.baseSalary;
            var salary = currentYearSalary;
            var currentYear = currentDate.Year;
            var firstYearOfWork = employee.hireDate.Year;
            var yearsOfExperience = (currentDate.Year - employee.hireDate.Year) - 1;

            if (position == null)
            {
                return NotFound();
            }
            for (int i = yearsOfExperience + 1; i <= currentYear; i++)
            {
                currentYearSalary = (float)(currentYearSalary * 1.15);
            }

            return new JsonResult(currentYearSalary, options);
        }

        // Future Salary
        [HttpGet("Future Salary")]
        public async Task<ActionResult<Employees>> getFutureSalary(string id, int year)
        {
            var employee = _dbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            var position = _dbContext.Positions.Find(employee.position_Id);
            if (position == null)
            {
                return NotFound();
            }
            var currentYear = DateTime.Now.Year;
            var totalYear = year - 1;
            var salary = position.baseSalary;

            if (totalYear <= currentYear)
            {
                return BadRequest();
            }

            for (int i = currentYear + 1; i <= totalYear; i++)
            {
                salary = (float)(salary * 1.15);
            }

            return Ok(salary);
        }

        // Post
        [HttpPost]
        public async Task<ActionResult<Employees>> createEmployees(Employees Employees)
        {
            try
            {
                var position = _dbContext.Positions.FirstOrDefault(p => p.positionId == Employees.position_Id);
                if (position == null)
                {
                    return BadRequest("Invalid position ID");
                }
                _dbContext.Employees.Add(Employees);
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
            return Ok(Employees);
        }

        // Put
        [HttpPut]
        public async Task<ActionResult<Employees>> putEmployees(string id, Employees newEmployees)
        {
            try
            {
                if (_dbContext.Positions.FirstOrDefault(p => p.positionId == newEmployees.position_Id) == null)
                {
                    return BadRequest("Invalid position ID");
                }
                var employees = await _dbContext.Employees.FindAsync(id);
                if (employees == null)
                {
                    return NotFound();
                }
                employees.empName = newEmployees.empName;
                employees.email = newEmployees.email;
                employees.phoneNumber = newEmployees.phoneNumber;
                employees.hireDate = newEmployees.hireDate;
                employees.position_Id = newEmployees.position_Id;

                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }
            return Ok(newEmployees);
        }
    }
}
