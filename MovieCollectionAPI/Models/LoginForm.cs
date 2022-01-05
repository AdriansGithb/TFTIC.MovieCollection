using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollectionAPI.Models
{
    public class LoginForm
    {
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        [Display(Name = "Adresse e-mail")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        [Display(Name = "Saisissez votre mot de passe")]
        public string Password { get; set; }

    }
}
