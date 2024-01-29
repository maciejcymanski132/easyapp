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
    public class AircraftModelsController : Controller
    {
        private readonly AirtraficContext _context;

        public AircraftModelsController(AirtraficContext context)
        {
            _context = context;
        }

        // GET: AircraftModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.AircraftModel.ToListAsync());
        }

        // GET: AircraftModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraftModel = await _context.AircraftModel
                .FirstOrDefaultAsync(m => m.AircraftModelId == id);
            if (aircraftModel == null)
            {
                return NotFound();
            }

            return View(aircraftModel);
        }

        // GET: AircraftModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AircraftModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AircraftModelId,Name,Seats,CruiseSpeed")] AircraftModel aircraftModel)
        {
            if (ModelState.IsValid)
            {
                aircraftModel.AircraftModelId = Guid.NewGuid();
                _context.Add(aircraftModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aircraftModel);
        }

        // GET: AircraftModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraftModel = await _context.AircraftModel.FindAsync(id);
            if (aircraftModel == null)
            {
                return NotFound();
            }
            return View(aircraftModel);
        }

        // POST: AircraftModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AircraftModelId,Name,Seats,CruiseSpeed")] AircraftModel aircraftModel)
        {
            if (id != aircraftModel.AircraftModelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aircraftModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AircraftModelExists(aircraftModel.AircraftModelId))
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
            return View(aircraftModel);
        }

        // GET: AircraftModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraftModel = await _context.AircraftModel
                .FirstOrDefaultAsync(m => m.AircraftModelId == id);
            if (aircraftModel == null)
            {
                return NotFound();
            }

            return View(aircraftModel);
        }

        // POST: AircraftModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var aircraftModel = await _context.AircraftModel.FindAsync(id);
            if (aircraftModel != null)
            {
                _context.AircraftModel.Remove(aircraftModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AircraftModelExists(Guid id)
        {
            return _context.AircraftModel.Any(e => e.AircraftModelId == id);
        }
    }
}
