using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollectionAPI.Models
{
    public class Movie
    {
        public int IdMovie { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Synopsys { get; set; }
        public string TrailerLink { get; set; }
        public bool IsDeleted { get; set; }
        public string OriginCountry { get; set; }
        public string Audience { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
        public IEnumerable<Artist> Producers { get; set; }
        public IEnumerable<Artist> Directors { get; set; }


    }
}
