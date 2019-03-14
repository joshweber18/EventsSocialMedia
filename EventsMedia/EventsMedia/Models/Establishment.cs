using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventsMedia.Models
{
    public class Establishment
    {
            [Key]
            public int EstablishmentId { get; set; }
            public string EstablishmentName { get; set; }
  
    }
}
