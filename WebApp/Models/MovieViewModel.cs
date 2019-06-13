using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class MovieViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Runtime { get; set; }
        public string Plot { get; set; }
        public double ImdbRating { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public List<GenreViewModel> Genres { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Released { get; set; }
    }
}
