using Employee_Management.Data;
using Employee_Management.DTO;
using Employee_Management.IRepo;
using Employee_Management.Model.Entity;
using Employee_Management.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly EmpDbContext _context;
		private readonly IEmployeeRepository employee;

		public EmployeeController(EmpDbContext context, IEmployeeRepository employee)
		{
			_context = context;
			this.employee = employee;
			
		}

		// Get all employees
		[HttpGet]
		public IActionResult GetEmployees()
		{
			var employees = employee.GetAllEmployees();
			return Ok(employees);
		}

		// Get a specific employee by ID
		[HttpGet("{id}")]
		public IActionResult GetEmployee(int id)
		{
			var employees = employee.GetEmployeeById(id);

			if (employee == null)
			{
				return NotFound();
			}
			return Ok(employees);
		}

		[HttpPost]
		public IActionResult CreateEmployee([FromBody] EmployeeDTO employeeDto)
		{
			

			if (employeeDto == null)
			{
				return BadRequest("Employee data is required.");
			}	

			var createdEmployee = employee.CreateEmployee(employeeDto);
			return CreatedAtAction(nameof(GetEmployee),
			new { id = createdEmployee.CompanyId }, createdEmployee);
		}



		[HttpDelete("{id}")]
		public IActionResult DeleteEmployee(int id)
		{
			// Find the employee by ID
			var employeeToDelete = employee.DeleteEmployee(id);

			// If the employee doesn't exist, return a NotFound response
			if (employeeToDelete == null)
			{
				return NotFound();
			}

			return Ok("everything is Deleted");
		}

		[HttpPut("{id}")]
		public IActionResult UpdateEmployee(int id, [FromBody] EmployeeDTO employeeDto)
		{
			if (employeeDto == null)
			{
				return BadRequest("Employee data is required.");
			}

			// Retrieve the existing employee and company
			var existingEmployee = employee.GetEmployeeById(id);

			// If the employee doesn't exist, return a NotFound response
			if (existingEmployee == null)
			{
				return NotFound();
			}

			// Call the repository method to update the employee and their associated company
			var updatedEmployee = employee.UpdateEmployee(id, employeeDto);

			return Ok(updatedEmployee); // Return the updated employee information
		}


	}
}