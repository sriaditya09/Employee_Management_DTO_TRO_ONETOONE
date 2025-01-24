namespace Employee_Management.Model.Entity
{
	public class Salary
	{

		public int Id { get; set; }
		public string? Grade { get; set; }
		public int Salary_Amount { get; set; }

		public Employees Employee { get; set; }
	}
}
