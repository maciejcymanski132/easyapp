using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Airtrafic.Data;
using Airtrafic.Models;

namespace Airtrafic.Controllers
{
    public class AircraftManufacturersController : Controller
    {
        private readonly AirtraficContext _context;

        public AircraftManufacturersController(AirtraficContext context)
        {
            _context = context;
        }

        // GET: AircraftManufacturers
        public async Task<IActionResult> Index()
        {
            return View(await _context.AircraftManufacturer.ToListAsync());
        }

        // GET: AircraftManufacturers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraftManufacturer = await _context.AircraftManufacturer
                .FirstOrDefaultAsync(m => m.AircraftManufacturerId == id);
            if (aircraftManufacturer == null)
            {
                return NotFound();
            }

            return View(aircraftManufacturer);
        }

        // GET: AircraftManufacturers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AircraftManufacturers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AircraftManufacturerId,Name")] AircraftManufacturer aircraftManufacturer)
        {
            if (ModelState.IsValid)
            {
                aircraftManufacturer.AircraftManufacturerId = Guid.NewGuid();
                _context.Add(aircraftManufacturer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aircraftManufacturer);
        }

        // GET: AircraftManufacturers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraftManufacturer = await _context.AircraftManufacturer.FindAsync(id);
            if (aircraftManufacturer == null)
            {
                return NotFound();
            }
            return View(aircraftManufacturer);
        }

        // POST: AircraftManufacturers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AircraftManufacturerId,Name")] AircraftManufacturer aircraftManufacturer)
        {
            if (id != aircraftManufacturer.AircraftManufacturerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aircraftManufacturer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AircraftManufacturerExists(aircraftManufacturer.AircraftManufacturerId))
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
            return View(aircraftManufacturer);
        }

        // GET: AircraftManufacturers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraftManufacturer = await _context.AircraftManufacturer
                .FirstOrDefaultAsync(m => m.AircraftManufacturerId == id);
            if (aircraftManufacturer == null)
            {
                return NotFound();
            }

            return View(aircraftManufacturer);
        }

        // POST: AircraftManufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var aircraftManufacturer = await _context.AircraftManufacturer.FindAsync(id);
            if (aircraftManufacturer != null)
            {
                _context.AircraftManufacturer.Remove(aircraftManufacturer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AircraftManufacturerExists(Guid id)
        {
            return _context.AircraftManufacturer.Any(e => e.AircraftManufacturerId == id);
        }
    }
}
