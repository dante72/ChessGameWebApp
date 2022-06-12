using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGameWebApp.Shared
{
    public class AuthModel
    {
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "4 - 20 character")]
        public string Username { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "4 - 20 character")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password is not equal")]
        public string Confirm { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "4 - 20 character")]
        public string Login { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "4 - 20 character")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Error e-mail")]
        public string Email { get; set; }
    }
}
