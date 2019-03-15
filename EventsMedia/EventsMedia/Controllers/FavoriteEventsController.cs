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
    public class FavoriteEventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FavoriteEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FavoriteEvents
        public IActionResult Index()
        {
            ViewModel favoritesList = new ViewModel();
            string userid = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Favorites.Where(f => f.UserId == userid).FirstOrDefault();
            favoritesList.Favorites = _context.Favorites.Where(f => f.UserId == user.UserId).ToList();
            favoritesList.adventures = _context.AdventuresTable.Where(a => favoritesList.Favorites.Any(f => f.AdventureId == a.AdventureId)).ToList();

            return View(favoritesList);
        }

        // GET: FavoriteEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteEvents = await _context.Favorites
                .Include(f => f.Adventure)
                .Include(f => f.ApplicationUser)
                .FirstOrDefaultAsync(m => m.FavoriteId == id);
            if (favoriteEvents == null)
            {
                return NotFound();
            }

            return View(favoriteEvents);
        }

        // GET: FavoriteEvents/Create
        public async Task<IActionResult> Create(int id, string userid)
        {
            //ViewData["AdventureId"] = id;
            //ViewData["UserId"] = userid;
            FavoriteEvents favorite = new FavoriteEvents();
            favorite.UserId = userid;
            favorite.AdventureId = id;
            _context.Add(favorite);
            await _context.SaveChangesAsync();
          
            return RedirectToAction("Index", "Home");
        }

        // POST: FavoriteEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("FavoriteId,AdventureId,UserId")] FavoriteEvents favoriteEvents)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(favoriteEvents);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["AdventureId"] = new SelectList(_context.AdventuresTable, "AdventureId", "AdventureId", favoriteEvents.AdventureId);
        //    ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", favoriteEvents.UserId);
        //    return View(favoriteEvents);
        //}

        // GET: FavoriteEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteEvents = await _context.Favorites.FindAsync(id);
            if (favoriteEvents == null)
            {
                return NotFound();
            }
            ViewData["AdventureId"] = new SelectList(_context.AdventuresTable, "AdventureId", "AdventureId", favoriteEvents.AdventureId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", favoriteEvents.UserId);
            return View(favoriteEvents);
        }

        // POST: FavoriteEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FavoriteId,AdventureId,UserId")] FavoriteEvents favoriteEvents)
        {
            if (id != favoriteEvents.FavoriteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(favoriteEvents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FavoriteEventsExists(favoriteEvents.FavoriteId))
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
            ViewData["AdventureId"] = new SelectList(_context.AdventuresTable, "AdventureId", "AdventureId", favoriteEvents.AdventureId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", favoriteEvents.UserId);
            return View(favoriteEvents);
        }

        // GET: FavoriteEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favoriteEvents = await _context.Favorites
                .Include(f => f.Adventure)
                .Include(f => f.ApplicationUser)
                .FirstOrDefaultAsync(m => m.FavoriteId == id);
            if (favoriteEvents == null)
            {
                return NotFound();
            }

            return View(favoriteEvents);
        }

        // POST: FavoriteEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var favoriteEvents = await _context.Favorites.FindAsync(id);
            _context.Favorites.Remove(favoriteEvents);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FavoriteEventsExists(int id)
        {
            return _context.Favorites.Any(e => e.FavoriteId == id);
        }
    }
}
