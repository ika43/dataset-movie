using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class UpdateCommentDto
    {
        [Required]
        public string Text { get; set; }
        public string Id { get; set; }
    }
}
