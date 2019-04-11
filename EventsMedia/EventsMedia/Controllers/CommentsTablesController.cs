using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventsMedia.Data;
using EventsMedia.Models;

namespace EventsMedia.Controllers
{
    public class CommentsTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentsTablesController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: CommentsTables
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Comments.Include(c => c.AdventurePost).Include(c => c.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CommentsTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentsTable = await _context.Comments
                .Include(c => c.AdventurePost)
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (commentsTable == null)
            {
                return NotFound();
            }

            return View(commentsTable);
        }

        // GET: CommentsTables/Create
        public IActionResult Create(string userid, int id)
        {
            //ViewData["AdventurePostId"] = id;
            //ViewData["UserId"] = userid;
            CommentsTable commentsTable = new CommentsTable()
            {
                UserId = userid,
                AdventurePostId = id
            };
            return View(commentsTable);
        }

        // POST: CommentsTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Comment,CommentDate,AdventurePostId,UserId")] CommentsTable commentsTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commentsTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdventurePostId"] = new SelectList(_context.AdventuresPost, "PostId", "PostId", commentsTable.AdventurePostId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", commentsTable.UserId);
            return View(commentsTable);
        }

        // GET: CommentsTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentsTable = await _context.Comments.FindAsync(id);
            if (commentsTable == null)
            {
                return NotFound();
            }
            ViewData["AdventurePostId"] = new SelectList(_context.AdventuresPost, "PostId", "PostId", commentsTable.AdventurePostId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", commentsTable.UserId);
            return View(commentsTable);
        }

        // POST: CommentsTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentId,Comment,CommentDate,AdventurePostId,UserId")] CommentsTable commentsTable)
        {
            if (id != commentsTable.CommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commentsTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentsTableExists(commentsTable.CommentId))
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
            ViewData["AdventurePostId"] = new SelectList(_context.AdventuresPost, "PostId", "PostId", commentsTable.AdventurePostId);
            ViewData["UserId"] = new SelectList(_context.User, "Id", "Id", commentsTable.UserId);
            return View(commentsTable);
        }

        // GET: CommentsTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commentsTable = await _context.Comments
                .Include(c => c.AdventurePost)
                .Include(c => c.ApplicationUser)
                .FirstOrDefaultAsync(m => m.CommentId == id);
            if (commentsTable == null)
            {
                return NotFound();
            }

            return View(commentsTable);
        }

        // POST: CommentsTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var commentsTable = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(commentsTable);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentsTableExists(int id)
        {
            return _context.Comments.Any(e => e.CommentId == id);
        }
    }
}
