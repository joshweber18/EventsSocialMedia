using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventsMedia.Models
{
    public class Adventure
    {
        [Key]
        public int AdventureId { get; set; }

        public string EventName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }
        public string ImagePath { get; set; }

        [ForeignKey("AdventurePost")]
        public int AdventurePostId { get; set; }
        public AdventurePost AdventurePost { get; set; }
    }
}
