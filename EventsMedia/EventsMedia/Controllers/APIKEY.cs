using Newtonsoft.Json;
using Restaurants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EventsMedia.Controllers
{
    public class APIKEY
    {
        public static string apikey = "d9d2ad9c6bb2b26f98f4db8c581a6a17";
        public void GetAPIKey()
        {
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
                
            }
        }
    }
}
