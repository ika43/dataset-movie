using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTO
{
    public class UserLoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
