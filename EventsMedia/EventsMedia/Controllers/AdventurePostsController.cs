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
    public class AdventurePostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdventurePostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdventurePosts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AdventuresPost.Include(a => a.ApplicationUser);
            ViewData["userid"] = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AdventurePosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adventurePost = await _context.AdventuresPost
                .Include(a => a.ApplicationUser)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (adventurePost == null)
            {
                return NotFound();
            }

            return View(adventurePost);
        }

        // GET: AdventurePosts/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id");
            return View();
        }

        // POST: AdventurePosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,PostTitle,UserId")] AdventurePost adventurePost)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                adventurePost.UserId = userId;
                _context.Add(adventurePost);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Adventures", new { id = adventurePost.PostId });
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", adventurePost.UserId);
            return View(adventurePost);
        }

        // GET: AdventurePosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adventurePost = await _context.AdventuresPost.FindAsync(id);
            if (adventurePost == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", adventurePost.UserId);
            return View(adventurePost);
        }

        // POST: AdventurePosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,PostTitle,UserId")] AdventurePost adventurePost)
        {
            if (id != adventurePost.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adventurePost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdventurePostExists(adventurePost.PostId))
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
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", adventurePost.UserId);
            return View(adventurePost);
        }

        // GET: AdventurePosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adventurePost = await _context.AdventuresPost
                .Include(a => a.ApplicationUser)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (adventurePost == null)
            {
                return NotFound();
            }

            return View(adventurePost);
        }

        // POST: AdventurePosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adventurePost = await _context.AdventuresPost.FindAsync(id);
            _context.AdventuresPost.Remove(adventurePost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdventurePostExists(int id)
        {
            return _context.AdventuresPost.Any(e => e.PostId == id);
        }
    }
}
