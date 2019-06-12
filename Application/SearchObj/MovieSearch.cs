using System;
using System.Collections.Generic;
using System.Text;

namespace Application.SearchObj
{
    public class MovieSearch : BaseSearch
    {
        public string Title { get; set; }
        public double? MinImdbRating { get; set; }
        public double? MaxImdbRating { get; set; }
        public int? MinRuntime { get; set; }
        public int? MaxRunTime { get; set; }
        public string Genre { get; set; }
    }
}
