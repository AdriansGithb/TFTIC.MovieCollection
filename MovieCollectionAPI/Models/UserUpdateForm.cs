using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollectionAPI.Models
{
    public class UserUpdateForm
    {
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        [Display(Name = "Nouvelle Adresse e-mail")]
        public string Email { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "Nouveau nom")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        [Display(Name = "Saisissez votre mot de passe actuel")]
        public string Password { get; set; }

    }
}
