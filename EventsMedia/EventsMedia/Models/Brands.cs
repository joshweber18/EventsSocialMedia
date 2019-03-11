using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventsMedia.Models
{
    public class Brands
    {
        [Key]
        public int BrandId { get; set; }

        public List<Brands> BrandName { get; set; }

    }
}
