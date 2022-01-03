using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollectionAPI.Models
{
    public class Actor : Artist
    {
        public string Character { get; set; }
    }
}
