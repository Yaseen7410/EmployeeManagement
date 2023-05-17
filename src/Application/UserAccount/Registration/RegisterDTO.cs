using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UserAccount.Registration
{
   public class RegisterDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]

        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]

        public string Address { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        public bool isVerified { get; set; } = false;
        public int RolesId { get; set; }
    }
}
