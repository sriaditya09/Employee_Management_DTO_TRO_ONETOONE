﻿	namespace Employee_Management.Model.Entity
	{
		public class Employees
		{
			public int Id { get; set; }
			public string? Username { get; set; }
			public int Password { get; set; }
			public string? Name { get; set; }
			public int Age { get; set; }
			public string? Email { get; set; }
			public string? Phone { get; set; }


		public int CompanyId { get; set; }

		public Company? Company { get; set; }
		public int SalaryId { get; set; }

		public  Salary Salary { get; set; }




	}
}
