using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollectionAPI.Models
{
    public class UpdatePassForm
    {
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        [Display(Name = "Saisissez votre mot de passe actuel")]
        public string ActualPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        [Display(Name = "Saisissez votre nouveau mot de passe")]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MaxLength(50)]
        [Display(Name = "Confirmez votre nouveau mot de passe")]
        [Compare(nameof(NewPassword), ErrorMessage = "Les 2 mots de passe doivent correspondre")]
        public string RepeatNewPassword { get; set; }

    }
}
