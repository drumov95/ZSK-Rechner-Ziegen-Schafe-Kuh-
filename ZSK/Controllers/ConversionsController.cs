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
    public class ConversionsController : Controller
    {
        private readonly ZskDbContext _context;

        public ConversionsController(ZskDbContext context)
        {
            _context = context;
        }

        // GET: Conversions
        public async Task<IActionResult> Index()
        {
            var zskDbContext = _context.conversions.Include(c => c.AnimalRate);
            return View(await zskDbContext.ToListAsync());
        }

        // GET: Conversions/Details/5
        

        // GET: Conversions/Create
        public IActionResult Create()
        {
            ViewData["AnimalRateId"] = new SelectList(_context.animalRates, "Id", "Name");
            return View();
        }

        // POST: Conversions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AnimalRateId,Direction,EuroAmount,Quantity,UnitEuroAtCreation,CreatedAt")] Conversion conversion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conversion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalRateId"] = new SelectList(_context.animalRates, "Id", "Name", conversion.AnimalRateId);
            return View(conversion);
        }

        // GET: Conversions/Edit/5
    
        

        // POST: Conversions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        

        // GET: Conversions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversion = await _context.conversions
                .Include(c => c.AnimalRate)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conversion == null)
            {
                return NotFound();
            }

            return View(conversion);
        }

        // POST: Conversions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conversion = await _context.conversions.FindAsync(id);
            if (conversion != null)
            {
                _context.conversions.Remove(conversion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConversionExists(int id)
        {
            return _context.conversions.Any(e => e.Id == id);
        }
    }
}
