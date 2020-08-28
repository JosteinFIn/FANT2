﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FANT2.Data;
using FANT2.Models;
using FANT2.ViewModels;

namespace FANT2.Controllers
{
    public class AnnonsesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnnonsesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Annonses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Annonse.ToListAsync());
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
            if (annonse == null)
            {
                return NotFound();
            }

            return View(annonse);
        }

        // GET: Annonses/Create
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

		//public async Task<IActionResult> Create(Annonse annonse)


		public async Task<IActionResult> Create(CreateAnnonse annonse)

        {
            if (ModelState.IsValid)
            {
                var model = new Annonse
                {
                    CategoryId = annonse.CategoryId,
                    Title = annonse.Title,
                    Description = annonse.Description,
                    IsValuable = annonse.IsValuable,
                    TypeAnnonse = annonse.TypeAnnonse,
                    Date = annonse.Date,
                    Image = annonse.Image,

                };
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
