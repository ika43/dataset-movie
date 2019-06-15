using System;
using System.Collections.Generic;
using System.Text;

namespace Application.SearchObj
{
    public abstract class BaseSearch
    {
        public bool ?IsDeleted { get; set; }
        public int? page { get; set; }
        public int? PageSize { get; set; }
    }
}
