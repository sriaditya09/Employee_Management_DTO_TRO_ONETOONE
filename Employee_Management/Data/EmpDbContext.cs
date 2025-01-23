using Employee_Management.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management.Data
{
	public class EmpDbContext : DbContext
	{
		public EmpDbContext(DbContextOptions options) : base(options) 
		{
		
		}

		public DbSet<Employees> Emp {  get; set; }
		public DbSet<Company> Company { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Configuring the one-to-one relationship between Employees and Company
			modelBuilder.Entity<Company>()
				.HasOne(e => e.Employees)  
				.WithOne() 
				.HasForeignKey<Company>(e => e.EmployeeId); 
			base.OnModelCreating(modelBuilder);
		}

	}
}
