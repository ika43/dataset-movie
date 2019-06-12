using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class CommentDto : BaseDto
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public  UserDto User { get; set; }
        [Required]
        public MovieDto Movie { get; set; }
    }
}
