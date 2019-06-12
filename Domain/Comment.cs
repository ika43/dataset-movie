﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Comment : BaseEntity
    {
        public string Text { get; set; }
        public string MovieId { get; set; }
        public Movie Movie { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
