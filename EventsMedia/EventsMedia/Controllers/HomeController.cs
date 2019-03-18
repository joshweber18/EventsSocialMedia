using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventsMedia.Models;
using Newtonsoft.Json.Linq;

// for api calls
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Restaurants;
using EventsMedia.Data;

namespace EventsMedia.Controllers
{
    public class HomeController : Controller
    {

        public static string apikey = "d9d2ad9c6bb2b26f98f4db8c581a6a17";


        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewModel viewmodel = new ViewModel();
            //var groups = _context.Likes.GroupBy(l => l.AdventurePostId);
            var result = from user in _context.Likes
                         group user by new { user.AdventurePostId } into g
                         select new { g.Key.AdventurePostId, MyCount = g.Count() };
            
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void GetAPIKey()
        {
            // CreateWebHostBuilder(args).Build().Run();
            HttpWebRequest webRequest = WebRequest.Create("https://api.zomato.com/v2.1/Restaurant/restaurant") as HttpWebRequest;
            HttpWebResponse webResponse = null;
            webRequest.Headers.Add("X-Zomato-API-Key", apikey);
            //you can get KeyValue by registering with Zomato.
            webRequest.Method = "GET";
            webResponse = (HttpWebResponse)webRequest.GetResponse();
            if (webResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
                string responseData = responseReader.ReadToEnd();
                Restaurant restaurant = JsonConvert.DeserializeObject<Restaurant>(responseData);
                Console.WriteLine();
                Console.ReadLine();
                // CreateWebHostBuilder(args).Build().Run();
            }
        }
    }
}
