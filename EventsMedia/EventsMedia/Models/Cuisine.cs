using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventsMedia.Models
{
    public class Cuisine
    {
            [Key]
            public int CuisineId { get; set; }
            public string CuisineName { get; set; }
    }
}
