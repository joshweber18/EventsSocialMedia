using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsMedia.Models
{
    public class Customer
    {
        public Dictionary<int, string> cuisines = new Dictionary<int, string>() { };

        public Dictionary<int,string> restaurants = new Dictionary<int,string>() { };

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double entity_id { get; set; }
        public string entity_type { get; set; }
        public double city_id { get; set; }
        public double cuisine_id { get; set; }
        public string cuisine_name { get; set; }
        public double restaurant_id { get; set; }
        public string RestaurantName { get; set; }
        public string Address { get; set; }
        public string PhotoURL { get; set; }
        public double CostForTwo { get; set; }
        public string ReviewText { get; set; }
        


    }
}
