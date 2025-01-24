using Employee_Management.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management.Data
{
	public class EmpDbContext : DbContext
	{
		public EmpDbContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<Employees> Employees { get; set; }
		public DbSet<Company> Company { get; set; }
		public DbSet<Salary> Salaries { get; set; }

		//protected override void OnModelCreating(ModelBuilder modelBuilder)
		//{



		//	base.OnModelCreating(modelBuilder);

		//	// Company to Employee (One-to-Many)
		//	modelBuilder.Entity<Company>()
		//		.HasMany(c => c.Employees)  
		//		.WithOne(e => e.Company)    
		//		.HasForeignKey(e => e.Id);  

		//	// Employee to Salary (One-to-One or One-to-Many)
		//	modelBuilder.Entity<Employees>()
		//		.HasOne(e => e.Salaries)
		//		.WithOne()
		//		.HasForeignKey<Employees>(s => s.SalaryId);

		//}



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Employees>()
	        .HasOne(e => e.Salary)
	        .WithOne(s => s.Employee)
	        .HasForeignKey<Employees>(e => e.SalaryId)
	        .OnDelete(DeleteBehavior.Cascade);


			// Configure the relationship between Employee and Salary
			modelBuilder.Entity<Employees>()
				.HasOne(e => e.Salary)  // Employee has one Salary (navigation property)
				.WithOne(s => s.Employee)  // Salary has one Employee (navigation property)
				.HasForeignKey<Employees>(e => e.SalaryId)  // SalaryId is the foreign key on Employee
				.OnDelete(DeleteBehavior.Cascade);  // Enable cascading delete
	

			//next
		modelBuilder.Entity<Employees>()
				.HasOne(e => e.Salary)  // Employee has one Salary
				.WithOne()  // Salary has one Employee
				.HasForeignKey<Employees>(e => e.SalaryId);  // Foreign key on Employee
		}
	}
}

