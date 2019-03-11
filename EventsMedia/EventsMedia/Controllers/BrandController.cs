using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsMedia.Data;
using EventsMedia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventsMedia.Controllers
{
    public class BrandController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrandController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}