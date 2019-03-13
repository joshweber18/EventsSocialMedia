using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventsMedia.Data;
using EventsMedia.Models;
using System.Security.Claims;

namespace EventsMedia.Controllers
{
    public class AdventuresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdventuresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Adventures
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AdventuresTable.Include(a => a.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Adventures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adventure = await _context.AdventuresTable
                .Include(a => a.ApplicationUser)
                .FirstOrDefaultAsync(m => m.AdventureId == id);
            if (adventure == null)
            {
                return NotFound();
            }

            return View(adventure);
        }

        // GET: Adventures/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: Adventures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdventureId,EventName,Date,Location,Description,Id")] Adventure adventure)
        {

            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                adventure.UserId = userId;
                _context.Add(adventure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.User, "Id", "Id", adventure.UserId);
            return View(adventure);
        }

        // GET: Adventures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adventure = await _context.AdventuresTable.FindAsync(id);
            if (adventure == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.User, "Id", "Id", adventure.UserId);
            return View(adventure);
        }

        // POST: Adventures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdventureId,EventName,Date,Location,Description,Id")] Adventure adventure)
        {
            if (id != adventure.AdventureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adventure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdventureExists(adventure.AdventureId))
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
            ViewData["Id"] = new SelectList(_context.User, "Id", "Id", adventure.UserId);
            return View(adventure);
        }

        // GET: Adventures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adventure = await _context.AdventuresTable
                .Include(a => a.ApplicationUser)
                .FirstOrDefaultAsync(m => m.AdventureId == id);
            if (adventure == null)
            {
                return NotFound();
            }

            return View(adventure);
        }

        // POST: Adventures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adventure = await _context.AdventuresTable.FindAsync(id);
            _context.AdventuresTable.Remove(adventure);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdventureExists(int id)
        {
            return _context.AdventuresTable.Any(e => e.AdventureId == id);
        }
    }
}
