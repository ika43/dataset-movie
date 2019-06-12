using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class CreateCommentDto
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public string MovieId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
