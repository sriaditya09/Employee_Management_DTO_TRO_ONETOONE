using Employee_Management.Data;
using Employee_Management.DTO;
using Employee_Management.IRepo;
using Employee_Management.Model.Entity;
using Employee_Management.RTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Employee_Management.Repository
{
	public class EmployeeRepository : IEmployeeRepository 
	{
		private readonly EmpDbContext context;

		public EmployeeRepository(EmpDbContext context)
		{
			this.context = context;
		}

		public List<EmployeeRTO> GetAllEmployees()
		{



			var employees = context.Company
						  .Include(a => a.Employees)  // Include related employee data
						  .ThenInclude(b => b.Salary)  // Include related Salary data for each employee
						  .Select(a => new EmployeeRTO
						  {
							  Id = a.Id, // Assuming this is the company's ID
							  Company_Name = a.Company_Name,
							  Company_Address = a.Company_Address,
							  Username = a.Employees.FirstOrDefault().Username,
							  Name = a.Employees.FirstOrDefault().Name,
							  Email = a.Employees.FirstOrDefault().Email,
							  Salary_Amount = a.Employees.FirstOrDefault().Salary.Salary_Amount
						  }).ToList();

			return employees;
		}



		public EmployeeDTO CreateEmployee(EmployeeDTO employeeDto)
		{
			var company = new Company()
			{
				Company_Name = employeeDto.Company_Name,
				Company_Address = employeeDto.Company_Address,
				Pincode = employeeDto.Pincode
			};

			context.Company.Add(company);
			context.SaveChanges();



			var employee = new Employees()  // Assuming the entity is named Employee
			{
				Username = employeeDto.Username,
				Password = employeeDto.Password,
				Name = employeeDto.Name,
				Age = employeeDto.Age,
				Email = employeeDto.Email,
				Phone = employeeDto.Phone,
				CompanyId = company.Id,  // Assuming the company is already retrieved
				SalaryId = employeeDto.Salary// Set SalaryId, ensure you have a valid Salary entity
			};


			context.Employees.Add(employee);
			context.SaveChanges();

			var salary = new Salary()
			{
				Grade = employeeDto.Grade,
				Salary_Amount = employeeDto.Salary,
			};

			context.Salaries.Add(salary);
			context.SaveChanges();

			var createdEmployeeDto = new EmployeeDTO
			{
				Username = employee.Username,
				Name = employee.Name,
				Age = employee.Age,
				Email = employee.Email,
				Phone = employee.Phone,
				Company_Name = company.Company_Name  
			};
			return createdEmployeeDto;
		}
		//public bool DeleteEmployee(int id)
		//{
		//	// Find the company by the employee ID
		//	var company = context.Company
		//						 .Include(c => c.Employees)
		//						 .ThenInclude(a => a.Salaries)// Ensure employee data is included
		//						 .FirstOrDefault(c => c.Id == id); // Find company by employee ID

		//	if(company == null)
		//	{
		//		return false;
		//	}

		//	// Remove the company (which will also remove the associated employee due to the relationship)
		//	//if(company.Employees != null)
		//	//{
		//	//	context.Emp.Remove(company.Employees);
		//	//}

		//	if (company.Employees != null && company.Employees.Any())
		//	{
		//		context.Company.RemoveRange(company.Employees); // Remove all employees of the company
		//	}


		//	context.Company.Remove(company);
		//	context.SaveChanges();

		//	return true; // Return true if deletion was successful
		//}

		//public EmployeeDTO UpdateEmployee(int id, EmployeeDTO employeeDto)
		//{
		//	// Find the company by employee ID
		//	var company = context.Company
		//						 .Include(c => c.Employees) // Include the employee data
		//						 .FirstOrDefault(c => c.Id == id); // Find company by employee ID

		//	// If the company (and employee) is not found, return null
		//	if (company == null)
		//	{
		//		return null; // Return null if no company or employee is found
		//	}

		//	// Update the employee details
		//	company.Employees!.Username = employeeDto.Username;
		//	company.Employees.Password = employeeDto.Password;
		//	company.Employees.Name = employeeDto.Name;
		//	company.Employees.Age = employeeDto.Age;
		//	company.Employees.Email = employeeDto.Email;
		//	company.Employees.Phone = employeeDto.Phone;

		//	// Update the company details
		//	company.Company_Name = employeeDto.Company_Name;
		//	company.Company_Address = employeeDto.Company_Address;
		//	company.Pincode = employeeDto.Pincode;

		//	// Save the changes to the database
		//	context.SaveChanges();

		//	// Return the updated employee data
		//	return new EmployeeDTO
		//	{
		//		Username = company.Employees.Username,
		//		Password = company.Employees.Password,
		//		Name = company.Employees.Name,
		//		Age = company.Employees.Age,
		//		Email = company.Employees.Email,
		//		Phone = company.Employees.Phone,
		//		Company_Name = company.Company_Name,
		//		Company_Address = company.Company_Address,
		//		Pincode = company.Pincode,
		//		EmployeeId = company.Employees.Id
		//	};
		//}

		//object IEmployeeRepository.GetEmployeeById(int id)
		//{
		//	var company = context.Company
		//				  .Include(a => a.Employees)
		//				  .Where(a => a.Id == id)
		//				  .Select(a => new EmployeeRTO
		//				  {
		//					  Id = a.Id,
		//					  Username = a.Employees!.Username,
		//					  Name = a.Employees.Name,
		//					  Email = a.Employees.Email,
		//					  Company_Name = a.Company_Name,
		//					  Company_Address = a.Company_Address,
		//					  Pincode = a.Pincode
		//				  }).ToList();
		//	return company;
		//}
	}
}

