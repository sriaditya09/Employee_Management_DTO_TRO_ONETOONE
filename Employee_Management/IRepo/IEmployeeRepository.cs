using Employee_Management.DTO;
using Employee_Management.RTO;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management.IRepo
{
	public interface IEmployeeRepository
	{
		IEnumerable<EmployeeRTO> GetAllEmployees();
		EmployeeDTO CreateEmployee(EmployeeDTO employeeDto);

		bool DeleteEmployee(int id);

		EmployeeDTO UpdateEmployee(int id, EmployeeDTO employeeDto);
		object GetEmployeeById(int id);
	}
}
