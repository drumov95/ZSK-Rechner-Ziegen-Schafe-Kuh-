using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZSK.Data;
using ZSK.Models;

namespace ZSK.Controllers
{
    public class ConverterController : Controller
    {
        private readonly ZskDbContext _db;
        public ConverterController(ZskDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var animals = await _db.animalRates.OrderBy(a => a.Name).ToListAsync();
            return View(animals);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EuroToAnimals(int animalRateId, decimal euro)
        {
            var animal = await _db.animalRates.FindAsync(animalRateId);
            if (animal is null) return NotFound();

            var qty = animal.EuroValue > 0 ? Math.Floor(euro / animal.EuroValue) : 0;

            _db.conversions.Add(new Conversion
            {
                AnimalRateId = animal.Id,
                Direction = ConversionDirection.EuroToAnimals,
                EuroAmount = (int)qty * animal.EuroValue,
                Quantity = (int)qty,
                UnitEuroAtCreation = animal.EuroValue,
                CreatedAt = DateTime.UtcNow
            });
            await _db.SaveChangesAsync();

            ViewBag.Mode = "EuroToAnimals";
            ViewBag.Animal = animal.Name;
            ViewBag.Euro = euro;
            ViewBag.Quantity = qty;

            var animals = await _db.animalRates.OrderBy(a => a.Name).ToListAsync();
            return View("Index", animals);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AnimalsToEuro(int[] animalIds, int[] quantities)
        {
      
            decimal totalEuro = 0m;
            var rows = new List<(string Name, int Count, decimal Unit, decimal Sum)>();

            for (int i = 0; i < animalIds.Length; i++)
            {
                var qty = quantities[i];
                if (qty <= 0) continue;

                var animal = await _db.animalRates.FindAsync(animalIds[i]);
                if (animal is null) continue;

                var sum = animal.EuroValue * qty;
                totalEuro += sum;

                _db.conversions.Add(new Conversion
                {
                    AnimalRateId = animal.Id,
                    Direction = ConversionDirection.AnimalsToEuro,
                    EuroAmount = sum,
                    Quantity = qty,
                    UnitEuroAtCreation = animal.EuroValue,
                    CreatedAt = DateTime.UtcNow
                });

                rows.Add((animal.Name, qty, animal.EuroValue, sum));
            }
            await _db.SaveChangesAsync();

            ViewBag.Mode = "AnimalsToEuroMulti";
            ViewBag.Rows = rows;
            ViewBag.Total = totalEuro;

            var animals2 = await _db.animalRates.OrderBy(a => a.Name).ToListAsync();
            return View("Index", animals2);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Wechselgeld(decimal euro)
        {
            if (euro <= 0)
            {
                ViewBag.Error = "Bitte Betrag > 0 eingeben.";
                var animals0 = await _db.animalRates.OrderBy(a => a.Name).ToListAsync();
                return View("Index", animals0);
            }

            var animals = await _db.animalRates.OrderByDescending(a => a.EuroValue).ToListAsync();
            var result = new List<(string Name, int Count, decimal Value)>();
            decimal rest = euro;

            foreach (var a in animals)
            {
                if (a.EuroValue <= 0) continue;
                int count = (int)(rest / a.EuroValue);
                if (count > 0)
                {
                    result.Add((a.Name, count, a.EuroValue)); rest -= count * a.EuroValue;
                    var conv = new Conversion
                    {
                        AnimalRateId = a.Id,
                        Direction = ConversionDirection.EuroToAnimals,
                        EuroAmount = count * a.EuroValue,
                        Quantity = count,
                        UnitEuroAtCreation = a.EuroValue,
                        CreatedAt = DateTime.UtcNow
                    };
                    _db.conversions.Add(conv);
                }
            }
            await _db.SaveChangesAsync();
            ViewBag.Mode = "Wechselgeld";
            ViewBag.Euro = euro;
            ViewBag.Rest = rest;
            ViewBag.Results = result;

            var animals3 = await _db.animalRates.OrderBy(a => a.Name).ToListAsync();
            return View("Index", animals3);
        }
    }
}
