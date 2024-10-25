using InterviewPrctice_Webapi.Model;
using InterviewPrctice_Webapi.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewPrctice_Webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetEmployees()
        {
            var employees = await _context.Employees
                .Include(e => e.Experiences)
                .Select(e => new EmployeeDTO
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeName = e.EmployeeName,
                    IsActive = e.IsActive,
                    JoinDate = e.JoinDate,
                    Experiences = e.Experiences.Select(x => new ExperienceDTO
                    {
                        ExperienceId = x.ExperienceId,
                        Title = x.Title,
                        Duration = x.Duration
                    }).ToList()
                }).ToListAsync();

            return Ok(employees);
        }

        // GET: api/Employee/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> GetEmployee(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Experiences)
                .Where(e => e.EmployeeId == id)
                .Select(e => new EmployeeDTO
                {
                    EmployeeId = e.EmployeeId,
                    EmployeeName = e.EmployeeName,
                    IsActive = e.IsActive,
                    JoinDate = e.JoinDate,
                    Experiences = e.Experiences.Select(x => new ExperienceDTO
                    {
                        ExperienceId = x.ExperienceId,
                        Title = x.Title,
                        Duration = x.Duration
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> PostEmployee(EmployeeDTO employeeDTO)
        {
            var employee = new Employee
            {
                EmployeeName = employeeDTO.EmployeeName,
                IsActive = employeeDTO.IsActive,
                JoinDate = employeeDTO.JoinDate,
                Experiences = employeeDTO.Experiences.Select(x => new Experience
                {
                    Title = x.Title,
                    Duration = x.Duration
                }).ToList()
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            employeeDTO.EmployeeId = employee.EmployeeId;

            return CreatedAtAction(nameof(GetEmployee), new { id = employee.EmployeeId }, employeeDTO);
        }

        // PUT: api/Employee/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeDTO employeeDTO)
        {
            if (id != employeeDTO.EmployeeId)
            {
                return BadRequest();
            }

            var employee = await _context.Employees.Include(e => e.Experiences).FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.EmployeeName = employeeDTO.EmployeeName;
            employee.IsActive = employeeDTO.IsActive;
            employee.JoinDate = employeeDTO.JoinDate;

            // Update Experiences
            employee.Experiences.Clear();
            foreach (var exp in employeeDTO.Experiences)
            {
                employee.Experiences.Add(new Experience
                {
                    Title = exp.Title,
                    Duration = exp.Duration
                });
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Employee/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.Include(e => e.Experiences).FirstOrDefaultAsync(e => e.EmployeeId == id);

            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }



}
