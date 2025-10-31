using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZSK.Data;
using ZSK.Models;

namespace ZSK.Controllers
{
    public class AnimalRatesController : Controller
    {
        private readonly ZskDbContext _context;

        public AnimalRatesController(ZskDbContext context)
        {
            _context = context;
        }

        // GET: AnimalRates
        public async Task<IActionResult> Index()
        {
            return View(await _context.animalRates.ToListAsync());
        }

        

        // GET: AnimalRates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnimalRates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,EuroValue")] AnimalRate animalRate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animalRate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animalRate);
        }

        // GET: AnimalRates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalRate = await _context.animalRates.FindAsync(id);
            if (animalRate == null)
            {
                return NotFound();
            }
            return View(animalRate);
        }

        // POST: AnimalRates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,EuroValue")] AnimalRate animalRate)
        {
            if (id != animalRate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animalRate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalRateExists(animalRate.Id))
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
            return View(animalRate);
        }

        // GET: AnimalRates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animalRate = await _context.animalRates
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animalRate == null)
            {
                return NotFound();
            }

            return View(animalRate);
        }

        // POST: AnimalRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animalRate = await _context.animalRates.FindAsync(id);
            if (animalRate != null)
            {
                _context.animalRates.Remove(animalRate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalRateExists(int id)
        {
            return _context.animalRates.Any(e => e.Id == id);
        }
    }
}
