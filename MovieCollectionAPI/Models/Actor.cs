using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollectionAPI.Models
{
    public class Actor : Artist
    {
        public int IdMovie { get; set; }
        public string Character { get; set; }
        // Dictionary :
        //      key = int = the movie id
        //      value = list<string> = list of characters of this movie
        //public Dictionary<int, List<string>> Characters { get; set; }
    }
}
