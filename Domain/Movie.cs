using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Movie : BaseEntity
    {
        public string Title { get; set; }
        public DateTime Released { get; set; }
        public int Runtime { get; set; }
        public string Plot { get; set; }
        public double ImdbRating { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
