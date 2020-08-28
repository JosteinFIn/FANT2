using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FANT2.Models;

namespace FANT2.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<Annonse> Annonse { get; set; }

		public DbSet<Category> Category { get; set; }

		//protected override void OnModelCreating(ModelBuilder builder)
		//{
		//	builder.Entity<Category>()
		//		.HasData(
		//		new Category { Id = 0, Name = "Mobil" },
		//		new Category { Id = 1, Name = "Klær" },
		//		new Category { Id = 1, Name = "Stuff" }
		//		);
		//}
	}
}
