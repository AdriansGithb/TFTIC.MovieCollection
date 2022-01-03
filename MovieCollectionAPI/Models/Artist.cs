using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollectionAPI.Models
{
    public class Artist
    {
        public int IdArtist { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
