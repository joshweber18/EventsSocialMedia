using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventsMedia.Data;
using EventsMedia.Models;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EventsMedia.Controllers
{
    public class AdventuresController : Controller
    {
        private readonly ApplicationDbContext _context;
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=aspnet-EventsMedia-17293A85-812F-4154-8FC4-91CF9B2D6E5F;Trusted_Connection=True;MultipleActiveResultSets=true";

        public AdventuresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Adventures
        public ActionResult Index(int id, string userid)
        {
            ViewModel adventure = new ViewModel();
            adventure.adventures = _context.AdventuresTable.Where(a => a.AdventurePostId == id).ToList();
            ViewData["userid"] = userid;
            return View(adventure);
        }

        // GET: Adventures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adventure = await _context.AdventuresTable
                .Include(a => a.AdventurePost)
                .FirstOrDefaultAsync(m => m.AdventureId == id);
            if (adventure == null)
            {
                return NotFound();
            }

            return View(adventure);
        }

        // GET: Adventures/Create
        public IActionResult Create(int id)
        {
            //Adventure adventure = new Adventure();
            //adventure.AdventurePostId = id;
            //_context.Add(adventure);
            //await _context.SaveChangesAsync();
            ViewData["AdventurePostId"] = new SelectList(_context.AdventuresPost, "PostId", "PostId");
            return View();
        }

        // POST: Adventures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdventureId,EventName,Date,Location,Description,AdventurePostId,ImagePath")] Adventure adventure, IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                //string imageLocation;
                //string storePath = "wwwroot/images/";
                //if (form.Files == null || form.Files[0].Length == 0)
                //    return RedirectToAction("Index");


                //var path = Path.Combine(
                //            Directory.GetCurrentDirectory(), storePath,
                //            form.Files[0].FileName);

                //using (var stream = new FileStream(path, FileMode.Create))
                //{
                //    await form.Files[0].CopyToAsync(stream);
                //}
                //imageLocation = (storePath + form.Files[0].FileName);
                //adventure.ImagePath = imageLocation;
                //ViewBag.ImagePaths = adventure.ImagePath;

                _context.Add(adventure);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create");
            }
            ViewData["AdventurePostId"] = new SelectList(_context.AdventuresPost, "PostId", "PostId", adventure.AdventurePostId);
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
            ViewData["AdventurePostId"] = new SelectList(_context.AdventuresPost, "PostId", "PostId", adventure.AdventurePostId);
            return View(adventure);
        }

        // POST: Adventures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdventureId,EventName,Date,Location,Description,AdventurePostId")] Adventure adventure)
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
            ViewData["AdventurePostId"] = new SelectList(_context.AdventuresPost, "PostId", "PostId", adventure.AdventurePostId);
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
                .Include(a => a.AdventurePost)
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

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormCollection form)
        {
            string storePath = "wwwroot/images/";
            if (form.Files == null || form.Files[0].Length == 0)
                return RedirectToAction("Index");


            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), storePath,
                        form.Files[0].FileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await form.Files[0].CopyToAsync(stream);
            }

            StoreInDB(storePath + form.Files[0].FileName);
            return View();

        }

        public ActionResult StoreInDB(string path)
        {
            return View();
        }

        public ActionResult LikedPosts()
        {
            string userloggedin = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewModel viewmodel = new ViewModel();
            viewmodel.Likes = _context.Likes.Where(l => l.UserId == userloggedin).ToList();
            viewmodel.Posts = _context.AdventuresPost.Where(a => viewmodel.Likes.Any(l => a.PostId == l.AdventurePostId)).ToList();
            viewmodel.adventures = _context.AdventuresTable.Where(a => viewmodel.Posts.Any(l => a.AdventurePostId == l.PostId)).ToList();
            return View(viewmodel);
        }

        public ActionResult Table()
        {
            return View();
        }
    }
}
