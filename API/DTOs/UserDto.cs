using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UserDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Token { get; set; }
    }
}