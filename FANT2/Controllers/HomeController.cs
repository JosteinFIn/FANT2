using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FANT2.Models;
using Microsoft.AspNetCore.Authorization;
using FANT2.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FANT2.Controllers
{

    public class HomeController : Controller
	{
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
		{
            _context = context;
            _logger = logger;
		}

        public IActionResult OnBoarding()
        {
            return View();
        }
        public IActionResult OnBoarding1()
        {
            return View();
        }

        public IActionResult OnBoarding2()
        {
            return View();
        }

		[Authorize]
        public IActionResult LandingPage()
        {
            return View();
        }


		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Intro()
		{
			return View();
		}

        public IActionResult Start()
        {
            return View();
        }

        public async Task<IActionResult> Map()
        {
            return View(await _context.Annonse.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

        
	}
}
