using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsMedia.Models
{
    public partial class ZomatoGeocode
    {
        public int city_id { get; set; }
        public int entity_id { get; set; }
        public string entity_type { get; set; }
    }
}
