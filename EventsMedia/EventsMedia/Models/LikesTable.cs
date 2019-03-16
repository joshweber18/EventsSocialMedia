using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventsMedia.Models
{
    public class LikesTable
    {
        [Key]
        public int LikeId { get; set; }

        [ForeignKey("AdventurePost")]
        public int AdventurePostId { get; set; }
        public AdventurePost AdventurePost { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
