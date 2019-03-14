using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventsMedia.Models
{
    public class FavoriteEvents
    {
        [Key]
        public int FavoriteId { get; set; }

        [ForeignKey("Adventure")]
        public int AdventureId { get; set; }
        public Adventure Adventure { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
