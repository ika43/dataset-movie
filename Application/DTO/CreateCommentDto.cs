using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class CreateCommentDto
    {
        [Required]
        [StringLength(150, ErrorMessage ="Comment cannot have more then 150 character an less then 3 character", MinimumLength =3)]
        public string Text { get; set; }
        [Required]
        public string MovieId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
