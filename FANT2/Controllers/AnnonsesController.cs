using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FANT2.Data;
using FANT2.Models;
using FANT2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace FANT2.Controllers
{
    public class AnnonsesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AnnonsesController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Annonses
        public async Task<IActionResult> Index(string annonseCategory, string searchString)
        {
	        IQueryable<string> categoryQuery = from m in _context.Annonse
		        select m.Category.Name;

	        var annonse = from m in _context.Annonse
		        select m;
	        if (!string.IsNullOrEmpty(searchString))
	        {
		        annonse = annonse.Where(x => x.Title.Contains(searchString));
	        }

	        if (!string.IsNullOrEmpty(annonseCategory))
	        {
		        annonse = annonse.Where(x => x.Category.Name.Contains(annonseCategory));
	        }

            var annonseCategoryVM = new AnnonseCategoryModel()
	        {
		        Categorys = new SelectList(await categoryQuery.Distinct().ToListAsync()),
		        Annonses = await annonse.ToListAsync()
	        };

	        return View(annonseCategoryVM);
        }

        // GET: Annonses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
	        

            if (id == null)
            {
                return NotFound();
            }

            var annonse = await _context.Annonse
                .FirstOrDefaultAsync(m => m.Id == id);
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == annonse.UserId);
            if (annonse == null)
            {
                return NotFound();
            }

            string loggedInUserId = string.Empty;

            if (User.Identity.IsAuthenticated)
            {
                loggedInUserId = (await _userManager.GetUserAsync(User)).Id;
            }

            var result = new UserAnnonse
            {
	            annonse = annonse,
	            userEmail = user.Email,
                userId = loggedInUserId,
                loggedIn = User.Identity.IsAuthenticated
                
            };
            return View(result);
        }

        // GET: Annonses/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
	        var categories = await _context.Category.ToListAsync();
            
            return View(new CreateAnnonse()
            {
                Categories = categories.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList(),
            });
        }

        // POST: Annonses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [DisableRequestSizeLimit]
		//public async Task<IActionResult> Create(Annonse annonse)


		public async Task<IActionResult> Create(CreateAnnonse annonse)

        {
            if (ModelState.IsValid)
            {
                
	            var user = await _userManager.GetUserAsync(User);
                string relPath = "";


                if (annonse.Image != null)
				{
				    //GET IMAGE
				    var base64string = annonse.Image.Substring(annonse.Image.LastIndexOf(',') + 1);

				    var base64array = Convert.FromBase64String(base64string);
				    relPath = "/img/annonse/" + Guid.NewGuid().ToString() + ".jpg";
				    var filePath = _webHostEnvironment.WebRootPath + relPath;


				    //RESIZE
				    using (MemoryStream memStream = new MemoryStream(base64array))
				    {
					    MemoryStream myMemStream = new MemoryStream(base64array);
					    Image fullsizeImage = Image.FromStream(myMemStream);
					    if (fullsizeImage.Width > 500)
					    {
						    var scaleRatio = (500.0 / fullsizeImage.Width);
						    Image newImage = fullsizeImage.GetThumbnailImage((int)(fullsizeImage.Width * scaleRatio), (int)(fullsizeImage.Height * scaleRatio), null, IntPtr.Zero);
						    MemoryStream myResult = new MemoryStream();
						    newImage.Save(myResult, System.Drawing.Imaging.ImageFormat.Jpeg);  //Or whatever format you want.
						    base64array = myResult.ToArray();  //Returns a new byte array.
					    }
				    }
                    base64string = "";

                    System.IO.File.WriteAllBytes(filePath, base64array);
				}


				var model = new Annonse
                {
                    UserId = user.Id,
                    CategoryId = annonse.CategoryId,
                    Title = annonse.Title,
                    Description = annonse.Description,
                    IsValuable = annonse.IsValuable,
                    TypeAnnonse = annonse.TypeAnnonse,
                    Date = annonse.Date,
                    Image = relPath,
                    Lat = annonse.Lat,
                    Lng = annonse.Lng
                };

                if (model.IsValuable)
                {
                    model.Image = null;
                }

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(annonse);
        }

        // GET: Annonses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var annonse = await _context.Annonse.FindAsync(id);
            if (annonse == null)
            {
                return NotFound();
            }
            return View(annonse);
        }

        // POST: Annonses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Date")] Annonse annonse)
        {
            if (id != annonse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(annonse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnonseExists(annonse.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(annonse);
        }

        // GET: Annonses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var annonse = await _context.Annonse
                .FirstOrDefaultAsync(m => m.Id == id);
            if (annonse == null)
            {
                return NotFound();
            }

            return View(annonse);
        }

        // POST: Annonses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var annonse = await _context.Annonse.FindAsync(id);

            var filePath = _webHostEnvironment.WebRootPath + annonse.Image;
            try
            {
                // Check if file exists with its full path    
                if (System.IO.File.Exists(filePath))
                {
                    // If file found, delete it    
                    System.IO.File.Delete(filePath);
                    Console.WriteLine("File deleted.");
                }
                else Console.WriteLine("File not found");
            }
            catch (IOException ioExp)
            {
                Console.WriteLine(ioExp.Message);
            }

            _context.Annonse.Remove(annonse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnonseExists(int id)
        {
            return _context.Annonse.Any(e => e.Id == id);
        }
    }
}
