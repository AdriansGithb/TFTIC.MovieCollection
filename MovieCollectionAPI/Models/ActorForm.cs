using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollectionAPI.Models
{
    public class ActorForm
    {
        [Required]
        public int IdArtist { get; set; }
        [Required]
        public int IdMovie { get; set; }
        [Required]
        [MaxLength(50)]
        public string Character { get; set; }

    }
}
