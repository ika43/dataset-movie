using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class GenreDto : BaseDto
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z '.-]*$", ErrorMessage ="Invalid genre name!")]
        public string Name { get; set; }
    }
}
