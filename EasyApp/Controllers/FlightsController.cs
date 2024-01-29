using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Airtrafic.Data;
using Airtrafic.Models;
using Airtrafic.Services;

namespace Airtrafic.Controllers
{
    public class FlightsController : Controller
    {
        private readonly AirtraficContext _context;
        private readonly FlightCalculatorService FlightCalculatorService;

        public FlightsController(AirtraficContext context,FlightCalculatorService flightCalculatorService)
        {
            _context = context;
            FlightCalculatorService = flightCalculatorService;
        }

        // GET: Flights
        public async Task<IActionResult> Index()
        {
            var airtraficContext = _context.Flight.Include(f => f.Aircraft).Include(f => f.ArrivalAirport).Include(f => f.DepartureAirport).Include(f=>f.Aircraft).ThenInclude(r => r.AircraftModel);
            return View(await airtraficContext.ToListAsync());
        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight
                .Include(f => f.Aircraft)
                .Include(f => f.ArrivalAirport)
                .Include(f => f.DepartureAirport)
                .FirstOrDefaultAsync(m => m.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            ViewData["AircraftId"] = new SelectList(_context.Aircraft.Include(a => a.AircraftModel), "AircraftId", "AircraftId");
            ViewData["ArrivalAirportId"] = new SelectList(_context.Airport, "Id", "Id");
            ViewData["DepartureAirportId"] = new SelectList(_context.Airport, "Id", "Id");
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightId,AircraftId,DepartureAirportId,ArrivalAirportId,DepartureTime")] Flight flight, DateTime lastInspection)
        {
            if (ModelState.IsValid)
            {
                flight.FlightId = Guid.NewGuid();
                flight.EstimatedArrivalTime = flight.DepartureTime.AddMinutes(this.FlightCalculatorService.CalculateTravelTime(flight.Aircraft, flight.DepartureAirport, flight.ArrivalAirport));
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AircraftId"] = new SelectList(_context.Aircraft, "AircraftId", "AircraftId", flight.AircraftId);
            ViewData["ArrivalAirportId"] = new SelectList(_context.Airport, "Id", "Id", flight.ArrivalAirportId);
            ViewData["DepartureAirportId"] = new SelectList(_context.Airport, "Id", "Id", flight.DepartureAirportId);
            return View(flight);
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            ViewData["AircraftId"] = new SelectList(_context.Aircraft, "AircraftId", "AircraftId", flight.AircraftId);
            ViewData["ArrivalAirportId"] = new SelectList(_context.Airport, "Id", "Id", flight.ArrivalAirportId);
            ViewData["DepartureAirportId"] = new SelectList(_context.Airport, "Id", "Id", flight.DepartureAirportId);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FlightId,AircraftId,DepartureAirportId,ArrivalAirportId,DepartureTime,EstimatedArrivalTime")] Flight flight)
        {
            if (id != flight.FlightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.FlightId))
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
            ViewData["AircraftId"] = new SelectList(_context.Aircraft, "AircraftId", "AircraftId", flight.AircraftId);
            ViewData["ArrivalAirportId"] = new SelectList(_context.Airport, "Id", "Id", flight.ArrivalAirportId);
            ViewData["DepartureAirportId"] = new SelectList(_context.Airport, "Id", "Id", flight.DepartureAirportId);
            return View(flight);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flight
                .Include(f => f.Aircraft)
                .Include(f => f.ArrivalAirport)
                .Include(f => f.DepartureAirport)
                .FirstOrDefaultAsync(m => m.FlightId == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var flight = await _context.Flight.FindAsync(id);
            if (flight != null)
            {
                _context.Flight.Remove(flight);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(Guid id)
        {
            return _context.Flight.Any(e => e.FlightId == id);
        }
    }
}
