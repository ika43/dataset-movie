using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public abstract class BaseDto
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
