using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsMedia.Models
{
    public class ViewModel
    {
        public AdventurePost post { get; set; }
        public Adventure adventure { get; set; }
        public List<Adventure> adventures { get; set; }

    }
}
