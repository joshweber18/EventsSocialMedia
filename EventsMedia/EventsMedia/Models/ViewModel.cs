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
        public ApplicationUser User { get; set; }
        public List<Adventure> adventures { get; set; }
        public List<FavoriteEvents> Favorites { get; set; }
        public List<LikesTable> Likes { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public List<AdventurePost> Posts { get; set; }
        public Dictionary<AdventurePost, ApplicationUser> PopularPosts { get; set; }
        public List<int> LikeCounter { get; set; }

    }
}
