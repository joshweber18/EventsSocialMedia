using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using EventsMedia.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Restaurants;


namespace EventsMedia.Controllers
{
    public class RestaurantsController : Controller
    {
        public static string apikey = "d9d2ad9c6bb2b26f98f4db8c581a6a17";

        // GET
        public IActionResult GetCity()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetCity(string location)
        {
            HttpWebRequest webRequest = WebRequest.Create($"https://maps.googleapis.com/maps/api/geocode/json?address={location}&sensor=false&key=AIzaSyDPd_kaIJibv8PaW5y-FmYqXnxuD1jRT14") as HttpWebRequest;
            HttpWebResponse webResponse = null;
            webRequest.Method = "GET";
            webResponse = (HttpWebResponse)webRequest.GetResponse();
            Customer coordinate = new Customer() { };
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
                coordinate.Latitude = Convert.ToDouble(test5);
                coordinate.Longitude = Convert.ToDouble(test6);
            }
            return RedirectToAction("GetCityZomato", "Restaurants", coordinate);
        }



        public IActionResult GetCityZomato(Customer coordinate)
        {
            HttpWebRequest webRequest = WebRequest.Create($"https://developers.zomato.com/api/v2.1/geocode?lat={coordinate.Latitude}&lon={coordinate.Longitude}") as HttpWebRequest;
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
                var test5 = test["longitude"];
                var test6 = test["latitude"];
                coordinate.city_id = Convert.ToInt32(test2);
                coordinate.entity_id = Convert.ToInt32(test3);
                coordinate.entity_type = Convert.ToString(test4);
            }
            return RedirectToAction("GetCuisine", "Restaurants", coordinate);
        }


        // GET
        public IActionResult GetCuisine(Customer coordinate)
        {
            HttpWebRequest webRequest = WebRequest.Create($"https://developers.zomato.com/api/v2.1/cuisines?city_id={coordinate.city_id}&lat={coordinate.Latitude}&lon={coordinate.Longitude}") as HttpWebRequest;
            HttpWebResponse webResponse = null;
            webRequest.Headers.Add("X-Zomato-API-Key", apikey);
            //you can get KeyValue by registering with Zomato.
            webRequest.Method = "GET";
            webResponse = (HttpWebResponse)webRequest.GetResponse();
            Dictionary<int, string> cuisines = new Dictionary<int, string>();
            if (webResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
                string responseData = responseReader.ReadToEnd();
                JObject cuisine = JsonConvert.DeserializeObject<JObject>(responseData);

                var names = cuisine.Values().Select(x => x.ToObject<object>()).ToList();
                var test = cuisine["cuisines"];
                var test3 = test.Values("cuisine");
                var test4 = test3["cuisine_id"];
                var test5 = test3["cuisine_name"];

                List<int> ids = new List<int>();
                List<string> name = new List<string>();
                foreach (var item in test4)
                {
                    ids.Add(item.Value<int>());
                }
                foreach (var item in test5)
                {
                    name.Add(item.Value<string>());
                }
                for (int i = 0; i < ids.Count(); i++)
                {
                    cuisines.Add(ids[i], name[i]);
                }
              
            }
            coordinate.cuisines = cuisines;
            return View(coordinate);
        }


        public IActionResult GetCuisine1(Customer coordinate, int cuisine)
        {

            HttpWebRequest webRequest = WebRequest.Create($"https://developers.zomato.com/api/v2.1/cuisines?city_id={coordinate.city_id}&lat={coordinate.Latitude}&lon={coordinate.Longitude}") as HttpWebRequest;
            HttpWebResponse webResponse = null;
            webRequest.Headers.Add("X-Zomato-API-Key", apikey);
            //you can get KeyValue by registering with Zomato.
            webRequest.Method = "GET";
            webResponse = (HttpWebResponse)webRequest.GetResponse();
            //Customer customer = new Customer() { };
            if (webResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
                string responseData = responseReader.ReadToEnd();
                JObject chosencuisine = JsonConvert.DeserializeObject<JObject>(responseData);

                var names = chosencuisine.Values().Select(x => x.ToObject<object>()).ToList();
                var test = chosencuisine["cuisines"];
                var test3 = test.Values("cuisine");
                var test4 = test3["cuisine_id"];
                var test5 = test3["cuisine_name"];

                coordinate.cuisine_id = cuisine;

                //List<int> ids = new List<int>();
                //List<string> name = new List<string>();
                
                //foreach (var item in test4)
                //{
                //    ids.Add(item.Value<int>());
                //}
                //foreach (var item in test5)
                //{
                //    name.Add(item.Value<string>());
                //}
                //for (int i = 0; i < ids.Count(); i++)
                //{
                //    coordinate.cuisines.Add(ids[i], name[i]);
                //}
                //var test8 = coordinate.cuisines.Where(x => x.Key.Equals(cuisine)).Select(x => x.Key);
            }
            return RedirectToAction("GetRestaurant", "Restaurants", coordinate);
        }

        public IActionResult GetRestaurant(Customer coordinate)
        {
            HttpWebRequest webRequest = WebRequest.Create($"https://developers.zomato.com/api/v2.1/search?entity_id={coordinate.entity_id}&entity_type={coordinate.entity_type}&lat={coordinate.Latitude}&lon={coordinate.Longitude}&cuisines={coordinate.cuisine_id}") as HttpWebRequest;
            HttpWebResponse webResponse = null;
            webRequest.Headers.Add("X-Zomato-API-Key", apikey);
            //you can get KeyValue by registering with Zomato.
            webRequest.Method = "GET";
            webResponse = (HttpWebResponse)webRequest.GetResponse();
            //List<string> restaurantname = new List<string>();
            if (webResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
                string responseData = responseReader.ReadToEnd();
                JObject searchrestaurant = JsonConvert.DeserializeObject<JObject>(responseData);

                var names = searchrestaurant.Values().Select(x => x.ToObject<object>()).ToList();
                var test = searchrestaurant["restaurants"];
                var test3 = test.Values("restaurant");
                var test4 = test3["name"];
                var test5 = test3.Values("R");
                var test6 = test5.Values("res_id");

                List<int> ids = new List<int>();
                List<string> name = new List<string>();

                foreach (var item in test6)
                {
                    ids.Add(item.Value<int>());
                }
                foreach (var item in test4)
                {
                    name.Add(item.Value<string>());
                }
                for (int i = 0; i < ids.Count(); i++)
                {
                    coordinate.restaurants.Add(ids[i], name[i]);
                }
                //foreach (var item in test4)
                //{
                //    coordinate.restaurants.Add(item.Value<string>());
                //}
            }
            return View(coordinate);
        }

        public IActionResult GetRestaurant1(Customer coordinate, int restaurant)
        {
            
            HttpWebRequest webRequest = WebRequest.Create($"https://developers.zomato.com/api/v2.1/search?entity_id={coordinate.entity_id}&entity_type={coordinate.entity_type}&lat={coordinate.Latitude}&lon={coordinate.Longitude}&cuisines={coordinate.cuisine_id}") as HttpWebRequest;
            HttpWebResponse webResponse = null;
            webRequest.Headers.Add("X-Zomato-API-Key", apikey);
            //you can get KeyValue by registering with Zomato.
            webRequest.Method = "GET";
            webResponse = (HttpWebResponse)webRequest.GetResponse();
            List<string> restaurantname = new List<string>();
            if (webResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
                string responseData = responseReader.ReadToEnd();
                JObject searchrestaurant = JsonConvert.DeserializeObject<JObject>(responseData);

                var names = searchrestaurant.Values().Select(x => x.ToObject<object>()).ToList();
                var test = searchrestaurant["restaurants"];
                var test3 = test.Values("restaurant");
                var test5 = test3.Values("name");
                var test6 = test3.Values("R");
                var test4 = test6.Values("res_id");

                coordinate.restaurant_id = restaurant;

                //List<int> resid = new List<int>();
                //List<string> resname = new List<string>();
                //Dictionary<int, string> restaurants = new Dictionary<int, string>();
                //foreach (var item in test4)
                //{
                //    resid.Add(item.Value<int>());
                //}
                //foreach (var item in test5)
                //{
                //    resname.Add(item.Value<string>());
                //}
                //for (int i = 0; i < resid.Count(); i++)
                //{
                //    restaurants.Add(resid[i], resname[i]);
                //}
                //for (int i = 0; i < restaurants.Count(); i++)
                //{
                //    var test8 = restaurants.Where(x => x.Value.Equals(chosenrestaurant)).Select(x => x.Key);
                //}
            }
            return RedirectToAction("GetDetails", coordinate);
        }

        public IActionResult GetDetails(Customer coordinate)
        {
            HttpWebRequest webRequest = WebRequest.Create($"https://developers.zomato.com/api/v2.1/restaurant?res_id={coordinate.restaurant_id}") as HttpWebRequest;
            HttpWebResponse webResponse = null;
            webRequest.Headers.Add("X-Zomato-API-Key", apikey);
            //you can get KeyValue by registering with Zomato.
            webRequest.Method = "GET";
            webResponse = (HttpWebResponse)webRequest.GetResponse();
            Restaurant details = new Restaurant() { };
            if (webResponse.StatusCode == HttpStatusCode.OK)
            {
                StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
                string responseData = responseReader.ReadToEnd();
                JObject restaurant = JsonConvert.DeserializeObject<JObject>(responseData);

                var names = restaurant.Values().Select(x => x.ToObject<object>()).ToList();
                var test = restaurant["location"];
                var test2 = test["address"];
                var test3 = restaurant["name"];
                var test4 = restaurant["photos_url"];
                var test5 = restaurant["average_cost_for_two"];
                details.Location.Address = Convert.ToString(test2);
                details.Name = Convert.ToString(test3);
            }
            return View(details);
        }
    }
}