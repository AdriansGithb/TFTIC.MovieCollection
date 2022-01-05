using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollectionAPI.Models
{
    public class RegisterForm
    {
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        [Display(Name="Adresse e-mail")]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Votre nom")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        [Display(Name = "Saisissez votre mot de passe")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        [Display(Name = "Confirmez votre mot de passe")]
        [Compare(nameof(Password), ErrorMessage = "Les 2 mots de passe doivent correspondre")]
        public string RepeatPassword { get; set; }

    }
}
