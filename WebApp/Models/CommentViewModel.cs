using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class CommentViewModel
    {
        public string Text { get; set; }
        public UserViewModel User { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
