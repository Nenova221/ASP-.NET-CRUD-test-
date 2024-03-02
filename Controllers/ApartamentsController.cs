using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarManagementApp.Data;
using CarManagementApp.Models;

namespace CarManagementApp.Controllers
{
    public class ApartamentsController : Controller
    {
        private readonly CarManagementAppContext _context;

        public ApartamentsController(CarManagementAppContext context)
        {
            _context = context;
        }

        // GET: Apartaments
        public async Task<IActionResult> Index()
        {
              return _context.Apartament != null ? 
                          View(await _context.Apartament.ToListAsync()) :
                          Problem("Entity set 'CarManagementAppContext.Apartament'  is null.");
        }

        // GET: Apartaments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Apartament == null)
            {
                return NotFound();
            }

            var apartament = await _context.Apartament
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apartament == null)
            {
                return NotFound();
            }

            return View(apartament);
        }

        // GET: Apartaments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Apartaments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,number,street,city,zipcode,country")] Apartament apartament)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apartament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(apartament);
        }

        // GET: Apartaments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Apartament == null)
            {
                return NotFound();
            }

            var apartament = await _context.Apartament.FindAsync(id);
            if (apartament == null)
            {
                return NotFound();
            }
            return View(apartament);
        }

        // POST: Apartaments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,number,street,city,zipcode,country")] Apartament apartament)
        {
            if (id != apartament.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartament);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartamentExists(apartament.Id))
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
            return View(apartament);
        }

        // GET: Apartaments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Apartament == null)
            {
                return NotFound();
            }

            var apartament = await _context.Apartament
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apartament == null)
            {
                return NotFound();
            }

            return View(apartament);
        }

        // POST: Apartaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Apartament == null)
            {
                return Problem("Entity set 'CarManagementAppContext.Apartament'  is null.");
            }
            var apartament = await _context.Apartament.FindAsync(id);
            if (apartament != null)
            {
                _context.Apartament.Remove(apartament);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApartamentExists(int id)
        {
          return (_context.Apartament?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
