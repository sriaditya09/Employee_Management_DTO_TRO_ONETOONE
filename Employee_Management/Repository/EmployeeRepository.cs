using Employee_Management.Data;
using Employee_Management.DTO;
using Employee_Management.IRepo;
using Employee_Management.Model.Entity;
using Employee_Management.RTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management.Repository
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly EmpDbContext context;

		public EmployeeRepository(EmpDbContext context)
		{
			this.context = context;
		}

		public IEnumerable <EmployeeRTO> GetAllEmployees()
		{
		
			var employees = context.Company
						   .Include(a => a.Employees)
						   // Include related Company data
						   .Select(a => new EmployeeRTO
						   {
							   Id = a.EmployeeId,
							   Company_Name = a.Company_Name,
							   Company_Address = a.Company_Address,
							   //CompanyId = a.Id,
							   //Pincode = a.Pincode,
							   Username = a.Employees.Username,
							   //Password = a.Employees.Password,
							   Name = a.Employees.Name,
							   //Age = a.Employees.Age,
							   Email = a.Employees.Email,
							   //Phone = a.Employees.Phone,
							   //EmployeeId = a.Employees.Id,

						   }).ToList();
			return employees;
		}



		public EmployeeDTO CreateEmployee(EmployeeDTO employeeDto)
		{


			var company = new Company
			{
				Company_Name = employeeDto.Company_Name,
				Company_Address = employeeDto.Company_Address,
				Pincode = employeeDto.Pincode,
				Employees = new Employees()
				{
					Username = employeeDto.Username,
					Password= employeeDto.Password,
					Name = employeeDto.Name,
					Age= employeeDto.Age,
					Email = employeeDto.Email,
					Phone = employeeDto.Phone,
				}
			};

			// Save to the database
			context.Company.Add(company);
			context.SaveChanges();

			// Map back database-generated fields to EmployeeDTO
			return employeeDto;
		}

		public bool DeleteEmployee(int id)
		{
			// Find the company by the employee ID
			var company = context.Company
								 .Include(c => c.Employees) // Ensure employee data is included
								 .FirstOrDefault(c => c.Employees.Id == id); // Find company by employee ID

			if(company == null)
			{
				return false;
			}

			// Remove the company (which will also remove the associated employee due to the relationship)
			if(company.Employees != null)
			{
				context.Emp.Remove(company.Employees);
			}

			
			context.Company.Remove(company);
			context.SaveChanges();

			return true; // Return true if deletion was successful
		}

		public EmployeeDTO UpdateEmployee(int id, EmployeeDTO employeeDto)
		{
			// Find the company by employee ID
			var company = context.Company
								 .Include(c => c.Employees) // Include the employee data
								 .FirstOrDefault(c => c.Employees.Id == id); // Find company by employee ID

			// If the company (and employee) is not found, return null
			if (company == null)
			{
				return null; // Return null if no company or employee is found
			}

			// Update the employee details
			company.Employees.Username = employeeDto.Username;
			company.Employees.Password = employeeDto.Password;
			company.Employees.Name = employeeDto.Name;
			company.Employees.Age = employeeDto.Age;
			company.Employees.Email = employeeDto.Email;
			company.Employees.Phone = employeeDto.Phone;

			// Update the company details
			company.Company_Name = employeeDto.Company_Name;
			company.Company_Address = employeeDto.Company_Address;
			company.Pincode = employeeDto.Pincode;

			// Save the changes to the database
			context.SaveChanges();

			// Return the updated employee data
			return new EmployeeDTO
			{
				Username = company.Employees.Username,
				Password = company.Employees.Password,
				Name = company.Employees.Name,
				Age = company.Employees.Age,
				Email = company.Employees.Email,
				Phone = company.Employees.Phone,
				Company_Name = company.Company_Name,
				Company_Address = company.Company_Address,
				Pincode = company.Pincode,
				CompanyId = company.Id,
				EmployeeId = company.Employees.Id
			};
		}

		object IEmployeeRepository.GetEmployeeById(int id)
		{
			var company = context.Company
						  .Include(a => a.Employees)
						  .Where(a => a.Id == id)
						  .Select(a => new EmployeeRTO
						  {
							  Id = a.Id,
							  Username = a.Employees!.Username,
							  Name = a.Employees.Name,
							  Email = a.Employees.Email,
							  Company_Name = a.Company_Name,
							  Company_Address = a.Company_Address,
							  Pincode = a.Pincode
						  }).ToList();
			return company;
		}
	}
}

