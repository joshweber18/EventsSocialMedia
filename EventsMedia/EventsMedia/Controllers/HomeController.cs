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

namespace EventsMedia.Controllers
{
    public class HomeController : Controller
    {

        public static string apikey = "d9d2ad9c6bb2b26f98f4db8c581a6a17";

        //public IActionResult Index()
        //{
        //    HttpWebRequest webRequest = WebRequest.Create("htt://developers.zomato.com/api/v2.1/restaurant?res_id=9") as HttpWebRequest;
        //    HttpWebResponse webResponse = null;
        //    webRequest.Headers.Add("X-Zomato-API-Key", apikey);
        //    //you can get KeyValue by registering with Zomato.
        //    webRequest.Method = "GET";
        //    webResponse = (HttpWebResponse)webRequest.GetResponse();
        //    if (webResponse.StatusCode == HttpStatusCode.OK)
        //    {
        //        StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
        //        string responseData = responseReader.ReadToEnd();
        //        JObject restaurant = JsonConvert.DeserializeObject<JObject>(responseData);

        //        var names = restaurant.Values().Select(x => x.ToObject<object>()).ToList();
        //        var test = restaurant.GetValue("location");
        //        var test2 = restaurant.GetValue("city");

        //    }
        //    return View();
        //}

        public IActionResult Index(GoogleGeocode coordinate)
        {
            double lat = coordinate.Latitude;
            double lng = coordinate.Longitude;
            HttpWebRequest webRequest = WebRequest.Create($"https://developers.zomato.com/api/v2.1/geocode?lat={lat}&lon={lng}") as HttpWebRequest;
            HttpWebResponse webResponse = null;
            webRequest.Headers.Add("X-Zomato-API-Key", apikey);
            //you can get KeyValue by registering with Zomato.
            webRequest.Method = "GET";
            webResponse = (HttpWebResponse)webRequest.GetResponse();
            if (webResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
                string responseData = responseReader.ReadToEnd();
                JObject coordinates = JsonConvert.DeserializeObject<JObject>(responseData);

                var names = coordinates.Values().Select(x => x.ToObject<object>()).ToList();
                var test = coordinates.GetValue("location");
                var test2 = test["city_id"];
                var test3 = test["entity_id"];
                var test4 = test["entity_type"];
                ZomatoGeocode geocode = new ZomatoGeocode() { };
                geocode.city_id = Convert.ToInt32(test2);
                geocode.entity_id = Convert.ToInt32(test3);
                geocode.entity_type = Convert.ToString(test4);
            }
            return View();
        }


        public IActionResult Index2(string city)
        {
            string thething = city;
            HttpWebRequest webRequest = WebRequest.Create($"https://maps.googleapis.com/maps/api/geocode/json?address=Milwaukee&sensor=false&key=AIzaSyDPd_kaIJibv8PaW5y-FmYqXnxuD1jRT14") as HttpWebRequest;
            HttpWebResponse webResponse = null;
            //webRequest.Headers.Add("X-Zomato-API-Key", apikey);
            //you can get KeyValue by registering with Zomato.
            webRequest.Method = "GET";
            webResponse = (HttpWebResponse)webRequest.GetResponse();
            if (webResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
                string responseData = responseReader.ReadToEnd();
                JObject coordinates = JsonConvert.DeserializeObject<JObject>(responseData);

                var names = coordinates.Values().Select(x => x.ToObject<object>()).ToList();
                var test = coordinates["results"];
                var test2 = test[0];
                var test3 = test2["geometry"];
                var test4 = test3["location"];
                var test5 = test4["lat"];
                var test6 = test4["lng"];
                GoogleGeocode coordinate = new GoogleGeocode() { };
                coordinate.Latitude = Convert.ToDouble(test5);
                coordinate.Longitude = Convert.ToDouble(test6);
            }
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
