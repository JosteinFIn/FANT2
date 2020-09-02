using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using FANT2.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

namespace FANT2
{
	public class Startup
	{
		//private string _myDbConnection = null;
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		//lagt til kommentar for azure

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//_myDbConnection = Configuration["MyDbConnection"];

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("MyDBConnection")));

			services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
				.AddEntityFrameworkStores<ApplicationDbContext>();

			//services.AddAuthentication().
			//	AddGoogle(options =>
			//	{
			//		options.ClientId = Configuration["App:GoogleClient"];
			//		options.ClientSecret = Configuration["App:GoogleClientSecret"];
			//	}).
			//	AddFacebook(option =>
			//	{
			//		option.AppId = Configuration["App:FacebookClientId"];
			//		option.ClientSecret = Configuration["App:FacebookClientSecret"];
			//	});
			services.AddControllersWithViews();
			services.AddRazorPages();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			/*
			app.Run(async (context) =>
			{
				var result = string.IsNullOrEmpty(_myDbContext) ? "Null" : _myDbContext;
				await context.Response.WriteAsync($"Secret is {result}");
			});*/
			

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{ 
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Intro}/{id?}");
				endpoints.MapRazorPages();
			});
		}
	}
}
