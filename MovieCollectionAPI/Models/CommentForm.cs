using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollectionAPI.Models
{
    public class CommentForm
    {
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)] 
        public string Text { get; set; }
        [Required]
        public int IdMovie { get; set; }
        [Required]
        public Guid CreatedBy { get; set; }

    }
}
