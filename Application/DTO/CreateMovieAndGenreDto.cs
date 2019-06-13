using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class CreateMovieAndGenreDto : BaseDto
    {
        [Required]
        [StringLength(128, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        public DateTime Released { get; set; }
        [Required]
        [Range(1, 500)]
        public int Runtime { get; set; }
        [Required]
        public string Plot { get; set; }
        [Required]
        public string Genre { get; set; }
    }
}
