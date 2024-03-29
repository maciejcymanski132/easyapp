﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Airtrafic.Data;
using Airtrafic.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Airtrafic.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AircraftController : Controller
    {
        private readonly AirtraficContext _context;

        public AircraftController(AirtraficContext context)
        {
            _context = context;
        }

        // GET: Aircraft
        public async Task<IActionResult> Index()
        {
            var userRoles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
            return View(await _context.Aircraft.ToListAsync());
        }

        // GET: Aircraft/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircraft
                .FirstOrDefaultAsync(m => m.AircraftId == id);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        // GET: Aircraft/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aircraft/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AircraftId,LastInspection,ProductionDate")] Aircraft aircraft)
        {
            if (ModelState.IsValid)
            {
                aircraft.AircraftId = Guid.NewGuid();
                _context.Add(aircraft);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aircraft);
        }

        // GET: Aircraft/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircraft.FindAsync(id);
            if (aircraft == null)
            {
                return NotFound();
            }
            return View(aircraft);
        }

        // POST: Aircraft/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AircraftId,LastInspection,ProductionDate")] Aircraft aircraft)
        {
            if (id != aircraft.AircraftId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aircraft);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AircraftExists(aircraft.AircraftId))
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
            return View(aircraft);
        }

        // GET: Aircraft/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircraft
                .FirstOrDefaultAsync(m => m.AircraftId == id);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        // POST: Aircraft/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var aircraft = await _context.Aircraft.FindAsync(id);
            if (aircraft != null)
            {
                _context.Aircraft.Remove(aircraft);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AircraftExists(Guid id)
        {
            return _context.Aircraft.Any(e => e.AircraftId == id);
        }
    }
}
