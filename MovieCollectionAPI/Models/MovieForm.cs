using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollectionAPI.Models
{
    public class MovieForm
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int ReleaseYear { get; set; }
        public string Synopsys { get; set; }
        public string TrailerLink { get; set; }
        public int? OriginCountryId { get; set; }
        public int? IdAudience { get; set; }

    }
}
