using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollectionDAL.Entities
{
    public class Artist
    {
        public int IdArtist { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
