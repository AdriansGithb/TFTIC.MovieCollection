using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCollectionDAL.Entities
{
    public class Movie
    {
        public int IdMovie { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Synopsys { get; set; }
        public string TrailerLink { get; set; }
        public bool IsDeleted { get; set; }
        public int IdCountry { get; set; }
        public int IdAudience { get; set; }
    }
}
