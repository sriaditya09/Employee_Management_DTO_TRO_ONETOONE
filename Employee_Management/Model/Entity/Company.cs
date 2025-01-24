	namespace Employee_Management.Model.Entity
	{
		public class Company
		{
			public int Id { get; set; }
			public string? Company_Name { get; set; }
			public string? Company_Address { get; set; }

			public int Pincode { get; set; }

		//foreign key
		//public int EmployeesId { get; set; }

		//Navigation property
		public List<Employees> Employees { get; set; } = new List<Employees>();  // Collection of employees

	}
}
