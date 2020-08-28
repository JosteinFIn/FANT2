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
		public DbSet<FANT2.Models.Annonse> Annonse { get; set; }
<<<<<<< HEAD
=======
		public DbSet<ImageStore> ImageStore { get; set; }

		public DbSet<Category> Category { get; set; }
>>>>>>> origin/Lema
	}
}
